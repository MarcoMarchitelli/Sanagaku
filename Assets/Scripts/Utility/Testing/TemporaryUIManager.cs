using UnityEngine;

public class TemporaryUIManager : MonoBehaviour
{
    public ManaUITest playerManaUI;
    public HealthUITest playerHealthUI;

    public void SetupPlayerHUD()
    {
        playerManaUI.SetUp();
        playerHealthUI.SetUp();
    }
}
