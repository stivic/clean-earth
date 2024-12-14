using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;

public class WorldInit : MonoBehaviourPunCallbacks
{
    public int trashCanStartNumber;
    public int garbageStartNumber;
    public int aiStartNumber;
    public int badAIStartNumber;
    public GameObject trashCan;
    public List<GameObject> garbage;
    public GameObject positionTester;
    public List<Transform> aiPlayers;
    public List<GameObject> badAIPlayers;
    public Timer timer;

    public int waveNumber = 1;
    public int duration;
    public int allowedGarbageDecrease = 1;
    public bool waveCompleted = false;
    public bool gameOver = false;
    public float score = 0;
    
    
    private const float minY = -100f;
    private const float minX = -100f;
    private const float maxY = 100f;
    private const float maxX = 90f;

    public int allowedGarbageCount;
    public int currentGarbageCount = 0;
    public int currentReportCount = 0;

    
    
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
        SpawnAI((uint)aiStartNumber);
        SpawnBadAI(waveNumber);
        SpawnGarbage((uint)garbageStartNumber);
        SpawnTrashCans((uint)trashCanStartNumber);
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

    public void SpawnBadAI(int badAINumber)
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

        currentReportCount = badAINumber + 2;
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

    public void TimeTillNewRestriction()
    {
        Timer.Instance.duration = duration;
        Timer.Instance.Begin();

        //yield return new WaitUntil(() => Timer.Instance.timeRemaining <= 0);
        
        Invoke("CheckEndConditions", duration);
    }

    private void CheckEndConditions()
    {
        if (currentGarbageCount > allowedGarbageCount)
        {
            gameOver = true;
            IncreaseScore();
        }
        else if (allowedGarbageCount > 0)
        {
            allowedGarbageCount -= allowedGarbageDecrease;
            Invoke("TimeTillNewRestriction", 1f);
        }
        
    }

    public float GetAvgKarma()
    {
        if (aiPlayers == null)
        {
            return 0;
        }
        float sum = 0;
        foreach (var ai in aiPlayers)
        {
            sum += ai.GetComponent<PlayerInfo>().GetKarma();
        }

        return sum / aiPlayers.Count;
    }

    public void IncreaseScore()
    {
        Debug.Log("Score before: " + score);
        score += waveNumber * Mathf.Clamp((allowedGarbageCount - currentGarbageCount),  0, (allowedGarbageCount - currentGarbageCount))* GetAvgKarma() * 10;
        Debug.Log("Avg Karma: " + GetAvgKarma());
        Debug.Log("Allowed: " + (allowedGarbageCount));
        Debug.Log("Current: " + (currentGarbageCount));
        Debug.Log("Score: "+ score);
    }

    public void WaveSetup()
    {
       waveCompleted = true;
       waveNumber++;
       IncreaseScore();
       allowedGarbageDecrease = waveNumber;
       allowedGarbageCount = garbageStartNumber;
       SpawnBadAI(waveNumber);
       SpawnAI((uint)waveNumber);
       SpawnGarbage((uint)(garbageStartNumber-currentGarbageCount));
       duration += waveNumber * 2;
    }
}
