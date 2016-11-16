using System;

namespace System.Reflection
{
    public static class AssemblyExtensions
    {
        public static ProcessorArchitecture ToProcessorArchitecture(this PortableExecutableKinds peKind)
        {
            PortableExecutableKinds portableExecutableKinds = peKind & ~PortableExecutableKinds.ILOnly;
            switch (portableExecutableKinds)
            {
                case PortableExecutableKinds.Required32Bit:
                    return ProcessorArchitecture.X86;
                case PortableExecutableKinds.ILOnly | PortableExecutableKinds.Required32Bit:
                    break;
                case PortableExecutableKinds.PE32Plus:
                    return ProcessorArchitecture.Amd64;
                default:
                    if (portableExecutableKinds == PortableExecutableKinds.Unmanaged32Bit)
                    {
                        return ProcessorArchitecture.X86;
                    }
                    break;
            }
            if ((peKind & PortableExecutableKinds.ILOnly) == PortableExecutableKinds.NotAPortableExecutableImage)
            {
                return ProcessorArchitecture.None;
            }
            return ProcessorArchitecture.MSIL;
        }

        public static PortableExecutableKinds GetPeKind(this Assembly assembly)
        {
            PortableExecutableKinds result;
            ImageFileMachine imageFileMachine;
            assembly.ManifestModule.GetPEKind(out result, out imageFileMachine);
            return result;
        }

        public static ProcessorArchitecture GetArchitecture(this Assembly assembly)
        {
            return assembly.GetPeKind().ToProcessorArchitecture();
        }

        public static ProcessorArchitecture TryGetArchitecture(string assemblyName)
        {
            ProcessorArchitecture result;
            try
            {
                result = AssemblyName.GetAssemblyName(assemblyName).ProcessorArchitecture;
            }
            catch
            {
                result = ProcessorArchitecture.None;
            }
            return result;
        }

        public static ProcessorArchitecture TryGetArchitecture(this Assembly assembly)
        {
            ProcessorArchitecture result;
            try
            {
                result = assembly.GetArchitecture();
            }
            catch
            {
                result = ProcessorArchitecture.None;
            }
            return result;
        }
    }
}
