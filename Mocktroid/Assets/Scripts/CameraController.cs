using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f; // The bigger this value, the quicker the camera snaps to the target. The slower it is, the longer it takes. Place between 0 and 1.
    public Vector3 offset; // If target is set to not be the player, change offset X to be 0!

    // We're using LateUpdate as the target's position is usually modified in an update function, and if we were using Update the camera movement may be jittery.
    // Once LateUpdate is called, the target will have done all of its movement.
    void LateUpdate()
    {
        if (!target)
            return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // TODO: Try smoothdamp and maybe turn on rigidbody interpolation for the character?
        transform.position = smoothedPosition;
    }
}
