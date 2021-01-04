using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;

public class WorldInit : MonoBehaviourPunCallbacks
{
    public uint trashCanStartNumber;
    public uint garbageStartNumber;
    public uint aiStartNumber;
    public GameObject trashCan;
    public List<GameObject> garbage;
    public GameObject ai;
    public GameObject positionTester;
    
    private const float minY = -100f;
    private const float minX = -100f;
    private const float maxY = 100f;
    private const float maxX = 100f;
    
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
        GameObject tester = Instantiate( positionTester, position, Quaternion.identity );
        bool allClear = tester.GetComponent<PositionTester>().allClear;
        Destroy(tester);
        
        if (allClear) 
        {
            return position;
        }
        print("nova meta.");
        return FindNewTarget();
    }

    public void SpawnObjects()
    {
        SpawnAI(aiStartNumber);
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
