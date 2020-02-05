using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] float rayCastDistance = 100.0f;
    [SerializeField] ParticleSystem muzzleFlash;

    override public void Shoot()
    {
        if (GetProjectilesInMagazine() > 0 && GetIsReloading() == false)
        {
            //Play the firing sound effect
            audioSource.PlayOneShot(gunFireSound);

            //Spawn the muzzle flash
            Vector3 muzzleFlashOffset = new Vector3(0, 0, 0.15f);
            ParticleSystem tempMuzzleFlash = Instantiate(muzzleFlash, GetBarrel().transform.position + muzzleFlashOffset, GetBarrel().transform.rotation) ;
            tempMuzzleFlash.Play();

            //Destroy the muzzle flash after a small period
            Destroy(tempMuzzleFlash.gameObject, 0.02f);

            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, rayCastDistance))
            {
                Debug.Log(hitInfo.transform.name);
            }

            Debug.DrawRay(transform.position, transform.forward, Color.red, 0.3f);
            ReduceProjectileInMagazine();
        }
    }
}
