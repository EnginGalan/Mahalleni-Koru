using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermiKutusuOlustur : MonoBehaviour
{
    public List<GameObject> MermiKutusuNoktasi;
    public GameObject MermiKutusu;
    public static bool mermiKutusuVarMi = false;
    public float kutuCikmaSuresi;
    // Start is called before the first frame update
    void Start()

    {
        StartCoroutine(MermiKutusuYap());
    }

    IEnumerator MermiKutusuYap()
    {
        while (true)
        {
            yield return null;
            if (!mermiKutusuVarMi)
            {
                yield return new WaitForSeconds(kutuCikmaSuresi);

                if (!mermiKutusuVarMi)
                {
                    int randomSayi = Random.Range(0, 5);
                    Instantiate(MermiKutusu, MermiKutusuNoktasi[randomSayi].transform.position, MermiKutusuNoktasi[randomSayi].transform.rotation);
                    mermiKutusuVarMi = true;
                }
            }
        }
    }
}
