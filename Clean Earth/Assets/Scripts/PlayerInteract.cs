using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject currentInterObj = null;
    private Inventory inventory;
    private PlayerInfo info;

    void Start()
    {
        info = GetComponent<PlayerInfo>();
        inventory = GetComponent<Inventory>();
    }

    public void Update()
    {
        if (Input.GetButtonDown("InteractUp") &&  currentInterObj)
        {
            //if (currentInterObjScript.inventory)
            {
                inventory.AddItem(currentInterObj);
            }
            

        }
        if (Input.GetButtonDown("InteractDown"))
        {
            Debug.Log("Krece bacati");
            inventory.ThrowItem();
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("interactObject"))
        {
            Debug.Log(collision.name);
            currentInterObj = collision.gameObject;
        }
        else if(collision.CompareTag("trashCan"))
        {
            info.insideTrashCanArea = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("interactObject"))
        {
            if(collision.gameObject == currentInterObj)
            {
                currentInterObj = null;
            }
        }
        else if (collision.CompareTag("trashCan"))
        {
            info.insideTrashCanArea = false;
        }
    }
}
