using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Camera m_Camera;

    private Vector3 m_MousePos;
    private WeaponController m_WeaponController;
    private int m_Health;

    //Getters
    public int GetHealth() { return m_Health; }

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        m_WeaponController = GetComponent<WeaponController>();
        m_Health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        AimAtMouse();
        CheckFireWeapon();
        CheckReload();
        CheckPauseGame();
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
            CheckGunToMouseDistance(point);
        }
    }

    private void CheckGunToMouseDistance(Vector3 point)
    {
        float StopLookingDistance = 4.0f;
        if (Vector2.Distance(new Vector2(point.x, point.z), 
                             new Vector2(transform.position.x, transform.position.z)) > StopLookingDistance)
            m_WeaponController.Aim(point);
    }

    private void CheckFireWeapon()
    {
        //If left mouse button was pressed
        if (Input.GetMouseButton(0))
            m_WeaponController.Shoot();
    }

    private void CheckReload()
    {
        //If 'R' was pressed
        if (Input.GetKeyDown(KeyCode.R))
            m_WeaponController.Reload();
    }

    private void CheckPauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            GameManager.Instance().PauseGame();
    }
}
