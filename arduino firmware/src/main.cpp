#include "main.h"


void setup(void)
{
	uint8_t i = 0;
	Serial0.begin(115200);	Serial0.println("Starting up... ");

	// startup SPI bus (?)


	// startup ADC system
	for(i = 0; i < 8; i++)
		analog[i] = 0;

	// startup actuation interface
	for(i = 0; i < 3; i++)
		speed[i]  = 0;

	Serial0.println("Actuation Interface Started");

	// startup controller-computer interface
	Serial0.println("Welcome to the SmartOrthotics User Console");

}

int main(void)
{
	init();
	setup();
    
//	loop();
	while(1)
	{
		// update ADC status


		// control & actuation 'update'


		// check for messages
		if(Serial0.available() > 0)
		{
			uint8_t len = 0x00, ret = 0x00;
			char input[MAX_MSG_LENGTH] = {0x00};

			for(; Serial0.available() > 0; len++)
				input[len] = Serial0.read();

			ret = CLI(&input[0], len);

			/* // deal with return-status values
			switch(ret)
			{
				case 0x00:
				case 0xFF:
				default:
				{
					break;
				}
			}*/

		}///END message-check


		// maintenance?


	}
	return 0;
}


uint8_t CLI(char *input, uint8_t length)
{	
	if(0 == length)
	{
		Serial0.println("CLI received 0-length message, error!");
		return 0xFF;
	}
	
	char header = input[0];
	
	switch(header)
	{
	/*** MOTOR CONTROL ***/
	case 'a':
	{
		speed[0] += 5;
		output1.set_percent(speed[0]);

		Serial0.print("Motor 1 speed set to :");
		Serial0.println(speed[0], DEC);
	}
	case 's':
	{
		speed[0] -= 5;
		output1.set_percent(speed[0]);

		Serial0.print("Motor 1 speed set to :");
		Serial0.println(speed[0], DEC);
	}

	/*** SYSTEM ***/
	case 0x00:
	case 0x01:
	default:
	{
		Serial0.println("Received default request!");
		return 0x00;
		break;
	}
	}
	
	return 0xFF;
}


