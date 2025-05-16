using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    public Material material;
    public float rotationSpeed = 0.5f;  // Vitesse de rotation
    public float inwardSpeed = 0.1f;    // Vitesse d’aspiration vers le centre

    private float angle = 0f;
    private Vector2 scrollOffset = Vector2.zero;

    void Update()
    {
        // Rotation
        angle += rotationSpeed * Time.deltaTime;

        // Calcul de l'offset de rotation
        float x = Mathf.Cos(angle) * 0.05f;
        float y = Mathf.Sin(angle) * 0.05f;

        scrollOffset += new Vector2(x, y) * inwardSpeed * Time.deltaTime;

        // Appliquer l'offset en boucle
        material.SetTextureOffset("_MainTex", scrollOffset);
        material.SetTextureOffset("_EmissionMap", scrollOffset);
    }
}