using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{

    //initializing game object to show in display

    public GameObject objectToInstantiate;
    public GameObject ObjectParent;

    public float radius = 1;
    public Vector2 regionSize = Vector2.one;
    public int rejectionSamples = 30;
    public float displayRadius = 1;

    List<Vector2> points;

    private TileAutomata tileAutomata;

    void OnValidate()
    {
        points = PoissonDiscSampling.GeneratePoints(radius, regionSize, rejectionSamples);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(0,0,0), regionSize);
    }

    void Start()
    {
        tileAutomata = FindObjectOfType<TileAutomata>();
    }

    public void GenerateObjects()
    {
        if (points != null)
        {
            foreach (Vector2 point in points)
            {
                //int xPosCeil = Mathf.CeilToInt(point.x -1);
                //int yPosCeil = Mathf.CeilToInt(point.y -1);

                //tileAutomata.GetTerrainMap()[xPosCeil, yPosCeil] == 1

                int xPosFloor = Mathf.FloorToInt(point.x);
                int yPosFloor = Mathf.FloorToInt(point.y);


                //if (tileAutomata.GetTerrainMap()[xPosFloor, yPosFloor] == 1)
                //{
                    //Debug.Log(tileAutomata.GetTerrainMap()[xPosFloor, yPosFloor] + "  " + point.x + " " + point.y);
                    GameObject object1 = Instantiate(objectToInstantiate, new Vector2(-xPosFloor + regionSize.x / 2, -yPosFloor + regionSize.y / 2), Quaternion.identity) as GameObject;        //showing the points / trees on screen 
                    object1.GetComponent<SpriteRenderer>().sortingLayerName = "Trees";
                //}

                //Gizmos.DrawSphere(point, displayRadius);

                //Intatiate trees here
                //GameObject object1 = Instantiate(objectToInstantiate, point, Quaternion.identity, ObjectParent.transform) as GameObject;		//showing the points / trees on screen 
                //object1.GetComponent<SpriteRenderer>().sortingLayerName = "Trees";
            }
        }
    }
}