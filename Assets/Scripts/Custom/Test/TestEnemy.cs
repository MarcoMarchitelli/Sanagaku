using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : BaseUnit {

    public float waitTime;
    public Transform path;
    public GameObject deathParticles;

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

    private void Start()
    {
        if(path)
            StartCoroutine(FollowPathAnim(MoveSpeed, waitTime));
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
            if(transform.position == nextPoint)
            {
                nextPointIndex = (nextPointIndex + 1) % wayPoints.Length;
                nextPoint = wayPoints[nextPointIndex].position;
                yield return new WaitForSeconds(waitTime);
            }
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        IProjectile bullet = collision.collider.GetComponent<IProjectile>();

        if (bullet != null)
        {
            TakeDamage(bullet.Damage);
        }
    }

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
