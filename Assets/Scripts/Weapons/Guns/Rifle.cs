using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    [SerializeField] float rayCastDistance = 100.0f;

    override public void FireWeapon()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(m_Barrel.transform.position, m_Barrel.transform.forward, out hitInfo, rayCastDistance))
        {
            Debug.Log(hitInfo.transform.name);
        }
    }


}
