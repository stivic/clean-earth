using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //public static int inventoryNum = 10;
    public GameObject[] inventory = new GameObject[10];
   
    

    public void AddItem(GameObject item)
    {
        bool isFull = false;
        for(int i = 0; i<inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                Debug.Log(item.name + " is added");
                isFull = true;
                item.SendMessage("DoInteraction");
                break;
            }
        }
        if (!isFull)
        {
            Debug.Log("Inventory is full");
        }
        
        
    }
    //za sada baca prvi item po redu iz inventory-a
    public void ThrowItem()
    {
        if (isInventoryEmpty())
        {
            Debug.Log("Inventory is empty");
        }
        else
        {
            GameObject item = inventory[0];
            SpriteRenderer r = gameObject.GetComponent<SpriteRenderer>();
            Debug.Log(r.sprite);
            //Debug.Log(gameObject.transform.position.x + " " + gameObject.transform.position.y + " " + item.transform.position.z);
            Vector3 newPosition = new Vector3((float)(gameObject.transform.position.x), (float)(gameObject.transform.position.y), item.transform.position.z);
            item.transform.position = newPosition;
            //item.SendMessage("Throw");
            item.SetActive(true);

            GameObject[] inventoryTmp = new GameObject[10];
            for(int i = 1; i<inventory.Length; i++)
            {
                inventoryTmp[i - 1] = inventory[i];
            }

            inventory = inventoryTmp;

        }
    }

    public bool isInventoryEmpty()
    {
        
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null)
            {
                Debug.Log("Inventory ima nesto");
                return false;
            }
        }
        Debug.Log("Inventory je prazan");
        return true;
    }
}
