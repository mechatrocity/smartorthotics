#ifndef MAIN_H_
#define MAIN_H_

#include "../arduino/WProgram.h"
#include "API.h"
#include "motor.h"


/***** FIRMWARE FUNCTIONS *****/
void 	   setup	   (void);
int 	   main	       (void);

uint8_t	   CLI		   (char *,  uint8_t);	 /* Command Line Interpreter */
uint8_t    motor_cntrl (uint8_t, uint8_t);	 /* Motor-Control Interface */
uint8_t    sense_cntrl (uint8_t, uint8_t);   /* ADC Interface */

uint16_t   sense_update(uint8_t);			 /* ADC Update */



/*** Global Variables ***/
volatile byte16	   adc_sml[12];	//ADC (input)
volatile byte16	   adc_med[4]; 	//
volatile byte16	   adc_lrg[2]; 	//
volatile byte16	   adc_flx[4]; 	//

volatile int       speed[3]  = {0};	//Motor PWM (output)


#endif /* MAIN_H_ */
