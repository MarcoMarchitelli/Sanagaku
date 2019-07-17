using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    public Animator animator;

    
    public void DoorOpen()
    {
        animator.SetTrigger("DoorOpen");
    }

    public void DoorClose()
    {
        animator.SetTrigger("DoorClose");
    }
}
