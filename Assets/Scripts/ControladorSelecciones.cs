using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorSelecciones : MonoBehaviour
{
    [SerializeField] private Image[] images;
    [SerializeField] private int escena;
    private string [] urls;

    
    private NetworkManager m_networkmanager = null;

    
    public void ImagenesFinales(){

        if (images.Length < 1)
        {
            Debug.LogError("El arreglo de imagenes está vacío.");
            return;
        }

        if(escena==1)
        {
            urls = new string[images.Length];

            for (int i = 0; i < urls.Length; i++)
            {
                urls[i] = PlayerPrefs.GetString("seleccionrespuesta"+i.ToString());
                Debug.Log(urls[i]);
                m_networkmanager.GetImage(urls[i], images[i]);
            }
        }
        else
        {
            urls = new string[images.Length];
            urls[0] = PlayerPrefs.GetString("seleccionrespuesta4");
            urls[1] = PlayerPrefs.GetString("seleccionrespuesta5");
            Debug.Log("URLS ARBOLES");
            Debug.Log(urls[0]);
            Debug.Log(urls[1]);
            m_networkmanager.GetImage(urls[0], images[0]);
            m_networkmanager.GetImage(urls[1], images[1]);
        }
    }
    
    void Awake()
    {
        m_networkmanager = GameObject.FindObjectOfType<NetworkManager> ();
        ImagenesFinales();
    }
    
}
