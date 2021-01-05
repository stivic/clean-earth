using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;

public class WorldInit : MonoBehaviourPunCallbacks
{
    public uint trashCanStartNumber;
    public uint garbageStartNumber;
    public uint aiStartNumber;
    public uint badAIStartNumber;
    public GameObject trashCan;
    public List<GameObject> garbage;
    public GameObject positionTester;
    public List<Transform> aiPlayers;
    public List<GameObject> badAIPlayers;
    
    private const float minY = -100f;
    private const float minX = -100f;
    private const float maxY = 100f;
    private const float maxX = 90f;

    public uint allowedGarbageCount;
    public uint currentGarbageCount = 0;
    public uint currentReportCount = 0;

    
    
    private static WorldInit _instance;
    public static WorldInit Instance { get { return _instance; } }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public Vector3 FindNewTarget()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        Vector3 position = new Vector3(x, y, 0);
        
        Collider2D[] colliders = new Collider2D[100];
        ContactFilter2D contact = new ContactFilter2D();
        contact.useLayerMask = true;
        contact.useTriggers = false;
        contact.SetLayerMask(LayerMask.GetMask("Collision"));
        
        GameObject tester = Instantiate( positionTester, position, Quaternion.identity );

        bool allClear =
            Physics2D.OverlapCollider(tester.GetComponent<CircleCollider2D>(), contact, colliders) == 0;
        Destroy(tester);
        
        
        
        if (allClear) 
        {
            Debug.Log("Iz prve.");
            return position;
        }
        Debug.Log("nova meta.");
        return FindNewTarget();
    }

    public void SpawnObjectsOnStart()
    {
        SpawnAI(aiStartNumber);
        SpawnBadAI(badAIStartNumber);
        SpawnGarbage(garbageStartNumber);
        SpawnTrashCans(trashCanStartNumber);
    }

    public void SpawnAI(uint aiNumber)
    {
        for (int i = 0; i < aiNumber; i++)
        {
            Debug.Log("Spawning AI");
            GameObject ai = PhotonNetwork.Instantiate(Path.Combine("Prefabs/People", "AI"), FindNewTarget(), Quaternion.identity);
            //ai.GetPhotonView().OwnershipTransfer = OwnershipOption.Takeover;
            if (ai.name.EndsWith("(Clone)"))
            {
                ai.name = ai.name.Remove(ai.name.Length - 7);
            }
            aiPlayers.Add(ai.GetComponentInChildren<Transform>().Find("AIPlayer"));
        }
    }

    public void SpawnBadAI(uint badAINumber)
    {
        for (int i = 0; i < badAINumber; i++)
        {
            Debug.Log("Spawning AI");
            GameObject badAI = PhotonNetwork.Instantiate(Path.Combine("Prefabs/People", "BadAI"), FindNewTarget(), Quaternion.identity);
            if (badAI.name.EndsWith("(Clone)"))
            {
                badAI.name = badAI.name.Remove(badAI.name.Length - 7);
            }
            badAIPlayers.Add(badAI);
        }
    }

    public void SpawnGarbage(uint garbageNumber)
    {
        for (int i = 0; i < garbageNumber; i++)
        {
            Debug.Log("Spawning garbage");
            GameObject item = PhotonNetwork.Instantiate(Path.Combine("Prefabs/Objects/Garbage", garbage[Random.Range(0, garbage.Count)].name), FindNewTarget(), Quaternion.identity);
            //item.GetPhotonView().OwnershipTransfer = OwnershipOption.Takeover;
            if (item.name.EndsWith("(Clone)"))
            {
                item.name = item.name.Remove(item.name.Length - 7);
            }

            currentGarbageCount++;
        }
    }
    
    public void SpawnTrashCans(uint trashCanNumber)
    {
        for (int i = 0; i < trashCanNumber; i++)
        {
            GameObject item = PhotonNetwork.Instantiate(Path.Combine("Prefabs/Objects", trashCan.name), FindNewTarget(), Quaternion.identity);
            if (item.name.EndsWith("(Clone)"))
            {
                item.name = item.name.Remove(item.name.Length - 7);
            }
        }
    }
}
