using UnityEngine;
using UnityEngine.UI;
using Sangaku;

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
        layoutGroup.childForceExpandHeight = true;
        layoutGroup.childForceExpandWidth = true;
        layoutGroup.childControlHeight = true;
        layoutGroup.childControlWidth = true;

        for (int i = 0; i < damageReceiver.GetHealth(); i++)
        {
            Instantiate(healthChunkPrefab, transform);
        }

        layoutGroup.childControlHeight = false;
        layoutGroup.childControlWidth = false;
        layoutGroup.childForceExpandHeight = false;
        layoutGroup.childForceExpandWidth = false;
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

    //TEMPORARY
    private void Start()
    {
        SetUp();
    }

}