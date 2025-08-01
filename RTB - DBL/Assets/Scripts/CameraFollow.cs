using UnityEngine;

public class CameraFollowRotation : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 5f, -8f);  // Position behind and above the player
    public float tiltAngle = 20f;                     // Look-down tilt (X-axis)

    void LateUpdate()
    {
        // Position the camera behind the player based on their rotation
        Vector3 desiredPosition = player.position + player.TransformDirection(offset);
        transform.position = desiredPosition;

        // Make the camera look at the player with a downward tilt
        Quaternion lookRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Euler(tiltAngle, lookRotation.eulerAngles.y, 0);
    }
}