using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    private float minX = 0, maxX = 206;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    private void Update()
    {
        CameraPosition();
    }
    private void CameraPosition()
    {
        if (player != null)
        {
            Vector3 cameraPosition = transform.position;
            cameraPosition.x = player.position.x;
            if (cameraPosition.x < minX) cameraPosition.x = 0;
            if (cameraPosition.x > maxX) cameraPosition.x = maxX;
            transform.position = cameraPosition;
        }
    }
}
