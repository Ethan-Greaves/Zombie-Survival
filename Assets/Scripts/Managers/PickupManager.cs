using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    private static PickupManager pickupMangerInstance;
    public static PickupManager Instance()
    {
        if (pickupMangerInstance == null)
            pickupMangerInstance = new PickupManager();
     
        return pickupMangerInstance;
    }

    //Used for initialising variables or game states
    private void Awake()
    {
        pickupMangerInstance = new PickupManager();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
