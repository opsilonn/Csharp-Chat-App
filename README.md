# Werewolf Companion App'

Made as a project for the following :

* School : Efrei Paris - Semester 7 in Software Engineering
* Course : Software Factory : C# and .NET
* Goal : Design and build a Chat Application, at least in console, and hopefully with a GUI.



## Chat App'

### What is it ?
Basically, it is a platform where people can send messages to one another, or send messages to a group / forum.
They can create account too ! Oh wait, that's the basic feature...


### How does it work ?
It works thanks to C#'s TcpClient, which handles the communication between applications using (de)serialization.


### The Structure
I won't get too technical, but the project is structured with 4 folders :
* Back_Communication : A library of classes that is accessible to both BackEnd and FrontEnd sides of the project.
* Back_Server : The BackEnd core of the application; it stores and manage the database and its inputs.
* Front_Console : A console interface for the user.
* Front_WindowsForm : A GUI interface for the user.



## Getting started
(I actually don't know if you need to have .NET installed or not... sorry in advance).

First, you need to launch the BackEnd project (absolutely required ! it manages the database and its inputs...).
it can be done by double-pressing the executable file :
```
Project Csharp - Chat App\Back_Server\bin\Debug\Back_Server.exe
```

Second, you can launch any FrontEnd interface you want, either the console or GUI.
it can be done by double-pressing the executable file :
```
Project Csharp - Chat App\Front_XXX\bin\Debug\Front_XXX.exe
```

And here you go !!


## Built With
* [Visual Studio 2019 Studio](https://visualstudio.microsoft.com/fr/vs/) - The IDE used to develop the app'




## Authors

It was made by the following Efrei Paris students :
* **BEGEOT Hugues** - [his Git repository](https://github.com/opsilonn)

See also the list of [contributors](https://github.com/opsilonn/Csharp-Chat-App/contributors) who participated in this project.

Note : made in the 4th year of Software Engineering cursus (1st year of Master).



## Acknowledgments
:)
