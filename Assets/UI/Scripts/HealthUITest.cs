using UnityEngine;
using UnityEngine.UI;
using Sangaku;
using System.Collections.Generic;

public class HealthUITest : MonoBehaviour
{
    [SerializeField] List<GameObject> healthChunks;
    [SerializeField] DamageReceiverBehaviour damageReceiver;

    public void SetUp(DamageReceiverBehaviour _drb = null)
    {
        if (!damageReceiver && _drb)
            damageReceiver = _drb;

        if (damageReceiver)
            damageReceiver.OnHealthChanged.AddListener(UpdateUI);
    }

    public void UpdateUI(int _newHealth)
    {
        for (int i = 0; i < healthChunks.Count; i++)
        {
            if (i >= _newHealth)
            {
                healthChunks[i].SetActive(false);
            }
            else
            {
                healthChunks[i].SetActive(true);
            }
        }
    }

    //TEMPORARY
    private void Start()
    {
        SetUp();
    }

}