using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookToCamera : MonoBehaviour
{
    void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }
}
