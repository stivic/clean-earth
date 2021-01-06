using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Random = UnityEngine.Random;

public class Inventory : MonoBehaviourPun
{
    private static int inventorySize = 10;
    public List<String> inventory;
    private static GameObject[] _garbage;
    private PlayerInfo info;
    private float inTrashCoef = 0.5f;
    private float timeForNewItem = 120f;

    private void Awake()
    {
        inventory = new List<String>(inventorySize);
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDestroy()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    void Start()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        info = GetComponent<PlayerInfo>();
        _garbage = Resources.LoadAll<GameObject>("Prefabs/Objects/Garbage");
        InvokeRepeating(nameof(AddRandomItem), 30f, timeForNewItem);
    }

    public bool AddItem(GameObject item)
    {

        if (inventory.Count < inventorySize)
        {
            if (item.name.EndsWith("(Clone)"))
            {
                item.name = item.name.Remove(item.name.Length - 7);
            }

            if (item.GetPhotonView().Owner.UserId != PhotonNetwork.LocalPlayer.UserId)
            {
                item.GetPhotonView().TransferOwnership(PhotonNetwork.LocalPlayer);
            }
            
            if (item.GetPhotonView().Owner.UserId == PhotonNetwork.LocalPlayer.UserId)
            {
                inventory.Add(item.name);
                info.IncreaseKarma(item.name);
                WorldInit.Instance.currentGarbageCount--;
                PhotonNetwork.Destroy(item);
            }
            
            
            return true;
        }
        return false;
    }
    //za sada baca prvi item po redu iz inventory-a
    public void ThrowItem()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        if (!inventory.Any())
        {
            print("Inventory is empty!");
        }
        else
        {
            int randomItem = Random.Range(0, inventory.Count);
            GameObject item = PhotonNetwork.Instantiate(Path.Combine("Prefabs/Objects/Garbage",inventory[randomItem]), transform.position, Quaternion.identity);
            item.GetPhotonView().OwnershipTransfer = OwnershipOption.Takeover;
            if (item.name.EndsWith("(Clone)"))
            {
                item.name = item.name.Remove(item.name.Length - 7);
            }
            inventory.RemoveAt(randomItem);
            //item.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            //item.SetActive(true);
            if (info.insideTrashCanArea)
            {
                if(item != null)
                {
                    info.IncreaseKarma(item.name, inTrashCoef);
                    PhotonNetwork.Destroy(item);
                    
                }
            }
            else
            {
                info.DecreaseKarma(item.name);
                WorldInit.Instance.currentGarbageCount++;
            }

        }
    }
    
    public void AddRandomItem()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        if (inventory.Count < inventorySize)
        {
            int itemIndex = Random.Range (0, _garbage.Length);
            //GameObject item = Instantiate (_garbage [itemIndex]);
            inventory.Add(_garbage[itemIndex].name);
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

