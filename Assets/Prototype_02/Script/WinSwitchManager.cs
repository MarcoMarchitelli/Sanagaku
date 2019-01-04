using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this class name is a joke, too;
#region quick instructions for switches
//remeber to attach a rigidbody to the object you want to trigger (Prisms for today)
//attach this script to the empty object WinSwitchManager
//then attach prismSwitch A and B scripts to each Prism (your trigger) and drag stuff in inspector
//yeah super clear instructions that nobody will never read! 
#endregion

public class WinSwitchManager : MonoBehaviour
{
    public GameObject trigger_0;
    public GameObject trigger_1;

    public bool trigger_s0 = false;
    public bool trigger_s1 = false;

    public GameObject FinalDoor;
    public bool DoorIsOpen = false;

    void Start()
    {
        StartCoroutine(OpenDoorCoroutine());
    }


    public void TriggerSwitches()
    {
        if (trigger_s0 == true && trigger_s1 == true && DoorIsOpen == false)
        {
            OpenDoor();
            DoorIsOpen = true;                        
        }
  
    }

    IEnumerator OpenDoorCoroutine()
    {
        while (DoorIsOpen == false)
        {
            TriggerSwitches();
            yield return null;
        }
        yield break;
    }

    public void OpenDoor()
    {
        FinalDoor.SetActive(false);
        DoorIsOpen = true;
        Debug.Log("Open Final Door ー(￣～￣)ξ");
    }
}
