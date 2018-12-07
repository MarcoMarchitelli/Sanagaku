using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : BaseProjectile {

    public enum Type{ FirstPerson, TopDown};
    public Type ProjectileBehaviour;
    public LayerMask collisionMask;
    public bool DeathByTime = true;
    public bool DeathByBounces = false;
    float timer;
    int bounceCount;

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
                    bounceCount++;
                    if (bounceCount >= lifeInBounce)
                        Die();
                }
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(ProjectileBehaviour == Type.FirstPerson)
        {
            //if(collision.collider.gameObject.layer == collisionMask)
            //{
                Vector3 bounceDirection = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
                transform.forward = bounceDirection;
            //}
        }
    }
}