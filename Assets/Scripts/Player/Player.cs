using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] Camera cam;
    [SerializeField] Transform hand;

    private Vector3 vectorInput;
    private Vector3 mousePos;
    private Rigidbody playerRB;
    private WeaponController weaponController;

    // Start is called before the first frame update
    void Start()
    {
        //Acquire the player game object's Rigidbody component and save it.
        playerRB = GetComponent<Rigidbody>();
        weaponController = GetComponent<WeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        AimAtMouse();
        FireWeapon();
        Reload();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        playerRB.AddForce(vectorInput * speed * Time.fixedDeltaTime);
    }

    private void GetInput()
    {
        vectorInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0,
                                            Input.GetAxisRaw("Vertical"));
    }

    private void AimAtMouse()
    {
        float rayDistance;

        //Create a ray which starts at the camera and goes through the mouses position
        Ray camToMouseRay = cam.ScreenPointToRay(Input.mousePosition);

        //Create a plane so that the ray can be intersected. 
        Plane plane = new Plane(Vector3.up, transform.position);

        //If the ray interscets with the plane then return true and determine the distance between the intersection position and the origin
        if (plane.Raycast(camToMouseRay, out rayDistance))
        {
            //Create a point where the ray intersects
            Vector3 point = camToMouseRay.GetPoint(rayDistance);

            //Have the player look at the point on the x and z axis.
            transform.LookAt(new Vector3(point.x, transform.position.y, point.z));

            Debug.DrawRay(camToMouseRay.origin, point, Color.red);
        }
    }

    private void FireWeapon()
    {
        //If left mouse button was pressed
        if (Input.GetMouseButtonDown(0))
        {
            weaponController.Shoot();
        }
    }

    private void Reload()
    {
        //If 'R' was pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            weaponController.Reload();
        }
    }
}
