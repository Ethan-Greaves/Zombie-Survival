using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] float rayCastDistance = 100.0f;
    [SerializeField] Enemy m_Enemy;

    override public void FireWeapon()
    {
        //Play the firing sound effect
        audioSource.PlayOneShot(m_GunFireSound);

        ReduceProjectileInMagazine();

        RaycastHit hitInfo;
        if (Physics.Raycast(GetBarrel().transform.position, GetBarrel().transform.forward, out hitInfo, rayCastDistance))
        {
            Debug.Log(hitInfo.transform.name);
            DamageEnemy();
        }
    }

    private void DamageEnemy()
    {
        m_Enemy.TakeDamage();
    }
}
