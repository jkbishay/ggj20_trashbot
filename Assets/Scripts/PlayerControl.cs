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
    private Vector3 cameraZoomOffset;
    [HideInInspector] public bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = 13;
        rgbd = this.GetComponent<Rigidbody>();
        cameraOffset = MainCam.transform.position - this.transform.position;
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        // make camera follow player 
        cameraZoomOffset = MainCam.transform.forward * this.transform.localScale.magnitude;
        MainCam.transform.position = this.transform.position + cameraOffset - cameraZoomOffset;

        // movement when not at max speed
        if (rgbd.velocity.magnitude < maxSpeed && isAlive)
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
        if (isAlive)
        {
            this.transform.localScale -= Vector3.one * 0.0005f;
        }

        // die when too smol
        if (this.transform.localScale.magnitude < 0.7f)
        {
            this.GetComponentInChildren<MeshRenderer>().enabled = false;
            isAlive = false;
            foreach (Collider col in this.GetComponents<Collider>())
            {
                col.enabled = false;
            }
            rgbd.useGravity = false;
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

    private void OnCollisionEnter(Collision collision)
    {
        // die when hit by WEEB
        if (collision.gameObject.tag == "WEEB")
        {
            this.GetComponentInChildren<MeshRenderer>().enabled = false;
            isAlive = false;
            foreach (Collider col in this.GetComponents<Collider>())
            {
                col.enabled = false;
            }
            rgbd.useGravity = false;
        }
    }
}
