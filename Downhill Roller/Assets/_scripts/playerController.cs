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
    private bool bounce;
    private bool doPowerUp = false;
    private float timer;
    private Vector3 normal = Vector3.zero;
    private float powerupTime = 5f;
    private Transform timerBar;
    private buttonController bc;
    private bool first;

    public float flipperForce = 12f;
    public float jumpForce = 5f;
    public float speedForce = 1f;
    
    void Awake()
    {
        timerBar = GameObject.Find("Remaining Time").GetComponent<Transform>();
        bc = GameObject.Find("EventSystem").GetComponent<buttonController>();
        ball = this.gameObject;
        ballRB = ball.GetComponent<Rigidbody>();
        ballRB.isKinematic = true;
    }

	// Use this for initialization
	void Start () {
        currentScene = SceneManager.GetActiveScene().name;
        first = true;
        powerUps = bc.getPowerUps();
        timer = Time.realtimeSinceStartup - 5;
        
        GameObject levelAudio = GameObject.Find("Audio Source");
        if (levelAudio != null)
        {
            if (GameObject.Find("KeptAudio") == null)
            {
                levelAudio.name = "KeptAudio";
                DontDestroyOnLoad(levelAudio);
            }
            else
            {
                levelAudio.GetComponent<AudioSource>().Stop();
            }
        }
    }

    // Update is called once per frame
    void Update () {

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) && first)
        {
            ballRB.isKinematic = false;
            first = false;
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
                bounce = false;
                powerUps[2].SetActive(false);
                powerUps[0].SetActive(false);
                timer = 0;

            }
            float scale = (powerupTime - (Time.realtimeSinceStartup - timer)) / powerupTime;
            timerBar.localScale = new Vector3(scale, 1, 1);
            //Debug.Log();
        }
        else if (doPowerUp && timer + powerupTime < Time.realtimeSinceStartup)
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
            
            ParticleSystem ps = GameObject.Find("Victory Fireworks").GetComponent<ParticleSystem>();
            ball.transform.rotation = new Quaternion(0, 0, 0, 0);
            if (ps)
            {
                ps.Play();
            }
            ballRB.isKinematic = true;

            
            if (SceneManager.GetActiveScene().buildIndex >= 10)
            {
                StartCoroutine(bc.fadeOut(0));
            }
            else
            {
                buttonController.levelsAdded[SceneManager.GetActiveScene().buildIndex+1] = true;
                StartCoroutine(bc.fadeOut(SceneManager.GetActiveScene().buildIndex + 1));
            }
        }
        if (c.CompareTag("pu_Boost") && !(boost||bounce||hover))
        {
            c.gameObject.SetActive(false);
            powerUps[0].SetActive(true);
            powerUps[1].SetActive(true);
            boost = true;
            GameObject.Find("Remaining Time").transform.localScale = new Vector3(1, 1, 1);
        }
        if (c.CompareTag("pu_Bounce") && !(boost || bounce || hover))
        {
            c.gameObject.SetActive(false);
            powerUps[0].SetActive(true);
            powerUps[2].SetActive(true);
            bounce = true;
            GameObject.Find("Remaining Time").transform.localScale = new Vector3(1, 1, 1);
        }
        if (c.CompareTag("pu_Hover") && !(boost || bounce || hover))
        {
            c.gameObject.SetActive(false);
            StartCoroutine(respawn(c.gameObject));
            powerUps[0].SetActive(true);
            powerUps[3].SetActive(true);
            hover = true;
            GameObject.Find("Remaining Time").transform.localScale = new Vector3(1, 1, 1);
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

    IEnumerator respawn(GameObject obj)
    {
        yield return new WaitForSeconds(5f);

        obj.SetActive(true);
    }
}
