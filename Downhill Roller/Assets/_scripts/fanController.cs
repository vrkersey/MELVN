using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fanController : MonoBehaviour {
    private GameObject self;
    private Vector3 thisPosition;
    private float force;
    private Animator anim;
    private ParticleSystem[] wind;

    public float inactiveForce = 25f;
    public float activeForce;
    public bool alwaysActive = false;

	// Use this for initialization
	void Start () {
        self = this.gameObject;
        thisPosition = self.transform.position;
        anim = GetComponent<Animator>();
        anim.speed = inactiveForce;
        wind = self.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem w in wind)
        {
            w.enableEmission = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        force = inactiveForce;
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) && !alwaysActive)
        {
            force = activeForce;
            anim.speed = anim.speed <= activeForce ? anim.speed + .5f: activeForce;
            foreach (ParticleSystem w in wind)
            {
                w.enableEmission = true;
            }
        }
        else
        {
            anim.speed = anim.speed >= inactiveForce ? anim.speed - .5f : inactiveForce;
            foreach (ParticleSystem w in wind)
            {
                w.enableEmission = false;
            }
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
