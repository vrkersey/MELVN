
using UnityEngine;

public class customCamera : MonoBehaviour {

    private bool locked = true;
    private Vector3 offset;
    private Vector3 m_CurrentVelocity;

    public Transform Player;
	public float zoomSpeed;

    void Start()
    {
        offset = new Vector3(0,0,-10f);
    }

	// Update is called once per frame
	void Update ()
    {
        float mag = Player.GetComponent<Rigidbody>().velocity.magnitude;

        transform.position = new Vector3(Player.position.x, Player.position.y, transform.position.z);
        offset.z = -10 - mag;
        Vector3 zoom = Player.position + offset;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, zoom, ref m_CurrentVelocity, 2.5f);
        transform.position = newPos;
        if (Input.anyKey && locked)
        {
            locked = false;
            transform.position = Player.position + offset;
        }
   	}
}
