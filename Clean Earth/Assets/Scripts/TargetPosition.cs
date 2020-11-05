using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPosition : MonoBehaviour
{
    private Transform position;
    public Transform aiPosition;
    private bool targetReached = false;
    public float maxWaitingPeriod;

    // Start is called before the first frame update
    void Start()
    {
        position = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector3.Distance(position.position, aiPosition.position) < 5) && targetReached == false)
        {
            StartCoroutine(ChangeTargetCo());
        }

    }

    private IEnumerator ChangeTargetCo()
    {
        targetReached = true;
        yield return new WaitForSeconds(Random.Range(0f, Random.Range(0f, maxWaitingPeriod)));
        int x = Random.Range(-95, 95);
        int y = Random.Range(-35, 115);
        position.SetPositionAndRotation(new Vector3(x, y, 0), new Quaternion(0, 0, 0, 0));
        targetReached = false;
    }

}