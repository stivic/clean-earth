using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTester : MonoBehaviour
{
    public bool allClear = true;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other)
    {
        allClear = false;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        allClear = true;
    }
}
