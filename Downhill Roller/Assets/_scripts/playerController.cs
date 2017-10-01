using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour {

    private GameObject ball;
    private Rigidbody ballRB;
    private string currentScene;

	// Use this for initialization
	void Start () {
        ball = this.gameObject;
        ballRB = ball.GetComponent<Rigidbody>();
        currentScene = SceneManager.GetActiveScene().name;

        ballRB.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            ballRB.useGravity = true;
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
    }
}
