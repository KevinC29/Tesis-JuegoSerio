using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


public class ControladorRespuestas : MonoBehaviour
{
    [SerializeField] private GameObject continuar_1;
    private string imagen;
    private string url;
    private int valor;

    void Start()
    {
        continuar_1.SetActive(false);
    }

    public void OnButtonClick(int buttonValue)
    {
        int range = Mathf.CeilToInt(buttonValue / 3.0f);
        switch (range)
        {
            case 1:
                GuardarSeleccion("respuesta0", buttonValue);
                break;
            case 2:
                GuardarSeleccion("respuesta1", buttonValue);
                break;
            case 3:
                GuardarSeleccion("respuesta2", buttonValue);
                break;
            case 4:
                GuardarSeleccion("respuesta3", buttonValue);
                break;
            case 5:
                GuardarSeleccion("respuesta4", buttonValue);
                break;
            case 6:
                GuardarSeleccion("respuesta5", buttonValue);
                break;
            default:
                Debug.Log("No existe el valor seleccionado");
                break;
        }
        
    }

    private void GuardarSeleccion(string nombre, int id_image)
    {
        imagen = PlayerPrefs.GetString("imagen" + id_image.ToString());
        url = PlayerPrefs.GetString("link" + id_image.ToString());
        valor = PlayerPrefs.GetInt("valorImagen" + id_image.ToString());

        Debug.Log("IMAGEEN:: " +  imagen);

        PlayerPrefs.SetString(nombre, imagen); //Guarda el id de las imagenes
        PlayerPrefs.SetString("seleccion" + nombre, url);
        PlayerPrefs.SetInt("valor" + nombre, valor);

        Debug.Log("Button is selected " + id_image);
        continuar_1.SetActive(true);
    }
}
