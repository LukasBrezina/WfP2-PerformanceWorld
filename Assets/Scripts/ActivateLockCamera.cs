using UnityEngine;

public class ActivateLockCamera : MonoBehaviour
{
    public LockCamera lockCamera;
    public Transform destination; 
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {

        // Debug.Log("Hallo");
        // lock camera to position
        if (other.CompareTag(playerTag) && lockCamera != null)
        {
            lockCamera.destination = destination;
            lockCamera.EnableLock(other.transform);
        }
    }
}
