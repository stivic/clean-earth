using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    public bool inventory;          //if true we can add this object in inventory

    public void DoInteraction()
    {
        gameObject.SetActive(false);
    }

    public void Throw()
    {
        gameObject.SetActive(true);
        Debug.Log("Smece baceno");
    }
}
