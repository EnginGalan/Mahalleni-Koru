using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    Animator animator;

    [Header("Ayarlar")]
    public bool atesEdebilirmi;
    float İceridenAtesEtmeSikligi;
    public float disaridanAtesEtmeSikligi;
    public float menzil;
    public GameObject Cross;
    public GameObject Scope;
    [Header("Sesler")]
    public AudioSource AtesSesi;
    public AudioSource SarjorSesi;
    public AudioSource MermiBitisSesi;
    public AudioSource MermiAlmaSesi;
    [Header("Efektler")]
    public ParticleSystem AtesEfekt;
    public ParticleSystem mermiIzi;
    public ParticleSystem kanEfekti;
    [Header("Digerleri")]
    public Camera benimCam;
    float camFieldPov;
    public float yaklasmaPov;
    [Header("Sİlah Ayarları")]
    public string silahinAdi;
    int toplamMermiSayisi;
    public int SarjorKapasitesi;
    int kalanMermi;
    public TextMeshProUGUI toplamMermi_Text;
    public TextMeshProUGUI kalanMermi_Text;
    public float darbeGucu;
    public bool kovanCiksinMi;
    public GameObject kovanCikisNoktasi;
    public GameObject kovanObjesi;

    public MermiKutusuOlustur mermiKutusuOlustur;
    void Start()
    {
        toplamMermiSayisi = PlayerPrefs.GetInt(silahinAdi + "Mermi");
        PlayerPrefs.SetInt("kalanMermi", SarjorKapasitesi);
        kalanMermi = PlayerPrefs.GetInt("kalanMermi"); 
        kovanCiksinMi = true;
        BaslangicMermiDoldur();
        SarjorDoldurmaTeknikFonksiyon("NormalYaz");
        animator = GetComponent<Animator>();
        camFieldPov = benimCam.fieldOfView;
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (atesEdebilirmi && Time.time > İceridenAtesEtmeSikligi && kalanMermi != 0)
            {
                AtesEt();
                İceridenAtesEtmeSikligi = Time.time + disaridanAtesEtmeSikligi;
            }
            if (kalanMermi == 0)
            {
                if (!MermiBitisSesi.isPlaying)
                    MermiBitisSesi.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (kalanMermi < SarjorKapasitesi && toplamMermiSayisi != 0)
                animator.Play("SarjorDegistirme");
        }

        if (Input.GetKey(KeyCode.E))
        {
            MermiAl();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Zoom(true);
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Zoom(false);
        }
    }
    IEnumerator KameraTitreme(float titremeSuresi, float magnitude)
    {
        Vector3 originalPozisyon = benimCam.transform.localPosition;
        float gecenSure = 0f;
        while (gecenSure < titremeSuresi)
        {
            float x = Random.Range(-1f, 1) * magnitude;
            benimCam.transform.localPosition = new Vector3(x, originalPozisyon.y, originalPozisyon.x);
            gecenSure += Time.time;
            yield return null;
        }
        benimCam.transform.localPosition = originalPozisyon;
    }
    void Zoom(bool durum)
    {
        if (durum)
        {
            Cross.SetActive(false);
            benimCam.cullingMask = ~(1 << 7);
            animator.SetBool("zoom", durum);
            benimCam.fieldOfView = yaklasmaPov;
            Scope.SetActive(true);
        }
        else
        {
            Scope.SetActive(false);
            benimCam.cullingMask = -1;
            animator.SetBool("zoom", durum);
            benimCam.fieldOfView = camFieldPov;
            Cross.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mermi"))
        {
            MermiKaydet(other.transform.gameObject.GetComponent<MermiKutusu>().olusanSilahinTuru, other.transform.gameObject.GetComponent<MermiKutusu>().olusanMermiSayisi);
            mermiKutusuOlustur.NoktalariKaldir(other.transform.gameObject.GetComponent<MermiKutusu>().noktasi);
            Destroy(other.transform.gameObject);
        }

        if (other.gameObject.CompareTag("CanKutusu"))
        {
            mermiKutusuOlustur.GetComponent<GameControl>().SaglikDoldur();
            CanKutusuOlustur.canKutusuVarMi = false;
            Destroy(other.transform.gameObject);
        }
    }

    void SarjorDegistirme()
    {
        SarjorSesi.Play();
        if (kalanMermi < SarjorKapasitesi && toplamMermiSayisi != 0)
        {
            if (kalanMermi != 0)
            {
                SarjorDoldurmaTeknikFonksiyon("MermiVar");
            }
            else
            {
                SarjorDoldurmaTeknikFonksiyon("MermiYok");
            }

        }
    }

    void AtesEt()
    {
        AtesEtmeTeknikİslemleri();
        RaycastHit hit;

        if (Physics.Raycast(benimCam.transform.position, benimCam.transform.forward, out hit, menzil))
        {
            if (hit.transform.gameObject.CompareTag("Dusman"))
            {
                Instantiate(kanEfekti, hit.point, Quaternion.LookRotation(hit.normal));
                hit.transform.gameObject.GetComponent<Dusman>().DarbeAl(darbeGucu);
            }
            else if (hit.transform.gameObject.CompareTag("DevrilebilirObje"))
            {
                Rigidbody rg = hit.transform.GetComponent<Rigidbody>();
                rg.AddForce(-hit.normal * 50f);
                Instantiate(mermiIzi, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else
                Instantiate(mermiIzi, hit.point, Quaternion.LookRotation(hit.normal));
        }
        StartCoroutine(KameraTitreme(.1f, .2f));
    }
    void MermiAl()
    {
        RaycastHit hit;
        if (Physics.Raycast(benimCam.transform.position, benimCam.transform.forward, out hit, 4))
        {
            if (hit.transform.gameObject.CompareTag("Mermi"))
            {
                MermiKaydet(hit.transform.gameObject.GetComponent<MermiKutusu>().olusanSilahinTuru, hit.transform.gameObject.GetComponent<MermiKutusu>().olusanMermiSayisi);
                mermiKutusuOlustur.NoktalariKaldir(hit.transform.gameObject.GetComponent<MermiKutusu>().noktasi);
                Destroy(hit.transform.gameObject);
            }
        }
    }
    void BaslangicMermiDoldur()
    {
        if (toplamMermiSayisi <= SarjorKapasitesi)
        {
            int olusanToplamDeger = toplamMermiSayisi + kalanMermi;
            if (olusanToplamDeger > SarjorKapasitesi)
            {
                kalanMermi = SarjorKapasitesi;
                PlayerPrefs.SetInt("kalanMermi", kalanMermi);
                toplamMermiSayisi = olusanToplamDeger - SarjorKapasitesi;
                PlayerPrefs.SetInt(silahinAdi + "Mermi", toplamMermiSayisi);
            }
            else
            {
                kalanMermi += toplamMermiSayisi;
                PlayerPrefs.SetInt("kalanMermi", kalanMermi);
                toplamMermiSayisi = 0;
                PlayerPrefs.SetInt(silahinAdi + "Mermi", 0);
            }
            
        }
        else
        {
            toplamMermiSayisi -= SarjorKapasitesi - kalanMermi;
            kalanMermi = SarjorKapasitesi;
            PlayerPrefs.SetInt("kalanMermi", kalanMermi);
            PlayerPrefs.SetInt(silahinAdi + "Mermi", toplamMermiSayisi);
        }
    }
    void SarjorDoldurmaTeknikFonksiyon(string tur)
    {
        switch (tur)
        {
            case "MermiVar":
                if (toplamMermiSayisi <= SarjorKapasitesi)
                {
                    int olusanToplamDeger = kalanMermi + toplamMermiSayisi;
                    if (olusanToplamDeger > SarjorKapasitesi)
                    {
                        kalanMermi = SarjorKapasitesi;
                        PlayerPrefs.SetInt("kalanMermi", kalanMermi);
                        toplamMermiSayisi = olusanToplamDeger - SarjorKapasitesi;
                        PlayerPrefs.SetInt(silahinAdi + "Mermi", toplamMermiSayisi);
                    }
                    else
                    {
                        kalanMermi += toplamMermiSayisi;
                        PlayerPrefs.SetInt("kalanMermi", kalanMermi);
                        toplamMermiSayisi = 0;
                        PlayerPrefs.SetInt(silahinAdi + "Mermi", 0);

                    }
                }
                else
                {
                    toplamMermiSayisi -= SarjorKapasitesi - kalanMermi;
                    kalanMermi = SarjorKapasitesi;
                    PlayerPrefs.SetInt("kalanMermi", kalanMermi);
                    PlayerPrefs.SetInt(silahinAdi + "Mermi", toplamMermiSayisi);

                }
                toplamMermi_Text.text = toplamMermiSayisi.ToString();
                kalanMermi_Text.text = kalanMermi.ToString();
                break;

            case "MermiYok":
                if (toplamMermiSayisi <= SarjorKapasitesi)
                {
                    kalanMermi = toplamMermiSayisi;
                    toplamMermiSayisi = 0;
                    PlayerPrefs.SetInt("kalanMermi",kalanMermi);
                    PlayerPrefs.SetInt(silahinAdi + "Mermi", 0);

                }
                else
                {
                    toplamMermiSayisi -= SarjorKapasitesi;
                    kalanMermi = SarjorKapasitesi;
                    PlayerPrefs.SetInt("kalanMermi", kalanMermi);
                    PlayerPrefs.SetInt(silahinAdi + "Mermi", toplamMermiSayisi);
                }
                toplamMermi_Text.text = toplamMermiSayisi.ToString();
                kalanMermi_Text.text = kalanMermi.ToString();
                break;

            case "NormalYaz":
                toplamMermi_Text.text = toplamMermiSayisi.ToString();
                kalanMermi_Text.text = kalanMermi.ToString();
                break;
        }
    }
    void KovanCikartma()
    {
        if (kovanCiksinMi)
        {
            GameObject obje = Instantiate(kovanObjesi, kovanCikisNoktasi.transform.position, kovanCikisNoktasi.transform.rotation);
            Rigidbody rb = obje.GetComponent<Rigidbody>();
            rb.AddRelativeForce(new Vector3(-8f, 2, 0) * 30);
        }
    }
    void AtesEtmeTeknikİslemleri()
    {
        kalanMermi--;
        PlayerPrefs.SetInt(silahinAdi + "Mermi", toplamMermiSayisi - SarjorKapasitesi + kalanMermi);
        kalanMermi_Text.text = kalanMermi.ToString();
        animator.Play("AtesEt");
        AtesSesi.Play();
        AtesEfekt.Play();
    }
    void MermiKaydet(string silahTuru, int mermiSayisi)
    {
        MermiAlmaSesi.Play();
        switch (silahTuru)
        {
            case "Taramali":
                PlayerPrefs.SetInt("taramaliMermi", PlayerPrefs.GetInt("taramaliMermi") + mermiSayisi);
                break;

            case "Pompali":
                PlayerPrefs.SetInt("pompaliMermi", PlayerPrefs.GetInt("pompaliMermi") + mermiSayisi);
                break;

            case "Magnum":
                PlayerPrefs.SetInt("magnumMermi", PlayerPrefs.GetInt("magnumMermi") + mermiSayisi);
                break;

            case "Sniper":
                toplamMermiSayisi += mermiSayisi;
                PlayerPrefs.SetInt(silahinAdi + "Mermi", toplamMermiSayisi);
                SarjorDoldurmaTeknikFonksiyon("NormalYaz");
                break;
        }
    }
}
