using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class AgentCollider : MonoBehaviour
{
    public bool drawDebugBox = true;
    public List<GameObject> objectsInsideCollider = new List<GameObject>();
    public string tagToDetect = "AutonomousVehicle";
    public Dictionary<GameObject, float> timesInsideCollider = new Dictionary<GameObject, float>();

    public TextMeshProUGUI averageTimeText;

    public TextMeshProUGUI slowDownText;

    private int timesLateCounter = 0;

    private void OnDrawGizmosSelected()
    {
        if (drawDebugBox)
        {
            BoxCollider myCollider = GetComponent<BoxCollider>();
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(myCollider.bounds.center, myCollider.bounds.size);
        }

        foreach (GameObject gameObject in objectsInsideCollider)
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();

            if (rb == null || rb.velocity.magnitude < 0.01f)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.green;
            }

            Gizmos.DrawSphere(new Vector3(gameObject.transform.position.x, 1f, gameObject.transform.position.z), 0.1f);

            if (timesInsideCollider.ContainsKey(gameObject))
            {
                float timeInside = Time.time - timesInsideCollider[gameObject];
                string timeString = timeInside.ToString("F1");
                Vector3 textPosition = new Vector3(gameObject.transform.position.x, 1.5f, gameObject.transform.position.z);
                GUIStyle style = new GUIStyle();
                style.alignment = TextAnchor.MiddleCenter;
                style.normal.textColor = Color.black;
                Handles.Label(textPosition, timeString, style);
            }
        }
    }

    public void UpdateAverageTimeText()
    {
        float totalTime = 0;
        int count = 0;

        foreach (GameObject gameObject in objectsInsideCollider)
        {
            if (timesInsideCollider.ContainsKey(gameObject))
            {
                float timeInside = Time.time - timesInsideCollider[gameObject];
                totalTime += timeInside;
                count++;
            }
        }

        if (count > 0)
        {
            float averageTime = totalTime / count;
            averageTimeText.text = "Average time inside: " + averageTime.ToString("F1") + " seconds";

            if (averageTime > 15)
            {
                averageTimeText.color = Color.red;
                timesLateCounter++;
            }
            else
            {
                averageTimeText.color = Color.black;
            }

            slowDownText.text = "Vezes que ficou lento: " + timesLateCounter.ToString("F0");
        }
        else
        {
            averageTimeText.text = "Average time inside: N/A";
            averageTimeText.color = Color.black;
        }
    }

    private void Update()
    {
        for (int i = objectsInsideCollider.Count - 1; i >= 0; i--)
        {
            GameObject gameObject = objectsInsideCollider[i];
            if (gameObject == null)
            {
                // The game object has been destroyed, remove it from the list
                objectsInsideCollider.RemoveAt(i);
            }
        }
        UpdateAverageTimeText();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagToDetect))
        {
            objectsInsideCollider.Add(other.gameObject);
            timesInsideCollider[other.gameObject] = Time.time;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(tagToDetect))
        {
            objectsInsideCollider.Remove(other.gameObject);
            float timeEntered = timesInsideCollider[other.gameObject];
            float timeInside = Time.time - timeEntered;
            // Debug.Log(other.gameObject.name + " was inside the collider for " + timeInside + " seconds");
            timesInsideCollider.Remove(other.gameObject);
        }
    }
}