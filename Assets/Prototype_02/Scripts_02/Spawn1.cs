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
	
	// Trigger condition and execution
	void OnTriggerEnter (Collider other)
    {
		if  (other.gameObject.tag == "Player" && isTriggered == false)
        {
            
            isTriggered = true;
            for (int i = 0; i < enemiesSpawn1.Length; i++)
            {
                enemiesSpawn1[i].SetActive(true);
            }
        }
	}
}
