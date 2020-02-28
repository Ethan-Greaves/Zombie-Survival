using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    //Create an array of projectiles
    [Header("Projectiles")]
    [SerializeField] Projectile[] m_Projectiles;

    public override void FireWeapon()
    {
        float offsetAmount = 1.0f;

        //loop through all projectiles
        for (int i = 0; i < m_Projectiles.Length; i++)
        {
            //give them slightly different transforms to create random spread
            Vector3 randomOffset = new Vector3(Random.Range(-offsetAmount, offsetAmount), Random.Range(-offsetAmount, offsetAmount), Random.Range(-offsetAmount, offsetAmount));

            //instantiate all 3 projectiles
            Projectile tempProjectile = Instantiate(m_Projectiles[i], (m_Barrel.position + randomOffset), m_Barrel.rotation);

        }
    }
}
