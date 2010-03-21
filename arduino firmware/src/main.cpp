#include "main.h"


void setup(void)
{
	uint8_t i = 0;
	Serial0.begin(115200);	Serial0.println("Starting up... ");

/// startup ADC system
	// add slave devices to SPI bus
	ADC1 = MCP3308_1.add_slave(30);
	ADC2 = MCP3308_2.add_slave(50);
	ADC3 = MCP3308_2.add_slave(51);

	// blank values
	for(i = 0; i < 12; i++)
		adc_sml[i].container = 0;
	for(i = 0; i < 4; i++)
		adc_med[i].container = 0;
	for(i = 0; i < 2; i++)
		adc_lrg[i].container = 0;
	for(i = 0; i < 4; i++)
		adc_flx[i].container = 0;

	enable_1 = 0;
	enable_2 = 0;
	enable_3 = 0;

	Serial0.println("ADC interface started");


/// startup actuation interface
	for(i = 0; i < 4; i++)
		speed[i]  = 0;

	// This should be controlled by the GUI, but is necessary for Motors to work
	output1.start();
	output2.start();
	output3.start();
	output4.start();

	output1.set_speed(speed[0]);
	output2.set_speed(speed[1]);
	output3.set_speed(speed[2]);
	output4.set_speed(speed[3]);

	Serial0.println("Actuation interface started");

/// other system start-up routines
	Serial0.println();
	Serial0.println("Welcome to the SmartOrthotics User Console");
}

