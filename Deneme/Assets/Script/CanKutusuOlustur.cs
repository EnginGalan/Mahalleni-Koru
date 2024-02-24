using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanKutusuOlustur : MonoBehaviour
{
    public List<GameObject> CanKutusuNoktasi;
    public GameObject CanKutusu;
    public float kutuCikmaSuresi;
    public static bool canKutusuVarMi;
    // Start is called before the first frame update
    void Start()
    {
        canKutusuVarMi = false;
        StartCoroutine(CanKutusuYap());
    }

    IEnumerator CanKutusuYap()
    {
        while (true)
        {
            yield return new WaitForSeconds(kutuCikmaSuresi);
            if (!canKutusuVarMi)
            {
                int randomSayim = Random.Range(0, 6);
                Instantiate(CanKutusu, CanKutusuNoktasi[randomSayim].transform.position, CanKutusuNoktasi[randomSayim].transform.rotation);
                canKutusuVarMi=true;
            }
        }
    }
}
