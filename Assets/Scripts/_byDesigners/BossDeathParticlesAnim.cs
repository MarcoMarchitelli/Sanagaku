using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathParticlesAnim : MonoBehaviour
{
    public Animator animator;    
          
    public void DeathStart()
    {
        animator.SetTrigger("BossDeath");
    }


}
