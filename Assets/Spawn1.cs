using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn1 : MonoBehaviour {

    public GameObject[] enemiesSpawn1;
    private bool isTriggered;


	// Use this for initialization
	void Start () {

        isTriggered = false;


	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other)
    {
		if  (other.gameObject.tag == "Player" && isTriggered == false)
        {
            
        }
	}
}
