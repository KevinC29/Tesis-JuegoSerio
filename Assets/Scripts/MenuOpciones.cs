using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuOpciones : MonoBehaviour
{

    [SerializeField] private Slider sliderAudio;
    [SerializeField] private float sliderValueAudio;
    [SerializeField] private Image imagenMute;


    void Start()
    {      
        PlayerPrefs.SetFloat("volumenAudio", 0.5f);
        sliderAudio.value = PlayerPrefs.GetFloat("volumenAudio");
        AudioListener.volume = sliderAudio.value;
        CheckMute();
    }


    public void ChangeSliderAudio(float valor)
    {
        sliderValueAudio = valor;
        PlayerPrefs.SetFloat("volumenAudio", sliderValueAudio);
        AudioListener.volume = sliderAudio.value;
        CheckMute();
    }


    public void CheckMute()
    {
        if (sliderValueAudio == 0)
        {
            imagenMute.enabled = true;
        }
        else
        {
            imagenMute.enabled = false;
        }
    }

    public void UserGuide()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void Return()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
