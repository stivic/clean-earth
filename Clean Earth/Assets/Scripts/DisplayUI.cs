using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUI : MonoBehaviour
{
    public Text karmaLabel;
    public  Text inventoryLabel;
    public float karma;
    public float inventoryCount;

    private static DisplayUI _instance;
    public static DisplayUI Instance { get { return _instance; } }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        karmaLabel.text = "Karma: " + karma;
        inventoryLabel.text = "Inventory: " + inventoryCount;
    }
}
