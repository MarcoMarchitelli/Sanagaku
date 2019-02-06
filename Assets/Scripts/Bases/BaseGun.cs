using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGun : MonoBehaviour {

    [SerializeField] protected float timeBetweenShots;
    [SerializeField] protected int magazineSize;
    [SerializeField] protected Transform projectileToShoot;
    [SerializeField] protected Transform projectileSpawnPoint;
    [SerializeField] protected ParticleSystem MuzzleFlash;

    protected float timer = 0;
    protected int bulletShotCount = 0;

    public float TimeBetweenShots
    {
        get
        {
            return timeBetweenShots;
        }

        set
        {
            timeBetweenShots = value;
        }
    }

    public int MagazineSize
    {
        get { return magazineSize; }
        set { magazineSize = value; }
    }

    public Transform ProjectileToShoot
    {
        get
        {
            return projectileToShoot;
        }

        set
        {
            projectileToShoot = value;
        }
    }

    public Transform ProjectileSpawnPoint
    {
        get
        {
            return projectileSpawnPoint;
        }

        set
        {
            projectileSpawnPoint = value;
        }
    }

    public virtual void Shoot(PlayerController _p){

        if(timer >= TimeBetweenShots && bulletShotCount < MagazineSize)
        {
            timer = 0f;
            TestBullet b = Instantiate(projectileToShoot, ProjectileSpawnPoint.position, ProjectileSpawnPoint.rotation).GetComponent<TestBullet>();
            b.Init(_p);
            //Destroy(bulletShot.gameObject, bulletShot.LifeTime);
            bulletShotCount++;
            if (MuzzleFlash)
                MuzzleFlash.Play();
        }
    }

    protected virtual void Update () {
        timer += Time.deltaTime;
	}
}