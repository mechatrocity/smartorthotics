/*
 * SPI.cpp
 *
 *  Created on: 2010-03-19
 *      Author: Jeff
 *    Based on: http://www.arduino.cc/playground/Code/MCP3208
 */

#include "SPI.h"

SPI::SPI(uint8_t CLK_PIN, uint8_t MOSI_PIN, uint8_t MISO_PIN)
{
	CLK  = CLK_PIN;
	MOSI = MOSI_PIN;
	MISO = MISO_PIN;

	SPI();
}

SPI::SPI()
{
	// pin directions
	pinMode(CLK,  OUTPUT);
	pinMode(MOSI, OUTPUT);
	pinMode(MISO, INPUT );

	// initial values
	 digitalWrite(MOSI, LOW);
	 digitalWrite(CLK,  LOW);

	// blank slave device list
	 int i;
	 for(i = 0; i < 8; i++)
		 slave_pins[i] = 0;
}


SPI::~SPI()
{
	// TODO Auto-generated destructor stub
}


uint8_t SPI::add_slave(uint8_t pin)
{
	uint8_t i;
	for(i = 0; i < 8; i++)
	{
		if(0 == slave_pins[i])	// slot available
		{
			slave_pins[i] = pin;
			pinMode(pin, OUTPUT);
			digitalWrite(pin, HIGH); //disable to begin with

			return i;
		}
	}

	return 0xFF;	// if a slot was available, we wouldn't have gotten here
}


uint16_t SPI::send_get(uint8_t slave, uint8_t cmd, uint8_t bytes)
{
	uint8_t  i;
	uint16_t ADCval = 0x0000;

	//select slave
	digitalWrite(slave_pins[slave],LOW);

	// send command bit
	for(i = 7; i >= 3; i--)
	{
		digitalWrite(MOSI, cmd&1<<i);
		digitalWrite(CLK,  HIGH);	// cycle clock
		digitalWrite(CLK,  LOW);	//
	}

	digitalWrite(CLK, HIGH);// NULL bit
	digitalWrite(CLK, LOW);	//
	digitalWrite(CLK, HIGH);// SIGN bit (only used in differential mode)
	digitalWrite(CLK, LOW);	//

	//read bits from adc
	for (i = 11; i >= 0; i--)
	{
		ADCval += digitalRead(MISO)<<i;

		digitalWrite(CLK, HIGH);// cycle clock
		digitalWrite(CLK, LOW); //
	}

	//turn slave 'off'
	digitalWrite(slave_pins[slave], HIGH);

	return ADCval;
}


SPI MCP3308(50,43,42);

