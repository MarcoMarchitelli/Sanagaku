using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counters : MonoBehaviour {

    public static Counters instance;

    public TextMeshProUGUI killsCounterText;
    public TextMeshProUGUI hitCounterText;

    int kills, hits;

    private void Awake()
    {
        if (!instance)
            instance = this;
        if (!killsCounterText || !hitCounterText)
        {
            Debug.LogError(name + " has missing reference!");
            return;
        }
        killsCounterText.text = kills.ToString();
        hitCounterText.text = hits.ToString();
    }

    public void UpdateKills(int amount)
    {
        kills+= amount;
        killsCounterText.text = kills.ToString();
    }

    public void UpdateHits(int amount)
    {
        hits+= amount;
        hitCounterText.text = hits.ToString();
    }

}
