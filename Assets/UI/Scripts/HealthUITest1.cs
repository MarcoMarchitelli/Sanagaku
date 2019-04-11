using UnityEngine;
using UnityEngine.UI;
using Sangaku;
using System.Collections.Generic;

public class HealthUITest1 : MonoBehaviour
{
    [SerializeField] List<GameObject> healthbar;
    [SerializeField] DamageReceiverBehaviour damageReceiver;

    public void SetUp(DamageReceiverBehaviour _drb = null)
    {
        if (!damageReceiver && _drb)
            damageReceiver = _drb;

        damageReceiver.OnHealthChanged.AddListener(UpdateUI);
    }

    public void UpdateUI(int _newHealth)
    {
        for (int i = 0; i < healthbar.Count; i++)
        {
            if (i >= _newHealth)
            {
                healthbar[i].SetActive(false);
            }
            else
            {
                healthbar[i].SetActive(true);
            }
        }
    }

    //TEMPORARY
    private void Start()
    {
        SetUp();
    }

}