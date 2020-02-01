using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed;
    public Camera MainCam;

    private float maxSpeed;
    private Rigidbody rgbd;
    private Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = 13;
        rgbd = this.GetComponent<Rigidbody>();
        cameraOffset = MainCam.transform.position - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // make camera follow player 
        MainCam.transform.position = this.transform.position + cameraOffset;

        // movement when not at max speed
        if (rgbd.velocity.magnitude < maxSpeed)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rgbd.AddForce(Vector3.forward * moveSpeed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                rgbd.AddForce(Vector3.left * moveSpeed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rgbd.AddForce(Vector3.back * moveSpeed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rgbd.AddForce(Vector3.right * moveSpeed);
            }
        }

        // decay over time
        this.transform.localScale -= Vector3.one * 0.00025f;

        // die when too smol
        if (this.transform.localScale.magnitude < 0.7f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // pickup trash
        if (other.tag == "trash")
        {
            Destroy(other.gameObject);
            this.transform.localScale += Vector3.one * 0.1f;
        }
    }
}
