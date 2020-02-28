using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;

    private void Start()
    {
        RandomDirection();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        

        Destroy(gameObject, 2.0f);
    }

    private void RandomDirection()
    {
        float Rotation = Random.Range(0, 40);
        transform.Rotate(0, Rotation, 0);
    }
}
