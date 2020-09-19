using System;
using System.Windows;
using System.Windows.Interop;

namespace Fohlio.RevitReportsIntegration.Utils
{
    public static class WindowOwnerUtil
    {
        public static WindowInteropHelper BindWithMainWindow(Window window, IntPtr mainWindowHandle)
        {
            return new WindowInteropHelper(window)
                {
                    Owner = mainWindowHandle
                };
        }
    }
}