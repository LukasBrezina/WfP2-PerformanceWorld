using UnityEngine;

public class LockCamera : MonoBehaviour
{
    public Transform destination;
  
    public bool isLocked = false;
    private Transform originalTarget;

   
    public void EnableLock(Transform target)
    {
        isLocked = true;
        originalTarget = target;
    }

  
    public void DisableLock()
    {
        isLocked = false;
    }

    void LateUpdate()
    {
        if (isLocked && destination != null)
        {
            
            transform.LookAt(destination);
        }
    }

    void Update()
    {
    
        if (isLocked && Input.GetKeyDown(KeyCode.LeftShift))
        {
            DisableLock();
        }
    }
}
