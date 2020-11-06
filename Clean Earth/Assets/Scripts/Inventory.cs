using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.iOS;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //public static int inventoryNum = 10;
    private static int inventorySize = 10;
    public List<GameObject> inventory;
    public static GameObject[] garbage;
    private PlayerInfo info;
    private Transform player;
    private float timeForNewItem = 120f;
   
    void Start()
    {
        inventory = new List<GameObject>();
        info = GetComponent<PlayerInfo>();
        garbage = Resources.LoadAll<GameObject>("Prefabs/Objects/Garbage");
        player = GetComponent<Transform>();
        InvokeRepeating("AddRandomItem", 30f, timeForNewItem);
    }

    public bool AddItem(GameObject item)
    {
        if (inventory.Count < inventorySize)
        {
            inventory.Add(item);
            item.SetActive(false);
            info.IncreaseKarma();
            return true;
        }
        return false;
    }
    //za sada baca prvi item po redu iz inventory-a
    public void ThrowItem()
    {
        if (!inventory.Any())
        {
            Debug.Log("Inventory is empty!");
        }
        else
        {
            int randomItem = Random.Range(0, inventory.Count);
            GameObject item = inventory[randomItem];
            
            inventory.RemoveAt(randomItem);
            SpriteRenderer r = gameObject.GetComponent<SpriteRenderer>();
            //Debug.Log(gameObject.transform.position.x + " " + gameObject.transform.position.y + " " + item.transform.position.z);
            Vector3 newPosition = new Vector3((float)(gameObject.transform.position.x), (float)(gameObject.transform.position.y), item.transform.position.z);
            item.transform.position = newPosition;
            //item.SendMessage("Throw");
            item.SetActive(true);
            if (info.insideTrashCanArea)
            {
                Destroy(item);
                info.IncreaseKarma();
            }
            else
            {
                info.DecreaseKarma();
            }

        }
    }
    
    public void AddRandomItem()
    {
        int itemIndex = Random.Range (0, garbage.Length);
        GameObject myObj = Instantiate (garbage [itemIndex]) as GameObject;
        if (!AddItem(myObj))
        {
            Destroy(myObj);
        }

    }

    public bool Empty()
    {
        return !inventory.Any();
    }

    public int Count()
    {
        return inventory.Count;
    }

    public int Size()
    {
        return inventorySize;
    }
    
}

