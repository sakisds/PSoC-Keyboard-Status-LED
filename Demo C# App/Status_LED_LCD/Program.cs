using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Status_LED_LCD {
    class Program {
        const int ClockSpeed = 15; // In Hz
        const int Delay = 1000 / ClockSpeed;

        static void Main(string[] args) {
            Console.WriteLine("Clock (Hz): " +  ClockSpeed);

            while (true) {
                // Read one line
                var text = Console.ReadLine();       // Read line
                if (text.Length > 16) { text = text.Substring(0, 16); }
                
                var bits = new BitArray(
                    Encoding.UTF8.GetBytes(
                        text.ToCharArray()
                        )
                    ); 

                StatusLED.NumLock = false;
                StatusLED.CapsLock = false;
                StatusLED.ScrollLock = true;

                // New way
                // Numlock: DATA 1
                // CapsLock: DATA 2
                // ScrollLock: CLK
                for (var i = 0; i < bits.Length; i += 2) {
                    StatusLED.NumLock = bits[i];
                    StatusLED.CapsLock = bits[i + 1];

                    Thread.Sleep(Delay / 3);
                    StatusLED.ScrollLock = true;
                    Thread.Sleep(Delay / 3);
                    StatusLED.ScrollLock = false;
                    Thread.Sleep(Delay / 3);
                }

                Thread.Sleep(1000);
                StatusLED.NumLock = true;
                StatusLED.CapsLock = false;
            }
        }
    }
}
