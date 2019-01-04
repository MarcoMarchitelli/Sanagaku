using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this class name is a joke.
//also, it's not as accurate as the "Door1Manager" script system, with arrays and stuff, but it's a prototype, not the definitive switch system.
#region quick instructions
//if we want more switches we must create a new object, create a new Room1_triggerN" script (copied from Room1_trigger0.cs), and modify a bit THIS script:
//#1 create the new gameObjet and attach to it the the NEW "Room1_triggerN" script; ALSO add a NEW gameobject variable in THIS script, and then drag the new gameObject to this script in inspector;
//#2 in THIS script we need to add a new bool ("trigger_bN") and a new nested if just BEFORE the current final one (with the OpenDoor() function), remeber to add this bool also in that final if condition;
//#3 in the new "Room1_triggerN.cs"(copied) remeber to modify class name, then REPLACE the reference of the (old copied) bool with a raferences of the new bool (that is on THIS script);

//REJOICE! Now everyone can use this twisted switch system! HALLELUJAH!
#endregion

public class DownwardsCorridor_triggerManager : MonoBehaviour
{
    public GameObject trigger_0;
    //public GameObject trigger_1;
    //public GameObject trigger_2;
    //public GameObject trigger_3;
    public bool trigger_b0 = false;
    //public bool trigger_b1 = false;
    //public bool trigger_b2 = false;
    //public bool trigger_b3 = false;
  
    //trigger that will define which object is which. EDIT: not anymore, but I keep them for posterity
    //public bool trigger_b0a;
    //public bool trigger_b1a;
    //public bool trigger_b2a;
    //public bool trigger_b3a;

    public GameObject Door;
    public bool DoorIsOpen = false;

    void Start()
    {
        //StartCoroutine(SwitchTriggers());
        StartCoroutine(OpenDoorCoroutine());
    }


    public void TriggerSwitches()
    {
        {
            if (trigger_b0 == true && DoorIsOpen == false)
            {
                
                OpenDoor();
                DoorIsOpen = true;
                
            }
        }
    }

    #region Update (ex matrioska if). Use this instead of coroutine if it gives problems.
    ////Matrioska if. Most of these bools are set in other scripts (room1_triggerN)
    //public void Update()
    //{
    //    if (trigger_b0 == true && DoorIsOpen == false)
    //    {
    //        if (trigger_b0 == true && trigger_b1 == true)
    //        {
    //            if (trigger_b0 == true && trigger_b1 == true && trigger_b2 == true)
    //            {
    //                if (trigger_b0 == true && trigger_b1 == true && trigger_b2 == true && trigger_b3 == true)
    //                {
    //                    if (trigger_b0 == true && trigger_b1 == true && trigger_b2 == true && trigger_b3 == true)
    //                    {
    //                        OpenDoor();
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
    #endregion
    
    //I just didn't want to put stuff in update, BUT if Unity explodes, comment this thing and use Update
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
        //it's a prototype that will be erased in 2 days, I', going to use this since is the fastest way. This comment has already taken me more time then this setActive stuff
        Door.SetActive(false);
        DoorIsOpen = true;
        //StopCoroutine(SwitchTriggers());
        Debug.Log("Open Door ー(￣～￣)ξ");
    }
}
