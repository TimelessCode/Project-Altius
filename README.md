# Project-Altius
A framework which will hopefully act as a starting point for any monogame client server thing I make.

# How will it work?
It will , quite simply, provide functions creating an extremely high level way to send int's or any other datatype, to the server  
or to any other clients in paticular.  
You will hopefully be able to flag certain messages as a certain type, which the server will be able to discern using yet another function.  
A big plan is eventually incoporating the ability to define how a class can be sent over wifi (first part will be this field, second part will be that field etc) which you can copy and paste (or put in a file) for the server to interpret and easily  
with one function, or put simply, defining templates for how a class is sent over

# overall structure plan?
in the client , something like this
```c#
sendMessage(server,myPlayer,myPlayerTemplate);
myPlayer.update();
```
and in the server, some code to parse that class using the ```myPlayerTemplate``` and send that info to everyone else
