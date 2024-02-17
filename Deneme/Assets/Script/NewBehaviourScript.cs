using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public bool atesEdilebilirlik;
    public float atisSikligi;
    float icerAtisSikligi;
    public float menzil;
    public AudioSource ses;
    public AudioSource SarjorSesi;
    public AudioSource MermiBitisSesi;
    public Camera cam;
    public ParticleSystem ps;
    public ParticleSystem mermiIzi;
    public ParticleSystem kanEfekti;
    Animator animator;
    public int kalanMermiSayisi;
    public int toplamMermiSayisi;
    public int sarjorKapasitesi;
    TextMeshProUGUI kalanMermiSayisi_Text;
    TextMeshProUGUI toplamMermiSayisi_Text;

    private void Start()
    {
        kalanMermiSayisi = sarjorKapasitesi;

        kalanMermiSayisi_Text.text = kalanMermiSayisi.ToString();
        toplamMermiSayisi_Text.text = toplamMermiSayisi.ToString();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) )
        {
            if (atesEdilebilirlik && Time.time > icerAtisSikligi && kalanMermiSayisi != 0)
            {
                atesEt();
                icerAtisSikligi = Time.time + atisSikligi;
            }
            if(kalanMermiSayisi==0)
            {
                if(!MermiBitisSesi.isPlaying)
                    MermiBitisSesi.Play();
            }                       
            
        }
        if (Input.GetKey(KeyCode.R))
        {
            StartCoroutine(ReloadYap());
        }

    }
    IEnumerator ReloadYap()
    {
        if(kalanMermiSayisi<sarjorKapasitesi&&toplamMermiSayisi!=0)
            animator.Play("SarjorDegistirme");
        yield return new WaitForSeconds(1.8f);
        if(kalanMermiSayisi<sarjorKapasitesi&&toplamMermiSayisi!=0)
        {
            if(kalanMermiSayisi!=0)
            {
                SarjorDoldurmaTeknikFonksiyon("MermiVar");
            }
            else
            {
                SarjorDoldurmaTeknikFonksiyon("MermiYok");
            }
        }
    }
    void SarjorDoldurmaTeknikFonksiyon(string tur)
    {
        switch(tur)
        {
            case "MermiVar":
                if (toplamMermiSayisi<=sarjorKapasitesi)
                {
                    int olusanToplamDeger = kalanMermiSayisi + toplamMermiSayisi;
                    if (olusanToplamDeger > sarjorKapasitesi)
                    {
                        kalanMermiSayisi = sarjorKapasitesi;
                        toplamMermiSayisi = olusanToplamDeger - sarjorKapasitesi;
                    }
                    else
                    {
                        kalanMermiSayisi += toplamMermiSayisi;
                        toplamMermiSayisi = 0;
                    }
                }
                else
                {
                    toplamMermiSayisi -= sarjorKapasitesi - kalanMermiSayisi;
                    kalanMermiSayisi = sarjorKapasitesi;
                }
                toplamMermiSayisi_Text.text = toplamMermiSayisi.ToString();
                kalanMermiSayisi_Text.text = kalanMermiSayisi.ToString();
                break;
            case "MermiYok":
                if (toplamMermiSayisi <= sarjorKapasitesi)
                {
                    kalanMermiSayisi=toplamMermiSayisi;
                    toplamMermiSayisi = 0;
                }
                else
                {
                    toplamMermiSayisi -= sarjorKapasitesi;
                    kalanMermiSayisi = sarjorKapasitesi;
                }      
                break;
        }
    }

    void SarjorSesiAktif()
    {
        SarjorSesi.Play();
    }
    void atesEt()
    {
        kalanMermiSayisi--;
        kalanMermiSayisi_Text.text = kalanMermiSayisi.ToString();
        animator.Play("AtesEt");
        ses.Play();
        ps.Play();
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, menzil))
        {
            if (hit.transform.gameObject.CompareTag("Dusman"))
                Instantiate(kanEfekti, hit.point, Quaternion.LookRotation(hit.normal));
            else if (hit.transform.gameObject.CompareTag("DevrilebilirObje"))
            {
                Rigidbody rg = hit.transform.GetComponent<Rigidbody>();
                rg.AddForce(-hit.normal * 50f);
                Instantiate(mermiIzi, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else
                Instantiate(mermiIzi, hit.point, Quaternion.LookRotation(hit.normal));

                Debug.Log(hit.transform.name);
        }
    }
    int[] mermiSayisi =
    {
        10,
        20,
        30,
        40
    };
    string[] silahTurleri =
    {
        "pompali",
        "taramali",
        "sniper",
        "magnum"
    };
    public int olusanMermiSayisi;
    public List<Sprite> silahResimleri = new();
    public Image silah;
    void basla()
    {
        int gelenAnahtar=Random.Range(0,silahTurleri.Length);
        silah.sprite = silahResimleri[gelenAnahtar];
        
    }
}

