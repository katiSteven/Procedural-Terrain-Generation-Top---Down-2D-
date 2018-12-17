using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour {

    public float speed;

    private Transform target;

    private Animator anim;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        // transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
       // var x = Input.GetAxisRaw("Horizontal");

        //if (x > 0)
        //{
        //    Debug.Log("Animating");
        //    anim.SetTrigger("Trig");
        //    //rectTransform.Rotate(new Vector3(0, 180, 0));
        //    GetComponent<Transform>().eulerAngles = new Vector3(0, -180, 0);
        //}

        //if (x < 0)
        //{
        //    Debug.Log("Animating");
        //    anim.SetTrigger("Trig");
        //    //rectTransform.Rotate(new Vector3(0, 180, 0));
        //    GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
        //}

        if (Vector2.Distance(transform.position, target.position) <= 3)
        {
            if (Vector2.Distance(transform.position, target.position) > 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                anim.SetTrigger("Spot");
                Debug.Log("Halt player");
            }
        }
        else
        {
            anim.SetTrigger("Idle");
        }
	}
}
