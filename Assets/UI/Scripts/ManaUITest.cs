using UnityEngine;
using UnityEngine.UI;
using Sangaku;

public class ManaUITest : MonoBehaviour
{
    [SerializeField] Slider manaSlider;
    [SerializeField] ManaBehaviour manaBehaviour;

    public void SetUp(ManaBehaviour _mb = null)
    {
        if (!manaBehaviour && _mb)
            manaBehaviour = _mb;

        manaBehaviour.OnManaChanged.AddListener(UpdateUI);

        manaSlider.maxValue = manaBehaviour.MaxMana;
    }

    public void UpdateUI(float _newMana)
    {
        manaSlider.value = _newMana;
    }

    //TEMPORARY
    private void Start()
    {
        SetUp();
    }

}