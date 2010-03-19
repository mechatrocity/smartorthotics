#define SELPIN    34 //Selection Pin 
#define DATAOUT   43 //MOSI 
#define DATAIN    42 //MISO 
#define SPICLOCK  50 //Clock 

int readvalue; 

void setup()
{ 
///set pin modes 
 pinMode(SELPIN,   OUTPUT); 
 pinMode(DATAOUT,  OUTPUT); 
 pinMode(DATAIN,   INPUT); 
 pinMode(SPICLOCK, OUTPUT); 
 
///disable device to start with 
 digitalWrite(SELPIN,   HIGH); 
 digitalWrite(DATAOUT,  LOW); 
 digitalWrite(SPICLOCK, LOW); 

 Serial.begin(115200); 
} 

int read_adc(int channel){
  int adcvalue = 0;
  byte commandbits = B11000000; //command bits - start, mode, chn (3), dont care (3)

  //allow channel selection
  commandbits|=((channel-1)<<3);

  digitalWrite(SELPIN,LOW); //Select adc
  // setup bits to be written
  for (int i=7; i>=3; i--){
    digitalWrite(DATAOUT,commandbits&1<<i);
    //cycle clock
    digitalWrite(SPICLOCK,HIGH);
    digitalWrite(SPICLOCK,LOW);    
  }

  digitalWrite(SPICLOCK,HIGH);    //ignores 2 null bits
  digitalWrite(SPICLOCK,LOW);
  digitalWrite(SPICLOCK,HIGH);  
  digitalWrite(SPICLOCK,LOW);

  //read bits from adc
  for (int i=11; i>=0; i--){
    adcvalue+=digitalRead(DATAIN)<<i;
    //cycle clock
    digitalWrite(SPICLOCK,HIGH);
    digitalWrite(SPICLOCK,LOW);
  }
  digitalWrite(SELPIN, HIGH); //turn off device
  return adcvalue;
}


void loop() { 
  
  int i;
  for(i = 1; i < 9; i++)
  {
    readvalue = read_adc(i); 
    Serial.print("Channel ");
    Serial.print(i, DEC);
    Serial.print(" : ");
    Serial.println(readvalue,DEC);
  }
  
  delay(500); 
} 

