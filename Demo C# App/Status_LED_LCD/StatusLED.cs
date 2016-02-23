using System.Runtime.InteropServices;

namespace Status_LED_LCD {
    /// <summary>
    /// A simple API for controlling the keyboard status LEDs.
    /// </summary>
    class StatusLED {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        private static extern short GetKeyState(int keyCode);

        [DllImport("user32.dll")]
        private static extern int GetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        private const byte VK_NUMLOCK = 0x90;
        private const byte VK_CAPSLOCK = 0x14;
        private const byte VK_SCROLL = 0x91;
        private const uint KEYEVENTF_EXTENDEDKEY = 1;
        private const int KEYEVENTF_KEYUP = 0x2;
        private const int KEYEVENTF_KEYDOWN = 0x0;

        /// <summary>
        /// Get the current state of num lock
        /// </summary>
        /// <returns>True for Num Lock on, false for off.</returns>
        private static bool GetNumLock() {
            return (((ushort)GetKeyState(VK_NUMLOCK)) & 0xffff) != 0;
        }

        /// <summary>
        /// Get the current state of caps lock
        /// </summary>
        /// <returns>True for Caps Lock on, false for off.</returns>
        private static bool GetCapsLock() {
            return (((ushort)GetKeyState(VK_CAPSLOCK)) & 0xffff) != 0;
        }

        /// <summary>
        /// Get the current state of scroll lock
        /// </summary>
        /// <returns>True for Scroll Lock on, false for off.</returns>
        private static bool GetScrollLock() {
            return (((ushort)GetKeyState(VK_SCROLL)) & 0xffff) != 0;
        }

        /// <summary>
        /// Keyboard status LED for Num Lock
        /// </summary>
        public static bool NumLock
        {
            get
            {
                return GetNumLock();
            }
            set
            {
                if (GetNumLock() != value) {
                    keybd_event(VK_NUMLOCK, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYDOWN, 0);
                    keybd_event(VK_NUMLOCK, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
                }
            }
        }

        /// <summary>
        /// Keyboard status LED for Caps Lock
        /// </summary>
        public static bool CapsLock
        {
            get
            {
                return GetCapsLock();
            }
            set
            {
                if (GetCapsLock() != value) {
                    keybd_event(VK_CAPSLOCK, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYDOWN, 0);
                    keybd_event(VK_CAPSLOCK, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
                }
            }
        }

        /// <summary>
        /// Keyboard status LED for Scroll Lock
        /// </summary>
        public static bool ScrollLock
        {
            get
            {
                return GetScrollLock();
            }
            set
            {
                if (GetScrollLock() != value) {
                    keybd_event(VK_SCROLL, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYDOWN, 0);
                    keybd_event(VK_SCROLL, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
                }
            }
        }
    }
}
