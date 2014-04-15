// LICENSE: GPL 3 - https://www.gnu.org/licenses/gpl-3.0.txt

// s. http://blog.marcel-kloubert.de
// s. http://stackoverflow.com/questions/108005/how-can-i-get-the-filetype-icon-that-windows-explorer-shows

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace MarcelJoachimKloubert.FileCompare.WPF.Classes
{
    /// <summary>
    /// Helper class for Win32 operations.
    /// </summary>
    public static class Win32Helper
    {
        #region Fields (3)

        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_LARGEICON = 0x0;

        // 'Large icon
        public const uint SHGFI_SMALLICON = 0x1;

        #endregion Fields

        #region Methods (3)

        // Public Methods (3) 

        [DllImport("User32.dll")]
        public static extern int DestroyIcon(IntPtr hIcon);

        // 'Small icon
        public static Icon GetSystemIcon(string sFilename)
        {
            //Use this to get the small Icon
            IntPtr hImgSmall; //the handle to the system image list

            //IntPtr hImgLarge; //the handle to the system image list
            SHFILEINFO shinfo = new SHFILEINFO();
            hImgSmall = SHGetFileInfo(sFilename, 0, ref shinfo,
                                      (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_LARGEICON);

            //Use this to get the large Icon
            //hImgLarge = SHGetFileInfo(fName, 0,
            //	ref shinfo, (uint)Marshal.SizeOf(shinfo),
            //	Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON);

            var ico = Icon.FromHandle(shinfo.hIcon);
            try
            {
                return ico.Clone() as Icon;
            }
            finally
            {
                DestroyIcon(shinfo.hIcon);
            }
        }

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        #endregion Methods

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            #region Data Members (5)

            public uint dwAttributes;
            public IntPtr hIcon;
            public IntPtr iIcon;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;

            #endregion Data Members
        };
    }
}