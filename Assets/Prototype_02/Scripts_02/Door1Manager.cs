using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Door1Manager : MonoBehaviour
{

    [SerializeField]
    GameObject[] Room1Switches;

    [SerializeField]
    GameObject Door1;

    int SwitchCount = 0;
    int FirstTrue;
    int SecondTrue;
    int ThirdTrue;
    int FourthTrue;
    static bool[] TriggerCheckStatus;


    void Start()
    {
        GetSwitchCount();        
    }

    //Getting total number of switches for the room
    public int GetSwitchCount()
    {
        int x = 0;

        for (int i = 0; i < Room1Switches.Length; i++)
        {
            if (Room1Switches[i].GetComponent<Room1Trigger>().isTriggered == false)
                x++;

            else if (Room1Switches[i].GetComponent<Room1Trigger>().isTriggered == true)
                SwitchCount--;
        }

        SwitchCount = x;
        return SwitchCount;

    }

    public int GetFirstTrue()
    {
        for (int i = 0; i < TriggerCheckStatus.Length; i++)
        {
            if (TriggerCheckStatus[i] == true)

            {
                FirstTrue = System.Array.IndexOf(TriggerCheckStatus, true);     
            }
            else
            {
                FirstTrue = -1;
            }

        }
        return FirstTrue;
    }

    public int GetSecondTrue()
    {
        for (int i = 1; i < TriggerCheckStatus.Length; i++)
        {
            if (TriggerCheckStatus[i] == true)

            {
                SecondTrue = System.Array.IndexOf(TriggerCheckStatus, true, 1);
            }
            else
            {
                SecondTrue = -1;
            }

        }
        return SecondTrue;
    }

    public int GetThirdTrue()
    {
        for (int i = 1; i < TriggerCheckStatus.Length; i++)
        {
            if (TriggerCheckStatus[i] == true)

            {
                ThirdTrue = System.Array.IndexOf(TriggerCheckStatus, true, 2);
            }
            else
            {
                ThirdTrue = -1;
            }

        }
        return ThirdTrue;
    }

    public int GetFourthTrue()
    {
        for (int i = 1; i < TriggerCheckStatus.Length; i++)
        {
            if (TriggerCheckStatus[i] == true)

            {
                FourthTrue = System.Array.IndexOf(TriggerCheckStatus, true, 3);
            }
            else
            {
                FourthTrue = -1;
            }

        }
        return FourthTrue;
    }

    void Update()
    {
        //creating boolean array for the four switches
        TriggerCheckStatus = new bool[Room1Switches.Length];

        for (int i = 0; i < Room1Switches.Length; i++)
        {
            TriggerCheckStatus[i] = Room1Switches[i].GetComponent<Room1Trigger>().isTriggered;
            
        }
        
        //debug control
        if (Input.GetKeyDown("space"))
        {
            Debug.Log(TriggerCheckStatus[0].ToString() + TriggerCheckStatus[1].ToString() + TriggerCheckStatus[2].ToString() + TriggerCheckStatus[3].ToString() + FirstTrue.ToString());
        }


        //switches to be triggered in the right order.
        //Part one: Checking if there is any active triggers.
        GetFirstTrue();

        //if there is any, check if it's #0 or else
        if (FirstTrue != -1)
        {
            CheckOrder1();
            GetSecondTrue();
        }

        
         
    }

    // function to check active triggers. 
    // if FirstTrue is trigger #0 nothing happens.
    // if FirstTrue is different from #0, it calls the Reset function.
    void CheckOrder1()
    {
        if (FirstTrue != 0)
        {
            ResetTriggers();
        }
    }

    void ResetTriggers()
    {
        for (int i = 0; i<Room1Switches.Length; i++)
        {
            Room1Switches[i].GetComponent<Room1Trigger>().isTriggered = false;
            
        }
    }


}






















