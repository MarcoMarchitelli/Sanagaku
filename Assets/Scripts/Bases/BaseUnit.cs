using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour, IMover, IDamageable {

    [SerializeField] protected float moveSpeed;
    [SerializeField] protected int health;

    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }

        set
        {
            moveSpeed = value;
        }
    }

    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
            Die();
    }

}