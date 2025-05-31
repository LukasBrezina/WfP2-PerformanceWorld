using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackFPS : MonoBehaviour
{
    private List<float> allFPS = new List<float>();
    private bool isRecording = false;

    // when pressure plate collides with "Player" then FPS recording is started
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isRecording)
        {
            StartCoroutine(RecordFPS());
        }
    }

    private IEnumerator RecordFPS()
    {
        // measuring 30 seconds and storing FPS in list
        isRecording = true;
        float recordingDuration = 30f;
        float elapsedTime = 0f;

        Debug.Log("Measuring START.");

        while (elapsedTime < recordingDuration)
        {
            float currentFPS = 1f / Time.unscaledDeltaTime;
            allFPS.Add(currentFPS);
            yield return new WaitForSeconds(1f);
            elapsedTime += 1f;
        }

        Debug.Log("Measuring DONE.");
        AnalyzeFPS();
    }

    private void AnalyzeFPS()
    {
        // print highest, lowest and average FPS in console
        if (allFPS.Count == 0)
        {
            Debug.LogWarning("No data");
            return;
        }

        float highest = Mathf.Max(allFPS.ToArray());
        float lowest = Mathf.Min(allFPS.ToArray());
        float average = 0f;

        foreach (float fps in allFPS)
        {
            average += fps;
        }
        average /= allFPS.Count;

        Debug.Log($"Measuring:\nHöchste FPS: {highest:F1}\nNiedrigste FPS: {lowest:F1}\nDurchschnittliche FPS: {average:F1}");
    }
}
