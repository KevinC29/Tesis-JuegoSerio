using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Net;
using UnityEngine.Networking;
using Newtonsoft.Json;


public class LoginManager : MonoBehaviour
{
    
    [Header("Login")]
    [SerializeField] private TMP_InputField UserInput = null;
    [SerializeField] private TMP_Text texterror = null;
    private string est;
    
    
    private NetworkManager m_networkmanager = null;

    private void Awake()
    {
        m_networkmanager = GameObject.FindObjectOfType<NetworkManager> ();
    }

    private void Start()
    {
        UserInput.text = "";
    }

    public void SubmitLogin()
    {
        if (UserInput.text == "")
        {
            texterror.text = "Por favor ingrese el usuario";
            return;
        }

        texterror.text = "Procesando...";

        m_networkmanager.CheckUser(UserInput.text, delegate(Response resp)
        {

            if(resp.message.Contains("failed"))
            {
                texterror.text = "Error de Conexión - Revise su Conexion a Internet";
            }
            else
            {
                texterror.text = resp.message;
                if (resp.message.Contains("ok"))
                {
                    StartCoroutine ("CargarDatos");
                }else{
                    texterror.text = "Estudiante  No Encontrado";
                    Debug.Log("No se encontro al estudiante>"); 
                    
                }
            }
        }); 
        

    }

    private IEnumerator CargarDatos()
    {
        texterror.text = "Estudiante Encontrado";
        est = PlayerPrefs.GetString("estudiante");
        Debug.Log("ESTUDIANTE!!!>" + est);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }
}

