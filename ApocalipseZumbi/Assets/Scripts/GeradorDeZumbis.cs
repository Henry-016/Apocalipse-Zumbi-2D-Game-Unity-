using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class GeradorDeZumbis : MonoBehaviour
{
    [SerializeField] private GameObject Zumbi;
    [SerializeField] private float tempoParaGerarZumbi = 1;
    [SerializeField] private LayerMask LayerZumbi;
    [SerializeField] private GameObject Jogador;

    private float contadorDeTempo;
    private float distanciaDeGeracao = 3;
    private float distanciaDoJogadorParaGeracao = 20;
    private int quantidadeMaximaDeZumbisVivos = 2;
    private int quantidadeDeZumbisVivosAtualmente;
    private float tempoDoProximoAumentoDeDificuldade = 15;
    private float contadorDeAumentarDificuldade;

    private void Start()
    {
        contadorDeAumentarDificuldade = tempoDoProximoAumentoDeDificuldade;

        for (int i = 0; i < quantidadeMaximaDeZumbisVivos; i++)
        {
            StartCoroutine(GerarUmNovoZumbi());
        }
    }

    private void Update()
    {
        bool isPossoGerarZumbiPelaDistancia = Vector3.Distance(transform.position, Jogador.transform.position) > distanciaDoJogadorParaGeracao;

        if (isPossoGerarZumbiPelaDistancia == true && (quantidadeDeZumbisVivosAtualmente < quantidadeMaximaDeZumbisVivos))
        {
            contadorDeTempo += Time.deltaTime;

            if (contadorDeTempo >= tempoParaGerarZumbi)
            {
                StartCoroutine(GerarUmNovoZumbi());
                contadorDeTempo = 0;
            }
        }

        if (Time.timeSinceLevelLoad > contadorDeAumentarDificuldade)
        {
            quantidadeMaximaDeZumbisVivos++;
            contadorDeAumentarDificuldade = Time.timeSinceLevelLoad + tempoDoProximoAumentoDeDificuldade;
        }
    }

    private IEnumerator GerarUmNovoZumbi()
    {
        Vector3 posicaoDeCriacao = AleatorizarPosicao();
        Collider[] colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);

        int tentativasMaximas = 10;
        int tentativasAtuais = 0;

        while (colisores.Length > 0 && tentativasAtuais < tentativasMaximas)
        {
            posicaoDeCriacao = AleatorizarPosicao();
            colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi); 
            tentativasAtuais++;

            if (tentativasAtuais >= tentativasMaximas)
                yield break;

            yield return null;
        }

        ControlaInimigo zumbi = Instantiate(Zumbi, posicaoDeCriacao, transform.rotation).GetComponent<ControlaInimigo>();
        zumbi.meuGerador = this;
        quantidadeDeZumbisVivosAtualmente++;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaDeGeracao);
    }

    private Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * distanciaDeGeracao;
        posicao += transform.position;
        posicao.y = 0.5f;

        return posicao;
    }

    public void DiminuirQuantidadeDeZumbisVivos()
    {
        quantidadeDeZumbisVivosAtualmente--;
    }
}
