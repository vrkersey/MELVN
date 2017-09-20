using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    private Controller thisFlipper;
    private float maxX;
    private float minX;
    private int dir;
    private bool action = false;
    private float angle = 0f;

    public float maxOffset = 45f;
    public float speed = 100f;

    // Use this for initialization
    void Start () {
        thisFlipper = this;
        dir = thisFlipper.CompareTag("Flipper_CW") ? 1 : -1;
        maxX = thisFlipper.transform.localEulerAngles.x + maxOffset * -dir;
        maxX = maxX >= 360 ? maxX - 360 : maxX;
        minX = thisFlipper.transform.localEulerAngles.x;

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
