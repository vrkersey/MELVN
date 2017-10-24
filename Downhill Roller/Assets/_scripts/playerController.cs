using System.Collections;
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
    private bool bounce = true;
    private bool doPowerUp = false;
    private float timer;

	// Use this for initialization
	void Start () {
        ball = this.gameObject;
        ballRB = ball.GetComponent<Rigidbody>();
        currentScene = SceneManager.GetActiveScene().name;

        ballRB.useGravity = false;

        powerUps = GameObject.Find("buttonManager").GetComponent<buttonController>().getPowerUps();
        timer = Time.realtimeSinceStartup - 5;

    }
	
	// Update is called once per frame
	void Update () {

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)))
        {
            ballRB.useGravity = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && (boost || hover || bounce))
        {
            //Debug.Log("activating power up");
            if (!doPowerUp)
            {
                timer = Time.realtimeSinceStartup;
            }

            doPowerUp = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) && (boost || hover || bounce))
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
                ballRB.AddForce(v * 100);
            }
            else if (hover)
            {
                //Debug.Log("Hovering");
                ballRB.useGravity = false;
            }
            else if (bounce)
            {
                //Vector3 v = Quaternion.Euler(0,0,90) * ballRB.velocity.normalized;
                //v.y = v.y > 0 ? v.y : -v.y;
                //Debug.Log(v);
                //ballRB.AddForce(v * 5000);
                //timer = 0;
                bounce = false;
            }
        }
        else if (doPowerUp && timer + 5 < Time.realtimeSinceStartup)
        {
            //Debug.Log("power up finished");
            doPowerUp = false;
            if (boost)
            {
                boost = false;
            }
            if (hover)
            {
                hover = false;
                ballRB.useGravity = true;
            }
        }
    }

    public void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Death_Zone"))
        {
            Debug.Log("Death");
            SceneManager.LoadScene(currentScene);
        }
        if (c.CompareTag("Win_Zone"))
        {
            Debug.Log("Winner");
            SceneManager.LoadScene(currentScene);
        }
        if (c.CompareTag("pu_Boost"))
        {
            Debug.Log("Boost obtained");
            c.gameObject.SetActive(false);
            powerUps[0].SetActive(true);
            powerUps[1].SetActive(true);
            boost = true;
        }
        if (c.CompareTag("pu_Bounce"))
        {
            Debug.Log("Bounce obtained");
            c.gameObject.SetActive(false);
            powerUps[0].SetActive(true);
            powerUps[2].SetActive(true);
            bounce = true;
        }
        if (c.CompareTag("pu_Hover"))
        {
            Debug.Log("Hover obtained");
            c.gameObject.SetActive(false);
            powerUps[0].SetActive(true);
            powerUps[3].SetActive(true);
            hover = true;
        }
    }
}
