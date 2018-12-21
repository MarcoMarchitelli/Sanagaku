using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyNew : BaseUnit
{
    [Range(0,3)]
    public int BouncesNeededToDie;
    public bool DiesFromBounces = false;
    public bool DiesFromDamage = false;
    public float waitTime;
    public Transform path;
    public GameObject deathParticles;
    public ParticleSystem walkSmoke;

    bool isMoving = false;
    Material material;

    public bool IsMoving
    {
        get { return isMoving; }
        private set
        {
            isMoving = value;
            if (!walkSmoke)
                return;
            if (isMoving)
                walkSmoke.Play();
            else
                walkSmoke.Stop();
        }
    }

    private void OnDrawGizmos()
    {
        if (path)
        {
            Gizmos.color = Color.cyan;
            Vector3 startPos = path.GetChild(0).position;
            Vector3 lastPos = startPos;

            foreach (Transform point in path)
            {
                Gizmos.DrawSphere(point.position, .5f);
                Gizmos.DrawLine(lastPos, point.position);
                lastPos = point.position;
            }
            Gizmos.DrawLine(lastPos, startPos);
        }
    }

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    private void Start()
    {
        if (path)
            StartCoroutine(FollowPathAnim(MoveSpeed, waitTime));
        if(DiesFromBounces)
            material.color = ColorContainer.Instance.Colors[BouncesNeededToDie - 1];
    }

    IEnumerator FollowPathAnim(float speed, float waitTime)
    {
        Transform[] wayPoints = new Transform[path.childCount];

        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i] = path.GetChild(i);
        }

        Vector3 nextPoint = wayPoints[0].position;
        int nextPointIndex = 1;

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPoint, speed * Time.deltaTime);
            IsMoving = true;
            if (transform.position == nextPoint)
            {
                nextPointIndex = (nextPointIndex + 1) % wayPoints.Length;
                nextPoint = wayPoints[nextPointIndex].position;
                IsMoving = false;
                yield return new WaitForSeconds(waitTime);
            }
            yield return null;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    IProjectile bullet = other.GetComponent<IProjectile>();

    //    if (bullet != null)
    //    {
    //        print("hit by:" + bullet.Bounces);
    //        if (DiesFromDamage)
    //        {
    //            TakeDamage(bullet.Damage);
    //        }
    //        else if (DiesFromBounces && bullet.Bounces == BouncesNeededToDie)
    //        {
    //            print("level: " + BouncesNeededToDie + "/ hit by: " + bullet.Bounces);
    //            Die();
    //        }
    //        else if (DiesFromBounces && bullet.Bounces < BouncesNeededToDie)
    //        {
    //            bullet.Die();
    //        }

    //    }
    //}

    public override void Die()
    {
        if (deathParticles)
        {
            GameObject instantiatedParticles = Instantiate(deathParticles, transform.position, Random.rotation);
            Destroy(instantiatedParticles, 2.5f);
        }
        base.Die();
    }

}
