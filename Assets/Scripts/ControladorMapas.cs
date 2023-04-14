using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ControladorMapas : MonoBehaviour
{
    static ControladorMapas current;

    [SerializeField] private TMP_Text contadorItems;
    [SerializeField] private TMP_Text contadorEstrellas;
    // [SerializeField] private TMP_Text contadorCerezas;
    [SerializeField] private TMP_Text mensajeFinal;

    [SerializeField] private ControladorEscenas controladorEscena;
    [SerializeField] private string mensajeSiguienteEscena;
    [SerializeField] private string nombreSiguienteEscena;
    [SerializeField] private string numeroMapa;

    private int items;
    private int estrellas;

    public static void SumaItem()
    {
        current.items++;
        if(current.items < 10) current.contadorItems.text = "0" + current.items;
        else current.contadorItems.text = current.items.ToString();
    }

    public static void SumaEstrella()
    {
        current.estrellas++;
        if(current.estrellas < 10) current.contadorEstrellas.text = "0" + current.estrellas;
        else current.contadorEstrellas.text = current.estrellas.ToString();
    }

    public  void MensajeFinal()
    {
        current.mensajeFinal.text = "Fin del Juego ";
    }

    public  void GuardarDatos()
    {
        string nombre1  = "items" + numeroMapa;
        string nombre2  = "estrellas" + numeroMapa;
        PlayerPrefs.SetInt(nombre1 , current.items);
        PlayerPrefs.SetInt(nombre2 , current.estrellas);       
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Jugador")
        {
            MensajeFinal();
            GuardarDatos();
            StartCoroutine(CambioEscena());
        }
    }


    private IEnumerator CambioEscena()
    {
        controladorEscena.MensajeEscena(mensajeSiguienteEscena);
        yield return new WaitForSeconds(3);
        controladorEscena.OpenScene(nombreSiguienteEscena);
    }

    private void Awake()
    {
        if(current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }
        current = this;
    }


}
