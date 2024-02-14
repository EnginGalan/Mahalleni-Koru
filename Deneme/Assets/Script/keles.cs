using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keles : MonoBehaviour
{
    public bool atesEdebilirmi;
    float İceridenAtesEtmeSikligi;
    public float disaridanAtesEtmeSikligi;
    public float menzil;
    public Camera benimCam;
    public AudioSource AtesSesi;
    public AudioSource SarjorSesi;
    public ParticleSystem AtesEfekt;
    public ParticleSystem mermiIzi;
    public ParticleSystem kanEfekti;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Mouse0) && atesEdebilirmi && Time.time > İceridenAtesEtmeSikligi)
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
