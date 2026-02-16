using UnityEngine;

public class KitMedico : MonoBehaviour
{
    private int quantidadeDeCura = 15;
    private int tempoDeDestruicao = 10;

    private void Start()
    {
        Destroy(gameObject, tempoDeDestruicao);
    }

    private void OnTriggerEnter(Collider objetoDeColisao)
    {
        if (objetoDeColisao.tag == "Jogador")
        {
            if (objetoDeColisao.GetComponent<ControlaJogador>().statusDoJogador.vida != objetoDeColisao.GetComponent<ControlaJogador>().statusDoJogador.vidaInicial)
            {
                objetoDeColisao.GetComponent<ControlaJogador>().CurarVida(quantidadeDeCura);
                Destroy(gameObject);
            }
        }
    }
}
