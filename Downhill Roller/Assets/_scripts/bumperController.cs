using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bumperController : MonoBehaviour {

    private GameObject bumper;

    public float bounceForce = 50f;

	// Use this for initialization
	void Start () {
        bumper = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision c)
    {
        GameObject otherObject = c.gameObject;

        if (otherObject.CompareTag("Player"))
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
