using UnityEngine;
using UnityEngine.UI;

public class DebugTeleport : MonoBehaviour
{
    public Transform[] pontosDeTeleporte; // Locais de teleporte
    public Button[] botoes; // Referência aos botões

    private void Start()
    {
        for (int i = 0; i < botoes.Length; i++)
        {
            int index = i; // Necessário para evitar referência incorreta na lambda
            botoes[i].onClick.AddListener(() => Teleportar(index));
        }
    }

    private void Teleportar(int index)
    {
        if (index < pontosDeTeleporte.Length) // Garante que o índice é válido
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player)
            {
                player.transform.position = pontosDeTeleporte[index].position;
            }
        }
    }
}
