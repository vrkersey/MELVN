using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEWflipperController : MonoBehaviour {

    private float flipperDamper = 15f;
    private HingeJoint hinge;
    private float springForce = 10000f;

    public float offset = 45f;
    

   void Start()
    {
        hinge = GetComponent<HingeJoint>();
        hinge.useSpring = true;
        hinge.useLimits = true;
        JointLimits j = new JointLimits();
        
        if (this.CompareTag("Flipper_CW"))
        {
            j.max = offset;
            j.min = 0;
        }
        else
        {
            j.max = 0;
            j.min = -offset;
            offset = -offset;
        }
        
        hinge.limits = j;
    }

    // Update is called once per frame
    void Update()
    {
        JointSpring spring = new JointSpring();
        spring.spring = springForce;
        spring.damper = flipperDamper;

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)))
        {
            spring.targetPosition = offset;
        }
        else
        {
            spring.targetPosition = 0f;
        }

        hinge.spring = spring;
        

    }
}
