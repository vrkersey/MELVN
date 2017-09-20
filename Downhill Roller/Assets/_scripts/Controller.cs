using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    private BoxCollider thisFlipper;
    private float maxX;
    private int cw;
    private bool action = false;

    public float maxOffset = 45f;

    // Use this for initialization
    void Start () {
        thisFlipper = GetComponent<BoxCollider>();
        cw = thisFlipper.CompareTag("Flipper_CW") ? 1 : -1;
        maxX = thisFlipper.transform.localEulerAngles.x + maxOffset * cw;

        Debug.Log("Game has Started");
        Debug.Log(thisFlipper.transform.rotation.eulerAngles.x);
        
    }

    // Update is called once per frame
    void Update () {
        int counter;

        if (Input.anyKey)
        {
            action = true;
        }

        if (action)
        {
            //move toward max

            //at max action = false
        }
        else
        {
            //mobe back to min
        }
        
    }
}
