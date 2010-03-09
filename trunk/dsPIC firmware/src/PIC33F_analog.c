#include "PIC33F_analog.h"

//page 257 of dsPIC datasheet
int ADC_enable(long pins, long speed)
{
	AD1CON1bits.ADON 	= 1;	// 1 = ADC module is operating
	AD1CON1bits.ADSIDL 	= 0;	// continue module operation in Idle mode
	AD1CON1bits.AD12B 	= 1;	// 1 = 12-bit, 1-channel ADC operation
							// 0 = 10-bit, 4-channel ADC operation
}



int ADC_disable(void)
{
	AD1CON1bits.ADON = 0;	//0 = ADC is off
	
	
	
}