using UnityEngine;

public class ControlaCamera : MonoBehaviour
{
    [SerializeField] private GameObject jogador;

    private Vector3 distanciaCompensar;

    private void Start()
    {
        distanciaCompensar = transform.position - jogador.transform.position;
    }

    private void Update()
    {
        transform.position = jogador.transform.position + distanciaCompensar;
    }
}
