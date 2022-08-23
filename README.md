# HoloLens-Egocentric Project
 This is the AR Project for HoloLens, it can be used to test the user's perception of the AR environment under different rendering method. 

 The main function is as follows:
 1. A fixed and random distance can be setted in Code (in meters). Ex: 1.53 meters, 2.56 meters.
 2. Randomly use two different rendering styles (solid or hollow).
 3. Saveing User's result in .CSV file.

 How does it work?
 The cube will appear between 1.45 and 9.87 meters. For the convenience of recording, we set 5 fixed distances as described in the table below. Each distance will pair with two different rendering styles of cubes. Each distance will pair with two different rendering styles of cubes. Each distance and rendering type will appear three times, so there will be 5*2*3=30 combinations. Users can use the physical keyboard to input the results directly into the application. At the beginning of the experiment, the application will start timing each time it enters a combination until the user sends the judgment result. When the result is sent, the application will record all the information, including the participant’s answer and cube’s condition, and the following combination will be entered. The cycle, as mentioned above, will continue until all 30 combinations are displayed, and the recorded data will be output as a CSV text file.

