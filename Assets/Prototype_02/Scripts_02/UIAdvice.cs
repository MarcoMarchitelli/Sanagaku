using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIAdvice : MonoBehaviour {

    public TextMeshProUGUI adviceText;

    void Start()
    {
        adviceText.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            adviceText.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            adviceText.enabled = false;
        }
    }
}
