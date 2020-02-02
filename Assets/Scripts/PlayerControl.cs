using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed;
    public Camera MainCam;
    public ParticleSystem FireEffect;
    public AudioClip yell;

    private float maxSpeed;
    private float rateOfDecay;
    private Rigidbody rgbd;
    private AudioSource aud;
    private Vector3 cameraOffset;
    private Vector3 cameraZoomOffset;
    [HideInInspector] public bool isAlive;
    private bool onFire;
    private float fireTimer;

    // Start is called before the first frame update
    void Start()
    {
        FireEffect.gameObject.SetActive(false);
        maxSpeed = 13;
        rateOfDecay = 0.0005f;
        rgbd = this.GetComponent<Rigidbody>();
        aud = this.GetComponent<AudioSource>();
        cameraOffset = MainCam.transform.position - this.transform.position;
        isAlive = true;
        onFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        // make camera and other effects follow player 
        cameraZoomOffset = MainCam.transform.forward * this.transform.localScale.magnitude;
        MainCam.transform.position = this.transform.position + cameraOffset - cameraZoomOffset;
        FireEffect.transform.position = this.transform.position + new Vector3(-0.25f, 0.375f, -0.75f);
        FireEffect.transform.localScale = this.transform.localScale;

        // make trash bag look at move direction
        if (rgbd.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(rgbd.velocity.x, 0, rgbd.velocity.z));
        }

        // decay over time
        if (isAlive)
        {
            this.transform.localScale -= Vector3.one * rateOfDecay;
        }

        // timer for being on fire
        if (onFire)
        {
            fireTimer += Time.deltaTime;
        }

        // reset after fire timer is finished 
        if (onFire && fireTimer >= 5)
        {
            onFire = false;
            fireTimer = 0;
            maxSpeed -= 10;
            moveSpeed -= 5;
            rateOfDecay /= 2.5f;
            FireEffect.gameObject.SetActive(false);
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

    private void FixedUpdate()
    {
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

        if (collision.gameObject.tag == "firehazard")
        {
            Destroy(collision.gameObject);
            if (!onFire)
            {
                maxSpeed += 10;
                moveSpeed += 5;
                rateOfDecay *= 2.5f;
            } 
            onFire = true;
            fireTimer = 0;
            FireEffect.gameObject.SetActive(true);
            aud.clip = yell;
            aud.Play();
        }
    }
}
