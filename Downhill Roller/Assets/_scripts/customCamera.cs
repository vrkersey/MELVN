
using UnityEngine;

public class customCamera : MonoBehaviour {

    private bool locked = true;
    private Vector3 offset;
    private Vector3 m_CurrentVelocity;
    private Transform Player;

	public float zoomSpeed;
    public float distanceFromBall = 10f;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = new Vector3(0, 0, distanceFromBall);
        distanceFromBall = -distanceFromBall;
        if (zoomSpeed == 0)
            zoomSpeed = 2.5f;
    }

	// Update is called once per frame
	void Update ()
    {
        float mag = Player.GetComponent<Rigidbody>().velocity.magnitude;
        if (!locked)
            transform.position = new Vector3(Player.position.x, Player.position.y, transform.position.z);
        offset.z = distanceFromBall - mag;
        Vector3 zoom = Player.position + offset;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, zoom, ref m_CurrentVelocity, zoomSpeed);
        newPos.z = newPos.z < distanceFromBall ? newPos.z : distanceFromBall;
        transform.position = newPos;
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) && locked)
        {
            locked = false;
            transform.position = Player.position + offset;
            zoomSpeed = 2.5f;
        }
    }
}
