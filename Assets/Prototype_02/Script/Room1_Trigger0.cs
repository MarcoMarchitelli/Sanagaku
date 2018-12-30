using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1_Trigger0 : MonoBehaviour {

    Color colorOff;
    Color colorOn;
    public Light TriggerLight;
    public GameObject triggerManager;

    void Start ()
    {
        colorOff = new Color(0.84f, 0.16f, 0.10f, 1f);
        colorOn = new Color(0.10f, 0.84f, 0.35f, 1f);
        TriggerLight.color = colorOff;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet")
        {
            TriggerLight.color = colorOn;
            Debug.Log("OOONE");
            triggerManager.GetComponent<Room1_triggerManager>().trigger_b0 = true;
        }
        else { }
    }
}

