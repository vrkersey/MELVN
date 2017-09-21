using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    private Controller thisFlipper;
    private int dir;
    private bool action = false;
    private float angle = 0f;

    public float maxOffset = 45f;
    public float speed = 200f;

    // Use this for initialization
    void Start () {
        thisFlipper = this;
        dir = thisFlipper.CompareTag("Flipper_CW") ? 1 : -1;

        Debug.Log("Game has Started");        
    }

    // Update is called once per frame
    void Update () {

        //Debug.Log(angle);
        if (Input.anyKey)
        {
            action = true;
        }

        if (action)
        {
            
            if (angle <= maxOffset)
            {
                float delta = speed * Time.deltaTime * dir;
                transform.Rotate(Vector3.up, delta);
                angle += Math.Abs(delta);
            }
            else
            {
                action = false;
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
}
