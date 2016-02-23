/**
 * @file
 * @author  Thanasis Georgiou <contact@thgeorgiou.com>
 * @version 1.0
 *
 * @section DESCRIPTION
 * Send/Receive functions for USB functionality;
 */

#include <project.h>
#include "usb.h"

void USB_Send(void) {
}

void USB_Receive(void) {
    // Read the incoming report data 
    Status_LED_Data[0] =
        USBFS_DEVICE0_CONFIGURATION0_INTERFACE0_ALTERNATE0_HID_OUT_BUF[0];
    
    // If numlock is enabled, turn on the LED
    if (NumLock_On) {
        Keyboard_Data[2] = 0x00;
        USBFS_LoadInEP(1, Keyboard_Data, 8);
        Pin_NumLock_Write(1);
    } else {
        Pin_NumLock_Write(0);
    }
    
    // If capslock is enabled, turn on the LED
    if (CapsLock_On) {
        Keyboard_Data[2] = 0x00;
        USBFS_LoadInEP(1, Keyboard_Data, 8);
        Pin_CapsLock_Write(1);
    } else {
        Pin_CapsLock_Write(0);
    }
    
    // If scroll lock is enabled, turn on the LED
    if (ScrollLock_On) {
        Keyboard_Data[2] = 0x00;
        USBFS_LoadInEP(1, Keyboard_Data, 8);
        Pin_ScrollLock_Write(1);
        
        // Scroll lock is also our clock so if it's on, we should
        // store the state of numlock and capslock as it's our
        // data.        
        if (lastClock == 0) {
            lastClock = 1;
            
            // Store numlock
            if (NumLock_On) {
                Input_Text[byteIndex] |= 1 << (bitIndex);
            }
            // Advance the bit index since we stored one bit.
            bitIndex++; 
            
            // Store caps lock
            if (CapsLock_On) { 
                Input_Text[byteIndex] |= 1 << (bitIndex);
            }
            bitIndex++; // Another bit
            
            // If this byte is received, move to the next one
            if (bitIndex == 8) {
                byteIndex++;
                bitIndex = 0;
            }
        }
    } else {
        Pin_ScrollLock_Write(0);
        
        // Clock cycle finished...
        if (lastClock == 1) {
            lastClock = 0;
            
            // .. check if we have received 16 bytes ...
            if (byteIndex == 16) {
                byteIndex = 0;
                
                // and print them on the screen
                LCD_Position(1, 0); // row 1, column 0
                
                unsigned char i;
                for (i = 0; i < 16; i++) {
                    LCD_PutChar(Input_Text[i]);
                    // Clear for the next batch
                    Input_Text[i] = ' ';
                }
            }
        }
    }
}