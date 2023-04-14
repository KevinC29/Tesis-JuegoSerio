using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorItem : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Jugador")
        {
            Debug.Log("Item recolectado");
            ControladorMapas.SumaItem();
            Destroy(gameObject);
        }
    }
}
