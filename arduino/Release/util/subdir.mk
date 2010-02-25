################################################################################
# Automatically-generated file. Do not edit!
################################################################################

# Add inputs and outputs from these tool invocations to the build variables 
CPP_SRCS += \
../util/HardwareSerial.cpp \
../util/Print.cpp \
../util/Tone.cpp \
../util/WMath.cpp 

C_SRCS += \
../util/WInterrupts.c \
../util/pins_arduino.c \
../util/wiring.c \
../util/wiring_analog.c \
../util/wiring_digital.c \
../util/wiring_pulse.c \
../util/wiring_shift.c 

OBJS += \
./util/HardwareSerial.o \
./util/Print.o \
./util/Tone.o \
./util/WInterrupts.o \
./util/WMath.o \
./util/pins_arduino.o \
./util/wiring.o \
./util/wiring_analog.o \
./util/wiring_digital.o \
./util/wiring_pulse.o \
./util/wiring_shift.o 

C_DEPS += \
./util/WInterrupts.d \
./util/pins_arduino.d \
./util/wiring.d \
./util/wiring_analog.d \
./util/wiring_digital.d \
./util/wiring_pulse.d \
./util/wiring_shift.d 

CPP_DEPS += \
./util/HardwareSerial.d \
./util/Print.d \
./util/Tone.d \
./util/WMath.d 


# Each subdirectory must supply rules for building sources it contributes
util/%.o: ../util/%.cpp
	@echo 'Building file: $<'
	@echo 'Invoking: AVR C++ Compiler'
	avr-g++ -Wall -Os -fpack-struct -fshort-enums -funsigned-char -funsigned-bitfields -fno-exceptions -mmcu=atmega1280 -DF_CPU=16000000UL -MMD -MP -MF"$(@:%.o=%.d)" -MT"$(@:%.o=%.d)" -c -o"$@" "$<"
	@echo 'Finished building: $<'
	@echo ' '

util/%.o: ../util/%.c
	@echo 'Building file: $<'
	@echo 'Invoking: AVR Compiler'
	avr-gcc -Wall -Os -fpack-struct -fshort-enums -funsigned-char -funsigned-bitfields -mmcu=atmega1280 -DF_CPU=16000000UL -MMD -MP -MF"$(@:%.o=%.d)" -MT"$(@:%.o=%.d)" -c -o"$@" "$<"
	@echo 'Finished building: $<'
	@echo ' '


