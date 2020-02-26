using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Gun Sounds")]
    [SerializeField] AudioClip m_GunReloadSound;
    [SerializeField] protected AudioClip m_GunFireSound;
    [SerializeField] protected AudioClip m_GunEmptyMagazineSound;

    [Header("VFX")]
    [SerializeField] protected ParticleSystem muzzleFlash;

    [Header("Gun Stats")]
    [SerializeField] protected float fireRate;
    [SerializeField] protected int magazineSize;
    [SerializeField] protected int startingAmmo;

    private float m_FireDelay;
    private Transform barrel;
    private bool isReloading;
    private int projectilesInMagazine;
    private int totalAmmo;
    protected float reloadSpeed;
    protected AudioSource audioSource;

    public abstract void FireWeapon();

    //Getters
    //TODO Are these needed?
    public int GetMagazineSize() { return magazineSize; }
    public int GetProjectilesInMagazine() { return projectilesInMagazine; }
    public int GetTotalAmmo() { return totalAmmo; }
    public float GetReloadSpeed() { return reloadSpeed; }
    public float GetFireRate() { return fireRate; }
    public Transform GetBarrel() { return barrel; }
    public bool GetIsReloading() { return isReloading; }
    public int GetStartingAmmo() { return startingAmmo; }

    public void SetTotalAmmo(int ammoToAdd) { totalAmmo = ammoToAdd; }

    // Start is called before the first frame update
    virtual public void Start()
    {
        barrel = transform.Find("Barrel").transform;
        audioSource = GetComponent<AudioSource>();
        projectilesInMagazine = magazineSize;
        isReloading = false;
        reloadSpeed = m_GunReloadSound.length;
        totalAmmo = startingAmmo;
        m_FireDelay = 0;
    }

    private void Update()
    {
        m_FireDelay -= Time.deltaTime;
    }

    protected void ReduceProjectileInMagazine()
    {
        projectilesInMagazine--;
    }

    public IEnumerator Reload()
    {
        //TODO Make it so that reload sfx cant be played agin whilst reloading 

        if(totalAmmo > 0 && projectilesInMagazine != magazineSize)
        {
            isReloading = true;

            //Play the reload sound
            audioSource.PlayOneShot(m_GunReloadSound);

            //Wait a few seconds to emualte a reload delay
            yield return new WaitForSeconds(reloadSpeed);

            DeductTotalAmmo();

            isReloading = false;
        }
    }

    private void DeductTotalAmmo()
    {
        //Create a variable which saves the total magazine size subtracted by
        //the current bullets in the mag to find out how many bullets have been fired
        int ammoFired = magazineSize - projectilesInMagazine;

        //If total ammo is greater than or equal to the ammo fired, then ammo to deduct
        //will equal the ammo fired, otherwise it will be equal to just the total ammo
        int totalAmmoToDeduct = (totalAmmo >= ammoFired) ? ammoFired : totalAmmo;

        //Reduce the total ammo
        totalAmmo -= totalAmmoToDeduct;

        //Whatever is taken away from totalAmmo, should be added to projectilesInMag
        projectilesInMagazine += totalAmmoToDeduct;
    }

    public void Shoot()
    {
        if(m_FireDelay <= 0)
        {
            //If there are bullets in the mag and the player isn't reloading
            if (projectilesInMagazine > 0 && isReloading == false)
            {
                ShowMuzzleFlash();
                FireWeapon();

                //Reset the delay
                m_FireDelay = fireRate;
            }
            //Otherwise, if there are no bullets in the magazine, and the player isn't reloading
            else if (projectilesInMagazine == 0 && isReloading == false)
            {
                //Give the player feedback that they need to reload
                audioSource.PlayOneShot(m_GunEmptyMagazineSound);

                //Make it so that the empty magazine sound is played at a regular rate
                m_FireDelay = 1.0f;
            }
        }
    }

    private void ShowMuzzleFlash()
    {
        //Spawn the muzzle flash
        ParticleSystem tempMuzzleFlash = Instantiate(muzzleFlash, barrel.transform.position, barrel.transform.rotation);
        tempMuzzleFlash.Play();

        //Destroy the muzzle flash after a small period
        Destroy(tempMuzzleFlash.gameObject, 0.02f);
    }
}
