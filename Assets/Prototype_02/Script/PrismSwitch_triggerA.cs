using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismSwitch_triggerA : MonoBehaviour {

    public Material startMat;
    public Material HitPrismColor;
    public GameObject WinSwitchManager;
    public GameObject Door_prismA;
    //public bool DoorAopen = false;
    //public bool DoorBopen = false;


void Start()
{
        this.gameObject.GetComponent<MeshRenderer>().material = startMat;
    }

public void OnTriggerEnter(Collider other)
{
        if (other.tag == "PlayerBullet")
        {
            this.gameObject.GetComponent<MeshRenderer>().material = HitPrismColor;
            WinSwitchManager.GetComponent<WinSwitchManager>().trigger_s0 = true;
            Door_prismA.SetActive(false);
        }

        //if (other.tag == "PlayerBullet")
        //{
        //    HitPrismColor.color = colorOn;
        //    if (WinSwitchManager.GetComponent<WinSwitchManager>().trigger_s0 == false && WinSwitchManager.GetComponent<WinSwitchManager>().trigger_s1 == false)
        //    {
        //        WinSwitchManager.GetComponent<WinSwitchManager>().trigger_s0 = true;
        //        Door_prismA.SetActive(false);
        //    }
        //    else if(WinSwitchManager.GetComponent<WinSwitchManager>().trigger_s0 == true && WinSwitchManager.GetComponent<WinSwitchManager>().trigger_s0 == false)
        //    {
        //        WinSwitchManager.GetComponent<WinSwitchManager>().trigger_s0 = true;
        //        Door_prismA.SetActive(false);
        //    }
        //    else if(WinSwitchManager.GetComponent<WinSwitchManager>().trigger_s0 == false && WinSwitchManager.GetComponent<WinSwitchManager>().trigger_s0 == true)
        //    {
        //        WinSwitchManager.GetComponent<WinSwitchManager>().trigger_s1 = true;
        //        Door_prismA.SetActive(false);
        //    }
        //}
        //else { }
    }
}

