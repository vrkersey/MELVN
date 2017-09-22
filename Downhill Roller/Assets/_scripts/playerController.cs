using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour {

    private playerController ball;
    private string currentScene;

	// Use this for initialization
	void Start () {
        ball = this;
        currentScene = SceneManager.GetActiveScene().name;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Death_Zone"))
        {
            print("Death");
            SceneManager.LoadScene(currentScene);
        }
        if (c.CompareTag("Win_Zone"))
        {
            print("Winner");
            SceneManager.LoadScene(currentScene);
        }
    }
}
