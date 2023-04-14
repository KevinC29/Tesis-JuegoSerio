using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using TMPro;


public class ControladorPuntuaciones : MonoBehaviour
{
    // [SerializeField] private TMP_Text score_cereza = null;
    // [SerializeField] private TMP_Text score_estrella1 = null;
    [SerializeField] private TMP_Text score_diamante1 = null;
    [SerializeField] private TMP_Text score_estrella2 = null;
    // [SerializeField] private TMP_Text score_diamante2 = null;
    // [SerializeField] private TMP_Text score_estrella3 = null;

    [SerializeField] private TMP_Text texterror = null;

    private NetworkManager m_networkmanager = null;

    private int totalCerezas;
    private int totalEstrellas1;
    private int totalDiamantes1;
    private int totalEstrellas2;
    private int totalDiamantes2;
    private int totalEstrellas3;


    void Awake()
    {
        // totalCerezas = PlayerPrefs.GetInt("items1");
        // Debug.Log(totalCerezas);
        // totalEstrellas1 = PlayerPrefs.GetInt("estrellas1");
        // Debug.Log(totalEstrellas1);
        totalDiamantes1 = PlayerPrefs.GetInt("items2");
        Debug.Log(totalDiamantes1);
        totalEstrellas2 = PlayerPrefs.GetInt("estrellas2");
        Debug.Log(totalEstrellas2);
        // totalDiamantes2 = PlayerPrefs.GetInt("items3");
        // Debug.Log(totalDiamantes2);
        // totalEstrellas3 = PlayerPrefs.GetInt("estrellas3");
        // Debug.Log(totalEstrellas3);
        m_networkmanager = GameObject.FindObjectOfType<NetworkManager> ();
    }


    void Start()
    {
        GuardarDatos();
        MostrarPuntuaciones();

    }

    public void GuardarDatos()
    {
        texterror.text = "Procesando Respuestas...";

        string est = PlayerPrefs.GetString("estudiante");
        Debug.Log(est);

        List<string> resultados = new List<string>();
        List<int> valoresResultados = new List<int>();

        for (int i = 1; i <= 5; i++) 
        {
            resultados.Add(PlayerPrefs.GetString("respuesta" + i.ToString()));
            Debug.Log(PlayerPrefs.GetString("respuesta" + i.ToString()));
            valoresResultados.Add(PlayerPrefs.GetInt("valorrespuesta" + i.ToString()));
            Debug.Log(PlayerPrefs.GetString("valorrespuesta" + i.ToString()));
        }

        StudentAnswers studentAnswers = new StudentAnswers(
            est,
            resultados.Select((r, i) => new Answer(r, valoresResultados[i])).ToList()
        );

        Debug.Log(est); 
        Debug.Log(studentAnswers);


        string respuestas = JsonUtility.ToJson(studentAnswers);
        Debug.Log("Respuestas finales> " + respuestas);

        m_networkmanager.SendTest(respuestas, delegate(Response resp)
        {

        if(resp.message.Contains("failed"))
        {
            texterror.text = "Error de Conexión - Revise su Conexion a Internet";
        }
        else
        {
            if (resp.message.Contains("ok"))
            {
                StartCoroutine ("CargarEnvio");
                texterror.text = "Juego Guardado exitosamente";
            }
            else
            {
                texterror.text = "El Juego Ya Existe";
                
            }
        }
        
    }); 

    }

    private IEnumerator CargarEnvio()
    {
        texterror.text = "Guardando Puntuaciones...";
        yield return new WaitForSeconds(4);
        
    }

    private void MostrarPuntuaciones()
    {
        // if(totalCerezas < 10) score_cereza.text = "0" + totalCerezas;
        // else score_cereza.text = totalCerezas.ToString();

        // if(totalEstrellas1 < 10) score_estrella1.text = "0" + totalEstrellas1;
        // else score_estrella1.text = totalEstrellas1.ToString();

        if(totalDiamantes1 < 10) score_diamante1.text = "0" + totalDiamantes1;
        else score_diamante1.text = totalDiamantes1.ToString();

        if(totalEstrellas2 < 10) score_estrella2.text = "0" + totalEstrellas2;
        else score_estrella2.text = totalEstrellas2.ToString();

        // if(totalDiamantes2 < 10) score_diamante2.text = "0" + totalDiamantes2;
        // else score_diamante2.text = totalDiamantes2.ToString();

        // if(totalEstrellas3 < 10) score_estrella3.text = "0" + totalEstrellas3;
        // else score_estrella3.text = totalEstrellas3.ToString();
    }

    
}


[Serializable]
public class StudentAnswers
{
    public string CIstudent;
    public List<Answer> answers;

    public StudentAnswers(string ci, List<Answer> ans)
    {
        CIstudent = ci;
        answers = ans;
    }

}

[Serializable]
public class Answer
{
    public string refImages;
    public int valueAnswer;

    public Answer(string refImages, int valueAnswer)
    {
        this.refImages = refImages;
        this.valueAnswer = valueAnswer;
    }
}
