#include <Wire.h>
#include <EEPROM.h>
#include "MotorDriver.h"
#include "Interval.h"
#include "Sonar.h"

#define LEFT_MOTOR_REVERSED false
#define RIGHT_MOTOR_REVERSED true

#define LEFT_MOTOR_ENABLE_PIN_A 8
#define LEFT_MOTOR_ENABLE_PIN_B 9
#define LEFT_MOTOR_ISENSE_PIN_A 2
#define LEFT_MOTOR_ISENSE_PIN_B 3
#define LEFT_MOTOR_PWM_PIN_A 11
#define LEFT_MOTOR_PWM_PIN_B 10

#define RIGHT_MOTOR_ENABLE_PIN_A 4
#define RIGHT_MOTOR_ENABLE_PIN_B 3
#define RIGHT_MOTOR_ISENSE_PIN_A 0
#define RIGHT_MOTOR_ISENSE_PIN_B 1
#define RIGHT_MOTOR_PWN_PIN_A 5
#define RIGHT_MOTOR_PWN_PIN_B 6

#define ACCELEROMETER_SENSE_PIN 5

#define SONAR_TRIGGER_PIN 2
#define SONAR_ECHO_PIN 7

#define TIMER_DIVISOR 64
#define OUT_OF_LEVEL_TRIPLEVEL 5
#define OUT_OF_LEVEL_TRIPTIME 500

MotorDriver leftMotor(LEFT_MOTOR_ENABLE_PIN_A, LEFT_MOTOR_ENABLE_PIN_B,
LEFT_MOTOR_ISENSE_PIN_A, LEFT_MOTOR_ISENSE_PIN_B,
LEFT_MOTOR_PWM_PIN_A, LEFT_MOTOR_PWM_PIN_B,
LEFT_MOTOR_REVERSED);

MotorDriver rightMotor(RIGHT_MOTOR_ENABLE_PIN_A, RIGHT_MOTOR_ENABLE_PIN_B, 
RIGHT_MOTOR_ISENSE_PIN_A, RIGHT_MOTOR_ISENSE_PIN_B, 
RIGHT_MOTOR_PWN_PIN_A, RIGHT_MOTOR_PWN_PIN_B, 
RIGHT_MOTOR_REVERSED);

Sonar sonar(SONAR_TRIGGER_PIN, SONAR_ECHO_PIN);

double upSpeedFactor = 1, downSpeedFactor = 0.5;
int moveDirection = 0;
bool moving = false;
double currentHeight = 0;
double currentLevel = 0;
double desiredLevel = readDesiredLevelFromEeprom();

unsigned long height = 0;
unsigned long desiredHeight = 0;

void setup()
{   
  // Set PWM frequency to 62.5kHz
  TCCR0B = TCCR0B & B11111000 | B00000001;
  TCCR1B = TCCR1B & B11111000 | B00000001;
  TCCR2B = TCCR2B & B11111000 | B00000001;

  stopMove();

  Serial.begin(19200);
  analogReference(EXTERNAL);
}

void stopMove() {
  leftMotor.StopMove();
  rightMotor.StopMove();
  moving = false;
  moveDirection = 0;
}

void calcSpeed(int direction, double levelError, byte& leftSpeedResult, byte& rightSpeedResult) {
  if (direction < 0) {
    levelError *= -1;
  }
  
  // Calculate speed drop based on level error
  double absError = abs(levelError);
  if (absError > 2) absError = 2;
  double clampFactor = (absError / 2) * 255.0;
  
  // Assume going UP right now.
  // Positive error means LEFT is too low so RIGHT must be clamped.
  double leftSpeed = 255 - (levelError < 0 ? clampFactor : 0);
  double rightSpeed = 255 - (levelError > 0 ? clampFactor : 0);  

  double speedFactor = moveDirection > 0 ? upSpeedFactor : downSpeedFactor;
  leftSpeedResult = (byte)(speedFactor * leftSpeed);
  rightSpeedResult = (byte)(speedFactor * rightSpeed); 
}

void setMove() {
  int direction = 0;
  if (height < desiredHeight) {
    direction = 1;
  }
  else if (height > desiredHeight) {
    direction = -1;
  } 
  else {
    return;
  }

  moveDirection = direction;
  leftMotor.Move(direction, 127 * moveDirection);
  rightMotor.Move(direction, 127 * moveDirection);
  delay(200);
  moving = true;
}

bool writeLevelInfo = false;
bool writeHeightInfo = false;
Interval outOfLevelCheckInterval(OUT_OF_LEVEL_TRIPTIME, 64);

