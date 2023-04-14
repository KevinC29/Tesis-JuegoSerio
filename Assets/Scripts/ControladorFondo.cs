using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorFondo : MonoBehaviour
{
    public Renderer fondo;

    void Update()
    {
        fondo.material.mainTextureOffset = fondo.material.mainTextureOffset + new Vector2(0.015f, 0) * Time.deltaTime;
    }
}
