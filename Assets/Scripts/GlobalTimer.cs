using UnityEngine;
using TMPro;

public class GlobalTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText; // referência ao objeto Text que exibirá o contador na tela
    private float startTimeRealtime; // variável para armazenar o tempo de início da contagem
    private float elapsedTimeRealtime; // variável para armazenar o tempo decorrido desde o início da contagem

    private void Start()
    {
        Time.timeScale = 10.0f;
        startTimeRealtime = Time.time; // armazena o tempo de início da contagem
    }

    private void Update()
    {
        float elapsedTimeRealtime = Time.realtimeSinceStartup - startTimeRealtime; // calcula o tempo decorrido em tempo real
        displayText.text = elapsedTimeRealtime.ToString("F1") + " segundos"; // atualiza o texto da UI com o novo valor do contador e do tempo decorrido
    }
}
