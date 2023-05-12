using UnityEngine;
using TMPro;

public class CountObjectsWithTag : MonoBehaviour
{
    public string tagName = "AutonomousVehicle";
    [SerializeField] private TextMeshProUGUI displayText;

    void FixedUpdate()
    {
        int count = GameObject.FindGameObjectsWithTag(tagName).Length;
        displayText.text = count.ToString() + " carros em cena";
    }
}
