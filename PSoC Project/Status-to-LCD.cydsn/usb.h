/**
 * @file
 * @author  Thanasis Georgiou <contact@thgeorgiou.com>
 * @version 1.0
 *
 * @section DESCRIPTION
 * Send/Receive functions for USB functionality;
 */

#define NumLock_On (Status_LED_Data[0] & 0x01)!= 0
#define CapsLock_On (Status_LED_Data[0] & 0x02)!= 0
#define ScrollLock_On (Status_LED_Data[0] & 0x04)!= 0

/* Array of Keycode information to send to PC */
static unsigned char Keyboard_Data[8] = {0, 0, 0, 0, 0, 0, 0, 0}; 
/* Status LEDs */
static unsigned char Status_LED_Data[1] = {0};

/* Characters for the LCD */
static unsigned char Input_Text[16] = {
    ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '
};

/* Which byte are we receiving right now */
unsigned char byteIndex = 0;

/* Which bit of the byte above are we receiving right now */
unsigned char bitIndex = 0;

/* Was the last cycle a clock? */
unsigned char lastClock = 0;

/**
 * Send data to the host.
 */
void USB_Send(void);

/**
 * Receive data from the host.
 */
void USB_Receive(void);