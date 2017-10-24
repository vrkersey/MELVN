using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonController : MonoBehaviour {
    private GameObject pauseMenu;
    private GameObject[] powerUps = new GameObject[4];

    // Use this for initialization
    void Start () {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            GameObject powerUp;
            GameObject boost;
            GameObject bounce;
            GameObject hover;

            pauseMenu = GameObject.Find("Pause");
            pauseMenu.SetActive(false);

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
            if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                pauseMenu.SetActive(true);
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
        switch (itemSelected)
        {
            case 0:
                level = "Ball Glow NEW";
                break;
            case 1:
                level = "Victors level";
                break;
            case 2:
                level = "ED";
                break;
        }

        SceneManager.LoadScene(level);
    }

    public GameObject[] getPowerUps()
    {
        return powerUps;
    }
}
