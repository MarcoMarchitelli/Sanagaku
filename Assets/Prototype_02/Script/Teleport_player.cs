using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_player : MonoBehaviour {
    public GameObject player;
    public GameObject spawn_point;
    //public Vector3 teleport_point;
    public Collider teleport_trigger;

	// Use this for initialization
	void Start ()
    {
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.transform.localPosition = spawn_point.transform.localPosition;
        }
    }


}
