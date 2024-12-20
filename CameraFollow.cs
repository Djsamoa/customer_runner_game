using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // Reference to the player's transform

    [Header("Camera Offset")]
    public Vector3 offset = new Vector3(0, 2, -10); // Offset relative to the player

    [Header("Smooth Settings")]
    public float smoothSpeed = 0.125f; // How smoothly the camera follows

    void LateUpdate()
    {
        if (target != null)
        {
            // Desired position based on the target's position and offset
            Vector3 desiredPosition = target.position + offset;

            // Smoothly interpolate the camera's position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Set the camera's position
            transform.position = smoothedPosition;
        }
    }
}