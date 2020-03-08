using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Pickup
{
    [SerializeField] Weapon m_Weapon;

    override protected void PickupAction(Collider player)
    {
        WeaponController playerWeaponController = player.GetComponent<WeaponController>();

        //Equip the weapon
        playerWeaponController.EquipWeapon(m_Weapon);
    }
}
