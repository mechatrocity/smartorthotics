/*
 * motor.cpp
 *
 *  Created on: 2010-02-25
 *      Author: Jeff
 */
#include "motor.h"


motor::motor() {
	// TODO Auto-generated constructor stub

	pwm_pos = 0x00;
	pwm_neg = 0x00;
}


motor::motor(int POS, int NEG)
{
	pinMode(POS, OUTPUT);   // set pins as outputs
	pinMode(NEG, OUTPUT);

	pin_pos = POS;
	pin_neg = NEG;

	digitalWrite(POS, LOW);
	digitalWrite(NEG, LOW);

	motor();
}


motor::~motor() {
	digitalWrite(POS, LOW);
	digitalWrite(NEG, LOW);
}


void motor::set_percent(int out_val)
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
	analogWrite(pin_pos, pwm_pos);
	analogWrite(pin_neg, pwm_neg);
}




/*********** DECLARE OBJECTS ***********/
motor output1(8, 9);
motor output2(10, 11);
motor output3(12, 13);
