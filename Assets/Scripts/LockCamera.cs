using UnityEngine;

public class LockCamera : MonoBehaviour
{
    // destination object to look at (invisible gameObject)
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

    // look at destination
    void LateUpdate()
    {
        if (isLocked && destination != null)
        {
            
            transform.LookAt(destination);
        }
    }

    // exit measuring state with SHIFT
    void Update()
    {
    
        if (isLocked && Input.GetKeyDown(KeyCode.LeftShift))
        {
            DisableLock();
        }
    }
}
