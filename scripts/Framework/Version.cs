using System;
using System.Xml.Serialization;

namespace Reactive.Framework
{
    public enum Accessibility
    {
        Private,
        Internal,
        ProtectedInternal,
        Protected,
        Public
    }

    [Serializable]
    public struct Version
    {

        [XmlAttribute]
        public Accessibility Accessibility
        {
            get;
            set;
        }

        public string PlatformLabel
        {
            get
            {
                return "(64-bit)";
            }
        }

        [XmlAttribute]
        public short Major
        {
            get;
            set;
        }

        [XmlAttribute]
        public short Minor
        {
            get;
            set;
        }

        [XmlAttribute]
        public short Revision
        {
            get;
            set;
        }

        [XmlAttribute]
        public short Serial
        {
            get;
            set;
        }

        public Version(long version)
        {
            Major = (short)(version >> 48 & 65535L);
            Minor = (short)(version >> 32 & 65535L);
            Revision = (short)(version >> 16 & 65535L);
            Serial = (short)(version & 65535L);
            Accessibility = Accessibility.Internal;
        }

        public override bool Equals(object obj)
        {
            return obj != null && (object.ReferenceEquals(obj, this) || (obj is Version && (Version)obj == this));
        }

        public override int GetHashCode()
        {
            string value = string.Format("0x{0:X1}{1:X2}{2:X3}{3:X2}", new object[]
            {
                this.Major,
                this.Minor,
                this.Revision,
                this.Serial
            });
            return Convert.ToInt32(value, 16);
        }

        public long ToLong()
        {
            return ((long)this.Major << 48) + ((long)this.Minor << 32) + ((long)this.Revision << 16) + (long)this.Serial;
        }

        public override string ToString()
        {
            return string.Format("v{0}.{1}.{2} S{3} {4}", new object[]
            {
                this.Major,
                this.Minor,
                this.Revision,
                this.Serial,
                this.PlatformLabel
            });
        }

        public string ToString(string format)
        {
            return string.Format(format, new object[]
            {
                this.Major,
                this.Minor,
                this.Revision,
                this.Serial,
                this.PlatformLabel
            });
        }

        public static bool operator ==(Version left, Version right)
        {
            return left.Major == right.Major && left.Minor == right.Minor && left.Revision == right.Revision && left.Serial == right.Serial;
        }

        public static bool operator !=(Version left, Version right)
        {
            return !(left == right);
        }

        public static bool operator >(Version left, Version right)
        {
            if (left.Major > right.Major)
            {
                return true;
            }
            if (left.Major == right.Major)
            {
                if (left.Minor > right.Minor)
                {
                    return true;
                }
                if (left.Minor == right.Minor && left.Revision > right.Revision)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool operator <(Version left, Version right)
        {
            if (left.Major > right.Major)
            {
                return false;
            }
            if (left.Major == right.Major)
            {
                if (left.Minor > right.Minor)
                {
                    return false;
                }
                if (left.Minor == right.Minor && left.Revision >= right.Revision)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator >=(Version left, Version right)
        {
            if (left.Major > right.Major)
            {
                return true;
            }
            if (left.Major == right.Major)
            {
                if (left.Minor > right.Minor)
                {
                    return true;
                }
                if (left.Minor == right.Minor && left.Revision >= right.Revision)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool operator <=(Version left, Version right)
        {
            if (left.Major > right.Major)
            {
                return false;
            }
            if (left.Major == right.Major)
            {
                if (left.Minor > right.Minor)
                {
                    return false;
                }
                if (left.Minor == right.Minor && left.Revision >= right.Revision)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
