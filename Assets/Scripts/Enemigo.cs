using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private Transform[] puntosMov;
    [SerializeField] private float velocidad;
    [SerializeField] private GameObject padre;
    [SerializeField] private GameObject muerte;
    [SerializeField] private GameObject enemigo;
    [SerializeField] public bool estadoEnemigo;

    private int i = 0;

    private Vector3 escalaIni, escalaTemp;
    private float miraDer = 1;

    void Start()
    {
        escalaIni = transform.localScale;
        estadoEnemigo = true;
    }

    void Update()
    {
        Movimiento(estadoEnemigo);
        
    }

    public void Movimiento(bool estado)
    {
        if(estado == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, puntosMov[i].transform.position, velocidad * Time.deltaTime);
            if(Vector2.Distance(transform.position, puntosMov[i].transform.position) < 0.1f)
            {
                if(puntosMov[i] != puntosMov[puntosMov.Length - 1]) i++;
                else i = 0;
                miraDer = Mathf.Sign(puntosMov[i].transform.position.x - transform.position.x);
                Giro(miraDer);
            }
        }
    }

    private void Giro(float lado)
    {
        if(miraDer == -1)
        {
            escalaTemp = transform.localScale;
            escalaTemp.x = escalaTemp.x * -1;
        }
        else escalaTemp = escalaIni;

        transform.localScale = escalaTemp;
    }
    

    public void Muere()
    {
        estadoEnemigo = false;
        enemigo.gameObject.SetActive(false);
        StartCoroutine ("EsperarMuerte");
        
    }

    IEnumerator EsperarMuerte()
    {
        muerte.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        Destroy(padre);
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Jugador")
        {
            Debug.Log("Danio Hacia el Jugador");
            estadoEnemigo = false;
            StartCoroutine ("EsperarMovimiento");
        }

    }

    IEnumerator EsperarMovimiento()
    {
        yield return new WaitForSeconds(1);
        estadoEnemigo = true;
    }
}
