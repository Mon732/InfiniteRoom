using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10;
    public float turnSpeed = 6;

    float speedMult = 10;
    float yaw;
    float pitch;

    new Rigidbody rigidbody;

    // Use this for initialization
    void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rigidbody = GetComponent<Rigidbody>();

        yaw = transform.rotation.eulerAngles.y;
        pitch = transform.rotation.eulerAngles.x;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.velocity = Vector3.zero;
        }

        if (Input.GetMouseButton(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Cursor.lockState == CursorLockMode.Locked)
        {

            float moveHorizontal = Input.GetAxis("Horizontal") * speed * speedMult;
            float moveVertical = Input.GetAxis("Vertical") * speed * speedMult;

            yaw += Input.GetAxis("Mouse X") * turnSpeed * speedMult * Time.deltaTime;
            pitch -= Input.GetAxis("Mouse Y") * turnSpeed * speedMult * Time.deltaTime;

            rigidbody.velocity = (transform.forward * moveVertical) + (transform.right * moveHorizontal);
            transform.rotation = Quaternion.Euler(pitch, yaw, 0);
        }
    }
}
