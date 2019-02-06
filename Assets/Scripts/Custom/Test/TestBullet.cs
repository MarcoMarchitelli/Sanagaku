using UnityEngine;
using Deirin.Utility;

public class TestBullet : MonoBehaviour
{
    public enum DeathBehaviour { byTime, byBounces };

    #region Serialized Fields

    //references
    public GameObject HitSmoke;
    public TrailRenderer Trail;
    public Timer DeathTimer;

    //behaviours
    public DeathBehaviour deathBehaviour = DeathBehaviour.byTime;
    public bool speedOverLifeTime = true;

    //parameters
    public string collisionLayer;
    public AnimationCurve speedOverLifeTimeCurve;
    public float deathTime;
    [Range(1, 3)]
    public int bouncesToDie;
    public float moveSpeed;
    public float lifeTime;
    public int damage;

    //events
    public FloatEvent OnLifeEnd;

    #endregion

    #region Other Vars

    [HideInInspector]
    public PlayerController player;
    int bounces;
    bool isDeparting = true;
    float timer;
    float distanceFromPlayer;
    float oldDistanceFromPlayer;
    Material material;
    // ---------------------------- NO
    public enum State { inMovement, inHands };
    protected State _currentState;
    public State CurrentState
    {
        set
        {
            ResetLifeTime();
            _currentState = value;
        }
        get
        {
            return _currentState;
        }
    }
    // ---------------------------- NO

    #endregion

    #region Properties

    public int Bounces
    {
        get
        {
            return bounces;
        }

        set
        {
            bounces = value;
            if (Bounces >= bouncesToDie && deathBehaviour == DeathBehaviour.byBounces)
                Die();
            //switch (value) -- //HACK: tolto perchè blocca il conteggio dei bounce quando il deathBehaviour è impostato su time.
            //{
            //    case 1:
            //        material.color = ColorContainer.Instance.Colors[value - 1];
            //        Trail.startColor = ColorContainer.Instance.Colors[value - 1];
            //        break;
            //    case 2:
            //        Trail.startColor = ColorContainer.Instance.Colors[value - 1];
            //        material.color = ColorContainer.Instance.Colors[value - 1];
            //        break;
            //    case 3:
            //        material.color = ColorContainer.Instance.Colors[value - 1];
            //        Trail.startColor = ColorContainer.Instance.Colors[value - 1];
            //        break;
            //    default:
            //        material.color = ColorContainer.Instance.Colors[ColorContainer.Instance.Colors.Length - 1];
            //        Trail.startColor = ColorContainer.Instance.Colors[ColorContainer.Instance.Colors.Length - 1];
            //        break;
            //}
        }
    }

    #endregion

    #region MonoBehaviour methods

    public void Init(PlayerController _p)
    {
        material = GetComponent<MeshRenderer>().material;
        player = _p;
        oldDistanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
    }

    private void Update()
    {
        #region Timer

        if (deathBehaviour == DeathBehaviour.byTime && CurrentState == State.inMovement)
        {
            timer += Time.deltaTime;
            if (timer >= lifeTime)
                OnLifeEnd.Invoke(deathTime);
        }

        #endregion

        #region Movement

        if (CurrentState == State.inMovement)
            Move();

        #endregion

        #region Player Distance

        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceFromPlayer > oldDistanceFromPlayer)
            isDeparting = true;
        else if(distanceFromPlayer < oldDistanceFromPlayer)
            isDeparting = false;
        oldDistanceFromPlayer = distanceFromPlayer;

        #endregion

        //#region Raycasting

        //Ray ray = new Ray(transform.position, transform.forward);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, Time.deltaTime * moveSpeed + .2f, collisionLayer))
        //{
        //    //check in case enemy hit
        //    TestEnemy enemyHit = hit.collider.GetComponent<TestEnemy>();
        //    if (enemyHit)
        //    {
        //        if (enemyHit.deathBehaviour == TestEnemy.DeathBeahviour.diesFromBounces && enemyHit.BouncesNeededToDie == Bounces)
        //        {
        //            enemyHit.Die();
        //        }
        //        else
        //        if (enemyHit.deathBehaviour == TestEnemy.DeathBeahviour.diesFromDamage)
        //        {
        //            enemyHit.TakeDamage(damage);

