using UnityEngine;
using UnityEngine.UI;
using Sangaku;
using System.Collections;

[RequireComponent(typeof(HorizontalLayoutGroup))]
public class DamageReceiverUI : MonoBehaviour
{
    [SerializeField] GameObject healthChunkPrefab;
    [SerializeField] DamageReceiverBehaviour damageReceiver;

    public void SetUp(DamageReceiverBehaviour _drb = null)
    {
        if (!damageReceiver && _drb)
            damageReceiver = _drb;

        damageReceiver.OnHealthChanged.AddListener(UpdateUI);

        HorizontalLayoutGroup layoutGroup = GetComponent<HorizontalLayoutGroup>();

        StartCoroutine(LayoutSetup(layoutGroup));

        //layoutGroup.childControlWidth = true;
        //layoutGroup.childControlHeight = true;
        //layoutGroup.childForceExpandWidth = true;
        //layoutGroup.childForceExpandHeight = true;

        //for (int i = 0; i < damageReceiver.GetHealth(); i++)
        //{
        //    Instantiate(healthChunkPrefab, transform);
        //}

        //layoutGroup.childControlWidth = false;
        //layoutGroup.childControlHeight = false;
        //layoutGroup.childForceExpandWidth = false;
        //layoutGroup.childForceExpandHeight = false;
    }

    public void UpdateUI(int _newHealth)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject healthChunk = transform.GetChild(i).gameObject;
            if (i >= _newHealth)
            {
                healthChunk.SetActive(false);
            }
            else
            {
                healthChunk.SetActive(true);
            }
        }
    }

    IEnumerator LayoutSetup(HorizontalLayoutGroup _l)
    {
        _l.childControlWidth = true;
        _l.childControlHeight = true;
        _l.childForceExpandWidth = true;
        _l.childForceExpandHeight = true;
        for (int i = 0; i < damageReceiver.GetHealth(); i++)
        {
            Instantiate(healthChunkPrefab, transform);
        }
        yield return null;
        _l.childControlWidth = false;
        _l.childControlHeight = false;
        _l.childForceExpandWidth = false;
        _l.childForceExpandHeight = false;
    }

    //TEMPORARY
    private void Start()
    {
        SetUp();
    }

}