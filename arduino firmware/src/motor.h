/*
 * motor.h
 *
 *  Created on: 2010-02-25
 *      Author: Jeff
 */

#ifndef MOTOR_H_
#define MOTOR_H_

#include "../arduino/WProgram.h"
#include "API.h"


class motor {
public:
	motor(void) {}						// common startup routine
	motor(uint8_t, uint8_t, uint8_t);	// startup with pin-assignments
	~motor();

	void start();
	void stop();
	void update() 	{ set_PWM(); }

	void set_speed (int);


private:
	void calc_PWM();
	void set_PWM();

	bool    enable;			// is motor control on or off?
	int 	percentage;		// motor-speed output (-100% to +100%)
	uint8_t pwm_pos, pwm_neg;			// PWM values for (+/-) output

	uint8_t pin_pos, pin_neg, pin_SS;	// pins for (+/-) motor output & Slave Select

};

extern motor output1, output2, output3, output4;

#endif /* MOTOR_H_ */
