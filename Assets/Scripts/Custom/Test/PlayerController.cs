using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : BaseUnit, IShooter {

    public enum Type { FirstPerson, TopDown };

    #region Public Vars

    [Header("Gameplay Parameters")]
    public Type PlayerBehaviour;
    public Transform gunPoint;
    [SerializeField] BaseGun equippedGun;
    [SerializeField] KeyCode shootInput = KeyCode.Mouse0;

    [Header("VFX References")]
    public ParticleSystem walkSmoke;

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

    void Awake () {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
	}
	
	void Update () {

        //topdown behaviour
        if (PlayerBehaviour == Type.TopDown)
        {
            //Move Input
            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
            //cameraBasedDirection = cam.transform.TransformDirection(globalDirection);
            //playerDirection = new Vector3(cameraBasedDirection.x, globalDirection.y, cameraBasedDirection.z);

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
        }

        //Shoot Input
        if (Input.GetKeyDown(shootInput) && EquippedGun)
        {
            print("Shoot Input");
            EquippedGun.Shoot();
        }

        if (moveDirection == Vector3.zero)
            IsMoving = false;
        else
            IsMoving = true;

    }

    private void FixedUpdate()
    {
        if(PlayerBehaviour == Type.TopDown)
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    public void EquipGun(BaseGun gunToEquip)
    {
        if (EquippedGun)
            Destroy(EquippedGun.gameObject);
        EquippedGun = gunToEquip;
        EquippedGun.transform.parent = gunPoint;
        EquippedGun.transform.position = gunPoint.position;
        EquippedGun.transform.rotation = gunPoint.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        BaseGun gun = collision.collider.GetComponent<BaseGun>();
        Floater f = collision.collider.GetComponent<Floater>();
        BoxCollider b = collision.collider.GetComponent<BoxCollider>();
        if (gun != null)
        {
            if (f)
                f.enabled = false;
            if (b)
                b.enabled = false;
            EquipGun(gun);
        }
    }

}
