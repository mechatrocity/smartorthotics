/*
 * motor.h
 *
 *  Created on: 2010-02-25
 *      Author: Jeff
 */

#ifndef MOTOR_H_
#define MOTOR_H_

#include "../arduino/WProgram.h"


class motor {
public:
	motor(void);		// common startup routine
	motor(int, int);	// startup with pin-assignments
	~motor();

	void update() //; update PWM for desired velocity
	{ set_PWM(); }
	void set_velocity(int);
	void set_percent (int);


private:
	void calc_PWM();
	void set_PWM();

	int percentage;	// motor-speed output (-100% to +100%)

	uint8_t pin_pos, pin_neg;	//pins for (+/-) motor output
	uint8_t pwm_pos, pwm_neg;	//PWM values for (+/-) output

};

#endif /* MOTOR_H_ */