int main(void)
{
	init();
	setup();

//	loop();
	while(1)
	{
		uint8_t i = 0, j = 0;

///		ADC update

		if(enable_1)
		{
			for(i = SENSE_SML_1, j = 0; i <= SENSE_SML_8; i++, j++)
			{
				adc_sml[j].container = sense_update(i);

				Serial0.print("Small ");
				Serial0.print(j+1, DEC);
				Serial0.print(" =  ");
				Serial0.println(adc_sml[j].container, DEC);

				//Serial0.write(i);
				//Serial0.write(adc_sml[j].high);
				//Serial0.write(adc_sml[j].low);
			}
			delay(500);
		}

		if(enable_2)
		{

			for(i = SENSE_SML_9; i <= SENSE_SML_12; i++, j++)
			{
				adc_sml[j].container = sense_update(i);

				Serial0.print("Small ");
				Serial0.print(j+1, DEC);
				Serial0.print(" =  ");
				Serial0.println(adc_sml[j].container, DEC);

				//Serial0.write(i);
				//Serial0.write(adc_sml[j].high);
				//Serial0.write(adc_sml[j].low);
			}

			for(i = SENSE_MED_1, j = 0; i <= SENSE_MED_4; i++, j++)
			{
				adc_med[j].container = sense_update(i);

				Serial0.print("Medium ");
				Serial0.print(j+1, DEC);
				Serial0.print(" =  ");
				Serial0.println(adc_med[j].container, DEC);

				//Serial0.write(i);
				//Serial0.write(adc_med[j].high);
				//Serial0.write(adc_med[j].low);
			}
			delay(500);
		}

		if(enable_3)
		{
			for(i = SENSE_LRG_1, j = 0; i <= SENSE_LRG_2; i++, j++)
			{
				adc_lrg[j].container = sense_update(i);

				Serial0.print("Large ");
				Serial0.print(j+1, DEC);
				Serial0.print(" =  ");
				Serial0.println(adc_lrg[j].container, DEC);

				//Serial0.write(i);
				//Serial0.write(adc_lrg[j].high);
				//Serial0.write(adc_lrg[j].low);
			}

			for(i = SENSE_FLX_1, j = 0; i <= SENSE_FLX_4; i++, j++)
			{
				adc_flx[j].container = sense_update(i);

				Serial0.print("Flex ");
				Serial0.print(j+1, DEC);
				Serial0.print(" =  ");
				Serial0.println(adc_flx[j].container, DEC);

				//Serial0.write(i);
				//Serial0.write(adc_flx[j].high);
				//Serial0.write(adc_flx[j].low);
			}

			delay(500);
		}
		/// END of ADC updates


/// 	control & actuation 'update'
		output1.update();
		output2.update();
		output3.update();


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
	if(0 == length)
	{
		Serial0.println("CLI received 0-length message, error!");
		return 0xFF;
	}
	
	char header = input[0];
	
	switch(header)
	{
/// MOTOR CONTROL
	// Motor 1
	case '1':
	{
		if((speed[0] + 5) <= 100)
		{
			speed[0] += 5;
			output1.set_speed(speed[0]);

			Serial0.print("Motor 1 speed increased to :");
			Serial0.println(speed[0], DEC);
		}
		else
			Serial0.println("Speed maxed out!");

		break;
	}
	case '2':
	{
		if((speed[0] - 5) >= -100)
		{
			speed[0] -= 5;
			output1.set_speed(speed[0]);

			Serial0.print("Motor 1 speed decreased to :");
			Serial0.println(speed[0], DEC);
		}
		else
			Serial0.println("Speed maxed out!");

		break;
	}

	//Motor 2
	case 'q':
	{
		if((speed[1] + 5) <= 100)
		{
			speed[1] += 5;
			output2.set_speed(speed[1]);

			Serial0.print("Motor 2 speed increased to :");
			Serial0.println(speed[1], DEC);
		}
		else
			Serial0.println("Speed maxed out!");

		break;
	}
	case 'w':
	{
		if((speed[1] - 5) >= -100)
		{
			speed[1] -= 5;
			output2.set_speed(speed[1]);

			Serial0.print("Motor 2 speed decreased to :");
			Serial0.println(speed[1], DEC);
		}
		else
			Serial0.println("Speed maxed out!");

		break;
	}

	//Motor 3
	case 'a':
	{
		if((speed[2] + 5) <= 100)
		{
			speed[2] += 5;
			output3.set_speed(speed[2]);

			Serial0.print("Motor 3 speed increased to :");
			Serial0.println(speed[2], DEC);
		}
		else
			Serial0.println("Speed maxed out!");

		break;
	}
	case 's':
	{
		if((speed[2] - 5) >= -100)
		{
			speed[2] -= 5;
			output3.set_speed(speed[2]);

			Serial0.print("Motor 3 speed decreased to :");
			Serial0.println(speed[2], DEC);
		}
		else
			Serial0.println("Speed maxed out!");

		break;
	}

	//Motor 4
	case 'z':
	{
		if((speed[3] + 5) <= 100)
		{
			speed[3] += 5;
			output4.set_speed(speed[3]);

			Serial0.print("Motor 4 speed increased to :");
			Serial0.println(speed[3], DEC);
		}
		else
			Serial0.println("Speed maxed out!");

		break;
	}
	case 'x':
	{
		if((speed[3] - 5) >= -100)
		{
			speed[3] -= 5;
			output4.set_speed(speed[3]);

			Serial0.print("Motor 4 speed decreased to :");
			Serial0.println(speed[3], DEC);
		}
		else
			Serial0.println("Speed maxed out!");

		break;
	}

	/*** SYSTEM ***/
	case 'I':
	{
		Serial0.println("ADC1 polling on");
		enable_1 = 1;
		break;
	}
	case 'i':
	{
		Serial0.println("ADC1 polling off");
		enable_1 = 0;
		break;
	}

	case 'O':
	{
		Serial0.println("ADC2 polling on");
		enable_2 = 1;
		break;
	}
	case 'o':
	{
		Serial0.println("ADC2 polling off");
		enable_2 = 0;
		break;
	}

	case 'P':
	{
		Serial0.println("ADC3 polling on");
		enable_3 = 1;
		break;
	}
	case 'p':
	{
		Serial0.println("ADC3 polling off");
		enable_3 = 0;
		break;
	}


	case TEST_MSG:
	default:
	{
		Serial0.print("Received default request (error); input = ");
		Serial0.println(header);

		return 0x00;
		break;
	}
	}
	
	return 0xFF;
}


uint16_t sense_update(uint8_t sensor)
{
	uint8_t  command = 0b11000000;	//single-ended configuration
	uint16_t value   = 0x0000;

	switch(sensor)
	{
	// ADC 1 (SS1)
	case SENSE_FLX_1:
	case SENSE_FLX_2:
	case SENSE_FLX_3:
	case SENSE_FLX_4:
	{
		command |= ((sensor - SENSE_FLX_1)<<3);

		value = MCP3308_1.send_get(ADC1, command);

		break;
	}
	case SENSE_LRG_1:
	case SENSE_LRG_2:
	{
		command |= ((sensor - SENSE_LRG_1 + 2)<<3);

		value = MCP3308_1.send_get(ADC1, command);

		break;
	}


	// ADC 2 (SS2)
	case SENSE_SML_1:
	case SENSE_SML_2:
	case SENSE_SML_3:
	case SENSE_SML_4:
	case SENSE_SML_5:
	case SENSE_SML_6:
	case SENSE_SML_7:
	case SENSE_SML_8:
	{
		//allow channel selection
		command |= ((sensor - SENSE_SML_1)<<3);

		value = MCP3308_2.send_get(ADC2, command);

		break;
	}
	// ADC 3 (SS3)
	case SENSE_SML_9:
	case SENSE_SML_10:
	case SENSE_SML_11:
	case SENSE_SML_12:
	{
		//allow channel selection
		command |= ((sensor - SENSE_SML_9)<<3);

		value = MCP3308_2.send_get(ADC3, command);

		break;
	}
	case SENSE_MED_1:
	case SENSE_MED_2:
	case SENSE_MED_3:
	case SENSE_MED_4:
	{
		//allow channel selection
		command |= ((sensor - SENSE_MED_1 + 4)<<3);

		value = MCP3308_2.send_get(ADC3, command);

		break;
	}


	default:
	{
		Serial0.println("sense_update() error!");
		return 0x0000;

		break;
	}
	}


	return value;
}

