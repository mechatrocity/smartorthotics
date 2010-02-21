#ifndef __dsPIC33_RTCC_H
#define __dsPIC33_RTCC_H


uint8_t RTCC_enable (void);
uint8_t RTCC_disable(void);
uint8_t RTCC_clock_config (uint16_t);
uint8_t RTCC_clock_set 	  (uint16_t);
uint8_t RTCC_alarm_config (uint16_t);
uint8_t RTCC_alarm_set 	  (uint16_t);



#endif