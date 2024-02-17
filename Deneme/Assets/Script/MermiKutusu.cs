using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            30
        };
    public string olusanSilahinTuru;
    public int olusanMermiSayisi;
    // Start is called before the first frame update
    void Start()
    {
        /*olusanSilahinTuru = silahlar[Random.Range(0, silahlar.Length-1)];
        olusanMermiSayisi = mermiSayisi[Random.Range(0, silahlar.Length-1)];*/
        olusanSilahinTuru = "Taramali";
        olusanMermiSayisi = 30;
    }

    // Update is called once per frame
    void Update()
    {

    }
   
}
