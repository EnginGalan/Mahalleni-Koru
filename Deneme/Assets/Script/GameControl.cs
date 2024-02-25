using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    int aktifSira;
    float health = 100;
    [Header("SAÐLIK AYARLARI")]
    public Image HealthBar;
    [Header("SÝLAH AYARLARI")]
    public GameObject[] silahlar;
    public AudioSource degisimSesi;
    [Header("DÜÞMAN AYARLARI")]
    public GameObject[] Dusmanlar;
    public GameObject[] CikisNoktalari;
    public GameObject[] HedefNoktalar;
    public float dusmanCikmaSuresi;
    public int baslangicDusmanSayisi;
    public static int kalanDusmanSayisi;
    public TextMeshProUGUI kalanDusmanText;
    [Header("DÝÐER AYARLAR")]
    public GameObject GameOverCanvas;
    public GameObject KazandinCanvas;
    // Start is called before the first frame update
    void Start()
    {
        kalanDusmanText.text = baslangicDusmanSayisi.ToString();
        kalanDusmanSayisi = baslangicDusmanSayisi;
        if (!PlayerPrefs.HasKey("oyunBasladiMi"))
        {
            PlayerPrefs.SetInt("taramaliMermi", 70);
            PlayerPrefs.SetInt("magnumMermi", 70);
            PlayerPrefs.SetInt("pompaliMermi", 70);
            PlayerPrefs.SetInt("sniperMermi", 70);
            PlayerPrefs.SetInt("kalanMermi", 30);
            PlayerPrefs.SetInt("oyunBasladiMi", 1);
        }
        StartCoroutine(DusmanCikar());
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
    IEnumerator DusmanCikar()
    {
        while (true)
        {
            yield return new WaitForSeconds(dusmanCikmaSuresi);
            if (baslangicDusmanSayisi != 0)
            {
                int dusman = Random.Range(0, 5);
                int cikisNoktasi = Random.Range(0, 2);
                int hedefNoktasi = Random.Range(0, 2);
                GameObject Obje = Instantiate(Dusmanlar[dusman], CikisNoktalari[cikisNoktasi].transform.position, Quaternion.identity);
                Obje.GetComponent<Dusman>().HedefBelirle(HedefNoktalar[hedefNoktasi]);
                baslangicDusmanSayisi--;
            }
        }
    }

    public void DusmanSayisiGuncelle()
    {
        kalanDusmanSayisi--;
        kalanDusmanText.text = kalanDusmanSayisi.ToString();
        if (kalanDusmanSayisi == 0)
        {
            Kazandin();
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
    public void DarbeAl(float darbeGucu)
    {
        health-=darbeGucu;
        HealthBar.fillAmount = health / 100;
        if (health <= 0)
        {
            GameOver();
        }
    }

    public void SaglikDoldur()
    {
        health = 100;
        HealthBar.fillAmount= health / 100;
    }

    void Kazandin()
    {
        KazandinCanvas.SetActive(true);
        Time.timeScale = 0;
    }
    void GameOver()
    {
        GameOverCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void BastanBasla()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
