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

    public float maxOffset = 45f;
    public float speed = 200f;
    public bool inversedFlipper = false;
    public float bounceForce = 75f;
   
    // Use this for initialization
    void Start () {
        thisFlipper = this;
        dir = thisFlipper.CompareTag("Flipper_CW") ? 1 : -1;
    }

    // Update is called once per frame
    void FixedUpdate () {

        //Debug.Log(angle);
        if (Input.anyKey)
        {
            action = true;
        }
        else
        {
            action = false;
        }

        if (action)
        {
            
            if (angle <= maxOffset)
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
            foreach (ContactPoint p in c.contacts)
            {
                Vector3 force = otherObject.transform.position - p.point;
                Rigidbody _rb = otherObject.GetComponent<Rigidbody>();
                _rb.AddForce(force * bounceForce);
            }
        }
    }
}
