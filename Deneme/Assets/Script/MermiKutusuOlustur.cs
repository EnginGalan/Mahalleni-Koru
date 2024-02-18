using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermiKutusuOlustur : MonoBehaviour
{
    public List<GameObject> MermiKutusuNoktasi;
    public GameObject MermiKutusu;
    public float kutuCikmaSuresi;
    List<int> noktalar = new();

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
            int randomSayim= Random.Range(0, 5);
            if (!noktalar.Contains(randomSayim))
            {
                noktalar.Add(randomSayim);
            }
            else
            {
                continue;
            }
            yield return new WaitForSeconds(kutuCikmaSuresi);

            GameObject obje = Instantiate(MermiKutusu, MermiKutusuNoktasi[randomSayim].transform.position, MermiKutusuNoktasi[randomSayim].transform.rotation);
            obje.transform.gameObject.GetComponent<MermiKutusu>().noktasi = randomSayim;
        }
    }
    public void NoktalariKaldir(int deger)
    {
        noktalar.Remove(deger);
    }
}
