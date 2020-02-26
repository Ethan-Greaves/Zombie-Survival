using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] protected Player m_PlayerCharacter;
    [SerializeField] protected AudioClip m_PickupSFX;

    protected AudioSource m_AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Play SFX
        //m_AudioSource.PlayOneShot()

        //Play VFX


        PickupAction();
        Destroy(gameObject);
    }

    protected abstract void PickupAction();
}
