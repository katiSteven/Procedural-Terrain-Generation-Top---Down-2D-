//Copyright Highwalker Studios 2016
//Author: Luc Highwalker
//package: 2D Day Night + Shadows

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shadows2DDN : MonoBehaviour {
	public static Shadows2DDN Handler { get; private set; }

	[Tooltip ("The prefab for the shadows. Do not change this unless you know what you're doing.")]
	public GameObject shadowPrefab;

	[Header ("Settings")]

	[Tooltip ("Whether or not all spawned shadows should be on a specified layer.")]
	/// <summary>
	/// Whether or not all spawned shadows should be on a specified layer.
	/// </summary>
	public bool allShadowsOnLayer;

	[Tooltip ("The name of the specified shadow layer")]
	/// <summary>
	/// The name of the shadow layer.
	/// </summary>
	public string shadowLayerName;

	[Space]
	[Tooltip ("The amount of time in seconds betwen each shadow location update.")]
	/// <summary>
	/// The amount of time in seconds betwen each shadow location update.
	/// </summary>
	[Range(0.01f, 2f)]public float shadowUpdateFreq;

	[Space]
	[Header ("Shadow Location Settings:")]

	[Tooltip ("The various shadow positions and colors the system cycles through.")]
	/// <summary>
	/// The shadow location settings for each cycle.
	/// </summary>
	public Vector2 day;
	public Color dayC;
	public Vector2 dusk;
	public Color duskC;
	public Vector2 night;
	public Color nightC;
	public Vector2 dawn;
	public Color dawnC;

	[HideInInspector]
	/// <summary>
	/// The location of the shadows relative to their parents.
	/// </summary>
	public Vector2 shadowLoc;

	[HideInInspector]
	/// <summary>
	/// The shadow's color.
	/// </summary>
	public Color shadowCol;

	float t;
	int cycle;

	List<Shadow2DDN> DarkSide;

	// Use this for initialization
	void Start () {
		// Sets the main shadow handler.
		if (Handler != null && Handler != this) {
			Destroy (gameObject);
		} else if (Handler == null) {
			Handler = this;
		}

		// Error if the use specified shadows to be on a specific layer, but forgot to specify the layer. 
		if (allShadowsOnLayer && shadowLayerName == "") {
			Debug.LogError ("NOT SO FAST! Looks like you told the shadow handler to put all shadows on a specific layer, but forgot to specify which layer.");
		}

		// Creates a new list to store the shadows in. 
		DarkSide = new List<Shadow2DDN> ();

		// Starts the shadow updating process.
		InvokeRepeating ("UpdateShadows", shadowUpdateFreq, shadowUpdateFreq);
	}

	// Update is called once per frame
	void Update () {
		UpdateShadowPos ();
	}

	/// <summary>
	/// Updates the main shadow position based on the time of day (kind of).
	/// </summary>
	void UpdateShadowPos () {
		// Gets the current time of day.
		t = Cycle2DDN.Handler.GetTime ();

		// Updates the position based on the cycle.
		switch (Cycle2DDN.Handler.GetCycle ()) {

		case 0:
			shadowLoc = Vector2.Lerp (day, dusk, t);
			shadowCol = Color.Lerp (dayC, duskC, t);
			break;

		case 1:
			shadowLoc = Vector2.Lerp (dusk, night, t);
			shadowCol = Color.Lerp (duskC, nightC, t);
			break;

		case 2:
			shadowLoc = Vector2.Lerp (night, dawn, t);
			shadowCol = Color.Lerp (nightC, dawnC, t);
			break;

		case 3:
			shadowLoc = Vector2.Lerp (dawn, day, t);
			shadowCol = Color.Lerp (dawnC, dayC, t);
			break;
		}
	}

	/// <summary>
	/// Updates the shadows' positions to match the time of day.
	/// </summary>
	void UpdateShadows () {
		for (int i = 0; i < DarkSide.Count; i++) {
			Shadow2DDN shadow = DarkSide [i];

			// Unnecessary to apply the position to shadows off screen.
			if (shadow.sprite.isVisible) {
				Vector2 parentLoc = shadow.transform.parent.transform.position;
				shadow.transform.position = new Vector2 (parentLoc.x + shadowLoc.x * shadow.shadowMoveMod, parentLoc.y + shadowLoc.y * shadow.shadowMoveMod);

				shadow.sprite.color = shadowCol;
			}
		}
	}

	/// <summary>
	/// Adds a shadow to the specified parent.
	/// </summary>
	/// <param name="parent">The parent of the shadow.</param>
	public void AddShadow (Transform parent) {
		GameObject obj = (GameObject) Instantiate (shadowPrefab, parent.position, parent.rotation);
		Shadow2DDN shadow = obj.GetComponent<Shadow2DDN> ();

		if (allShadowsOnLayer) {
			shadow.shadowLayer = true;
		}
		shadow.shadowMoveMod = 1;

		shadow.transform.parent = parent;
		shadow.transform.localScale = Vector3.one;
	}

	/// <summary>
	/// Adds a shadow to the specified parent. 
	/// </summary>
	/// <param name="parent">The parent of the shadow.</param>
	/// <param name="shadowLayer">If set to <c>true</c>, shadow will be on specified shadow layer. Otherwise, shadow will be on the parent's layer. Overrides default settings. </param>
	public void AddShadow (Transform parent, bool shadowLayer) {
		GameObject obj = (GameObject) Instantiate (shadowPrefab, parent.position, parent.rotation);
		Shadow2DDN shadow = obj.GetComponent<Shadow2DDN> ();

		shadow.shadowLayer = shadowLayer;
		shadow.shadowMoveMod = 1;

		shadow.transform.parent = parent;
		shadow.transform.localScale = Vector3.one;
	}

	/// <summary>
	/// Adds a shadow to the specified parent. 
	/// </summary>
	/// <param name="parent">The parent of the shadow.</param>
	/// <param name="parentSprite">The sprite this shadow immitates. Incase you want it to copy a sprite other than its parent's. The parent doesnt necessarily need a SpriteRenderer in this case.</param>
	public void AddShadow (Transform parent, SpriteRenderer parentSprite) {
		GameObject obj = (GameObject) Instantiate (shadowPrefab, parent.position, parent.rotation);
		Shadow2DDN shadow = obj.GetComponent<Shadow2DDN> ();

		if (allShadowsOnLayer) {
			shadow.shadowLayer = true;
		}
		shadow.shadowMoveMod = 1;

		shadow.transform.parent = parent;
		shadow.parentSprite = parentSprite;

		shadow.transform.localScale = Vector3.one;
	}

	/// <summary>
	/// Adds a shadow to the specified parent. 
	/// </summary>
	/// <param name="parent">Parent.</param>
	/// <param name="parentSprite">The sprite this shadow immitates. Incase you want it to copy a sprite other than its parent's. The parent doesnt necessarily need a SpriteRenderer in this case.</param>
	/// <param name="shadowLayer">If set to <c>true</c>, shadow will be on specified shadow layer. Otherwise, shadow will be on the parent's layer. Overrides default settings.</param>
	public void AddShadow (Transform parent, SpriteRenderer parentSprite, bool shadowLayer) {
		GameObject obj = (GameObject) Instantiate (shadowPrefab, parent.position, parent.rotation);
		Shadow2DDN shadow = obj.GetComponent<Shadow2DDN> ();

		shadow.shadowLayer = shadowLayer;
		shadow.shadowMoveMod = 1;

		shadow.transform.parent = parent;
		shadow.parentSprite = parentSprite;

		shadow.transform.localScale = Vector3.one;
	}

	/// <summary>
	/// Adds a shadow to the specified parent. 
	/// </summary>
	/// <param name="parent">Parent.</param>
	/// <param name="parentSprite">The sprite this shadow immitates. Incase you want it to copy a sprite other than its parent's. The parent doesnt necessarily need a SpriteRenderer in this case.</param>
	/// <param name="shadowLayer">If set to <c>true</c>, shadow will be on specified shadow layer. Otherwise, shadow will be on the parent's layer. Overrides default settings.</param>
	public void AddShadow (Transform parent, SpriteRenderer parentSprite, bool shadowLayer, float shadowMoveMod) {
		GameObject obj = (GameObject) Instantiate (shadowPrefab, parent.position, parent.rotation);
		Shadow2DDN shadow = obj.GetComponent<Shadow2DDN> ();

		shadow.shadowLayer = shadowLayer;
		shadow.shadowMoveMod = shadowMoveMod;

		shadow.transform.parent = parent;
		shadow.parentSprite = parentSprite;

		shadow.transform.localScale = Vector3.one;
	}

	/// <summary>
	/// Registers a shadow to the registry.
	/// </summary>
	/// <param name="sprite">The sprite to register. </param>
	public void RegShadow (Shadow2DDN shadow) {
		DarkSide.Add (shadow);
	}

	/// <summary>
	/// Deletes a shadow from the registry.
	/// </summary>
	/// <param name="sprite">The sprite to delte from the registry.</param>
	public void DelShadow (Shadow2DDN shadow) {
		DarkSide.Remove (shadow);
	}
}
