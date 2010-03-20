#include "main.h"


void setup(void)
{
	uint8_t i = 0;
	Serial0.begin(115200);	Serial0.println("Starting up... ");

/// startup ADC system
	// add slave devices to SPI bus
	//ADC1 = MCP3308.add_slave(50);
	//ADC2 = MCP3308.add_slave(51);
	ADC1 = MCP3308.add_slave(30);

	// blank values
	for(i = 0; i < 12; i++)
		adc_sml[i].container = 0;
	for(i = 0; i < 4; i++)
		adc_med[i].container = 0;
	for(i = 0; i < 2; i++)
		adc_lrg[i].container = 0;
	for(i = 0; i < 4; i++)
		adc_flx[i].container = 0;

	Serial0.println("ADC interface started");


/// startup actuation interface
	output1.set_speed(0);
	output2.set_speed(0);
	output3.set_speed(0);

	for(i = 0; i < 3; i++)
		speed[i]  = 0;

	Serial0.println("Actuation interface started");

/// startup controller-computer interface
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
		uint8_t i = 0, j = 0;

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

		delay(1000);

/*		for(i = SENSE_MED_1, j = 0; i <= SENSE_MED_4; i++, j++)
		{
			adc_med[j].container = sense_update(i);

			Serial0.print("Medium ");
			Serial0.print(j+1, DEC);
			Serial0.print(" =  ");
			Serial0.println(adc_sml[j].container, DEC);

			Serial0.write(i);
			Serial0.write(adc_med[j].high);
			Serial0.write(adc_med[j].low);
		}

		for(i = SENSE_LRG_1, j = 0; i <= SENSE_LRG_2; i++, j++)
		{
			adc_lrg[j].container = sense_update(i);

			Serial0.write(i);
			Serial0.write(adc_lrg[j].high);
			Serial0.write(adc_lrg[j].low);
		}

		for(i = SENSE_FLX_1, j = 0; i <= SENSE_FLX_4; i++, j++)
		{
			adc_flx[j].container = sense_update(i);

			Serial0.write(i);
			Serial0.write(adc_flx[j].high);
			Serial0.write(adc_flx[j].low);
		}
*/

		// control & actuation 'update'
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
	/*** MOTOR CONTROL ***/
	case 'a':	//H-BRIDGE TEST
	{
		speed[0] += 5;
		output1.set_speed(speed[0]);

		Serial0.print("Motor 1 speed increased to :");
		Serial0.println(speed[0], DEC);

		break;
	}
	case 's':	//H-BRIDGE TEST
	{
		speed[0] -= 5;
		output1.set_speed(speed[0]);

		Serial0.print("Motor 1 speed decreased to :");
		Serial0.println(speed[0], DEC);

		break;
	}

	/*** SYSTEM ***/
	case TEST_MSG:
	default:
	{
		Serial0.print("Received default request; input = ");
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

		value = MCP3308.send_get(ADC1, command);

		break;
	}

	// ADC 2 (SS2)
	case SENSE_SML_9:
	case SENSE_SML_10:
	case SENSE_SML_11:
	case SENSE_SML_12:
	{
		//allow channel selection
		command |= ((sensor - SENSE_SML_9)<<3);

		value = MCP3308.send_get(ADC2, command);

		break;
	}
	case SENSE_MED_1:
	case SENSE_MED_2:
	case SENSE_MED_3:
	case SENSE_MED_4:
	{
		//allow channel selection
		command |= ((sensor - SENSE_MED_1 + 4)<<3);

		value = MCP3308.send_get(ADC2, command);

		break;
	}

	// ADC 3 (SS3)
	case SENSE_LRG_1:
	case SENSE_LRG_2:
	case SENSE_FLX_1:
	case SENSE_FLX_2:
	case SENSE_FLX_3:
	case SENSE_FLX_4:
	{

	}

	default:
	{
		//Serial0.println("sense_update() error!");
		return 0x0000;

		break;
	}
	}


	return value;
}

