using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    public Transform target;  // The character you want to follow
    public float rotationSpeed = 2.0f;
    public float minYRotation = -60f; // Minimum Y rotation angle
    public float maxYRotation = 60f;  // Maximum Y rotation angle
    public float minDistance = 2.0f;  // Minimum distance from the player
    public float maxDistance = 10.0f; // Maximum distance from the player
    public float minCameraHeight = 1.0f; // Minimum height of the camera above the ground
    public LayerMask groundLayer;  // Layer mask for the ground

    void LateUpdate()
    {
        if (target == null)
            return;

        RotateAroundTarget();
        AdjustDistance();
        LookAtTarget();
        ClampCameraHeight();
    }

    void RotateAroundTarget()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;

        // Rotate the camera around the target
        transform.RotateAround(target.position, Vector3.up, mouseX);

        // Clamp the rotation to prevent flipping
        Vector3 eulerAngles = transform.eulerAngles;
        eulerAngles.x = Mathf.Clamp(eulerAngles.x, minYRotation, maxYRotation);
        transform.eulerAngles = eulerAngles;
    }

    void AdjustDistance()
    {
        // Calculate the current distance between the camera and the player
        float currentDistance = Vector3.Distance(transform.position, target.position);

        // Adjust the distance based on mouse scroll input
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        float newDistance = Mathf.Clamp(currentDistance - scrollWheel, minDistance, maxDistance);

        // Update the camera position
        transform.position = target.position - transform.forward * newDistance;
    }

    void LookAtTarget()
    {
        // Ensure the camera looks at the character
        transform.LookAt(target);
    }

    void ClampCameraHeight()
    {
        // Raycast to find the ground position
        RaycastHit hit;
        if (Physics.Raycast(target.position, -Vector3.up, out hit, Mathf.Infinity, groundLayer))
        {
            // Adjust the camera's height above the ground
            float distanceToGround = hit.distance;
            float heightAboveGround = Mathf.Max(transform.position.y - hit.point.y, minCameraHeight);

            // Update the camera position
            transform.position = new Vector3(transform.position.x, heightAboveGround + hit.point.y, transform.position.z);
        }
    }
}
