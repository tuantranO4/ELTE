7. Different kinds of plants live on a planet. If the nutrient of a plant runs out (its nutrient level becomes zero), the 
plant wastes away. There are three kinds of radiation on the planet: alpha, delta, no radiation. The different species 
of plants  react to radiation differently. The  reaction involves  a  change in the  nutrient level of the  plant and the 
radiation the next day. The radiation of the next day will be alpha radiation if the sum of the demand for alpha 
radiation over all plants is greater than the sum of the demand for delta radiation by at least three. If the demand 
for delta radiation is greater by at least three than the demand for alpha radiation, the radiation will be delta. If the 
difference is less than three, there will be no radiation. There is no radiation the first day. 
 
Each plant has a name (string), a nutrient level (int), and a boolean that denotes whether it's alive. The plant species 
are wombleroot, wittentoot and woreroot. The different plant species react to the different radiations as follows. 
The level of nutrients changes first. After that, the plant can influence the radiation of the next day if it's still alive. 
 
Wombleroot: Alpha radiation makes the nutrient level increase by 2, no radiation makes it decrease by 1, and 
delta  radiation makes it decrease by 2. It demands  alpha  radiation by a  strength of 10 regardless  of the current 
radiation. This plant also wastes away if its nutrient level increases above 10. 
 
Wittentoot: Alpha radiation makes the nutrient level decrease by 3, no radiation makes it decrease by 1, delta 
radiation makes it increase by 4. This plant demands delta radiation with strength 4 if its nutrient level is less than 
5, with strength 1 if its nutrient level is between 5 and 10, and doesn't influence the radiation if its nutrient level is 
greater than 10. 
 
Woreroot: Its nutrient level increases by 1 if there is alpha or delta radiation, and decreases by 1 if there is no 
radiation. Doesn't influence the radiation of the next day. 
 
Simulate the ecosystem of plants and give the name of the strongest plant which is still alive after n days. Print 
all the data of the plants and the level of radiation on each day. 
 
The program should read the data of the simulation from a text file. The first line contains the number of plants. 
Each of the next lines contains the data of one plant: its name, its species, and its starting nutrient level. The species 
can be: wom - wombleroot, wit - wittentoot, wor - woreroot. The last line of the file contains n, the number of days 
as an int. The program should ask for the filename and display the contents of the file. You can assume that the 
input file is correct. A possible input file: 
 
4 
Hungry wom 7 
Lanky wit 5 
Big wor 4 
Tall wit 3 
10 
 
  
