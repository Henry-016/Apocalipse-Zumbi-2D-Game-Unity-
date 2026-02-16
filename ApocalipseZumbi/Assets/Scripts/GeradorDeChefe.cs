using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GeradorDeChefe : MonoBehaviour
{
    [SerializeField] private GameObject chefePrefab;
    [SerializeField] private float tempoEntreGeracoes = 60;
    [SerializeField] private Transform[] posicoesDeCriacaoProChefe;

    private float tempoParaProximaGeracao = 0;
    private ControlaInterface scriptControlaInterface;
    private Transform posicaoDoJogador;

    private void Start()
    {
        tempoParaProximaGeracao = tempoEntreGeracoes;
        scriptControlaInterface = GameObject.FindAnyObjectByType(typeof(ControlaInterface)) as ControlaInterface;
        posicaoDoJogador = GameObject.FindWithTag("Jogador").GetComponent<Transform>();
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad >  tempoParaProximaGeracao)
        {
            Vector3 posicaoDeCriacao = CalcularPosicaoMaisDistanteDoJogador();

            Instantiate(chefePrefab, posicaoDeCriacao, Quaternion.identity);
            scriptControlaInterface.AvisoQueChefeApareceu();
            tempoParaProximaGeracao = Time.timeSinceLevelLoad + tempoEntreGeracoes;
        }
    }

    private Vector3 CalcularPosicaoMaisDistanteDoJogador()
    {
        Vector3 posicaoDeMaiorDistancia = Vector3.zero;
        float maiorDistancia = 0;

        foreach (Transform posicao in posicoesDeCriacaoProChefe)
        {
            float distanciaEntreOJogador = Vector3.Distance(posicao.position, posicaoDoJogador.position);

            if (distanciaEntreOJogador > maiorDistancia)
            {
                maiorDistancia = distanciaEntreOJogador;
                posicaoDeMaiorDistancia = posicao.position;
            }
        }

        return posicaoDeMaiorDistancia;
    }
}
