using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class keles : MonoBehaviour
{
    Animator animator;

    [Header("Ayarlar")]
    public bool atesEdebilirmi;
    float İceridenAtesEtmeSikligi;
    public float disaridanAtesEtmeSikligi;
    public float menzil;
    [Header("Sesler")]
    public AudioSource AtesSesi;
    public AudioSource SarjorSesi;
    [Header("Efektler")]
    public ParticleSystem AtesEfekt;
    public ParticleSystem mermiIzi;
    public ParticleSystem kanEfekti;
    [Header("Digerleri")]
    public Camera benimCam;
    [Header("Sİlah Ayarları")]
    public int toplamMermiSayisi;
    public int SarjorKapasitesi;
    public int kalanMermi;
    public TextMeshProUGUI toplamMermi_Text;
    public TextMeshProUGUI kalanMermi_Text;

    void Start()
    {
        toplamMermi_Text.text = toplamMermiSayisi.ToString();
        kalanMermi_Text.text = kalanMermi.ToString();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Mouse0) && atesEdebilirmi && Time.time > İceridenAtesEtmeSikligi && kalanMermi!=0)
        {
            AtesEt();
            İceridenAtesEtmeSikligi = Time.time + disaridanAtesEtmeSikligi;
        }

        if (Input.GetKey(KeyCode.R))
        {
            animator.Play("SarjorDegistirme");
        }
    }

    void SarjorDegistirme()
    {
        SarjorSesi.Play();

    }

    void AtesEt()
    {
        kalanMermi--;
        kalanMermi_Text.text=kalanMermi.ToString();

        animator.Play("AtesEt");
        AtesSesi.Play();
        AtesEfekt.Play();

        RaycastHit hit;

        if (Physics.Raycast(benimCam.transform.position,benimCam.transform.forward, out hit, menzil))
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
}
