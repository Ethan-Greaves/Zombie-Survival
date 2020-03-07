using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVFX : MonoBehaviour
{
    [Header("VFX")]
    [SerializeField] protected ParticleSystem m_TakeDamageVFX;

    public void TakeDamageVFX()
    {
        if (m_TakeDamageVFX != null)
        {
            //Spawn the vfx
            ParticleSystem tempTakeDamageVFX = Instantiate(m_TakeDamageVFX, gameObject.transform.position, gameObject.transform.rotation);
            tempTakeDamageVFX.Play();

            //Destroy the VFX after a small period
            Destroy(tempTakeDamageVFX.gameObject, 0.2f);
        }
        else
            Debug.LogWarning("m_TakeDamageVFX Has not been assigned!!!");
    }
}
