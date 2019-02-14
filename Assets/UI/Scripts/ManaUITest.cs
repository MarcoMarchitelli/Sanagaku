using UnityEngine;
using UnityEngine.UI;
using Sangaku;

public class ManaUITest : MonoBehaviour
{
    [SerializeField] Slider manaSlider;
    [SerializeField] PlayerManaBehaviour playerManaBehaviour;

    public void SetUp(PlayerManaBehaviour _pmb = null)
    {
        if (!playerManaBehaviour && _pmb)
            playerManaBehaviour = _pmb;

        playerManaBehaviour.OnManaChanged.AddListener(UpdateUI);

        manaSlider.maxValue = playerManaBehaviour.MaxMana;
        UpdateUI(playerManaBehaviour.GetMana());
    }

    public void UpdateUI(float _newMana)
    {
        manaSlider.value = _newMana;
    }
}