void loop()
{
  leftMotor.Sense();
  rightMotor.Sense();

  sonar.Run();
  if (sonar.MeasurementAvailable()) {
    height = sonar.GetLastMeasurement();
  }

  while (Serial.available() > 0) {
    byte cmd = Serial.read();
    String s;
    double newDesiredLevel;

    switch (cmd) {
    case '!': // STOP!
      stopMove();
      Serial.println("OK: Stop");
      break;

    case 'M': // Move
      desiredHeight = Serial.parseInt();
      setMove();
      s = "OK: MoveTo=";
      s += desiredHeight;
      break;

    case 'F': // Feed level data continuously
      writeLevelInfo = true;
      Serial.println("OK: LevelDataOutputOn");
      break;

    case 'G': // Stop feeding level data
      writeLevelInfo = false;
      Serial.println("OK: LevelDataOutputOff");
      break;

    case 'H': // Write desired level to EEPROM
      newDesiredLevel = Serial.parseFloat();
      desiredLevel = writeDesiredLevelToEeprom(newDesiredLevel);
      s = String("OK: SetDesiredLevel=") + desiredLevel;
      Serial.println(s);       
      break;

    case 'I': // Get desired level from EEPROM and output
      desiredLevel = readDesiredLevelFromEeprom();
      s = String("OK: DesiredLevel=") + desiredLevel + " DesiredHeight=" + desiredHeight;
      Serial.println(s);       
      break;

    case 'J': // Feed height data continuously
      writeHeightInfo = true;
      Serial.println("OK: HeightDataOutputOn");
      break;

    case 'K': // Stop feeding height data
      writeHeightInfo = false;
      Serial.println("OK: HeightDataOutputOff");
      break;

    default:
      Serial.println("WTF?");
      break;
    }
  }

  heartbeat(writeLevelInfo);

  if (moving) {
    double level = (desiredLevel - currentLevel);

    byte leftSpeed = 0, rightSpeed = 0;
    calcSpeed(moveDirection, level, leftSpeed, rightSpeed);
  
    leftMotor.Move(moveDirection, leftSpeed);
    rightMotor.Move(moveDirection, rightSpeed);
  }

  if (moving) {
    if (moveDirection < 0 && height <= desiredHeight) {
      stopMove();
      Serial.println("MOVE-COMPLETE");

    } 
    else if (moveDirection > 0 && height >= desiredHeight) {
      stopMove();
      Serial.println("MOVE-COMPLETE");
    }

    if (abs(currentLevel - desiredLevel) > OUT_OF_LEVEL_TRIPLEVEL) {
      if (outOfLevelCheckInterval.elapsed()) {
        stopMove();
        Serial.println("OUT_OF_LEVEL");
      }
    } 
    else {
      outOfLevelCheckInterval.reset();
    }
  }
}

unsigned long accValues = 0;
unsigned long accCount = 0;
unsigned long heightValues = 0;
unsigned long heightCount = 0;
Interval accReadInterval(1, TIMER_DIVISOR);
Interval accCalcInterval(150, TIMER_DIVISOR);
Interval heartbeatInterval(1000, TIMER_DIVISOR);

void heartbeat(bool writeLevelInfo) {
  if (accReadInterval.elapsed()) {
    int val = analogRead(ACCELEROMETER_SENSE_PIN);
    accCount++;
    accValues += val;

    heightCount++;
    heightValues += height;
  }

  if (accCalcInterval.elapsed()) {
    currentLevel = (double)accValues / (double)accCount;
    accCount = 0;
    accValues = 0;
    currentHeight = (double)heightValues / (double)heightCount;
    heightValues = 0;
    heightCount = 0;

    if (writeLevelInfo) {
      String s("L=");
      s += currentLevel;
      s += " D=";
      s += desiredLevel;
      s += " E=";
      s += (desiredLevel - currentLevel);
      Serial.println(s);
    }

    if (writeHeightInfo) {
      String s("HEIGHT=");
      s += currentHeight;
      Serial.println(s);
    }
  }

  if (heartbeatInterval.elapsed()) {
    String s("PSDESK");
    Serial.println(s);    
  }
}

double readDesiredLevelFromEeprom() {
  return EEPROM_readDouble(0);
}

double writeDesiredLevelToEeprom(double newValue) {
  EEPROM_writeDouble(0, newValue);
  return EEPROM_readDouble(0);
}

void EEPROM_writeDouble(int ee, double value)
{
  byte* p = (byte*)(void*)&value;
  for (int i = 0; i < sizeof(value); i++)
    EEPROM.write(ee++, *p++);
}

double EEPROM_readDouble(int ee)
{
  double value = 0.0;
  byte* p = (byte*)(void*)&value;
  for (int i = 0; i < sizeof(value); i++)
    *p++ = EEPROM.read(ee++);
  return value;
}


