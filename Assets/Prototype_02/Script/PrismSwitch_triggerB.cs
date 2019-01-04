using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismSwitch_triggerB : MonoBehaviour {

    public Material startMat;
    public Material HitPrismColor;
    public GameObject WinSwitchManager;
    public GameObject Door_prismB;

    void Start ()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = startMat;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet")
        {
            this.gameObject.GetComponent<MeshRenderer>().material = HitPrismColor;
            WinSwitchManager.GetComponent<WinSwitchManager>().trigger_s1 = true;
            Door_prismB.SetActive(false);
        }
        else { }
    }
}

