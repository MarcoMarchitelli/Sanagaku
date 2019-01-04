using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1_Trigger3 : MonoBehaviour {

    //public bool trigger_b1 = false;
    Color colorOff;
    Color colorOn;
    public Light TriggerLight;
    public GameObject triggerManager;

    void Start ()
    {
        //trigger_b1 = false;
        colorOff = new Color(0.84f, 0.16f, 0.10f, 1f);
        colorOn = new Color(0.10f, 0.84f, 0.35f, 1f);
        TriggerLight.color = colorOff;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet" && triggerManager.GetComponent<Room1_triggerManager>().trigger_b0 == true && triggerManager.GetComponent<Room1_triggerManager>().trigger_b2 == true)
        {
            TriggerLight.color = colorOn;
            triggerManager.GetComponent<Room1_triggerManager>().trigger_b3 = true;
        }
        else { }
    }
}
