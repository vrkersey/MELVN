
using UnityEngine;

public class customCamera : MonoBehaviour {

    private bool locked = true;
    private Vector3 offset;
    private Vector3 m_CurrentVelocity;

    public Transform Player;
	public float zoomSpeed;

    void Start()
    {
        offset = new Vector3(0, 0, -10f);
    }

	// Update is called once per frame
	void Update ()
    {

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) && locked)
        {
            locked = false;
            transform.position = new Vector3(Player.position.x, Player.position.y, -10f);
            Debug.Log(transform.position);
        }

        float mag = Player.GetComponent<Rigidbody>().velocity.magnitude;

        transform.position =
        offset.z = -10f - mag;
        offset.z = offset.z < -10f ? offset.z : -10f;
        Vector3 zoom = Player.position + offset;
        print(zoom);
        Vector3 newPos = Vector3.SmoothDamp(transform.position, zoom, ref m_CurrentVelocity, 2.5f);
        transform.position = newPos;
        
   	}
}
