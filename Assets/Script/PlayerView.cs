using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public float mouseSemsitive;
    public Transform PlayerBody;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock Currsor;
        
    }

    private void CameraRotation()
    {

    }
}
