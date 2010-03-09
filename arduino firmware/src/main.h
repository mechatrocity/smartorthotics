#ifndef MAIN_H_
#define MAIN_H_

#include "../arduino/WProgram.h"
#include "API.h"
#include "motor.h"

volatile uint16_t  analog[8] = {0x00}; 	//ADC (input)
//volatile motor     output1(9,10);

/***** FIRMWARE FUNCTIONS *****/
void 	setup	  (void);
int 	main	  (void);
uint8_t	CLI		  (char *, uint8_t);	/* Command Line Interpreter */
uint8_t mtr_cntrl (uint8_t, uint16_t);	/* Motor-Control Interface */



#endif /* MAIN_H_ */
