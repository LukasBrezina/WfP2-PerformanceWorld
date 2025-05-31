using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPathWalker : MonoBehaviour
{
    public List<Transform> waypoints;
    public float moveSpeed = 3f;     
    public float reachThreshold = 5f;
    public KeyCode stopKey = KeyCode.LeftShift;

    private bool isWalking = false;

    private Vector3 fixLookingAtDestination;
    private Coroutine walkCoroutine;

    public LockCamera lockCamera;

    void Update()
    {
        if (Input.GetKeyDown(stopKey))
        {
            StopWalking();
        }
    }


    // follow path, slowly moving towards each object (position) in list, but not used for measuring
    private IEnumerator FollowPath()
    {
        foreach (Transform targetWaypoint in waypoints)
        {

            fixLookingAtDestination = new Vector3(targetWaypoint.position.x, -4.5f, targetWaypoint.position.z);
            // create temp object to fix position where player is looking
            GameObject temp = new GameObject("FixLookingWhileWalking");
            temp.transform.position = fixLookingAtDestination;

            lockCamera.destination = temp.transform;

            while (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                                     new Vector3(targetWaypoint.position.x, 0, targetWaypoint.position.z)) > reachThreshold)
            {

                transform.position = Vector3.MoveTowards(transform.position,
                                                         new Vector3(targetWaypoint.position.x, transform.position.y, targetWaypoint.position.z),
                                                         moveSpeed * Time.deltaTime);

                yield return null;
            }

            Destroy(temp);

            yield return new WaitForSeconds(0.1f);
        }

        isWalking = false;
        walkCoroutine = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PressurePlate") && !isWalking)
        {
            StartWalking();
        }
    }

    private void StartWalking()
    {
        if (isWalking) return;
        isWalking = true;
        lockCamera.isLocked = true;

        walkCoroutine = StartCoroutine(FollowPath());
    }

    private void StopWalking()
    {
        if (!isWalking) return;

        isWalking = false;
        lockCamera.isLocked = false;

        if (walkCoroutine != null)
        {
            StopCoroutine(walkCoroutine);
            walkCoroutine = null;
        }
    }
}
