using UnityEngine;
using UnityEngine.Rendering;

public class Status : MonoBehaviour
{
    [SerializeField] public int vidaInicial = 100;
    [HideInInspector] public int vida = 100;
    [SerializeField] public float velocidade;

    private void Awake()
    {
        vida = vidaInicial;
    }
}
