using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonController : MonoBehaviour {
    private GameObject playButton;
    private GameObject[] powerUps = new GameObject[4];
    private Dropdown levelSelect;
    static private int levelsAdded;

    static public int LevelsAdded { get { return levelsAdded; } set { levelsAdded = value; } }


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
        else
        {
            levelSelect = GameObject.Find("Level Select").GetComponent<Dropdown>();
            for (int c = 0; c <= levelsAdded; c++)
            {
                levelSelect.AddOptions(new List<string> { "Level " + (c + 1) });
            }
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
        SceneManager.LoadScene(levelDropdown.value + 1);
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
