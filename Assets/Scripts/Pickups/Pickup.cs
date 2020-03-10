using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] protected AudioClip m_PickupSFX;
    [SerializeField] protected ParticleSystem m_PickupVFX;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //Play SFX
            SoundManager.Instance().PlaySFX(m_PickupSFX);

            //Play VFX
            PlayPickupVFX();

            //Apply action to player
            PickupAction(other);

            //Delete Pickup
            Destroy(gameObject);
        }
    }

    protected abstract void PickupAction(Collider player);

    private void PlayPickupVFX()
    {
        if (m_PickupVFX == null)
        {
            Debug.LogWarning("VFX not added to " + gameObject.name);
            return;
        }
        else
        {
            ParticleSystem tempVFX = Instantiate(m_PickupVFX, transform.position, m_PickupVFX.transform.rotation);
            tempVFX.Play();

            Destroy(tempVFX, 1f) ;
        }
    }
}
