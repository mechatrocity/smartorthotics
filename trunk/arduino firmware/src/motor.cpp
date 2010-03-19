/*
 * motor.cpp
 *
 *  Created on: 2010-02-25
 *      Author: Jeff
 */
#include "motor.h"


motor::motor(int POS, int NEG)
{
	pin_pos = POS;
	pin_neg = NEG;

	motor();
}


motor::motor()
{
	pinMode(pin_pos, OUTPUT);   // set pins as outputs
	pinMode(pin_neg, OUTPUT);

	digitalWrite(pin_pos, LOW);
	digitalWrite(pin_neg, LOW);

	enable = false;
}

motor::~motor() {
	digitalWrite(pin_pos, LOW);
	digitalWrite(pin_neg, LOW);
}


void motor::set_speed(int out_val)
{
	percentage = out_val;

	calc_PWM();
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
		pwm_neg = map(percentage, 0, 100, 0, 255);
		pwm_pos = 0;
	}
	
	else	Serial0.println("ERROR IN calc_PWM()");
}


void motor::set_PWM(void)
{
	if(enable)
	{
		analogWrite(pin_pos, pwm_pos);
		analogWrite(pin_neg, pwm_neg);
	}

	else
		//Serial0.println("Motor is disabled!");
	{
	}
}



/*********** DECLARE OBJECTS ***********/
motor output1(8, 9);
motor output2(10,11);
motor output3(12,13);
