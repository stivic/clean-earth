using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesTxt : MonoBehaviour
{
    public void ShowWaveTxt(int waveNum)
    {
        gameObject.SetActive(true);
        GetComponent<Text>().text = "Wave " + waveNum;
        Invoke("DisableTxt", 3);
    }

    void Disabletxt()
    {
        gameObject.SetActive(false);
    }
}
