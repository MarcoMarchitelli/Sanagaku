using UnityEngine;
using UnityEngine.Events;

public class TestBullet : MonoBehaviour
{
    public enum DeathBehaviour { byTime, byBounces };

    #region Serialized Fields

    //references
    public GameObject HitSmoke;
    public TrailRenderer Trail;

    //behaviours
    public DeathBehaviour deathBehaviour = DeathBehaviour.byTime;
    public bool speedOverLifeTime = true;

    //parameters
    public LayerMask collisionLayer;
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

    int bounces;
    float timer;
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
            print(name + " SONO IN STATE " + value.ToString());
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

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
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

        #region Raycasting

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Time.deltaTime * moveSpeed + .2f, collisionLayer))
        {
            //check in case enemy hit
            TestEnemy enemyHit = hit.collider.GetComponent<TestEnemy>();
            if (enemyHit)
            {
                if (enemyHit.deathBehaviour == TestEnemy.DeathBeahviour.diesFromBounces && enemyHit.BouncesNeededToDie == Bounces)
                {
                    enemyHit.Die();
                }
                else
                if (enemyHit.deathBehaviour == TestEnemy.DeathBeahviour.diesFromDamage)
                {
                    enemyHit.TakeDamage(damage);
                }
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
                    case BounceBehaviour.Type.catchAndFire:
                        transform.forward = bounceBehaviour.transform.forward;
                        PlayerController p = bounceBehaviour.GetComponentInParent<PlayerController>();
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
                    default:
                        break;
                }
                return;
            }

            //if this doesn't find a bounce behaviour component
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