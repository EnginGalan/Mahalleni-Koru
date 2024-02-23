using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject[] silahlar;
    public AudioSource degisimSesi;
    int aktifSira;
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SilahDegistir(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SilahDegistir(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SilahDegistir(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SilahDegistir(3);
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            QTusuVersiyonuSilahDegistir();
        }
    }
    void SilahDegistir(int siraNumarasi)
    {
        degisimSesi.Play();
        foreach (GameObject silah in silahlar)
        {
            silah.SetActive(false);
        }
        aktifSira = siraNumarasi;
        silahlar[siraNumarasi].SetActive(true);
    }
    
    void QTusuVersiyonuSilahDegistir()
    {
        degisimSesi.Play();
        foreach (GameObject silah in silahlar)
        {
            silah.SetActive(false);
        }

        if (aktifSira == 3)
        {
            aktifSira = 0;
            silahlar[aktifSira].SetActive(true);
        }
        else
        {
            aktifSira++;
            silahlar[aktifSira].SetActive(true);
        }
    }
}
