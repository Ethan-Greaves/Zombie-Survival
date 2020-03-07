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
    [SerializeField] protected ParticleSystem m_MuzzleFlash;

    [Header("Gun Stats")]
    [SerializeField] protected int m_Damage;
    [SerializeField] protected float m_FireRate;
    [SerializeField] protected int m_MagazineSize;
    [SerializeField] protected int m_StartingAmmo;

    protected float m_ReloadSpeed;
    protected Transform m_Barrel;

    private float m_FireDelay;
    private bool m_IsReloading;
    private int m_ProjectilesInMagazine;
    private int m_TotalAmmo;
    
    public abstract void FireWeapon();

    //Getters
    //TODO Are these needed?
    public int GetMagazineSize() { return m_MagazineSize; }
    public int GetProjectilesInMagazine() { return m_ProjectilesInMagazine; }
    public int GetTotalAmmo() { return m_TotalAmmo; }
    public float GetReloadSpeed() { return m_ReloadSpeed; }
    public float GetFireRate() { return m_FireRate; }
    public Transform GetBarrel() { return m_Barrel; }
    public bool GetIsReloading() { return m_IsReloading; }
    public int GetStartingAmmo() { return m_StartingAmmo; }

    public void SetTotalAmmo(int ammoToAdd) { m_TotalAmmo = ammoToAdd; }

    // Start is called before the first frame update
    void Start()
    {
        m_Barrel = transform.Find("Barrel").transform;
        m_ProjectilesInMagazine = m_MagazineSize;
        m_IsReloading = false;
        m_ReloadSpeed = m_GunReloadSound.length;
        m_TotalAmmo = m_StartingAmmo;
        m_FireDelay = 0;
    }

    private void Update()
    {
        m_FireDelay -= Time.deltaTime;
    }

    protected void ReduceProjectileInMagazine()
    {
        m_ProjectilesInMagazine--;
    }

    public IEnumerator Reload()
    {
        //TODO Make it so that reload sfx cant be played agin whilst reloading 

        if(m_TotalAmmo > 0 && m_ProjectilesInMagazine != m_MagazineSize)
        {
            m_IsReloading = true;

            //Play the reload sound
            SoundManager.Instance().PlaySFX(m_GunReloadSound);

            //Wait a few seconds to emualte a reload delay
            yield return new WaitForSeconds(m_ReloadSpeed);

            DeductTotalAmmo();

            m_IsReloading = false;
        }
    }

    private void DeductTotalAmmo()
    {
        //Create a variable which saves the total magazine size subtracted by
        //the current bullets in the mag to find out how many bullets have been fired
        int ammoFired = m_MagazineSize - m_ProjectilesInMagazine;

        //If total ammo is greater than or equal to the ammo fired, then ammo to deduct
        //will equal the ammo fired, otherwise it will be equal to just the total ammo
        int totalAmmoToDeduct = (m_TotalAmmo >= ammoFired) ? ammoFired : m_TotalAmmo;

        //Reduce the total ammo
        m_TotalAmmo -= totalAmmoToDeduct;

        //Whatever is taken away from totalAmmo, should be added to projectilesInMag
        m_ProjectilesInMagazine += totalAmmoToDeduct;
    }

    public void Shoot()
    {
        if(m_FireDelay <= 0)
        {
            //If there are bullets in the mag and the player isn't reloading
            if (m_ProjectilesInMagazine > 0 && m_IsReloading == false)
            {
                //Play the firing sound effect
                SoundManager.Instance().PlaySFX(m_GunFireSound);

                ReduceProjectileInMagazine();
                ShowMuzzleFlash();
                FireWeapon();

                //Reset the delay
                m_FireDelay = m_FireRate;
            }
            //Otherwise, if there are no bullets in the magazine, and the player isn't reloading
            else if (m_ProjectilesInMagazine == 0 && m_IsReloading == false)
            {
                //Give the player feedback that they need to reload
                SoundManager.Instance().PlaySFX(m_GunEmptyMagazineSound);

                //Make it so that the empty magazine sound is played at a regular rate
                m_FireDelay = 1.0f;
            }
        }
    }

    private void ShowMuzzleFlash()
    {
        //Spawn the muzzle flash
        ParticleSystem tempMuzzleFlash = Instantiate(m_MuzzleFlash, m_Barrel.transform.position, m_Barrel.transform.rotation);
        tempMuzzleFlash.Play();

        //Destroy the muzzle flash after a small period
        Destroy(tempMuzzleFlash.gameObject, 0.02f);
    }

    public void Aim(Vector3 aim)
    {
        transform.LookAt(aim);
    }
}
