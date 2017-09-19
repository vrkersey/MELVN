using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    GameObject[] ccwFlippers;
    GameObject[] cwFlippers;

    // Use this for initialization
    void Start () {
        ccwFlippers = GameObject.FindGameObjectsWithTag("Fipper_CCW");
        cwFlippers = GameObject.FindGameObjectsWithTag("Fipper_CW");
        Debug.Log("game has started with " + (ccwFlippers.Length + cwFlippers.Length) + " flippers");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
