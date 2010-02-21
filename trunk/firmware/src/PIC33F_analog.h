#ifndef __dsPIC33_analog_H
#define __dsPIC33_analog_H

#include <stdlib.h>

/*
#define	DAC1CON		//DAC Control Register
#define	DAC1STAT	//DAC Status & Control Register
#define	DAC1DFLT	//DAC Default Value when FIFO is empty
#define	DAC1LDAT	//Left Channel Data Register
#define DAC1RDAT	//Right Channel Data Register
*/

int ADC_enable (void);				//start with default capabilities
int ADC_enable (long pins, long speed);
int ADC_disable(void);
int ADC_config (void);


int DAC_enable (void);
int DAC_enable (int config);
int DAC_disable(void);
int DAC_config (void);


#endif
