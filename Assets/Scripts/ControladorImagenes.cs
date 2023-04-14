using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// using System.Net;
using UnityEngine.UI;
// using UnityEngine.Networking;
// using Newtonsoft.Json;
// using UnityEngine.SceneManagement;


public class ControladorImagenes : MonoBehaviour
{

    [SerializeField] private Button[] buttons;
    private Image[] images;


    public NetworkManager m_networkmanager;


    public void GetButtonImages()
    {
        // Verificar que el tamaño del arreglo de botones sea mayor que cero
        if (buttons.Length < 1)
        {
            Debug.LogError("El arreglo de botones está vacío.");
            return;
        }

        // Inicializar el arreglo de imágenes
        images = new Image[buttons.Length];

        // Obtener las imágenes correspondientes a cada botón
        for (int i = 0; i < buttons.Length; i++)
        {
            images[i] = buttons[i].GetComponent<Image>();
        }
    }

    private void Awake()
    {
        GetButtonImages();
    }

    private void Start()
    {
        int escena = images.Length;

        if(escena == 12)
        {
            m_networkmanager.GetImages(images, 1); 
        }
        else if(escena == 6)
        {
            m_networkmanager.GetImages(images, 2); 
        }
        // else
        // {
        //     m_networkmanager.GetImages(images, 3); 
        // }
    }
}
