using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public void AllInputs(WeaponController weaponController, ref Vector3 vectorInput)
    {
        InputFireWeapon(weaponController);
        InputReload(weaponController);
        InputPauseGame();
        InputMovement(ref vectorInput);
    }

    private void InputFireWeapon(WeaponController weaponController)
    {
        //If left mouse button was pressed
        if (Input.GetMouseButton(0))
            weaponController.Shoot();
    }

    private void InputReload(WeaponController weaponController)
    {
        if (Input.GetKeyDown(KeyCode.R))
            weaponController.Reload();
    }

    private void InputPauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            GameManager.Instance().PauseGame();
    }

    private void InputMovement(ref Vector3 vectorInput)
    {
        vectorInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0,
                                    Input.GetAxisRaw("Vertical"));
    }
}
