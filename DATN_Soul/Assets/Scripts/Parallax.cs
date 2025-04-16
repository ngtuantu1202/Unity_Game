using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Material material;
    [SerializeField] private float parallaxFactorX = 0.01f; 
    [SerializeField] private float parallaxFactorY = 0.01f; 
    private Vector2 offset;
    [SerializeField] private Transform cameraTransform; 

    private Vector3 lastCameraPosition;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        lastCameraPosition = cameraTransform.position;
    }

    void Update()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        offset.x += deltaMovement.x * parallaxFactorX;
        offset.y += deltaMovement.y * parallaxFactorY;

        material.SetTextureOffset("_MainTex", offset);
        lastCameraPosition = cameraTransform.position;
    }
}
