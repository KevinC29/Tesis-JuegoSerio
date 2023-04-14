using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorEscenas : MonoBehaviour
{   

    private AsyncOperation loadAsync;
    private string mensaje;

    public void MensajeEscena(string mensajeEscena)
    {
        PlayerPrefs.SetString("MensajeEscena", mensajeEscena);
    }

    public void OpenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnButtonClick(GameObject cargarEscena)
    {
        cargarEscena.SetActive(true);
    }

}
