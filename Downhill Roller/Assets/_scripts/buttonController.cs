using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//load availible levels to dropdown
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("exit pressed");
        }
	}

    public void Exit_Level()
    {

    }

    public void Start_Level(Dropdown levelDropdown)
    {
        print(levelDropdown.value);
    }
}
