#include "main.h"


void setup(void)
{
	Serial0.begin(115200);	Serial0.println("Starting up... ");

	// startup SPI bus

	// startup ADC system

	// startup actuation interface

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


			// deal with return-status values
			switch(ret)
			{
				case 0x00:
				case 0xFF:
				default:
				{
					break;
				}
			}
		}///END message-check


		// maintenance?


	}
	return 0;
}


uint8_t CLI(char *input, uint8_t length)
{

	switch(length)
	{
	case 0:
	{
		Serial0.println("MSG LENGTH 0... wtf?");
		break;
	}

	case 1:
	{

	}
	default:
	{
		break;
	}
	}

	return 0x00;
}

