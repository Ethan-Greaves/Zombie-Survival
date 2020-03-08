using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{

    override public void FireWeapon()
    {
        CreateRayCast();
    }

    
}
