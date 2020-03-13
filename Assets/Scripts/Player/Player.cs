using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(PlayerMovement), typeof(PlayerAnimation))]
[RequireComponent(typeof(WeaponController), typeof(PlayerSFX))]
public class Player : MonoBehaviour
{
    [SerializeField] Camera m_Camera;

    private int m_Health;
    private bool m_IsDead;
    private Vector3 m_MousePos;
    private Vector3 m_VectorInput;
    private PlayerSFX m_PlayerSFX;
    private PlayerInput m_PlayerInput;
    private PlayerMovement m_PlayerMovement;
    private PlayerAnimation m_PlayerAnimation;
    private WeaponController m_WeaponController;

    #region GETTERS
    //Getters
    public int GetHealth() { return m_Health; }
    public bool GetIsPlayerDead() { return m_IsDead; }

    #endregion

    #region INITILISATION
    private void Awake()
    {
        m_Health = 100;
        m_IsDead = false;
        m_PlayerInput = GetComponent<PlayerInput>();
        m_PlayerMovement = GetComponent<PlayerMovement>();
        m_PlayerAnimation = GetComponent<PlayerAnimation>();
        m_WeaponController = GetComponent<WeaponController>();
        m_PlayerSFX = GetComponent<PlayerSFX>();
    }

    #endregion

    #region UPDATE LOOP
    // Update is called once per frame
    void Update()
    {
        if (!m_IsDead)
        {
            AimAtMouse();
            m_PlayerInput.AllInputs(m_WeaponController, ref m_VectorInput);
            m_PlayerAnimation.AddMovementAnimation(ref m_VectorInput);
        }
    }

    void FixedUpdate()
    {
        m_PlayerMovement.Move(ref m_VectorInput);
    }

    #endregion

    #region MOUSE AIMING
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

    #endregion

    #region TAKE DAMAGE FUNCTIONALITY
    public void TakeDamage(int damageAmount)
    {
        //Take away a portion of health
        m_Health -= damageAmount;

        //Play damaged SFX
        m_PlayerSFX.PlayDamagedSFX();

        //if health is less than or equal to zero then kill the player
        if (m_Health <= 0)
            StartCoroutine(KillPlayer());
    }
    private IEnumerator KillPlayer()
    {
        m_IsDead = true;

        //Run death animation
        m_PlayerAnimation.PlayDeathAnimation();

        //Death sfx
        m_PlayerSFX.PlayDeathSFX();

        //Wait a couple of secnonds
        yield return new WaitForSeconds(5);

        //Load game over screen
        GameManager.Instance().GameOver();
    }
    #endregion
}
