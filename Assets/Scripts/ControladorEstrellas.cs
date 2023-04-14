using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorEstrellas : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Jugador")
        {
            Debug.Log("Estrella Recolectada");
            ControladorMapas.SumaEstrella();
            Destroy(gameObject);
        }
    }
}
