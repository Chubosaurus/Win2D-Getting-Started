# Win2D Getting Started: Windows Universal Application

The repo for Windows Universal Application: Getting Started With Win2D.

#### Overview

Awhile back I did a video series that went over programming a simple game engine from scratch.  I picked it back up about a week ago, but I can't guarantee updates since I'm a pretty busy person (2 jobs, very little free time).  I thought if any of you were interested you can have a look.  This was an experiment by me and recording myself programming was so weird.. in the end I found it easier to take it slow as possible.

---

To facilitate the engine, I used a fairly new drawing library from Microsoft:  [Win2D](https://github.com/Microsoft/Win2D) which is an [immediate mode](https://en.wikipedia.org/wiki/Immediate_mode_%28computer_graphics%29) drawing library.  You can use any drawing library you want and adapt accordingly.

In the video series, I go over

* Downloading the library with NuGet and importing it.  Drawing simple geometric shapes onto the canvas.<br>
* Creating a ***Content Pipeline*** and loading Images into it.<br>
* Making basic scene items (objects), putting them inside a scene (aka [Scene graph](https://en.wikipedia.org/wiki/Scene_graph)) and building a manager that manages all the scenes inside your game: A ***StoryBoard***.<br>
* Creating the Game Loop which has a specific Updates Per Second and Frame Per Second.<br>
* Creating an ***Input Manager*** such that the mouse (or anything you like) can push its events on to it, which the manager pushes its current input down the StoryBoard model.<br>
* Creating a Stack to model a scene history.<br>
* Slice and dice a Sprite Sheet into an animation object.<br>

---

##Videos In The Serie

A [Top Down View of The Simple Game Engine](http://i.imgur.com/bfIDTZ4.png)

Turn your volume up...my voice is low until EP #6... got a new mic :D

#####Season One

[EP #1 : Environment Setup & Drawing Primitives](https://www.youtube.com/watch?v=YtxHU5LWwTE)  
[EP #2 : Loading and Drawing Images](https://www.youtube.com/watch?v=uglDsbkjCio)  
[EP #3 : Coding The GenericItem](https://www.youtube.com/watch?v=uHpONsFCKkM)  
[EP #4 : Coding The GenericScene & SceneManager](https://www.youtube.com/watch?v=-rgE7nWKj8Q)  
[EP #5 : GameLoop and Keeping Time](https://www.youtube.com/watch?v=c2l5h_JGAog)  
[EP #6 : Input Manager and Game Engine Overview](https://www.youtube.com/watch?v=rNi2zigDIwA)  
[EP #7 : Capturing Mouse Events](https://www.youtube.com/watch?v=7cgOPd_JZx8)  
[EP #8 : Optimize Mouse Events And The Generic Button](https://www.youtube.com/watch?v=enxoZUkkXn4)  
[EP #9 : Scene Switcher and Generic Gaming Scenes (not recorded yet)](https://www.youtube.com/playlist?list=PLNVD5azsNYNsCp5WYZiZlbg0MXvVAmsd1)  

#####Season Two

[EP #1 : Generic Sprite Animation (not recorded yet)](https://www.youtube.com/playlist?list=PLNVD5azsNYNsCp5WYZiZlbg0MXvVAmsd1)  

Convenient Playlist of All Videos: [PLAYLIST](https://www.youtube.com/playlist?list=PLNVD5azsNYNsCp5WYZiZlbg0MXvVAmsd1) more to come (I hope).

---

####Setup

1. Pull the repository from GitHub.
2. Select which Episode you would like view and set it as the "Start-Up Project"
3. Compile and see the result from the episode.

---

####License Information

Win2D Getting Started: Windows Universal Application is licensed under **CC BY**

<a href="https://creativecommons.org/licenses/by/4.0/"><img src="https://licensebuttons.net/l/by/3.0/88x31.png"></a>

---

####Attribution Information

Some artwork by <a href="http://opengameart.org/users/tatermand">Tatermand</a>   
Some animations by *Christian da Silva*





