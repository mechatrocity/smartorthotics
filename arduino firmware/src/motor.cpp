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


	motor();
}

motor::~motor() {
	// TODO Auto-generated destructor stub
}

void motor::calc_PWM(void)
{

}

void motor::set_PWM(void)
{
	analogWrite(pin_pos, pwm_pos);
	analogWrite(pin_neg, pwm_neg);
}

void motor::set_percent(int out_val)
{
	percentage = out_val;

	calc_PWM();


}
