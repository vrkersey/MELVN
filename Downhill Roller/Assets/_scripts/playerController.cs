﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour {

    private GameObject ball;
    private Rigidbody ballRB;
    private string currentScene;
    private GameObject[] powerUps;
    private bool boost;
    private bool hover;
    private bool bounce;
    private bool doPowerUp = false;
    private float timer;
    private Vector3 normal = Vector3.zero;

    public float flipperForce = 12f;
    public float jumpForce = 5f;
    public float speedForce = 1f;
	// Use this for initialization
	void Start () {
        ball = this.gameObject;
        ballRB = ball.GetComponent<Rigidbody>();
        currentScene = SceneManager.GetActiveScene().name;

        ballRB.useGravity = false;

        powerUps = GameObject.Find("EventSystem").GetComponent<buttonController>().getPowerUps();
        timer = Time.realtimeSinceStartup - 5;
    }
	
	// Update is called once per frame
	void Update () {

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)))
        {
            ballRB.useGravity = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && (boost || hover || (bounce && normal != Vector3.zero)))
        {
            //Debug.Log("activating power up");
            if (!doPowerUp)
            {
                timer = Time.realtimeSinceStartup;
            }

            doPowerUp = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) && (boost || hover || (bounce && normal != Vector3.zero)))
        {
            //Debug.Log("let off mouse");
            timer = 0;
        }

        if (doPowerUp && (timer+5 > Time.realtimeSinceStartup))
        {
            if (boost)
            {
                //Debug.Log("Doing boost");
                Vector3 v = ballRB.velocity;
                v.Normalize();
                ballRB.AddForce(v * speedForce * 100);
            }
            else if (hover)
            {
                //Debug.Log("Hovering");
                ballRB.useGravity = false;
            }
            else if (bounce)
            {
                ballRB.AddForce(normal * jumpForce * 1000);
                //timer = 0;
                bounce = false;
                powerUps[2].SetActive(false);
                powerUps[0].SetActive(false);
            }
        }
        else if (doPowerUp && timer + 5 < Time.realtimeSinceStartup)
        {
            //Debug.Log("power up finished");
            doPowerUp = false;
            if (boost)
            {
                boost = false;
                powerUps[1].SetActive(false);
                powerUps[0].SetActive(false);
            }
            if (hover)
            {
                hover = false;
                ballRB.useGravity = true;
                powerUps[3].SetActive(false);
                powerUps[0].SetActive(false);
            }
        }
    }

    public void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Death_Zone"))
        {
            SceneManager.LoadScene(currentScene);
        }
        if (c.CompareTag("Win_Zone"))
        {
            Debug.Log(buttonController.LevelsAdded);
            buttonController.LevelsAdded++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

        }
        if (c.CompareTag("pu_Boost"))
        {
            c.gameObject.SetActive(false);
            powerUps[0].SetActive(true);
            powerUps[1].SetActive(true);
            boost = true;
        }
        if (c.CompareTag("pu_Bounce"))
        {
            c.gameObject.SetActive(false);
            powerUps[0].SetActive(true);
            powerUps[2].SetActive(true);
            bounce = true;
        }
        if (c.CompareTag("pu_Hover"))
        {
            c.gameObject.SetActive(false);
            powerUps[0].SetActive(true);
            powerUps[3].SetActive(true);
            hover = true;
        }
    }

    public void OnTriggerStay(Collider c)
    {
        if (c.CompareTag("Booster_Pipe"))
        {
            float boost = c.GetComponent<tunnelController>().boostForce/10;
            ballRB.velocity += (ballRB.velocity.normalized * boost); 
        }
    }

    public void OnCollisionEnter(Collision c)
    {
        normal = ball.transform.position - c.contacts[0].point;
        normal.Normalize();
    }

    public void OnCollisionExit(Collision c)
    {
        normal = Vector3.zero;
    }
}
