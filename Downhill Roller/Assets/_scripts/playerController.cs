using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    private playerController ball;
    

	// Use this for initialization
	void Start () {
        ball = this;
        Debug.Log(transform.parent);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
