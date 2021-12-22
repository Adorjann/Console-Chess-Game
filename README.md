# Console-Chess-Game

-Position evaluation and MinMax algorithm  is working at depth 2.</br>  					
If you change it to 3,4.. or deeper, bugs appear :D (I didn't have much time to look into it).</br>
</br>
-If anyone wants to look into the problem (and i welcome you to do so): </br>One would have to change the depth of MinMax manualy. To do so please follow these instructions: </br>
Simply go to class Game.cs navigate to line 238 and change the first argument of method call:</br> "MiniMaxRootNode(2,true);" (change the "2") </br>
</br>
-If you want to play against the Random moves Generator, simply comment the line 238 in class Game.cs and uncomment line 237</br>
Caution! Random moves Generator is very random (you could say: stupid.)</br>
</br>
-So far, the game doesn't have logic to recognize "check-mate", this is in my TODO list.</br>
</br>
-The special move called "En passan" (Franch for: In moving) is not implemented.</br>
-Special moves "castling" and "promotion" are implemented and working. (not extensively tested)



	  
