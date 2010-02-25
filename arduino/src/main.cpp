#include "main.h"

void setup()
{
	Serial0.begin(115200);	Serial0.println("SmartOrthotics Intelligent Insole");

//	setup SPI port

//	setup ADC

//	setup motor interface

//	setup computer interface

	Serial0.println("Starting console");

}


int main(void)
{
	init();
	setup();
    
	// program
	while(1)
	{
		//update sensor status

		//check for messages or commands

	}
        
	return 0;
}
