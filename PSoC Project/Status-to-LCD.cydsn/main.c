/* ========================================
 * PSoC HID Keyboard w/ LCD
 * Thanasis Georgiou
 * ========================================
*/
#include <project.h>
#include "usb.h"

int main() {
    // Enable global interrupts
    CyGlobalIntEnable;

    // Initialize the LCD
    VDAC8_Contrast_Start(); // Enable contrast pin output
    Opamp_Contrast_Start();
    LCD_Start(); // Clear LCD
    
    // Start USB
    USBFS_Start(0, USBFS_DWR_VDDD_OPERATION);
    LCD_PrintString("Waiting for host");
    
    // Wait for enumeration
    while (!USBFS_bGetConfiguration());
    // Begin USB traffic
    USBFS_LoadInEP(1, Keyboard_Data, 8);
    LCD_ClearDisplay();
    LCD_PrintString("Connnected.");

    // Main loop
    for (;;) {        
        /*Checks for ACK from host*/
		if (USBFS_bGetEPAckState(1)) {
            // Send data to host
            USB_Send();

            // Receive data from host	
            USB_Receive();
        }
    }
}