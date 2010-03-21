/*
 * SPI.h
 *
 *  Created on: 2010-03-19
 *      Author: Jeff
 */

#ifndef SPI_H_
#define SPI_H_

#include "../arduino/WProgram.h"
#include "API.h"

// delayMicroseconds(100) was removed
#define CYCLE_CLOCK	    		 \
		digitalWrite(CLK, HIGH); \
		digitalWrite(CLK, LOW);


class SPI {
public:
	SPI (uint8_t, uint8_t, uint8_t);
	SPI () {}
	~SPI() {}

	uint8_t  add_slave (uint8_t);	//returns index of "slave_pins" array, AKA Device ID
	uint16_t send_get  (uint8_t, uint8_t);
	//uint16_t original  (uint8_t, uint8_t);

private:
	uint8_t   CLK;
	uint8_t   MOSI;
	uint8_t   MISO;
	uint8_t   slave_pins[8];
};

extern SPI MCP3308_1;
extern SPI MCP3308_2;

#endif /* SPI_H_ */
