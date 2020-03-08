using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickup
{
    protected override void PickupAction(Collider player)
    {
        WeaponController weaponController = player.GetComponent<WeaponController>();
        Weapon playersEquippedWeapon = weaponController.GetEquippedWeapon();

        playersEquippedWeapon.SetTotalAmmo(playersEquippedWeapon.GetStartingAmmo());
    }
}
