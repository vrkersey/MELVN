using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tunnelController : MonoBehaviour {
    private GameObject tunnel;

    public float boostForce = 15f;

	// Use this for initialization
	void Start () {
        tunnel = this.gameObject;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionStay(Collision c)
    {
        GameObject otherObject = c.gameObject;

        if (otherObject.CompareTag("Player"))
        {
            Rigidbody _rb = otherObject.GetComponent<Rigidbody>();
            Vector3 dir = _rb.velocity;
            _rb.AddForce(dir * boostForce);

        }
    }
}
