using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    public Transform target;

    private PlayerInfo info;
    private Inventory inventory;
    private bool collectObject = false;
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
                else if(!inventory.isInventoryEmpty() && ThrowGarbage())
                {
                    Debug.Log("throwing garbage!");
                    inventory.ThrowItem();
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

    
}
