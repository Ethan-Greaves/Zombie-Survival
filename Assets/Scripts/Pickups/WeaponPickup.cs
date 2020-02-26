using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Pickup
{
    [SerializeField] Weapon m_Weapon;
    private WeaponController m_PlayerWeaponController;
    

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerWeaponController = m_PlayerCharacter.GetComponent<WeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override protected void PickupAction()
    {
        WeaponPickupAction();
    }

    void WeaponPickupAction()
    { 
        //Equip the weapon
        EquipSpecificWeapon(m_Weapon);
    }

    protected virtual void EquipSpecificWeapon(Weapon weaponToEquip)
    {
        m_PlayerWeaponController.EquipWeapon(weaponToEquip);
    }
}
