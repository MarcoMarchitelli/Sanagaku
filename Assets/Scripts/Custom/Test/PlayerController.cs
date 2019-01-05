using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : BaseUnit, IShooter
{
    public enum DirectionType { Global, Camera };

    #region Public Vars

    [Header("References")]
    public ParticleSystem walkSmoke;

    [Header("Behaviours")]
    public DirectionType InputDirection;

    [Header("Parameters")]
    public Transform gunPoint;
    [SerializeField] BaseGun equippedGun;
    [SerializeField] KeyCode shootInput = KeyCode.Mouse0;
    public LayerMask MaskToIgnore;
    public LayerMask aimLayer;

    #endregion

    #region Private Vars

    Vector3 moveDirection;
    Vector3 cameraBasedDirection;
    Vector3 playerDirection;
    Camera cam;
    Rigidbody rb;
    bool isMoving = false;

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
        cam = Camera.main;
    }

    void Update()
    {
        #region Input
        //Move Input
        if (InputDirection == DirectionType.Global)
        {
            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        }
        else
        if (InputDirection == DirectionType.Camera)
        {
            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
            cameraBasedDirection = cam.transform.TransformDirection(moveDirection);
            moveDirection = new Vector3(cameraBasedDirection.x, moveDirection.y, cameraBasedDirection.z);
        }
        //Shoot Input
        if (Input.GetKeyDown(shootInput) && EquippedGun)
        {
            EquippedGun.Shoot();
        }
        #endregion

        #region Aim

        //Aim Input
        RaycastHit hitInfo;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo, int.MaxValue, aimLayer))
        {
            transform.LookAt(new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z));
        }

        #endregion

        if (moveDirection == Vector3.zero)
            IsMoving = false;
        else
            IsMoving = true;
    }

    private void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        TestEnemy t = collision.collider.GetComponent<TestEnemy>();
        if (t && t.dealsDamageOnContact)
            TakeDamage(t.damage);
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

    #endregion

    public override void TakeDamage(int amount)
    {
        Counters.instance.UpdateHits(amount);
    }

}
