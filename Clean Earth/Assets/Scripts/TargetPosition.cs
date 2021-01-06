using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPosition : MonoBehaviour
{
    private Transform position;
    public Transform aiPosition;
    private bool targetReached = false;
    public float maxWaitingPeriod;
    public GameObject positionTester;
    private float minY = -35f;
    private float minX = -95f;
    private float maxY = 115f;
    private float maxX = 95f;

    // Start is called before the first frame update
    void Start()
    {
        position = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aiPosition == null)
        {
           return;
        }
        
        if ((Vector3.Distance(position.position, aiPosition.position) < 2f) && !targetReached)
        {
            StartCoroutine(ChangeTargetCo());
        }

    }

    private IEnumerator ChangeTargetCo()
    {
        targetReached = true;
        yield return new WaitForSeconds(Random.Range(0f, Random.Range(0f, maxWaitingPeriod)));
        position.SetPositionAndRotation(WorldInit.Instance.FindNewTarget(), Quaternion.identity);
        targetReached = false;
    }

    

}