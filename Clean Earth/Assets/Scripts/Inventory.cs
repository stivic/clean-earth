using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Inventory : MonoBehaviour
{
    private static int inventorySize = 10;
    public List<GameObject> inventory;
    private static GameObject[] _garbage;
    private PlayerInfo info;
    private float inTrashCoef = 0.5f;
    private float timeForNewItem = 120f;

    private void Awake()
    {
        inventory = new List<GameObject>(inventorySize);
    }

    void Start()
    {
        info = GetComponent<PlayerInfo>();
        _garbage = Resources.LoadAll<GameObject>("Prefabs/Objects/Garbage");
        InvokeRepeating(nameof(AddRandomItem), 30f, timeForNewItem);
    }

    public bool AddItem(GameObject item)
    {
        if (inventory.Count < inventorySize)
        {
            inventory.Add(item);
            item.SetActive(false);
            info.IncreaseKarma(item.name);
            return true;
        }
        return false;
    }
    //za sada baca prvi item po redu iz inventory-a
    public void ThrowItem()
    {
        if (!inventory.Any())
        {
            print("Inventory is empty!");
        }
        else
        {
            int randomItem = Random.Range(0, inventory.Count);
            GameObject item = inventory[Random.Range(0, inventory.Count)];
            inventory.RemoveAt(randomItem);
            item.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            item.SetActive(true);
            if (info.insideTrashCanArea)
            {
                Destroy(item);
                info.IncreaseKarma(item.name, inTrashCoef);
            }
            else
            {
                info.DecreaseKarma(item.name);
            }

        }
    }
    
    public void AddRandomItem()
    {
        if (inventory.Count < inventorySize)
        {
            int itemIndex = Random.Range (0, _garbage.Length);
            GameObject item = Instantiate (_garbage [itemIndex]);
            inventory.Add(item);
            item.SetActive(false);
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

