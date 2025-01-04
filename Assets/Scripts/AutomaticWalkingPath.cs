using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPathWalker : MonoBehaviour
{
    public List<Transform> waypoints;
    public float moveSpeed = 100f;     
    public float reachThreshold = 0.1f;
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


    // leider noch verbuggt, prinzipiell geht es aber der Player schaut am Ende immer nach oben :/
    private IEnumerator FollowPath()
    {
        foreach (Transform targetWaypoint in waypoints)
        {

            fixLookingAtDestination = new Vector3(targetWaypoint.position.x, 1.312f, targetWaypoint.position.z);
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
        if (isWalking) return; // Verhindern, dass erneut gestartet wird
        isWalking = true;
        lockCamera.isLocked = true;

        // Starte die Bewegung als Coroutine
        walkCoroutine = StartCoroutine(FollowPath());
    }

    private void StopWalking()
    {
        if (!isWalking) return;

        isWalking = false;
        lockCamera.isLocked = false;

        // Coroutine abbrechen, wenn sie läuft
        if (walkCoroutine != null)
        {
            StopCoroutine(walkCoroutine);
            walkCoroutine = null;
        }
    }
}
