using UnityEngine;
using UnityEngine.Rendering;

public class ControlaInimigo : MonoBehaviour, IMatavel
{
    [SerializeField] private AudioClip somDeMorte;
    [SerializeField] private GameObject kitMedicoPrefab;
    [SerializeField] private GameObject particulaDeSangue;

    private GameObject jogador;
    private Rigidbody fisicaDoZumbi;
    private Movimentacao movimentaInimigo;
    private AnimacaoPersonagem animacaoInimigo;
    private Status statusDoInimigo;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float contadorVagar;
    private float tempoEntrePosicoesAleatorias = 4;
    private float chanceDeDroparKitMedico = 0.1f;
    private ControlaInterface scriptControlaInterface;
    public GeradorDeZumbis meuGerador;

    private void Awake()
    {
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        fisicaDoZumbi = GetComponent<Rigidbody>();
        movimentaInimigo = GetComponent<Movimentacao>();
        statusDoInimigo = GetComponent<Status>();
    }

    private void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
        scriptControlaInterface = GameObject.FindFirstObjectByType(typeof(ControlaInterface)) as ControlaInterface;
        AleatorizarZumbis();
    }

    private void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, jogador.transform.position);

        if (distancia > 15)
        {
            Vagar();
        }
        else if (distancia > 2.5f)
        {
            direcao = (jogador.transform.position - transform.position).normalized;
            movimentaInimigo.Movimentar(direcao, statusDoInimigo.velocidade);
            animacaoInimigo.Atacar(false);
        }
        else
        {
            direcao = Vector3.zero;
            fisicaDoZumbi.linearVelocity = new Vector3(0, fisicaDoZumbi.linearVelocity.y, 0);
            animacaoInimigo.Atacar(true);
        }

        if (direcao != Vector3.zero)
        {
            movimentaInimigo.Rotacionar(direcao);
        }

        if (direcao == Vector3.zero)
        {
            Vector3 rotacaoAtual = transform.rotation.eulerAngles;

            transform.rotation = Quaternion.Euler(0, rotacaoAtual.y, 0);

            fisicaDoZumbi.angularVelocity = Vector3.zero;
        }

        animacaoInimigo.Movimentar(direcao.magnitude);
    }

    private void Vagar()
    {
        contadorVagar -= Time.fixedDeltaTime;

        if (contadorVagar <= 0)
        {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar = tempoEntrePosicoesAleatorias + Random.Range(-1f, 1f);
        }

        float distanciaAtePonto = Vector3.Distance(transform.position, posicaoAleatoria);

        if (distanciaAtePonto > 0.6f)
        {
            direcao = (posicaoAleatoria - transform.position).normalized;
            movimentaInimigo.Movimentar(direcao, statusDoInimigo.velocidade);
        }
        else
        {
            direcao = Vector3.zero;
            fisicaDoZumbi.linearVelocity = new Vector3(0, fisicaDoZumbi.linearVelocity.y, 0);
        }
    }

    private Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * 10;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }

    private void AtacaJogador()
    {
        float distanciaAtual = Vector3.Distance(transform.position, jogador.transform.position);

        if (distanciaAtual <= 2.5f)
        {
            int dano = Random.Range(20, 30);
            jogador.GetComponent<ControlaJogador>().TomarDano(dano);
        }
    }

    private void AleatorizarZumbis()
    {
        int geraTipoZumbi = Random.Range(1, transform.childCount);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    public void TomarDano(int dano)
    {
        statusDoInimigo.vida -= dano;


        if (statusDoInimigo.vida <= 0)
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
        Destroy(gameObject, 2);
        animacaoInimigo.Morrer();
        movimentaInimigo.Morrer();
        ControlaAudio.instancia.PlayOneShot(somDeMorte);
        enabled = false; 
        VerificarGeracaoDoKitMedico(chanceDeDroparKitMedico);
        scriptControlaInterface.AtualizarQuantidadeDeZumbiMortos();
        meuGerador.DiminuirQuantidadeDeZumbisVivos();
    }

    private void VerificarGeracaoDoKitMedico(float chanceDeDrop)
    {
        if (Random.value <= chanceDeDrop)
        {
            Instantiate(kitMedicoPrefab, transform.position, Quaternion.identity);
        }
    }
}