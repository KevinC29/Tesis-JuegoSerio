using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorBarra : MonoBehaviour
{
    public TextMeshProUGUI textProgess;
    public Slider sliderProgress;
    public float currentPercent;
    public TMP_Text textMessage;
    private AsyncOperation loadAsync;
    [SerializeField] private GameObject continuar;
    [SerializeField] private GameObject cerrar;
    private bool stopUpdate = true;

    private float contador = 0;

    void Start()
    {
        textMessage.text = PlayerPrefs.GetString("MensajeEscena");
        StartCoroutine(LoadScene());
        continuar.SetActive(false);
        sliderProgress.interactable = false;
    }

    public void OnButtonClick(GameObject cargarEscena)
    {
        cerrar.SetActive(false);
        cargarEscena.SetActive(true);
    }

    private IEnumerator LoadScene ()
    {
        textProgess.text = "Cargando... 00%";

        while(contador <= 100)
        {
            currentPercent = contador+1 * 100 /0.9f;
            textProgess.text = "Cargando... " + sliderProgress.value.ToString("00")+"%";
            yield return null;
        }
    }

    private void Update()
    {
        if (stopUpdate)
        {
            sliderProgress.value = Mathf.MoveTowards(sliderProgress.value, currentPercent, 10 * Time.deltaTime);

            if( currentPercent >= 100 && sliderProgress.value == 100)
            {
                stopUpdate = false;
                continuar.SetActive(true);
            }
        }    
    }
}
