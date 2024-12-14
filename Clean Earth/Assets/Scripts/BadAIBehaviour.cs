using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class BadAIBehaviour : MonoBehaviour
{
    public PlayerInfo info;
    public Inventory inventory;
    private float randomDistance = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>();
        info = GetComponent<PlayerInfo>();
        info.isBadGuy = true;
        Invoke("ThrowGarbage", 10f);
        Invoke("GetRandomGarbage", 10f);
    }
    
    void ThrowGarbage()
    {
        float randomTime = Random.Range(10f, 30f);
        if (!inventory.Empty())
        {
            inventory.ThrowItem();
        }

        Invoke("ThrowGarbage", randomTime);
    }
    
    void GetRandomGarbage()
    {
        float randomTime = Random.Range(10f, 30f);
        if (!inventory.Empty())
        {
            inventory.ThrowItem();
        }

        Invoke("GetRandomGarbage", randomTime);
    }


    
}
