using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fanController : MonoBehaviour {
    private GameObject self;
    private Vector3 thisPosition;
    private float force;
    private Animator anim;

    public float inactiveForce = 25f;
    public float activeForce;
    public bool alwaysActive = false;

	// Use this for initialization
	void Start () {
        self = this.gameObject;
        thisPosition = self.transform.position;
        anim = GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void Update () {
        //anim.Play();
        force = inactiveForce;
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) && !alwaysActive)
        {
            force = activeForce;
        }
        force *= 100;
    }
    
    void OnTriggerStay(Collider other)
    {
      
        Rigidbody other_rb = other.GetComponent<Rigidbody>();
        if (other.CompareTag("Player"))
        {
            Vector3 forceVector = (other.transform.position - thisPosition);
            float distance = forceVector.magnitude;
            forceVector.Normalize();
            other_rb.AddForce(forceVector * force/distance);
        }
    }
}
