//Copyright Highwalker Studios 2016
//Author: Luc Highwalker
//package: 2D Day Night + Shadows

using UnityEngine;
using System.Collections;

public class Shadow2DDN : MonoBehaviour {
	[Tooltip ("Whether or not this shadow is set to the shadow layer specified in the ShadowHandler.")]
	/// <summary>
	/// Whether or not the shadow should be set to a specified shadow layer.
	/// </summary>
	public bool shadowLayer;

	[Tooltip ("You can set the sprite this shadow immitates manually here.\nYou do not need to set this manually, it will work eitherway.")]
	/// <summary>
	/// The sprite this shadow imitates.
	/// </summary>
	public SpriteRenderer parentSprite;

	[HideInInspector]
	/// <summary>
	/// The shadow's own sprite.
	/// </summary>
	public SpriteRenderer sprite;

	[HideInInspector]
	/// <summary>
	/// The shadow movement modificator.
	/// </summary>
	public float shadowMoveMod;

	// Use this for initialization
	void Start () {
		// Gets the sprite from the parent object if one is not set manually.
		if (parentSprite == null) {
			parentSprite = transform.parent.GetComponent<SpriteRenderer> ();
		}

		// An error if the shadow fails to locate a parent sprite.
		if (parentSprite == null) {
			Debug.LogError ("OH NO! Shadow error attached to (" + transform.parent.gameObject.name + "): Could not find sprite to imitate." +
				"\nAre you sure this shadow's parent has a sprite renderer?" +
				"\nIf this problem persists and you do indeed have a sprite renderer attached to the parent. Try setting the parent sprite in the inspector.");
		}

		// Gets the shadow's own sprite.
		sprite = GetComponent<SpriteRenderer> ();

		// If set to a specified shadow layer, sets the sprite to that layer. Otherwise, sits on the same layer
		// as its parent minuse 1 on the layer order.
		if (!shadowLayer) {
			sprite.sortingLayerID = parentSprite.sortingLayerID;
			sprite.sortingOrder = parentSprite.sortingOrder - 1;
		} else {
			// Checks if the shadow layer name is set, otherwise throws an error.
			if (Shadows2DDN.Handler.shadowLayerName != "") {
				sprite.sortingLayerName = Shadows2DDN.Handler.shadowLayerName;
			} else {
				Debug.LogError ("DERP! It looks like you forgot to specify the shadow layer name in the shadow handler.");
			}

			if (sprite.sortingLayerName == "Default") {
				Debug.LogError ("THAT DIDN'T WORK! The shadow layer name you specified in the shadow handler doesn't seem to exist." +
					"\nMake sure you spelled it correctly and that it is not the Default layer.");
			}
		}

		// Registers the shadow to the shadow handler.
		Shadows2DDN.Handler.RegShadow (this);
	}
	
	// Update is called once per frame
	void Update () {
		// No need to imitate the parent sprite if the shadow is not visible.
		if (parentSprite.isVisible) {
			sprite.sprite = parentSprite.sprite;
		}
	}

	// Updates the shadows position when it becomes visible.
	void OnBecameVisible () {
		transform.localPosition = Shadows2DDN.Handler.shadowLoc;
	}

	// Deletes the shadow from the registry if it gets destroyed.
	void OnDestroy () {
		Shadows2DDN.Handler.DelShadow (this);
	}
}
