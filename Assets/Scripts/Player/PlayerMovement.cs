using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float m_Speed = 1000f;
    private Rigidbody m_RigidBody;

    void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

    public void Move(ref Vector3 vectorInput)
    {
        m_RigidBody.AddForce(vectorInput * m_Speed * Time.fixedDeltaTime);
    }

    
}
