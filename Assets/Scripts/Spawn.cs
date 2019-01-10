using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public Transform[] spawnPoints;
    public GameObject[] monsters;
    int randomSpawnPoint, randomMonster;
    public static bool spawnAllowed;
    int CallTime = 0;
	// Use this for initialization
	void Start () {
        spawnAllowed = true;
        InvokeRepeating("SpawnAMonster", 3f, 1f);

        //CancelInvoke();
	}
	
	// Update is called once per frame
	//void Update () {
		
	//}

    void SpawnAMonster()
    {

        if (spawnAllowed)
        {
            randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            randomMonster = Random.Range(0, monsters.Length);
            Instantiate(monsters[randomMonster], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            CallTime++;
            Debug.Log("Enemy Called");
            if(CallTime == 3)
            {
                CancelInvoke();
                Debug.Log("Stop Calling");
            }
        }
    }
       
}
