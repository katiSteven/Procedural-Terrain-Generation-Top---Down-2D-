using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    //public
    public GameObject player;
    //private
    private TileAutomata tileAutomata;


	// Use this for initialization
	void Start ()
    {
        tileAutomata = FindObjectOfType<TileAutomata>();
        //SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        Vector2Int spawnValue = tileAutomata.GetSpawnPoint();
        Debug.Log("spawning player");
        Instantiate(player, new Vector3Int(spawnValue.x, spawnValue.y, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
