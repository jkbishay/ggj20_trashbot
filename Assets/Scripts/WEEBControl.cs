using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WEEBControl : MonoBehaviour
{
    public GameObject Waifu;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        // CAN ADD SPAWN SOUND HERE
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(Waifu.transform.position);
        // aud.isPlaying to check if audio source is playing
    }
}
