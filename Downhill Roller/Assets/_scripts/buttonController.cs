using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonController : MonoBehaviour {
    private GameObject playButton;
    private GameObject[] powerUps = new GameObject[4];

    // Use this for initialization
    void Start () {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            GameObject powerUp;
            GameObject boost;
            GameObject bounce;
            GameObject hover;

            playButton = GameObject.Find("Play");
            playButton.SetActive(false);

            boost = GameObject.Find("Boost");
            boost.SetActive(false);
            bounce = GameObject.Find("Bounce");
            bounce.SetActive(false);
            hover = GameObject.Find("Hover");
            hover.SetActive(false);
            powerUp = GameObject.Find("Power Up");
            powerUp.SetActive(false);
            powerUps[0] = powerUp;
            powerUps[1] = boost;
            powerUps[2] = bounce;
            powerUps[3] = hover;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Menu")
        {
            //open menu
            if (!playButton.activeSelf)
            {
                playButton.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                playButton.SetActive(true);
                Time.timeScale = 0;
            }
        }
	}

    public void Exit_Level()
    {
        Application.Quit();
    }

    public void Start_Level(Dropdown levelDropdown)
    {
        string level = "Menu";
        int itemSelected = levelDropdown.value;
        print(itemSelected);
        menuDropdown script = GameObject.Find("Level Select").GetComponent<menuDropdown>();
        switch (itemSelected)
        {
            case 0:
                level = script.level1;
                break;
            case 1:
                level = script.level2; 
                break;
            case 2:
                level = script.level3; 
                break;
            case 3:
                level = script.level4;
                break;
        }

        SceneManager.LoadScene(level);
    }

    public void Resume()
    {
        playButton.SetActive(true);
        Time.timeScale = 1;
    }
    public void Pause()
    {
        playButton.SetActive(false);
        Time.timeScale = 0;
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public GameObject[] getPowerUps()
    {
        return powerUps;
    }
}
