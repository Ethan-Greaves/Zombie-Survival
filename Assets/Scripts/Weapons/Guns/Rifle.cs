using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    override public void FireWeapon()
    {
        CreateRayCast();
    }


}
