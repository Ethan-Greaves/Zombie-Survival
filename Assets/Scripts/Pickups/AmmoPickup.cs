using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickup
{
    private WeaponController m_WeaponController;
    private Weapon m_PlayersEquippedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        m_WeaponController = m_PlayerCharacter.GetComponent<WeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        m_PlayersEquippedWeapon = m_WeaponController.GetEquippedWeapon();
    }

    protected override void PickupAction()
    {
        m_PlayersEquippedWeapon.SetTotalAmmo(m_PlayersEquippedWeapon.GetStartingAmmo());
    }
}
