# Arduino-Project

This project includes the Arduino sketch and the C# script used to run the Arduino board I made.

# Description

This program uses an Arduino board to measure both sound and water levels while running. 
Users can request water level or sound value and change the sound threshold at any given moment using the windows form.
If the sound threshold is ever broken, the water level will be automatically sent to the user.
Users should submit the information for an email in the windows form. All data will be sent to this email.

# How to use/Warnings

To run this program, you'll need an Arduino Uno board and a sound sensor, a water sensor, and a light bulb (optional) to go with it.
Each of the parts should be connected to their corresponding pins on the Arduino board (seen in the .ino file).

This project is also missing the windows form that goes with the C# script, so to get that working you will also need to make a form based off the labels in the script.
Labels include btn_, checkbx_, and txtBox_, which correspond to labels for buttons, checkboxes, and textboxes, respectively.
