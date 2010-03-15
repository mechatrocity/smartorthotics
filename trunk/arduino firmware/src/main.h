#ifndef MAIN_H_
#define MAIN_H_

#include "../arduino/WProgram.h"
#include "API.h"
#include "motor.h"

volatile int       speed[3]  = {0};		//Motor PWM (output)
volatile uint16_t  analog[8] = {0x00}; 	//ADC (input)


/***** FIRMWARE FUNCTIONS *****/
void 	   setup	   (void);
int 	   main	       (void);

uint8_t	   CLI		   (char *, uint8_t);	 /* Command Line Interpreter */
uint8_t    motor_cntrl (uint8_t, uint16_t);	 /* Motor-Control Interface */
uint8_t    sense_cntrl (uint8_t, uint8_t);   /* ADC Interface */



#endif /* MAIN_H_ */
