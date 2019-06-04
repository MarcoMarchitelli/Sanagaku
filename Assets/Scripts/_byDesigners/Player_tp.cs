using UnityEngine;

//per teletrasportare il player, e tutto a costo 0! :D
public class Player_tp : MonoBehaviour
{
    public GameObject player;
    public Transform destinationPosition;
    public GameObject debugCam;
    public GameObject portal;
    private bool portalActive = false;

    //attivo\disattivo il portale
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            if(portalActive == false)
            {
                portal.SetActive(true);
                portalActive = true;
            }
            else if (portalActive)
            {
                portal.SetActive(false);
                portalActive = false;
            }
        }
    }

    //cambio la position del player con quella del trasform scelto
    //public void Teleport()
    //{
    //    player.GetComponent<Transform>().position = destinationPosition.position;
    //    debugCam.SetActive(true);
    //}
}
