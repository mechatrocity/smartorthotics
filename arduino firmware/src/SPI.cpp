/*
 * SPI.cpp
 *
 *  Created on: 2010-03-19
 *      Author: Jeff
 *    Based on: http://www.arduino.cc/playground/Code/MCP3208
 */

#include "SPI.h"

SPI::SPI(uint8_t CLK_PIN, uint8_t MOSI_PIN, uint8_t MISO_PIN)
	: CLK(CLK_PIN), MOSI(MOSI_PIN), MISO(MISO_PIN)
{
	//CLK  = CLK_PIN;
	//MOSI = MOSI_PIN;
	//MISO = MISO_PIN;

	// pin directions
	pinMode(CLK,  OUTPUT);
	pinMode(MOSI, OUTPUT);
	pinMode(MISO, INPUT );

	// initial values
	digitalWrite(MOSI, LOW);
	digitalWrite(CLK,  LOW);

	// blank slave device list
	for(int i = 0; i < 8; i++)
		slave_pins[i] = 0;
}


uint8_t SPI::add_slave(uint8_t pin)
{
	for(uint8_t i = 0; i < 8; i++)
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


uint16_t SPI::send_get(uint8_t slave, uint8_t cmd)
{
	int i;	//MUST be signed
	uint16_t ADCval = 0x0000;

	//select slave
	digitalWrite(slave_pins[slave],LOW);

	// send command bit
	for(i = 7; i >= 3; i--)
	{
		digitalWrite(MOSI, cmd&1<<i);
		CYCLE_CLOCK
	}

	/// ignore first two bits
	CYCLE_CLOCK	// NULL bit
	CYCLE_CLOCK	// SIGN bit (only used in differential mode)

	//read bits from adc
	for (i = 12; i >= 0; i--)
	{
		ADCval += digitalRead(MISO)<<i;

		CYCLE_CLOCK
	}

	//turn slave 'off'
	digitalWrite(slave_pins[slave], HIGH);

	return ADCval;
}


/*
uint16_t SPI::original(uint8_t slav, uint8_t channel)
{
  int adcvalue = 0;
  byte commandbits = B11000000; //command bits - start, mode, chn (3), dont care (3)

  //allow channel selection
  commandbits|=((channel-1)<<3);

  digitalWrite(slave_pins[slav], LOW); //Select adc
  // setup bits to be written
  for (int i=7; i>=3; i--){
	digitalWrite(MOSI,commandbits&1<<i);
	//cycle clock
	digitalWrite(CLK, HIGH);
	digitalWrite(CLK, LOW);
  }

  digitalWrite(CLK,HIGH);    //ignores 2 null bits
  digitalWrite(CLK,LOW);
  digitalWrite(CLK,HIGH);
  digitalWrite(CLK,LOW);

  //read bits from adc
  for (int i=11; i>=0; i--)
  {
	adcvalue+=digitalRead(MISO)<<i;
	//cycle clock
	digitalWrite(CLK,HIGH);
	digitalWrite(CLK,LOW);
  }
  digitalWrite(slave_pins[slav], HIGH); //turn off device

  return adcvalue;
}
*/

SPI MCP3308_1(36,32,34);	//FLEX & High-Load FSR's
SPI MCP3308_2(46,42,44);	//0.2'' & 0.5'' FSR's

