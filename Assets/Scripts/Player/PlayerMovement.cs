using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Responsible for dealing with player movement inout and adding force to move the player. Also player movement animations.
 */

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float m_Speed = 5f;

    private Rigidbody m_RigidBody;
    private Vector3 m_VectorInput;
    private Animator m_Animator;


    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        m_VectorInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0,
                                    Input.GetAxisRaw("Vertical"));
    }

    private void Move()
    {
        m_RigidBody.AddForce(m_VectorInput * m_Speed * Time.fixedDeltaTime);
        AddAnimation();
    }

    private void AddAnimation()
    {
        //Animation needs to be tied to the motion of the character not the player input
        m_Animator.SetFloat("VerticalForward", m_VectorInput.z);
        m_Animator.SetFloat("VerticalBackward", m_VectorInput.z);
        m_Animator.SetFloat("HorizontalLeft", m_VectorInput.x);
        m_Animator.SetFloat("HorizontalRight", m_VectorInput.x);
    }
}
