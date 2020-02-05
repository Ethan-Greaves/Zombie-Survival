using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] Projectile projectile;
    [SerializeField] Weapon startingWeapon;
    [SerializeField] Weapon equippedWeapon;
    [SerializeField] Transform hand;


    
    // Start is called before the first frame update
    void Start()
    {
        if(equippedWeapon == null)
            EquipWeapon(startingWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if (equippedWeapon != null)
            equippedWeapon.Shoot();
    }

    public void Reload()
    {
        StartCoroutine(equippedWeapon.Reload());
    }

    public void EquipWeapon(Weapon weaponToEquip)
    {
        //If the player is already holding a weapon
        if(equippedWeapon != null)
            Destroy(equippedWeapon.gameObject);

        equippedWeapon = Instantiate(weaponToEquip, hand.transform.position, hand.transform.rotation) as Weapon;
        equippedWeapon.transform.parent = hand;

    }
}
