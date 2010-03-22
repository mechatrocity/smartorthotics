/*
 * motor.cpp
 *
 *  Created on: 2010-02-25
 *      Author: Jeff
 */
#include "motor.h"


motor::motor(uint8_t POS, uint8_t NEG, uint8_t select)
	  : pin_pos(POS), pin_neg(NEG), pin_SS(select)
{
//	pin_pos = POS;
//	pin_neg = NEG;
//	pin_SS  = select;

	pinMode(pin_pos, OUTPUT);   // set pins as outputs
	pinMode(pin_neg, OUTPUT);	//
	pinMode(pin_SS,  OUTPUT);	//

	digitalWrite(pin_pos, LOW);
	digitalWrite(pin_neg, LOW);
	digitalWrite(pin_SS,  LOW);

	enable = false;
}


motor::~motor()
{
	digitalWrite(pin_pos, LOW);
	digitalWrite(pin_neg, LOW);
	digitalWrite(pin_SS,  LOW);
}


void motor::start()
{
	enable = true;
	digitalWrite(pin_SS, HIGH);

	update();
}


void motor::stop()
{
	enable = false;
	digitalWrite(pin_SS,  LOW);

	digitalWrite(pin_pos, LOW);
	digitalWrite(pin_neg, LOW);
}


void motor::set_speed(int out_val)
{
	percentage = out_val;

	calc_PWM();

	set_PWM();
}


void motor::calc_PWM(void)
{//map(value, fromLow, fromHigh, toLow, toHigh)

	if(percentage > 0)
	{	//forward operation
		pwm_pos = map(percentage, 0, 100, 0, 255);
		pwm_neg = 0;
	}
	
	else if(percentage <= 0)
	{	//backward operation
		pwm_neg = map(percentage, 0, -100, 0, 255);
		pwm_pos = 0;
	}
	
	else
		Serial0.println("ERROR IN calc_PWM()");
}


void motor::set_PWM(void)
{
	if(enable)
	{
		analogWrite(pin_pos, pwm_pos);
		analogWrite(pin_neg, pwm_neg);
	}

	else
		Serial0.println("Motor is disabled!");
}



/***** DECLARE OBJECTS *****/
motor output1(7, 9, 5);
//motor output2(10,13,5);
motor output3(11,12,6);
motor output2(2, 8, 6);		// PINS 3 & 4 DO NOT WORK ON JEFF's ARDUINO

