using UnityEngine;

public class TemporaryUIManager : MonoBehaviour
{
    public ManaUITest playerManaUI;
    public HealthUITest playerHealthUI;

    public void SetupPlayerHUD()
    {
        if (playerManaUI)
            playerManaUI.SetUp();
        if (playerHealthUI)
            playerHealthUI.SetUp();
    }
}
