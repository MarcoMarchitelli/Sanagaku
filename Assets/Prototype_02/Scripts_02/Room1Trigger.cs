using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1Trigger : MonoBehaviour {

    public bool isTriggered;
    Color colorOff;
    Color colorOn;
    public Light TriggerLight;

    void Start ()
    {
        isTriggered = false;
        colorOff = new Color(0.84f, 0.16f, 0.10f, 1f);
        colorOn = new Color(0.10f, 0.84f, 0.35f, 1f);
        
    }
    //if player bullet touches the trigger, switches the trigger to on
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "PlayerBullet")
        {
            Debug.Log("TriggerActivated");
            isTriggered = true;
            

        }
    }

    //light changes whether trigger is on or off
    void Update ()
    {
        if (isTriggered == true)
        {
            TriggerLight.color = colorOn;
        }
           
        if (isTriggered == false)
        {
            TriggerLight.color = colorOff;
        }
            
    }
}
