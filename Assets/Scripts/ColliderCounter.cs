using UnityEngine;
using TMPro;

public class ColliderCounter : MonoBehaviour
{
    public int count = 0; // contador de objetos que passaram pelo collider
    [SerializeField] private TextMeshProUGUI displayText; // referência ao objeto Text que exibirá o contador na tela
    private float startTime; // variável para armazenar o tempo de início da contagem
    private float elapsedTime; // variável para armazenar o tempo decorrido desde o início da contagem

    private void Start()
    {
        startTime = Time.time; // armazena o tempo de início da contagem
    }

    private void OnTriggerExit(Collider other)
    {
        count++; // incrementa o contador ao detectar a colisão com outro objeto
        elapsedTime = Time.time - startTime; // calcula o tempo decorrido desde o início da contagem
        float average = count / elapsedTime * 60; // calcula a média de objetos por minuto
        displayText.text = "Carros: " + count.ToString() + "\nMédia: " + average.ToString("F2") + " por minuto" + "\nTempo decorrido: " + elapsedTime.ToString("F0") + " segundos"; ; // atualiza o texto da UI com o novo valor do contador e da média
    }
}
