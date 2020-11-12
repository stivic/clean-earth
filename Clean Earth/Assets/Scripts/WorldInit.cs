using System.Collections.Generic;
using UnityEngine;

public class WorldInit : MonoBehaviour
{
    public uint trashCanNumber;
    public uint garbageNumber;
    public uint aiNumber;
    public uint playerNumber;
    public GameObject trashCan;
    public List<GameObject> garbage;
    public GameObject ai;
    public GameObject player;
    public GameObject positionTester;
    private float minY = -35f;
    private float minX = -95f;
    private float maxY = 115f;
    private float maxX = 95f;
    // Start is called before the first frame update
    void Start()
    {
        
        //ai = Resources.Load<GameObject>("Prefabs/People/AI");
        for (int i = 0; i < aiNumber; i++)
        {
            Instantiate(ai, FindNewTarget(), Quaternion.identity);
        }
        
        for (int i = 0; i < garbageNumber; i++)
        {
            Instantiate(garbage[Random.Range(0, garbage.Count)], FindNewTarget(), Quaternion.identity);
        }
        
        for (int i = 0; i < trashCanNumber; i++)
        {
            Instantiate(trashCan, FindNewTarget(), Quaternion.identity);
        }
        
        for (int i = 0; i < playerNumber; i++)
        {
            Instantiate(player, FindNewTarget(), Quaternion.identity);
        }
        
    }
    
    private Vector3 FindNewTarget()
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
}
