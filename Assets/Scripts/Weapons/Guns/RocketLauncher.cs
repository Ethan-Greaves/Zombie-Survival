using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon
{
    [SerializeField] RocketProjectile m_RocketProjectile;

    public Enemy FindClosestEnemy()
    {
        //TODO Maybe change this to list or different data structure
        //Find every enemy currently in the scene
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        //If there are no enemies present 
        if (enemies.Length == 0) { return null; }
        else
        {
            //Get the first enemy
            Enemy closestEnemy = enemies[0];

            //Calculate its distance from the rocket launcher
            var closestEnemyDistance = Vector2.Distance(transform.position, closestEnemy.transform.position);

            //Loop through every enemy in the scene currently
            for (int i = 0; i < enemies.Length; i++)
            {
                //If the distance of enemy[i] is less than the distance of the closest enemy
                if (Vector2.Distance(transform.position, enemies[i].transform.position) < closestEnemyDistance)
                {
                    //That enemy is now the closest enemy
                    closestEnemy = enemies[i];

                    //Shortest distance is now the distance of the new closest enemy
                    closestEnemyDistance = Vector2.Distance(transform.position, enemies[i].transform.position);
                }
            }

            //return closest enemy
            return closestEnemy;
        }
    }

    override public void FireWeapon()
    {
        //Play the firing sound effect
        m_AudioSource.PlayOneShot(m_GunFireSound);

        ReduceProjectileInMagazine();

        Instantiate(m_RocketProjectile, GetBarrel().transform.position, GetBarrel().rotation);
    }
}
