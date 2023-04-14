using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorAudio : MonoBehaviour
{
    private float valor;

    void Awake()
    {      
        Volumen();   
    }

    public void Volumen()
    {      
        valor = PlayerPrefs.GetFloat("volumenAudio");
        AudioListener.volume = valor;    
    }

    // void Start()
    // {      
        
    // }
}
