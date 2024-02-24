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
    void Start()
    {
        
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
        Destroy(gameObject) ;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hedef"))
        {
            GameObject anaKontrol = GameObject.FindWithTag ("GameController");
            anaKontrol.GetComponent<GameControl>().DarbeAl(dusmanDarbeGucu);
            Oldun();
        }
    }
}
