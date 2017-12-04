using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript2 : MonoBehaviour {
    public Transform startPoint;
    public Transform endPoint;
    LineRenderer laserLine;
	// Use this for initialization
	void Start () {
        laserLine = GetComponent<LineRenderer>();
        laserLine.startWidth = 0.2f;
        laserLine.endWidth = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {
        laserLine.SetPosition (0, startPoint.position);
        laserLine.SetPosition(1, endPoint.position );
	}
}
