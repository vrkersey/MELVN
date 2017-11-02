using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flipperController : MonoBehaviour {

    private flipperController thisFlipper;
    private int dir;
    private bool action = false;
    private float angle = 0f;
    private Vector3 normal;
    private bool col_flag = false;

    public float maxOffset = 45f;
    public float speed = 200f;
    public bool inversedFlipper = false;
    public float bounceForce = 12f;
   
    // Use this for initialization
    void Start () {
        thisFlipper = this;
        dir = thisFlipper.CompareTag("Flipper_CW") ? 1 : -1;
    }

    // Update is called once per frame
    void FixedUpdate () {

        //Debug.Log(angle);
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)))
        {
            action = true;
        }
        else
        {
            action = false;
        }

        if (action)
        {
            
            if (angle <= maxOffset && !col_flag)
            {
                float delta = speed * Time.deltaTime * dir;
                transform.Rotate(Vector3.up, delta);
                angle += Math.Abs(delta);
            }
       
        }
        else
        {
            if (angle > 0)
            {
                float delta = speed * Time.deltaTime * -dir;
                transform.Rotate(Vector3.up, delta);
                angle -= Math.Abs(delta);
            }
        }
    }
    
    public void OnCollisionStay(Collision c)
    {
        GameObject otherObject = c.gameObject;

        if (otherObject.CompareTag("Player") && (inversedFlipper ? !action : action))
        {
            if (bounceForce == -1)
            {
                bounceForce = otherObject.GetComponent<playerController>().flipperForce;
            }
            foreach (ContactPoint p in c.contacts)
            {
                Vector3 vector = otherObject.transform.position - p.point;
                vector.Normalize();
                Rigidbody _rb = otherObject.GetComponent<Rigidbody>();
                _rb.velocity = _rb.velocity + vector * bounceForce/5;
                //_rb.AddForce(force * bounceForce);
            }
        }
        //col_flag = true;
    }

    public void OnCollisionExit(Collision c)
    {
        col_flag = false;
    }
}
