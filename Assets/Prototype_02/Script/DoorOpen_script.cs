using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen_script : MonoBehaviour {

    public GameObject lastDoor;

	// Use this for initialization
	void Start ()
    {
        lastDoor.gameObject.SetActive(true);	
	}

    public void OnTriggerEnter(Collider other)
    {
       if (other.tag == "Player")
       {
            lastDoor.gameObject.SetActive(false);
       }
    }

}
