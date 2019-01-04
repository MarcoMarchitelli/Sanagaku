using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloser_script : MonoBehaviour {

    public GameObject Door;

	// Use this for initialization
	void Start ()
    {
        Door.gameObject.SetActive(false);	
	}

    public void OnTriggerEnter(Collider other)
    {
       if (other.tag == "Player")
        {
            Door.gameObject.SetActive(true);
        }
    }

}
