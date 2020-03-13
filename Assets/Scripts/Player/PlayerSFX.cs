using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] AudioClip m_DeathSFX;
    [SerializeField] AudioClip m_DamagedSFX;



    public void PlayDeathSFX() { SoundManager.Instance().PlaySFX(m_DeathSFX); }
    public void PlayDamagedSFX() { SoundManager.Instance().PlaySFX(m_DamagedSFX); }

}
