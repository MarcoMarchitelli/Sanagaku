using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSFX : MonoBehaviour
{
    public AudioSource OpenDoor;
    public AudioSource CloseDoor;

    public void PlayOpen()
    {
        OpenDoor.Play();

    }

    public void PlayClose()
    {
        CloseDoor.Play();

    }

}
