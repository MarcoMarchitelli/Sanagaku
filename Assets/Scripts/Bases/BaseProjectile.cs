using UnityEngine;

public class BaseProjectile : MonoBehaviour, IProjectile, IMover {

    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float lifeTime;
    [SerializeField] protected int lifeInBounce;
    [SerializeField] protected int damage;
    protected int bounces;

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

    public float LifeTime
    {
        get
        {
            return lifeTime;
        }

        set
        {
            lifeTime = value;
        }
    }

    public int Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage = value;
        }
    }

    public virtual int Bounces
    {
        get
        {
            return bounces;
        }

        set
        {
            bounces = value;
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

}
