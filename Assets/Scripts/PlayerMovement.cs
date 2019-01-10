using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public int PlayerHealth = 10;

    private Animator anim;
    private CapsuleCollider2D bodyCollider;

    //private
    //bool isAlive = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update ()
    {
        Movement();
        Die();
    }

    private void Die() {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            //isAlive = false;
            Debug.Log("player on water");
            //destroy game object
        }
    }

    private void Movement()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);
        transform.position = transform.position + movement * Time.deltaTime;
        var x = Input.GetAxisRaw("Horizontal");
        //Debug.Log("From here -1");
        //Debug.Log(x);
        if (x < 0)
        {
            anim.SetTrigger("Trig");
            GetComponent<Transform>().eulerAngles = new Vector3(0, -180, 0);
        }

        if (x > 0)
        {
            anim.SetTrigger("Trig");
            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);

        }
    }
}
