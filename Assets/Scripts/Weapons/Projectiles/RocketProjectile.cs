using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : MonoBehaviour
{
    [SerializeField] RocketLauncher m_RocketLauncher;
    [SerializeField] float m_Speed;

    private Rigidbody m_RigidBody;

    // Start is called before the first frame update
    void Start()
    {
        m_Speed = 50.0f;
        m_RigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Seek(m_RocketLauncher.FindClosestEnemy());
    }

    //Pursuit Steering Behaviour
    private void Seek(Enemy enemy)
    {
        transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, m_Speed * Time.deltaTime);
    }
}
