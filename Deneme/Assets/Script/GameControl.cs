using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("oyunBasladiMi"))
        {
            PlayerPrefs.SetInt("taramaliMermi", 70);
            PlayerPrefs.SetInt("magnumMermi", 70);
            PlayerPrefs.SetInt("pompaliMermi", 70);
            PlayerPrefs.SetInt("sniperMermi", 70);
            PlayerPrefs.SetInt("kalanMermi", 30);
            PlayerPrefs.SetInt("oyunBasladiMi", 1);
        }       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
