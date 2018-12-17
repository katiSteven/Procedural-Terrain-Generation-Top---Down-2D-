using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;
    private Animator anim;

    //public float runSpeed = 40f;

    //float horizontalMove = 0f;
    //bool jump = false;
    //bool crouch = false;

    // Use this for initialization
    void Start()
    {
          anim = GetComponent<Animator>();
          //RectTransform rectTransform = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update () {
        // Debug.Log("Inside ");
        // Debug.Log(Input.GetAxisRaw("Horizontal"));
        // horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        // Debug.Log("From here");
        // Debug.Log(horizontalMove);

        // if (horizontalMove > 0)
        // {
        ////     anim.SetTrigger("Run");
        // }
        // if (Input.GetButtonDown("Jump"))
        // {
        //     jump = true;
        //     horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        ////     anim.SetTrigger("Run");
        // }
        // if (Input.GetButtonDown("Crouch"))
        // {
        //     crouch = true;
        // } else if (Input.GetButtonUp("Crouch"))
        // {
        //     crouch = false;
        // }
        int PlayerHealth = 10;
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);
        transform.position = transform.position + movement * Time.deltaTime;
        Debug.Log("From here");
        var x = Input.GetAxisRaw("Horizontal");
        Debug.Log("From here -1");
        Debug.Log(x);
        //        anim.SetTrigger("Trig");
        //int tr = transform.position;
        if (x < 0)
        {
            Debug.Log("Animating");
            anim.SetTrigger("Trig");
            //rectTransform.Rotate(new Vector3(0, 180, 0));
            GetComponent<Transform>().eulerAngles = new Vector3(0,-180,0);
        }

        if (x > 0)
        {
            Debug.Log("Animating");
            anim.SetTrigger("Trig");
            //rectTransform.Rotate(new Vector3(0, 180, 0));
            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
            //transform.position = new Vector3(0, 0, 0);
            Debug.Log("Object Destroyed");
            
        }

        if (PlayerHealth == 0)
        {
            //CharacterController cc = GetComponent(typeof(CharacterController)) as CharacterController;
            //cc.enabled = false; // Turn off the component
            movement = new Vector3(0, 0, 0.0f);
            transform.position = new Vector3(0, 0, 0);
        }
    }
    

    //void FixedUpdate()
    //{
    //    Debug.Log("Inside Fixed Update");
    //controller.Move(horizontalMove* Time.fixedDeltaTime, crouch, jump);

    //    jump = false;

    //}
}
