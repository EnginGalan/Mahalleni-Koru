using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dusman : MonoBehaviour
{
    NavMeshAgent Ajan;
    GameObject Hedef;
    public float health;
    public float dusmanDarbeGucu;
    GameObject anaKontrol;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        anaKontrol = GameObject.FindWithTag("GameController");
        Ajan = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Ajan.SetDestination(Hedef.transform.position);
    }
    public void HedefBelirle(GameObject objem)
    {
        Hedef=objem;
    }

    public void DarbeAl(float darbeGucu)
    {
        health-=darbeGucu;
        if(health <= 0)
        {
            Oldun();
        }
    }
    void Oldun()
    {
        animator.SetTrigger("Olme");
        Destroy(gameObject,5f) ;
        anaKontrol.GetComponent<GameControl>().DusmanSayisiGuncelle();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hedef"))
        {
            anaKontrol.GetComponent<GameControl>().DarbeAl(dusmanDarbeGucu);
            Oldun();
        }
    }
}
