using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Reactive.Framework.UI
{
    public static class MyMessageBox
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "MessageBox")]
        private static extern MessageBoxResult Show(IntPtr hWnd, string text, string caption, int options);

        public static MessageBoxResult Show(IntPtr hWnd, string text, string caption, MessageBoxOptions options)
        {
            return MyMessageBox.Show(hWnd, text, caption, (int)options);
        }
    }

    [Flags]
    public enum MessageBoxOptions : uint
    {
        OkOnly = 0u,
        OkCancel = 1u,
        AbortRetryIgnore = 2u,
        YesNoCancel = 3u,
        YesNo = 4u,
        RetryCancel = 5u,
        CancelTryContinue = 6u,
        IconHand = 16u,
        IconQuestion = 32u,
        IconExclamation = 48u,
        IconAsterisk = 64u,
        UserIcon = 128u,
        DefButton2 = 256u,
        DefButton3 = 512u,
        DefButton4 = 768u,
        SystemModal = 4096u,
        TaskModal = 8192u,
        Help = 16384u,
        NoFocus = 32768u,
        SetForeground = 65536u,
        DefaultDesktopOnly = 131072u,
        Topmost = 262144u,
        Right = 524288u,
        RTLReading = 1048576u
    }

    public enum MessageBoxResult : uint
    {
        Ok = 1u,
        Cancel,
        Abort,
        Retry,
        Ignore,
        Yes,
        No,
        Close,
        Help,
        TryAgain,
        Continue,
        Timeout = 32000u
    }
}
