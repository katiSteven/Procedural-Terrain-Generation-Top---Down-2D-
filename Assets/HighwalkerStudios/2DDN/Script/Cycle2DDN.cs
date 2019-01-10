//Copyright Highwalker Studios 2016
//Author: Luc Highwalker
//package: 2D Day Night + Shadows

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Cycle2DDN : MonoBehaviour {
	public static Cycle2DDN Handler { get; private set; }

	[Header ("Settings:")]

	[Tooltip ("Wether or not the system should change the colors of the world.\nIf set to false, the screen will only darken, and colors will remain the same.\nDefault: True")]
	public bool colored;

	[Tooltip ("The amount of time in seconds between each non animated sprite's color update.")]
	/// <summary>
	/// The amount of time it takes for non animated sprites and mic to update their colors.
	/// </summary>
	[Range(0, 5)]public float staticUpdateFreq;

	[Tooltip ("The starting cycle.\n0 = day\n1 = dusk\n2 = night\n3 = dawn")]
	/// <summary>
	/// The starting cycle.
	/// </summary>
	[Range(0, 3)]public int startingCycle; 

	[Tooltip ("The delay for when the Day and Night cycle begins. 0 to disable delay.")]
	/// <summary>
	/// The amount of time for the day night system to start updating colors.
	/// </summary>
	public float cycleStartDelay;

	[Tooltip ("The amount of time in seconds it takes for each day night cycle to pass.")]
	/// <summary>
	/// The amount of time it takes for each individual cycle.
	/// </summary>
	public float cycleTime;
	float cycleUpdate;

	/// <summary>
	/// The lists to hold all the renderers.
	/// </summary>
	List<SpriteRenderer> AnimatedSprites, StaticSprites;
	List<Renderer> MiscRenderers;	// Uncomment this and line 147 - 151 and use this list for misc mesh renderers.

	[Header ("Color settings")]

	/// <summary>
	/// The current world color.
	/// </summary>
	Color mainColor; 

	[Tooltip ("The color of the concurrent cycle\nNote: The alpha value corresponds to the overall darkness of the screen." +
		" The alpha will reset to 255 during runtime. This is normal.\nHigher alpha value = darker screen.")]
	/// <summary>
	/// The colors that the system cycles through.
	/// </summary>
	public Color day, dusk, night, dawn;

	/// <summary>
	/// The current cycle that the system is on.
	/// </summary>
	int cycle = 0;

	/// <summary>
	/// A time variable used for color lerping.
	/// </summary>
	float t = 0;

	/// <summary>
	/// Properly starts the color cycling.
	/// </summary>
	bool started;

	/// <summary>
	/// Used for the darkening of the screen.
	/// </summary>
	Image screenDark;
	Color scrnColor, scrnDay, scrnDusk, scrnNight, scrnDawn;

	// Use this for initialization
	void Start () {
		// Sets the main DayNight handler.
		if (Handler != null && Handler != this) {
			Destroy (gameObject);
		} else if (Handler == null) {
			Handler = this;
		}

		// Sets the proper cycle duration for each of the 4 phases
		cycleUpdate = cycleTime / 4;

		// Creates new lists to store all the renderer variables.
		AnimatedSprites = new List<SpriteRenderer> ();
		StaticSprites = new List<SpriteRenderer> ();
		MiscRenderers = new List<Renderer> ();

		// Creates the proper colors for the screen darkening effect.
		scrnDay = new Color (0, 0, 0, day.a);
		scrnDusk = new Color (0, 0, 0, dusk.a);
		scrnNight = new Color (0, 0, 0, night.a);
		scrnDawn = new Color (0, 0, 0, dawn.a);

		// Resets the default colors' alpha to 255.
		day = new Color (day.r, day.g, day.b, 1);
		dusk = new Color (dusk.r, dusk.g, dusk.b, 1);
		night = new Color (night.r, night.g, night.b, 1);
		dawn = new Color (dawn.r, dawn.g, dawn.b, 1);

		// Gets the image needed for the darkening effect.
		screenDark = GetComponentInChildren<Image> ();

		// Jumps to the specified starting cycle.
		JumpToCycle (startingCycle);

		// Starts the delay and the static updates.
		StartCoroutine ("StartDelay");
		InvokeRepeating ("UpdateStatic", cycleStartDelay, staticUpdateFreq);
	}

	/// <summary>
	/// Delays the start.
	/// </summary>
	/// <returns>The delay.</returns>
	IEnumerator StartDelay () {
		yield return new WaitForSeconds (cycleStartDelay);
		started = true;
	}

	// Update is called once per frame
	void Update () {
		if (started && colored) {
			UpdateMainColor ();
			UpdateAnimated ();
		} else if (started && !colored) {
			UpdateNoColor ();
		}
	}

	/// <summary>
	/// Updates the main color to correspond to the current cycle.
	/// </summary>
	void UpdateMainColor () {
		switch (cycle) {

		case 0:
			mainColor = Color.Lerp (day, dusk, t);
			scrnColor = Color.Lerp (scrnDay, scrnDusk, t);
			break;

		case 1:
			mainColor = Color.Lerp (dusk, night, t);
			scrnColor = Color.Lerp (scrnDusk, scrnNight, t);
			break;

		case 2:
			mainColor = Color.Lerp (night, dawn, t);
			scrnColor = Color.Lerp (scrnNight, scrnDawn, t);
			break;

		case 3:
			mainColor = Color.Lerp (dawn, day, t);
			scrnColor = Color.Lerp (scrnDawn, scrnDay, t);
			break;
		}

		// Resets t variable and increments the cycle.
		if (t < 1) {
			t += Time.deltaTime / cycleUpdate;
		} else {
			t = 0;

			if (cycle < 3) {
				cycle++;
			} else {
				cycle = 0;
			}
		}
	}

	/// <summary>
	/// Updates the main color to correspond to the current cycle when colored option is turned off.
	/// </summary>
	void UpdateNoColor () {
		switch (cycle) {

		case 0:
			scrnColor = Color.Lerp (scrnDay, scrnDusk, t);
			break;

		case 1:
			scrnColor = Color.Lerp (scrnDusk, scrnNight, t);
			break;

		case 2:
			scrnColor = Color.Lerp (scrnNight, scrnDawn, t);
			break;

		case 3:
			scrnColor = Color.Lerp (scrnDawn, scrnDay, t);
			break;
		}

		// Resets t variable and increments the cycle.
		if (t < 1) {
			t += Time.deltaTime / cycleUpdate;
		} else {
			t = 0;

			if (cycle < 3) {
				cycle++;
			} else {
				cycle = 0;
			}
		}
	}

	/// <summary>
	/// Updates the color for all animated sprites. This has to be called every
	/// frame, otherwise the sprites will flash.
	/// </summary>
	void UpdateAnimated () {
		for (int i = 0; i < AnimatedSprites.Count; i++) {
			if (AnimatedSprites [i].isVisible) {
				AnimatedSprites [i].color = mainColor;
			}
		}
	}

	/// <summary>
	/// Updates static sprites, as well as misc renderers and the ground if used.
	/// </summary>
	void UpdateStatic () {
		if (colored) {
			for (int i = 0; i < StaticSprites.Count; i++) {
				if (StaticSprites [i].isVisible) {
					StaticSprites [i].color = mainColor;
				}
			}

			for (int i = 0; i < MiscRenderers.Count; i++) {
				if (MiscRenderers [i].isVisible) {
					MiscRenderers [i].material.color = mainColor;
				}
			}
		}

		screenDark.color = scrnColor;
	}

	/// <summary>
	/// Jumps to selected cycle.
	/// 0 = day /
	/// 1 = dusk /
	/// 2 = night /
	/// 3 = dawn
	/// </summary>
	/// <param name="cycle">The cycle to jump to.</param>
	public void JumpToCycle (int selCycle) {

		// Error incase the selected cycle doesn't exist.
		if (selCycle < 0 || selCycle > 3) {
			Debug.LogError ("WHOOPSIE DAISY! Looks like you tried to make the day night handler jump to a cycle that doesn't exist." +
				"\nThe JumpToCycle only accepts values from 0 to 3." +
				"\n0 = day" +
				"\n1 = dusk" +
				"\n2 = night" +
				"\n3 = dawn");
		}

		cycle = selCycle;

		switch (cycle) {

		case 0:
			mainColor = day;
			scrnColor = scrnDay;
			break;

		case 1:
			mainColor = dusk;
			scrnColor = scrnDusk;
			break;

		case 2:
			mainColor = night;
			scrnColor = scrnNight;
			break;

		case 3:
			mainColor = dawn;
			scrnColor = scrnDawn;
			break;
		}

		// Updates the static colors to match that of the new cycle selected.
		UpdateStatic ();
	}

	/// <summary>
	/// Registers a sprite renderer to the day night system.
	/// </summary>
	/// <param name="render">The sprite renderer to register.</param>
	/// <param name="animated">Whether or not the sprite is animated.</param>
	public void RegRenderer (SpriteRenderer render, bool animated) {
		if (animated) {
			AnimatedSprites.Add (render);
		} else {
			StaticSprites.Add (render);
		}
	}

	/// <summary>
	/// Registers a miscellaneous renderer to the day night system.
	/// </summary>
	/// <param name="render">The misc renderer to register.</param>
	public void RegRenderer (Renderer render) {
		MiscRenderers.Add (render);
	}

	/// <summary>
	/// Removes a sprite renderer from the registry.
	/// </summary>
	/// <param name="render">The rsprite renderer to delete from the registry.</param>
	/// <param name="animated">Whether or not the sprite was animated.</param>
	public void DelRenderer (SpriteRenderer render, bool animated) {
		if (animated) {
			AnimatedSprites.Remove (render);
		} else {
			StaticSprites.Remove (render);
		}
	}

	/// <summary>
	/// Removes a miscellaneous renderer from the registry.
	/// </summary>
	/// <param name="render">The misc renderer to delete from the registry.</param>
	public void DelRenderer (Renderer render) {
		MiscRenderers.Remove (render);
	}

	/// <summary>
	/// Returns the current cycle color.
	/// </summary>
	public Color GetColor () {
		return mainColor;
	}

	/// <summary>
	/// Returns the time value. Used in the shadow handler, but could be useful for other things.
	/// </summary>
	public float GetTime () {
		return t;
	}

	/// <summary>
	/// Returns the current cycle.
	/// </summary>
	public int GetCycle () {
		return cycle;
	}
}
