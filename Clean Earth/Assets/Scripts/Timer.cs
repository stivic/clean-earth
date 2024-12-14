using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public int duration = 60;
    public int timeRemaining;
    public bool isCountingDown = false;
    
    private static Timer _instance;
    public static Timer Instance { get { return _instance; } }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
            
        }
    }
    public void Begin()
    {
        if (!isCountingDown) {
            isCountingDown = true;
            timeRemaining = duration;
            Invoke ( "_tick", 1f );
        }
    }
 
    private void _tick() {
        timeRemaining--;
        if(timeRemaining > 0) {
            Invoke ( "_tick", 1f );
        } else {
            isCountingDown = false;
        }
    }
}