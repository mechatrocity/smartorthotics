#include "PIC33F_RTCC.h"

RCFGCAL.RTCEN 	= 1;
RCFGCAL.RTCWREN = 0;	//locked out from being written
RCFGCAL.RTCSYNC = 0;	//"RTCC Value Registers Read Synchronization bit"
RCFGCAL.HALFSEC = 0;	//Half-second status-bit for first half period
RCFGCAL.RTCOE 	= 1; 	// RTCC output enable bit
RCFGCAL.RTCPTR  = 2;
PADCFG1.RTSECSEL = 0;	//0 = alarm pulse for RTCC pin
						//1 = clock pulse for RTCC pin
//RCFGCAL.CAL = [uint8_t]; //RTC Drift Calibration


ALCFGRPT.ALRMEN = 0;	//alarm is disabled