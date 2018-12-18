using UnityEngine;

public class TestBullet : BaseProjectile
{
    #region Variables

    public LayerMask collisionMask;
    public bool DeathByTime = true;
    public bool DeathByBounces = false;
    public GameObject HitSmoke;
    public TrailRenderer Trail;

    float timer;
    Material material;
    Camera cam;

    #endregion

    #region Properties
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

    #endregion

    #region MonoBehaviour methods

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        cam = Camera.main;
    }

    private void Update()
    {
        #region Timer
        if (DeathByTime)
        {
            timer += Time.deltaTime;
            if (timer >= LifeTime)
                Die();
        }
        #endregion

        #region Movement
        //movement
        transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
        if (cam.WorldToViewportPoint(new Vector3(transform.position.x - transform.localScale.x * .5f, transform.position.y, transform.position.z )).x <= 0)
        {
            Bounce(transform.forward, Vector3.right);
        }
        else if (cam.WorldToViewportPoint(new Vector3(transform.position.x + transform.localScale.x * .5f, transform.position.y, transform.position.z)).x >= 1)
        {
            Bounce(transform.forward, Vector3.left);
        }
        else if (cam.WorldToViewportPoint(new Vector3(transform.position.x, transform.position.y - transform.localScale.z * .5f, transform.position.z)).y <= 0)
        {
            Die();
        }
        else if (cam.WorldToViewportPoint(new Vector3(transform.position.x, transform.position.y + transform.localScale.z * .5f, transform.position.z)).y >= 1)
        {
            Bounce(transform.forward, Vector3.back);
        }
        #endregion

        #region Raycasting
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Time.deltaTime * MoveSpeed + .2f, collisionMask))
        {
            //check in case enemy hit
            TestEnemy enemyHit = hit.collider.GetComponent<TestEnemy>();
            if (enemyHit)
            {
                if (enemyHit.DiesFromBounces && enemyHit.BouncesNeededToDie == Bounces)
                {
                    enemyHit.Die();
                    Bounce(ray.direction, hit.normal);
                }
                else
                if (enemyHit.DiesFromBounces && enemyHit.BouncesNeededToDie > Bounces)
                {
                    Bounce(ray.direction, hit.normal);
                }
                else
                if (enemyHit.DiesFromDamage && enemyHit.Health == Damage)
                {
                    enemyHit.TakeDamage(Damage);
                    return;
                }
                return;
            }

            //check for other obj hit behaviours
            BounceBehaviour bounceBehaviour = hit.collider.GetComponent<BounceBehaviour>();
            if (bounceBehaviour)
            {
                switch (bounceBehaviour.BehaviourType)
                {
                    case BounceBehaviour.Type.realistic:
                        Bounce(ray.direction, hit.normal);
                        break;
                    case BounceBehaviour.Type.shield:
                        transform.forward = bounceBehaviour.transform.forward;
                        if (DeathByBounces)
                        {
                            Bounces++;
                            Instantiate(HitSmoke, transform.position + Vector3.forward * .5f, Random.rotation);
                        }
                        break;
                    case BounceBehaviour.Type.goThrough:
                        break;
                    case BounceBehaviour.Type.destroy:
                        Die();
                        break;
                    default:
                        break;
                }
                return;
            }

            //normal bounce behaviour
            Bounce(ray.direction, hit.normal);
        }
        #endregion
    }

    #endregion

    #region Bullet methods

    /// <summary>
    /// Sets the transform rotation to the new rotation given by the bounce.
    /// </summary>
    /// <param name="_direction">The direction of the body when bouncing.</param>
    /// <param name="_normal">The normal of the hit surface.</param>
    /// <returns></returns>
    void Bounce(Vector3 _direction, Vector3 _normal)
    {
        Vector3 bounceDirection = Vector3.Reflect(_direction, _normal);
        float rot = 90 - Mathf.Atan2(bounceDirection.z, bounceDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, rot, 0);
        if (DeathByBounces)
        {
            Bounces++;
            Instantiate(HitSmoke, transform.position + Vector3.forward * .5f, Random.rotation);
        }
    }

    public override void Die()
    {
        Instantiate(HitSmoke, transform.position + Vector3.forward * .5f, Random.rotation);
        base.Die();
    }

    #endregion

}