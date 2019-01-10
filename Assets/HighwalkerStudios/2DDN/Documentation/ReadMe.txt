-Copyright Highwalker Studios 2016
-Created by Luc Highwalker

------------------------------------------------------------
0. INDEX:
------------------------------------------------------------

1. Installation.
2. How to use.
3. Variables explained.
4. Enjoy, Thanks!


------------------------------------------------------------
1. INSTALLATION:
------------------------------------------------------------
Note: The shadow system is intended to be used with top down games. But will work with 
		any objects using a Sprite Renderer. The day night system is completely independant
		of the shadow system. If you do not wish to use the shadow system, the day night system 
		will work fine without it.


- Make sure that the Cycle2DDN script is at the top of your execution order, followed by the 
	Shadows script. To do this, go to Edit -> Project Settings -> Script Execution Order. 
	Then click the + symbol, and select the Cycle2DDN script, folllowed by the Shadows2DDN 
	script (Shadows with an "s"). Set a low negative numbers so	that the scripts are above 
	the Default Time block.

- Drop the DayNightHandler and the ShadowHandler prefabs into your scene. 

- Configure the handlers, or use the default settings.


------------------------------------------------------------
2. HOW TO USE:
------------------------------------------------------------

Day Night System:
-----------------

- Simply add a "Register2DDN" script to any objects that you want to be affected 
	by the system. 

	
Shadows System:
---------------

- Add a Shadow Prefab as a child to any object using a sprite renderer.
	(HighwalkerStudios -> Prefabs -> Other)
	
- Alternatively, you can add the following line of code in your script to achieve the same. 
	Shadows2DDN.Handler.AddShadow (transform);
	Various override methods available for extra control.


------------------------------------------------------------
3. VARIABLES EXPLAINED
------------------------------------------------------------

Day Night Handler:
-----------------

- Cycle start: The cycle at which the system will start.
				0 = day
				1 = dusk
				2 = night
				3 = dawn

- Cycle Start Delay: The amount of time it takes for the day and night system to start 
	after runtime.

- Cycle Time: This is the time in seconds it takes for each day to pass.

- Static Update Freq: The time in seconds that static sprites and misc renderers get
	their colors updated. A higher value will look choppy, but may save on 
	processing power if you have a lot of non-animated sprite objects. 

- Day, Dusk, Night, Dawn: The colors that the day night cycle will cycle through. 


Shadow Handler:
---------------

- Shadow Prefab: The prefab for the shadows. There's really no need to change this, and you shouldn't
	unless you know what you're doing. 

- All Shadows On Layer: Whether or not all shadows should be on the layer specified below.

- Shadow Layer Name: The name of the shadow layer mentioned above.

- Shadow Update Freq: The time in seconds between the shadows updating their locations.

- Day, Dusk, Night, Dawn: The various shadow locations that the system will cycle through.


Shadow:
---------------

- Shadow Layer: If this shadow is on the shadow layer specified in the shadow handler above.

- Parent Sprite: The sprite the shadow should imitate. You do not need to set this manually but you can.

------------------------------------------------------------
4. THANK YOU!
------------------------------------------------------------


Thank you so much for the download! I'm learning more every day and hope to improve these 
assets as much as my time allows. Your downloads keep me going. Ratings make my day. For feedback, 
suggestions, or help using this asset, don't hesitate to email me at luc@highwalkerstudios.com.

Thanks again! :)