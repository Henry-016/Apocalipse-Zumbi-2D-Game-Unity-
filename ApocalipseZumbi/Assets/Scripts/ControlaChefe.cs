using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ControlaChefe : MonoBehaviour, IMatavel
{
    [SerializeField] private GameObject kitMedicoPrefab;
    [SerializeField] private Slider sliderVidaChefe;
    [SerializeField] private Image imagemDoSlider;
    [SerializeField] private Color corDaVidaMaxima, corDaVidaMinima;
    [SerializeField] private GameObject particulaDeSangue;

    private Transform jogador;
    private NavMeshAgent agente;
    private Status statusChefe;
    private AnimacaoPersonagem animacaoChefe;
    private Movimentacao movimentoChefe;
    private ControlaInterface scriptControlaInterface;

    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        statusChefe = GetComponent<Status>();
        animacaoChefe = GetComponent<AnimacaoPersonagem>();
        movimentoChefe = GetComponent <Movimentacao>();
    }

    private void Start()
    {
        jogador = GameObject.FindWithTag("Jogador").transform;
        scriptControlaInterface = GameObject.FindFirstObjectByType(typeof(ControlaInterface)) as ControlaInterface;
        agente.speed = statusChefe.velocidade;
        sliderVidaChefe.maxValue = statusChefe.vidaInicial;
        AtualizarInterface();
    }

    private void Update()
    {
        agente.SetDestination(jogador.position);
        animacaoChefe.Movimentar(agente.velocity.magnitude);

        Vector3 direcao;

        if (agente.remainingDistance <= agente.stoppingDistance)
        {
            animacaoChefe.Atacar(true);
            direcao = jogador.position - transform.position;
        }
        else
        {
            animacaoChefe.Atacar(false);
            direcao = agente.steeringTarget - transform.position;
        }

        if (direcao != Vector3.zero)
        {
            movimentoChefe.Rotacionar(direcao);
        }
    }

    private void AtacaJogador()
    {
        float distanciaAtual = Vector3.Distance(transform.position, jogador.transform.position);

        if (distanciaAtual <= 5)
        {
            int dano = Random.Range(30, 40);
            jogador.GetComponent<ControlaJogador>().TomarDano(dano);
        }
    }

    public void TomarDano(int dano)
    {
        statusChefe.vida -= dano;
        AtualizarInterface();

        if (statusChefe.vida < 0)
        {
            Morrer();
        }
    }

    public void SoltarParticulaSangue(Vector3 posicao, Quaternion rotacao)
    {
        Instantiate(particulaDeSangue, posicao, rotacao);
    }

    public void Morrer()
    {
        animacaoChefe.Morrer();
        movimentoChefe.Morrer();

        scriptControlaInterface.AtualizarQuantidadeDeZumbiMortos();

        agente.isStopped = true;
        agente.enabled = false;

        this.enabled = false;

        Instantiate(kitMedicoPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject, 2f);
    }

    private void AtualizarInterface()
    {
        sliderVidaChefe.value = statusChefe.vida;
        float porcentagemDeVida = (float)statusChefe.vida / statusChefe.vidaInicial;
        Color corDaVida = Color.Lerp(corDaVidaMinima, corDaVidaMaxima, porcentagemDeVida);
        imagemDoSlider.color = corDaVida;
    }
}
