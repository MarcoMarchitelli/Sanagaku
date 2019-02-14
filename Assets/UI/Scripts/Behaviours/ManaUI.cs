using UnityEngine;
using UnityEngine.UI;
using Sangaku;
using System.Collections;

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

        StartCoroutine(LayoutSetup(layoutGroup));
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


    IEnumerator LayoutSetup(HorizontalLayoutGroup _l)
    {
        _l.childControlWidth = true;
        _l.childControlHeight = true;
        _l.childForceExpandWidth = true;
        _l.childForceExpandHeight = true;
        for (int i = 0; i < manaBehaviour.MaxMana; i++)
        {
            Instantiate(manaChunkPrefab, transform);
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