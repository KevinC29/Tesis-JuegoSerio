using System.Collections;
using UnityEngine;
using System;
using System.Net;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;
using System.Collections.Generic;



public class NetworkManager : MonoBehaviour
{       
    private static string uri_global = "https://apiseriusgame-production.up.railway.app/api/1.0";
    private string uri_login = uri_global + "/student/login";
    private string uri_respuesta = uri_global + "/caso/test/student";
    private string uri_imagenes = uri_global + "/testImages";
    private string resultado; //resultado de validar el usuario
    private string usuario;
    private string resultado2; //resultado del test
    private static string[] imageURLs;
    private int contador = 0;
    private int cont = 1;
    

    
    public void CheckUser(string username, Action<Response> response){
        StartCoroutine(CO_CheckUser(username, response));
    }

    public void SendTest(string respuestas, Action<Response> response){
        StartCoroutine(CO_SendTest(respuestas, response));
    }

    public void GetImage(string imageURL, Image buttonImages){
        StartCoroutine(CO_GetImages(imageURL, buttonImages));
    }

    public void GetImages(Image[] buttonImages, int id_escena){
        StartCoroutine(CO_GetImageURLs(buttonImages, id_escena));
    }

    private IEnumerator CO_CheckUser(string username, Action<Response> response){
        Debug.Log("Usuario network " + username);

        UserLogin user = new UserLogin();
        user.passwordTemporaly = username;

        string userlogin = JsonUtility.ToJson(user);
        Debug.Log("Usuario login " + userlogin);

        var request = new UnityWebRequest(uri_login, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(userlogin);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
        Debug.Log(request.downloadHandler.text);

        if (request.result == UnityWebRequest.Result.ConnectionError) 
        {
            Debug.Log(request.error);
            Conexion resp = new Conexion();
            resp.message = "failed";
            resultado = JsonUtility.ToJson(resp);
            Debug.Log(resultado);
            response(JsonUtility.FromJson<Response>(resultado));
        } 
        else 
        {
            Debug.Log("resultado1" + request.result);
            resultado = request.downloadHandler.text;
            ResponseData responseData = JsonUtility.FromJson<ResponseData>(resultado);
            usuario = responseData.data;
            GetUser();
            Debug.Log("resultado1" + resultado);
            response(JsonUtility.FromJson<Response>(resultado));

        }
    }

    private IEnumerator CO_SendTest(string respuestas, Action<Response> response)
    {
        // Crear una nueva solicitud
        Debug.Log("Respuestas Network" + respuestas);
        var request = new UnityWebRequest(uri_respuesta, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(respuestas);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        Conexion resp = new Conexion();
        // Comprobar si se ha producido un error
        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(request.error);
            resp.message = "failed";
            resultado2 = JsonUtility.ToJson(resp);
            response(JsonUtility.FromJson<Response>(resultado2));
        }
        else
        {
            resultado2 = request.downloadHandler.text;
            Debug.Log("RESULTADO DE ENVIO DE DATOS>" + resultado2);
            Debug.Log("JSON enviado exitosamente");
            response(JsonUtility.FromJson<Response>(resultado2));
        }
    }

    private IEnumerator CO_GetImages(string imageURL, Image buttonImage)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(imageURL))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(webRequest.error);
                Debug.Log("error de conexion");
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);
                buttonImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }
    }

    private IEnumerator CO_GetImageURLs(Image[] buttonImages, int id_escena)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri_imagenes))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                resultado = webRequest.downloadHandler.text;
                imageURLs  = ExtractImageLinks(resultado);

                if (id_escena == 1)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        StartCoroutine(CO_GetImages(imageURLs[i], buttonImages[contador]));
                        contador++;
                    }
                    
                }
                else if (id_escena == 2)
                {
                    for (int i = 12; i < 18; i++)
                    {
                        StartCoroutine(CO_GetImages(imageURLs[i], buttonImages[contador]));
                        contador++;
                    }
                }
                
            }
        }
    }

    public string[] ExtractImageLinks(string json)
    {
        Debug.Log(json);
        string id_img = "imagen";
        string name_url = "link";
        string valor_image = "valorImagen";
        
        string ruta;
        var imageLinks = new List<string>();
        var data = JsonUtility.FromJson<Data>(json);
        foreach (var item in data.data)
        {
            ruta = uri_global+item.link;
            PlayerPrefs.SetString(id_img + cont.ToString(), item._id);
            PlayerPrefs.SetString(name_url + cont.ToString(), ruta);
            Debug.Log(ruta);
            PlayerPrefs.SetInt(valor_image + cont.ToString(), item.value);
            imageLinks.Add(ruta);
            cont++;
        }
        return imageLinks.ToArray();
    }


    public void GetUser()
    {
        PlayerPrefs.SetString("estudiante", usuario);
    }

    

}

[Serializable] 
public class Conexion{
    public string message;
}


[Serializable] 
public class Response{
    public bool done = false;
    public string message = "";
}

[Serializable]
public class Data
{
    public List<ImageData> data;
}

[Serializable]
public class ImageData
{
    public string _id;
    public string name;
    public string link;
    public int value;
    public int section;
    public string createdAt;
    public string updatedAt;
}

[Serializable]
public class UserLogin
{
    public string passwordTemporaly;
}

[Serializable]
public class ResponseData
{
    public string message;
    public string data;
}