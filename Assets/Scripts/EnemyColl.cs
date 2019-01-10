using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyColl : MonoBehaviour
{
    public float speed;
    private Transform targetE;
    public float Starthealth = 100;
    private float health;
    public int attack = 0;

    [Header("Unity Stuff")]
    public Image healthBar;

    void Start()
    {
        health = Starthealth;
    }

    void Update()
    {
        targetE = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
        if (Vector2.Distance(transform.position, targetE.position) > 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position, speed * Time.deltaTime);
            Debug.Log("Halt player from enemy coll");
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.tag == "Player")
        {
            //mousectrl.OnMouseDown();
            //OnMouseDown();
            attack = 1;
        }
    }


    public void OnMouseDown()
    {
        if (attack == 1)
        {
            Debug.Log("Mouse Click");

            //  collision.gameObject.SendMessage("ApplyDamage", 10);
            health -= 10;
            Debug.Log("Collision detected --> Player");
            healthBar.fillAmount = health / Starthealth;

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