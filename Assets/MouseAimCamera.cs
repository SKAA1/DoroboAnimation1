using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAimCamera : MonoBehaviour
{
    public Transform target;  // The character you want to follow
    public float smoothSpeed = 5.0f;  // Adjust the smoothness of the camera movement
    public Vector3 offset = new Vector3(0, 2, -5);  // Adjust the camera position relative to the character

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}