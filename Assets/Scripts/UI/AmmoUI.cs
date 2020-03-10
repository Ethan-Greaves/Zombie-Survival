using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] Player m_Player;

    private GameObject m_MaxAmmoUI;
    private GameObject m_CurrentAmmoUI;
    private WeaponController m_weaponController;
    private Weapon m_CurrentlyEquippedWeapon;
    private TextMeshProUGUI m_MaxAmmoText;
    private TextMeshProUGUI m_CurrentAmmoText;

    private void Awake()
    {
        m_MaxAmmoUI = gameObject.transform.GetChild(0).gameObject;
        m_CurrentAmmoUI = gameObject.transform.GetChild(1).gameObject;

        m_weaponController = m_Player.GetComponent<WeaponController>();

        m_MaxAmmoText = m_MaxAmmoUI.GetComponent<TMPro.TextMeshProUGUI>();
        m_CurrentAmmoText = m_CurrentAmmoUI.GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentlyEquippedWeapon = m_weaponController.GetEquippedWeapon();

        m_MaxAmmoText.text = m_CurrentlyEquippedWeapon.GetTotalAmmo().ToString();
        m_CurrentAmmoText.text = m_CurrentlyEquippedWeapon.GetProjectilesInMagazine().ToString() + " /";
    }

    private void LateUpdate()
    {
        
    }
}
