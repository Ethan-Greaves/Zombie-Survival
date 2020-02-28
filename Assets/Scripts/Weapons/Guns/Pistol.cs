using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] float m_BulletDistance = 100.0f;
    [SerializeField] float m_ImpactForce = 30.0f;

    override public void FireWeapon()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(m_Barrel.transform.position, m_Barrel.transform.forward, out hitInfo, m_BulletDistance))
        {
            Debug.Log(hitInfo.transform.name);

            //Acquire the enemy
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();

            if(enemy != null)
            {
                enemy.TakeDamage(m_Damage);

                //if(hitInfo.rigidbody != null)
                //{
                //    hitInfo.rigidbody.AddForce(-hitInfo.normal * m_ImpactForce);
                //}
            }
        }
    }

    
}
