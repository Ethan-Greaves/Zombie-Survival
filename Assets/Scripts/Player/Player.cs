using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Camera m_Camera;

    private int m_Health;
    private Vector3 m_MousePos;
    private Vector3 m_VectorInput;
    private PlayerInput m_PlayerInput;
    private PlayerMovement m_PlayerMovement;
    private PlayerAnimation m_PlayerAnimation;
    private WeaponController m_WeaponController;

    //Getters
    public int GetHealth() { return m_Health; }

    private void Awake()
    {
        m_Health = 100;
        m_PlayerInput = GetComponent<PlayerInput>();
        m_PlayerMovement = GetComponent<PlayerMovement>();
        m_PlayerAnimation = GetComponent<PlayerAnimation>();
        m_WeaponController = GetComponent<WeaponController>();
    }

    // Start is called before the first frame update
    void Start(){
       

    }

    // Update is called once per frame
    void Update()
    {
        AimAtMouse();
        m_PlayerInput.AllInputs(m_WeaponController, ref m_VectorInput);
        m_PlayerAnimation.AddAnimation(ref m_VectorInput);
    }

    void FixedUpdate()
    {
        m_PlayerMovement.Move(ref m_VectorInput);
    }

    private void AimAtMouse()
    {
        float rayDistance;

        //Create a ray which starts at the camera and goes through the mouses position
        Ray camToMouseRay = m_Camera.ScreenPointToRay(Input.mousePosition);

        //Create a plane so that the ray can be intersected. 
        Plane plane = new Plane(Vector3.up, transform.position);

        //If the ray interscets with the plane then return true and determine the distance between the intersection position and the origin
        if (plane.Raycast(camToMouseRay, out rayDistance))
        {
            //Create a point where the ray intersects
            Vector3 point = camToMouseRay.GetPoint(rayDistance);

            //Have the player look at the point on the x and z axis.
            transform.LookAt(new Vector3(point.x, transform.position.y, point.z));

            // Check when the mouse is near the player and as such make the gun stop looking to avoid weird rotation
            //CheckGunToMouseDistance(point);

            m_WeaponController.Aim(point);
        }
    }

    private void CheckGunToMouseDistance(Vector3 point)
    {
        float StopLookingDistance = 2.5f;
        if (Vector2.Distance(new Vector2(point.x, point.z), 
                             new Vector2(transform.position.x, transform.position.z)) > StopLookingDistance)
            m_WeaponController.Aim(point);
    }
}
