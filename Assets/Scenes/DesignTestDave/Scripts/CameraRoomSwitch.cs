using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoomSwitch : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;

    //If the player collides with the trigger on the door, check if cam1 is active.
    //if false, switch cam1 on and cam2 off
    //if true, switch cam1 off and cam2 on

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //if (cam1.activeSelf == false)
            {
                cam1.SetActive(false);
                cam2.SetActive(true);
                
            }

            //else

            //{
            //    Debug.Log("shift2");
            //    cam2.SetActive(true);
            //    cam1.SetActive(false);


            //}
        }
    }

}
