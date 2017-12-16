using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonController : MonoBehaviour {
    private GameObject playButton;
    private GameObject[] powerUps = new GameObject[4];
    private Dropdown levelSelect;

    public CanvasGroup fader;
    static public bool[] levelsAdded = { true, true, false, false, false, false, false, false, false, false, false };

    // Use this for initialization
    void Start () {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            StartCoroutine(fadeIn());

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
            for (int c = 1; c < 11; c++)
            {
                if (levelsAdded[c])
                {
                    levelSelect.AddOptions(new List<string> { "Level " + c });
                }
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
        playButton.SetActive(false);
        Time.timeScale = 1;
    }
    public void Pause()
    {
        playButton.SetActive(true);
        Time.timeScale = 0;
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void Quit()
    {
        GameObject levelAudio = GameObject.Find("KeptAudio");
        if (levelAudio != null)
        {
            Destroy(levelAudio);
        }
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void UnlockLevels()
    {
        for (int c = 0; c < levelsAdded.Length; c++)
            buttonController.levelsAdded[c] = true;
        SceneManager.LoadScene(0);
       
    }
    public void LockLevels()
    {
        for (int c = 2; c < levelsAdded.Length; c++)
            buttonController.levelsAdded[c] = false;
        SceneManager.LoadScene(0);

    }

    public GameObject[] getPowerUps()
    {
        return powerUps;
    }

    public IEnumerator fadeIn()
    {
        float timer = 0f;

        while (timer < 1f)
        {
            timer += Time.deltaTime;

            fader.alpha = Mathf.Lerp(1f, 0f, timer);

            yield return null;
        }
    }
    public IEnumerator fadeOut(int toLevel)
    {
        float timer = 0f;
        GameObject levelAudio = GameObject.Find("KeptAudio");

        while (timer < 1f)
        {
            timer += Time.deltaTime;

            fader.alpha = Mathf.Lerp(0f, 1f, timer);

            yield return null;
        }
        if (levelAudio != null)
        {
            Destroy(levelAudio);
        }
        SceneManager.LoadScene(toLevel);
    }
}
