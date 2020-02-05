using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Projectile projectile;
    [SerializeField] AudioClip gunReloadSound;
    [SerializeField] protected AudioClip gunFireSound;

    private Transform barrel;
    private int magazineSize;
    private int projectilesInMagazine;
    private float reloadSpeed;
    private float fireRate;
    private bool isReloading = false;

    protected AudioSource audioSource;

    // Start is called before the first frame update
    public void Start()
    {
        barrel = transform.Find("Barrel").transform;
        audioSource = GetComponent<AudioSource>();

        magazineSize = 5;
        projectilesInMagazine = magazineSize;
        reloadSpeed = 3.0f;
        fireRate = 2.0f;
        reloadSpeed = 3.0f;
    }
    
    //Getters
    protected int GetMagazineSize() { return magazineSize; }
    protected int GetProjectilesInMagazine() { return projectilesInMagazine; }
    protected float GetReloadSpeed() { return reloadSpeed; }
    protected float GetFireRate() { return fireRate; }
    protected Transform GetBarrel() { return barrel; }
    protected bool GetIsReloading() { return isReloading; }

    //Setters
    protected void ReduceProjectileInMagazine()
    {
        projectilesInMagazine -= 1;
    }

    virtual public IEnumerator Reload()
    {
        isReloading = true;
        //Play the reload sound
        audioSource.PlayOneShot(gunReloadSound);
        //Wait a few seconds to emualte a reload delay
        yield return new WaitForSeconds(reloadSpeed);
        //reset the projectiles
        projectilesInMagazine = magazineSize;
        isReloading = false;
    }

    public abstract void Shoot();
}
