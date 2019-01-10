using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColl : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tree")
        {
          //  collision.gameObject.SendMessage("ApplyDamage", 10);
            Debug.Log("Collision is detected -->1");
        }
    }
}
