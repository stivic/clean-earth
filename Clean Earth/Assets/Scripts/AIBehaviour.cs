using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIBehaviour : MonoBehaviour
{
    public Transform target;

    private PlayerInfo info;
    private Inventory inventory;
    private bool collectObject = false;
    private bool disposeGarbage = false;
    private float randomDistance = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>();
        info = GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecieveTriggerEnter(string fromObject, Collider2D other){
        if(fromObject == "distanceCollider"){
            if (other.CompareTag("interactObject")) {
                Debug.Log("garbage!");
                if (CollectGarbage())
                {
                    Debug.Log("collecting garbage...");
                    collectObject = true;
                    target.SetPositionAndRotation(other.gameObject.transform.position, Quaternion.identity);
                }
                else if(!inventory.Empty() && ThrowGarbage())
                {
                    Debug.Log("throwing garbage!");
                    inventory.ThrowItem();
                }
            }
            else if (other.CompareTag("trashCan"))
            {
                Debug.Log("trash can!");
                if (!inventory.Empty() && DisposeGarbage())
                {
                    Debug.Log("disposing garbage...");
                    disposeGarbage = true;
                    Vector3 randomOffset = new Vector3(Random.Range(-randomDistance, randomDistance), 
                                                        Random.Range(-randomDistance, randomDistance),
                                                        0);
                    target.SetPositionAndRotation(other.gameObject.transform.position + randomOffset,
                                                    Quaternion.identity);
                    
                }
                
            }
        }
        else if(fromObject == "playerCollider"){
            if (other.CompareTag("interactObject")) {
                if (collectObject)
                {
                    if (inventory.AddItem(other.gameObject))
                    {
                        Debug.Log("garbage collected!");
                    }
                    collectObject = false;
                }
                
            }
            else if (other.CompareTag("trashCan"))
            {
                info.insideTrashCanArea = true;
                if (disposeGarbage)
                {
                    int numberOfItems = Random.Range(1, inventory.Count());
                    for (int i=0; i<numberOfItems; i++)
                    {
                        inventory.ThrowItem();
                    }
                    Debug.Log("garbage disposed!");
                    disposeGarbage = false;
                }
            }
        }
    }

    public void RecieveTriggerExit(string fromObject, Collider2D other)
    {
        if (fromObject == "playerCollider")
        {
            if (other.CompareTag("trashCan"))
            {
                info.insideTrashCanArea = false;
            }
        }
    }

    private bool CollectGarbage()
    {
        return Random.Range(0f, 1f) <= info.GetKarma()/2f;
    }

    private bool ThrowGarbage()
    {
        return Random.Range(0f, 1f) <= (1-info.GetKarma())/2f;
    }
    
    private bool DisposeGarbage()
    {
        return Random.Range(0f, 1f) <= (info.GetKarma() / 2f) * (1 + inventory.Count() / inventory.Size());

    }
    
}
