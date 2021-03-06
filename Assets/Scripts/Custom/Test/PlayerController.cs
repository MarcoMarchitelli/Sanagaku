﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : BaseUnit, IShooter {

    public enum Type { FirstPerson, TopDown };
    public enum DirectionType { Global, Camera };

    #region Public Vars

    [Header("Player Behaviours")]
    public Type PlayerBehaviour;
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

    void Awake () {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        if(PlayerBehaviour == Type.FirstPerson)
            MaskToIgnore = ~MaskToIgnore;
    }
	
	void Update () {

        //topdown behaviour
        if (PlayerBehaviour == Type.TopDown)
        {
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

        //firstperson behaviour
        if(PlayerBehaviour == Type.FirstPerson)
        {
            EquippedGun.transform.forward = Camera.main.transform.forward;
            //EquippedGun.transform.LookAt(Camera.main.transform.forward * 1000f);
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;
            //if (Physics.Raycast(ray, out hit, Mathf.Infinity, MaskToIgnore))
            //{
            //    print(":)");
            //    Debug.DrawLine(ray.origin, hit.point);
            //    EquippedGun.transform.LookAt(hit.point);
            //}
        }

        //Shoot Input
        if (Input.GetKeyDown(shootInput) && EquippedGun)
        {
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
