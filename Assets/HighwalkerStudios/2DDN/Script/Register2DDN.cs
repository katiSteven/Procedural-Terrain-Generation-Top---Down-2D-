using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register2DDN : MonoBehaviour {
	/// <summary>
	/// The renderer on this object
	/// </summary>
	private SpriteRenderer spriteRend; //------------} These are the variables you would set to 
	private Renderer miscRend; //--------------------} public if you keep getting the below errors.

	/// <summary>
	/// Whether or not the sprite is animated.
	/// </summary>
	bool animated;

	/// <summary>
	/// Finds a renderer attached to this object and register it to the day night handler.
	/// </summary>
	void Start () {
		// Tries to get a sprite renderer if one is not set manually.
		if (spriteRend == null) {
			spriteRend = GetComponent<SpriteRenderer> ();
		}
		// Tries to get a misc renderer if one is not set manually.
		if (miscRend == null) {
			miscRend = GetComponent<Renderer> ();
		}

		// If a sprite is present, checks if it is animated and registers it to the registry.
		if (spriteRend != null) {
			Animator anim = GetComponent<Animator> ();
			if (anim != null) {
				animated = true;
			} else {
				animated = false;
			}

			Cycle2DDN.Handler.RegRenderer (spriteRend, animated);
		}

		// If a sprite is not present and a misc renderer is found, registers that to the registry
		if (miscRend != null && spriteRend == null) {
			Cycle2DDN.Handler.RegRenderer (miscRend);
		}

		// An error in case the script fails to register any renderers.
		if (spriteRend == null && miscRend == null) {
			Debug.LogError ("OOPS! DayNightRegister error in object (" + this.gameObject.name + "): Could not register any renderers." +
				"\nAre you sure there is a renderer attached to this object?" +
				"\nIf this problem persists, and you have a renderer attached to this object. Try setting the 'Rend variables in this script " +
				"\nto public, and setting the variables manually through the inspector." +
				"\nNote: You do not need to set these for all objects. Only (" + this.gameObject.name + ") would need to be set manually.");
		}
	}

	void OnDestroy () {
		// Delets the renderer from the registry.
		if (spriteRend != null) {
			Cycle2DDN.Handler.DelRenderer (spriteRend, animated);
		}
		if (miscRend != null) {
			Cycle2DDN.Handler.DelRenderer (miscRend);
		}

		// An error in case the script fails to delete any renderers from the registry.
		if (spriteRend == null && miscRend == null) {
			Debug.LogError ("THERE IT GOES AGAIN! DayNightRegister error in object (" + this.gameObject.name + "): Could not delete any renderers from the registry." +
				"\nAre you sure there is a renderer attached to this object?" +
				"\nIf this problem persists, and you have a renderer attached to this object. Try setting the 'Rend variables in this script " +
				"\nto public, and setting the variables manually through the inspector." +
				"\nNote: You do not need to set these for all objects. Only (" + this.gameObject.name + ") would need to be set manually.");
		}
	}
}
