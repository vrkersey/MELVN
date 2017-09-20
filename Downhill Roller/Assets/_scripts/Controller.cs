using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    private BoxCollider thisFlipper;
    private float maxX;
    private int cw;

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

        if (Input.anyKey)
        {
            if (thisFlipper.transform.rotation.x >= maxX)
            {

            }
        }

        
    }
}
