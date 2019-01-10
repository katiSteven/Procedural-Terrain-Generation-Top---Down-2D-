using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCombat : MonoBehaviour {

    public float Starthealth = 100;
    private float health;
    public int attack = 0;

   // [Header("Unity Stuff")]
   // public Image healthBar;

    // Use this for initialization
    void Start () {
        health = Starthealth;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            health -= 10;
            //healthBar.fillAmount = health / Starthealth;

            //Enemy's Health

            Debug.Log(health);
            if (health <= 0)
            {
                //Death animation here
                Destroy(gameObject);
            }
        }
    }
}
