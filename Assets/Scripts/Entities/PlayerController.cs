using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace  Sangaku
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : BaseUnit, IShooter
    {
        #region Serialized Fields

        //References
        public ParticleSystem walkSmoke;
        [SerializeField] BaseGun equippedGun;
        public Transform gunPoint;
        public Transform projectileSpawnPoint;

        //Behaviours

        public bool parry = true;
        public bool automaticParry = false;
        public bool dash = true;

        //Parameters
        // --BaseUnit.Health
        // --BaseUnit.MoveSpeed

        public LayerMask MaskToIgnore;
        
        [Tooltip("Changes apply at game start")] public float parryRadius = 2f;
        public float parryTime = .5f;
        public float catchHoldTime = 2f;
        public float parryCooldown = 2f;


        //events
        public UnityEvent OnShoot;
        public FloatEvent OnParryStart, OnParryEnd, OnDashStart, OnDashEnd, OnBulletCatch, OnBulletParry;

        #endregion

        #region Other Vars



        Vector3 playerDirection;

        SphereCollider catchNFireArea;

        Rigidbody rb;

        bool isMoving = false;
        bool canParry = true;
        bool isParrying = false;
        bool canDash = true;


        TestBullet bulletInHands;

        #endregion

        #region Properties

        public BaseGun EquippedGun
        {
            get
            {
                return equippedGun;
            }

            set
            {
                equippedGun = value;
            }
        }

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

        #endregion

        #region MonoBehaviour methods

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            catchNFireArea = GetComponentInChildren<SphereCollider>();
            if (!catchNFireArea)
            {
                Debug.LogWarning(name + " did not find a catchAndFire collider!");
            }
            else
            {
                catchNFireArea.radius = parryRadius;
            }
            if (parry && automaticParry)
            {
                isParrying = true;
                BounceBehaviour bb = catchNFireArea.GetComponent<BounceBehaviour>();
                if (bb)
                {
                    bb.BehaviourType = BounceBehaviour.Type.catchAndFire;
                }
            }
            cam = Camera.main;
        }

        void Update()
        {
            if (bulletInHands)
            {
                bulletInHands.transform.position = projectileSpawnPoint.transform.position;
                bulletInHands.transform.rotation = projectileSpawnPoint.rotation;
            }

            if (moveDirection == Vector3.zero)
                IsMoving = false;
            else
                IsMoving = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            //check for enemy contact damage
            TestEnemy t = collision.collider.GetComponent<TestEnemy>();
            if (t && t.dealsDamageOnContact)
                TakeDamage(t.damage);
        }

        private void OnDrawGizmos()
        {
            if (parry)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, parryRadius);
            }
        }

        #endregion

        #region PlayeController methods

        public void EquipGun(BaseGun gunToEquip)
        {
            if (EquippedGun)
                Destroy(EquippedGun.gameObject);
            EquippedGun = gunToEquip;
            EquippedGun.transform.parent = gunPoint;
            EquippedGun.transform.position = gunPoint.position;
            EquippedGun.transform.rotation = gunPoint.rotation;
        }

        public override void TakeDamage(int amount)
        {
            if (Counters.instance)
                Counters.instance.UpdateHits(amount);
            base.TakeDamage(amount);
        }

        public void SetParryDoability(bool f)
        {
            canParry = f;
        }

        public void StartParry()
        {
            isParrying = true;
            canParry = false;
            OnParryStart.Invoke(parryTime);
        }

        public void StopParry()
        {
            isParrying = false;
            OnParryEnd.Invoke(parryCooldown);
        }

        public void StartDash()
        {
            canDash = false;
            canMove = false;
            StartCoroutine(DashRoutine());
        }

        public void EndDash()
        {
            canMove = true;
            OnDashEnd.Invoke(dashCooldown);
        }

        public void SetDashDoability(bool f)
        {
            canDash = f;
        }



        //------------------- ITS SHIT
        public void CatchBullet(TestBullet _b)
        {
            if (!bulletInHands)
            {
                bulletInHands = _b;
                bulletInHands.CurrentState = TestBullet.State.inHands;
                bulletInHands.transform.position = projectileSpawnPoint.transform.position;
                bulletInHands.transform.rotation = projectileSpawnPoint.rotation;
                print("HO PRESO UN BULLET");
                OnBulletCatch.Invoke(catchHoldTime);
            }
        }

        public void ParryBullet()
        {
            if (!bulletInHands)
            {
                Debug.LogWarning(name + " has no bullet to parry!");
                return;
            }

            bulletInHands.CurrentState = TestBullet.State.inMovement;
            bulletInHands = null;
            print("ORA QUEL BULLET LO PARRYO");
        }
        //----------------------ENT

        //BaseUnit.Die();

        #endregion
    } 
}


