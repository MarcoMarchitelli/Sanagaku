using UnityEngine;
using UnityEngine.UI;
using Sangaku;

[RequireComponent(typeof(HorizontalLayoutGroup))]
public class ManaUI : MonoBehaviour
{
    [SerializeField] GameObject manaChunkPrefab;
    [SerializeField] ManaBehaviour manaBehaviour;

    public void SetUp(ManaBehaviour _mb = null)
    {
        if (!manaBehaviour && _mb)
            manaBehaviour = _mb;

        manaBehaviour.OnManaChanged.AddListener(UpdateUI);

        HorizontalLayoutGroup layoutGroup = GetComponent<HorizontalLayoutGroup>();
        layoutGroup.childControlHeight = true;
        layoutGroup.childControlWidth = true;
        layoutGroup.childForceExpandHeight = true;
        layoutGroup.childForceExpandWidth = true;

        for (int i = 0; i < manaBehaviour.GetMana(); i++)
        {
            Instantiate(manaChunkPrefab, transform);
        }

        layoutGroup.childControlWidth = false;
        layoutGroup.childControlHeight = false;
        layoutGroup.childForceExpandWidth = false;
        layoutGroup.childForceExpandHeight = false;
    }

    public void UpdateUI(float _newMana)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Slider manaChunk = transform.GetChild(i).GetComponentInChildren<Slider>();
            if (i > (int)_newMana)
            {
                manaChunk.gameObject.SetActive(false);
            }
            else if (i == (int)_newMana)
            {
                manaChunk.gameObject.SetActive(true);
                manaChunk.value = _newMana - i;
            }
            else
            {
                manaChunk.gameObject.SetActive(true);
                manaChunk.value = 1;
            }
        }
    }

    //TEMPORARY
    private void Start()
    {
        SetUp();
    }

}