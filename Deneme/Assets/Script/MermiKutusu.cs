using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MermiKutusu : MonoBehaviour
{
    string[] silahlar = 
        {
            "Magnum",
            "Pompali",
            "Sniper",
            "Taramali"
        };
    int[] mermiSayisi =
    {
            10,
            20,
            5,
            60
        };
    public List<Sprite> silahResimleri = new();
    public Image silahResmi;

    public int noktasi;
    public string olusanSilahinTuru;
    public int olusanMermiSayisi;
    // Start is called before the first frame update
    void Start()
    {
        int gelenAnahtar = Random.Range(0, silahlar.Length);
        olusanSilahinTuru = silahlar[gelenAnahtar];
        olusanMermiSayisi = mermiSayisi[Random.Range(0, mermiSayisi.Length)];
        silahResmi.sprite = silahResimleri[gelenAnahtar];
        /*
        olusanSilahinTuru = "Taramali";
        olusanMermiSayisi = 30;*/
    }
    

    // Update is called once per frame
    void Update()
    {

    }
   
}
