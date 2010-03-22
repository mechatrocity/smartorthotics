/*
 * API.h
 *
 *  Created on: 2010-02-22
 *      Author: Jeff
 */
#ifndef API_H_
#define API_H_


#define MAX_MSG_LENGTH	64


/***** MESSAGE HEADERS *****/
/// System Values (input)
#define 	TEST_MSG		0x00
#define		ERROR_MTR		0xF1
#define		SUPER_ERROR		0xFF

#define		ADC_STOP		0x10
#define		ADC_START		0x1F
#define		ADC_RESET		0x1A

#define		MTR_STOP		0x20
#define		MTR_START		0x2F
#define		MTR_RESET		0x2A


/// Actuation Values (input)
#define		MTR_1_UP		0x22	//B00100010
#define		MTR_1_DOWN		0x23	//B00100011
#define		MTR_2_UP		0x24	//B00100100
#define		MTR_2_DOWN		0x25	//B00100101
#define		MTR_3_UP		0x26	//B00100110
#define		MTR_3_DOWN		0x27	//B00100111


/// Sensor Values (output)
#define		SENSE_SML_1		0xA1
#define		SENSE_SML_2		0xA2
#define		SENSE_SML_3		0xA3
#define		SENSE_SML_4		0xA4
#define		SENSE_SML_5		0xA5
#define		SENSE_SML_6		0xA6
#define		SENSE_SML_7		0xA7
#define		SENSE_SML_8		0xA8
#define		SENSE_SML_9		0xA9
#define		SENSE_SML_10	0xAA
#define		SENSE_SML_11	0xAB	// BROKEN!
#define		SENSE_SML_12	0xAC	// BROKEN!

#define		SENSE_MED_1		0xB1
#define		SENSE_MED_2		0xB2
#define		SENSE_MED_3		0xB3
#define		SENSE_MED_4		0xB4

#define		SENSE_LRG_1		0xC1
#define		SENSE_LRG_2		0xC2
#define		SENSE_LRG_3		0xC3
#define		SENSE_LRG_4		0xC4

#define		SENSE_FLX_1		0xD1
#define		SENSE_FLX_2		0xD2
#define		SENSE_FLX_3		0xD3
#define		SENSE_FLX_4		0xD4



union byte16
{
	uint16_t container;
	struct
	{
		uint16_t high: 8;	//high byte
		uint16_t low:  8;	//low byte
	};
};


#endif /* API_H_ */
