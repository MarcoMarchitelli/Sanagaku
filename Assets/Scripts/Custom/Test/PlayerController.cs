using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : BaseUnit, IShooter
{
    public enum DirectionType { Global, Camera };

    #region Public Vars

    [Header("Player Behaviours")]
    public DirectionType InputDirection;

    [Header("Gameplay Parameters")]
    public Transform gunPoint;
    [SerializeField] BaseGun equippedGun;
    [SerializeField] KeyCode shootInput = KeyCode.Mouse0;

    [Header("VFX References")]
    public ParticleSystem walkSmoke;

    [Header("Other Stuff :)")]
    public LayerMask MaskToIgnore;

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
        float distance;
        Plane aimPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (aimPlane.Raycast(ray, out distance))
        {
            Vector3 hitPoint = ray.origin + ray.direction * distance;
            Debug.DrawLine(cam.transform.position, hitPoint);
            transform.LookAt(new Vector3(hitPoint.x, transform.position.y, hitPoint.z));
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

}
