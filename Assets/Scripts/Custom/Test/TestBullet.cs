using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : BaseProjectile
{

    public enum Type { FirstPerson, TopDown };
    public Type ProjectileBehaviour;
    public LayerMask collisionMask;
    public bool DeathByTime = true;
    public bool DeathByBounces = false;
    public GameObject HitSmoke;
    public TrailRenderer Trail;

    float timer;
    Material material;
    //Material trailMaterial;

    public override int Bounces
    {
        get
        {
            return base.Bounces;
        }

        set
        {
            base.Bounces = value;
            if (Bounces >= lifeInBounce)
                Die();
            switch (value)
            {
                case 1:
                    material.color = ColorContainer.Instance.Colors[value - 1];
                    Trail.startColor = ColorContainer.Instance.Colors[value - 1];
                    break;
                case 2:
                    Trail.startColor = ColorContainer.Instance.Colors[value - 1];
                    material.color = ColorContainer.Instance.Colors[value - 1];
                    break;
                case 3:
                    material.color = ColorContainer.Instance.Colors[value - 1];
                    Trail.startColor = ColorContainer.Instance.Colors[value - 1];
                    break;
                default:
                    material.color = ColorContainer.Instance.Colors[ColorContainer.Instance.Colors.Length - 1];
                    Trail.startColor = ColorContainer.Instance.Colors[ColorContainer.Instance.Colors.Length - 1];
                    break;
            }
        }
    }

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        //if(Trail)
        //    trailMaterial = Trail.GetComponent<Material>();
    }

    private void Update()
    {
        //time check
        if (DeathByTime)
        {
            timer += Time.deltaTime;
            if (timer >= LifeTime)
                Die();
        }

        //movement
        transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);

        //raycast for topdown
        if (ProjectileBehaviour == Type.TopDown)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Time.deltaTime * MoveSpeed + .1f, collisionMask))
            {
                Vector3 bounceDirection = Vector3.Reflect(ray.direction, hit.normal);
                float rot = 90 - Mathf.Atan2(bounceDirection.z, bounceDirection.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, rot, 0);
                if (DeathByBounces)
                {
                    Bounces++;
                    Instantiate(HitSmoke, transform.position + Vector3.forward * .5f, Random.rotation);
                }
            }
        }
    }

    public override void Die()
    {
        Instantiate(HitSmoke, transform.position + Vector3.forward * .5f, Random.rotation);
        base.Die();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ProjectileBehaviour == Type.FirstPerson)
        {
            Vector3 bounceDirection = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
            Bounces++;
            Instantiate(HitSmoke, transform.position + Vector3.forward * .5f, Random.rotation);
            transform.forward = bounceDirection;
        }
    }
}