using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject target;
    [SerializeField] Transform targetTransform;
    [SerializeField] float smoothSpeed = 0.125f;
    [SerializeField] Vector3 offset;

    private void Awake()
    {
        target = GameObject.Find("Player");
        targetTransform = target.transform;
    }

    //Late Update is similar to Update. However, it is called right after. 
    void LateUpdate()
    {
        transform.position = targetTransform.position + offset;
    }
}
