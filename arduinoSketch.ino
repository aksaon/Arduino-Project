int waterVal = 0; //holds the water level value
int soundVal = 0; //holds sound level value 
int threshold = 200; //initialize threshold for sound sensor
const int waterPin = A5;//water sensor pin used
const int soundPin = A4;//sound sensor pin used
 
#include "my_library.h"
 
void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(soundPin, INPUT);
  pinMode (13, OUTPUT);//used to verify sound detection
}
 
void loop() {
  // put your main code here, to run repeatedly:
  
  waterVal = analogRead(waterPin); // read data from analog pin and store it to waterVal var
  soundVal = analogRead(soundPin); //read data to soundVal
 
  char option = Serial.read();
 
  //'w' will be the character to request print-out of water-level (by-passing the sound sensor)
  if (option == 'w')
  {
    printWater(waterVal);
  }
  else if (option == 's')
  {
    printSound(soundVal);
  }
  //if the user requests a change -> allows user to set the sound threshold
  else if (option == 'c')
  {
    Serial.println("Enter new threshold: ");
    threshold = Serial.read(); 
  }
  
  if (soundVal>=threshold)
  {
    digitalWrite(13,HIGH);
    printWater(waterVal);
  }
  else
  {
    digitalWrite(13,LOW);
  }
  delay(1000);
}
