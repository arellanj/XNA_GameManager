XNA_GameManager
===============

Xna application that manages smaller xna Games. The game class must be derived from the GameBase class.


--
**What works:**
  - Running a smaller game by updating and drawing
  - Initializing and Unloading games
  - Quiting a smaller game and returning to the GameManager

**Current problems:**
  - Working across multiple Desktops ( having the game manager open on one screen, and the current game on another )

--
**Attempt 1 (pre-GitHub)**
<p>
	I tried making a loaded game take over the window while it was running. This worked by using using a two-state state machine ( Menu, Game). When in Menu, only update and render Menu Items, the same goes for Game. The way I quit a game was by detecting an escape key press. This cause the game to unload and return to the Menu. This would work perfectly if I was only using one display, but the design requires that a second monitor would have the game menu. The second monitor should also be able to quit the current game and return to the list of games. 
</p>
--

**Attempt 2(pre-GitHub)**
<p>
In my second attempt I tried to partition the screen by using separate viewports. one side of the Xna Window would have the game manager, and the other side would display the actual game. A problem showed up when using the mouse as input. The mouse coordinates are based on the window coordinates, and using the mouse as input required an offset to be passed or stored in everything that uses the mouse in the games. For me, this is a bad idea. It makes regular xna games harder to port to this game manager, requiring them to take a mouse offset into account.
	Since we are going to use the Kinect as input for the game and a mouse for the Game Manager, this might not become a problem for us. Which is why I decide to put my project on GitHub before I continue to my third attempt, trying to use two graphicsDevices




