using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public bool atesEdilebilirlik;
    public float atisSikligi;
    float icerAtisSikligi;
    public float menzil;
    public AudioSource ses;
    public AudioSource SarjorSesi;
    public Camera cam;
    public ParticleSystem ps;
    public ParticleSystem mermiIzi;
    public ParticleSystem kanEfekti;
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && atesEdilebilirlik && Time.time > icerAtisSikligi)
        {
            atesEt();
            icerAtisSikligi = Time.time + atisSikligi;
            
        }
        if (Input.GetKey(KeyCode.R))
            animator.Play("SarjorDegistirme");
    }

    void SarjorSesiAktif()
    {
        SarjorSesi.Play();
    }
    void atesEt()
    {
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
}
