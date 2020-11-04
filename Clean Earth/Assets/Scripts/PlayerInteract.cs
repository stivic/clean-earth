using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject currentInterObj = null;
    public InteractionObject currentInterObjScript = null;
    public Inventory inventoryScript;

    public void Update()
    {
        if (Input.GetButtonDown("InteractUp") &&  currentInterObj)
        {
            if (currentInterObjScript.inventory)
            {
                inventoryScript.AddItem(currentInterObj);
            }
            

        }
        if (Input.GetButtonDown("InteractDown"))
        {
            Debug.Log("Krece bacati");
            inventoryScript.ThrowItem();
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("interactObject"))
        {
            Debug.Log(collision.name);
            currentInterObj = collision.gameObject;
            currentInterObjScript = currentInterObj.GetComponent<InteractionObject>();

        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("interactObject"))
        {
            if(collision.gameObject == currentInterObj)
            {
                currentInterObj = null;
                currentInterObjScript = null;
            }
        }
    }
}