        //        }
        //    }

        //    //check for other obj hit behaviours
        //    BounceBehaviour bounceBehaviour = hit.collider.GetComponent<BounceBehaviour>();
        //    if (bounceBehaviour)
        //    {
        //        switch (bounceBehaviour.BehaviourType)
        //        {
        //            case BounceBehaviour.Type.realistic:
        //                Bounce(ray.direction, hit.normal);
        //                break;
        //            case BounceBehaviour.Type.catchAndFire:
        //                transform.forward = bounceBehaviour.transform.forward;
        //                PlayerController p = bounceBehaviour.GetComponentInParent<PlayerController>();
        //                if (p)
        //                {
        //                    p.CatchBullet(this);
        //                }
        //                if (deathBehaviour == DeathBehaviour.byBounces)
        //                {
        //                    Bounces++;
        //                    Instantiate(HitSmoke, transform.position + Vector3.forward * .5f, Random.rotation);
        //                }
        //                break;
        //            case BounceBehaviour.Type.goThrough:
        //                break;
        //            case BounceBehaviour.Type.destroy:
        //                Die();
        //                break;
        //            default:
        //                break;
        //        }
        //        return;
        //    }

        //    //if this doesn't find a bounce behaviour component
        //    Bounce(ray.direction, hit.normal);
        //}

        //#endregion

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject g = collision.collider.gameObject;

        if (g.layer == LayerMask.NameToLayer(collisionLayer))
        {
            CheckBounceBehaviour(transform.forward, collision.contacts[0].normal, false, g.GetComponent<BounceBehaviour>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject g = other.gameObject;

        if(g.layer == LayerMask.NameToLayer(collisionLayer))
        {
            CheckBounceBehaviour(transform.forward, Vector3.zero, true, g.GetComponent<BounceBehaviour>());
            CheckEnemyHit(g.GetComponent<TestEnemy>());
        }
    }

    #endregion

    #region Bullet methods

    void CheckBounceBehaviour(Vector3 _direction, Vector3 _normal, bool _isTrigger, BounceBehaviour _b = null)
    {
        if (_b)
        {
            if (_isTrigger)
            {
                switch (_b.BehaviourType)
                {
                    case BounceBehaviour.Type.catchAndFire:
                        if (isDeparting)
                            return;
                        transform.forward = _b.transform.forward;
                        PlayerController p = _b.GetComponentInParent<PlayerController>();
                        if (p)
                        {
                            p.CatchBullet(this);
                        }
                        if (deathBehaviour == DeathBehaviour.byBounces)
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
                }
            }
            else
            {
                if(_b.BehaviourType == BounceBehaviour.Type.realistic)
                    Bounce(_direction, _normal);
            }
        }
        else
        {
            Bounce(_direction, _normal);
        }
    }

    void CheckEnemyHit(TestEnemy _e = null)
    {
        if (_e)
        {
            if (_e.deathBehaviour == TestEnemy.DeathBeahviour.diesFromBounces && _e.BouncesNeededToDie == Bounces)
            {
                _e.Die();
            }
            else
            if (_e.deathBehaviour == TestEnemy.DeathBeahviour.diesFromDamage)
            {
                _e.TakeDamage(damage);
            }
        }
    }

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
        //if (deathBehaviour == DeathBehaviour.byBounces) -- //HACK: tolto perchè blocca il conteggio dei bounce quando il deathBehaviour è impostato su time 
        //{
        Bounces++;
        Instantiate(HitSmoke, transform.position + Vector3.forward * .5f, Random.rotation);
        //} 
    }

    private void Move()
    {
        if (deathBehaviour == DeathBehaviour.byTime)
            transform.Translate(Vector3.forward * speedOverLifeTimeCurve.Evaluate(timer / lifeTime) * moveSpeed * Time.deltaTime);
        else
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    public void Die()
    {
        Instantiate(HitSmoke, transform.position + Vector3.forward * .5f, Random.rotation);
        Destroy(gameObject);
    }

    protected void ResetLifeTime()
    {
        timer = 0f;
    }

    #endregion

}