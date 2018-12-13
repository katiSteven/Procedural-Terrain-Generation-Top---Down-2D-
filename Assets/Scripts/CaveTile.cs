using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CaveTile : TerrainTile {


#if UNITY_EDITOR
    [MenuItem("Assets/Create/CaveTile",false,1)]
        private static void CreateIslandTile()
    {

        CaveTile myAT = new CaveTile();
        Sprite[] myTextures = myAT.GetSprite(); 

        if (myTextures != null)
        { 
            Debug.Log(myTextures.GetType() + "Length: " + myTextures.Length );
            Debug.Log(myTextures[0].name);
            }
        else
        {
            Debug.Log("Texture not loaded");
        }

        string fName2 = "CaveTile";

        myAT.name = fName2 + ".asset";
        //Has to be placed before Create of Asset data will be lost after saving
        myAT.InitiateSlots(myTextures);
        AssetDatabase.CreateAsset(myAT, "Assets/Tiles/" +myAT.name + "");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    //A preview of the tile
    [SerializeField]
    private Sprite preview;


    public Sprite[] GetSprite()
    {
        Sprite[] myTextures = Resources.LoadAll<Sprite>("Textures/cave/rock") ;
        return myTextures;
    }
#endif


    //Heritage from TerrainTile could do this with  GetTileData too?
    public override Sprite[] GetSprite(string textName)
    {
        Sprite[] myTextures= base.GetSprite(textName);
        return myTextures;
    }


}



