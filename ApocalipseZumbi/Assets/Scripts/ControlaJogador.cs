using UnityEngine;

public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel
{
    [SerializeField] private LayerMask mascaraDoChao;
    [SerializeField] public GameObject textoGameOver;
    [SerializeField] private ControlaInterface scriptControlaInterface;
    [SerializeField] private AudioClip somDeDano;
    [SerializeField] private AudioClip somAoPegarKit;
    [SerializeField] private GameObject localDoSangue;
    [SerializeField] private GameObject particulaDeSangue;

    [HideInInspector] public Status statusDoJogador;

    private Vector3 direcao;
    private Movimentacao movimentaPersonagem;
    private MovimentoJogador meuMovimentoJogador;
    private AnimacaoPersonagem animacaoJogador;

    private void Awake()
    {
        movimentaPersonagem = GetComponent<Movimentacao>(); 
        meuMovimentoJogador = GetComponent<MovimentoJogador>();
        animacaoJogador = GetComponent <AnimacaoPersonagem>();
        statusDoJogador = GetComponent<Status>();
    }

    private void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ).normalized;

        animacaoJogador.Movimentar(direcao.magnitude);  
    }

    private void FixedUpdate()
    {
        movimentaPersonagem.Movimentar(direcao, statusDoJogador.velocidade);

        meuMovimentoJogador.RotacaoJogador(mascaraDoChao);
    }

    public void TomarDano(int dano)
    {
        statusDoJogador.vida -= dano;
        scriptControlaInterface.AtualizarSliderDeVida();

        Vector3 posicaoDoSangue = transform.position + (Vector3.up * 1.5f) + (transform.right * 0.5f);
        Instantiate(particulaDeSangue, localDoSangue.transform.position, localDoSangue.transform.rotation);

        ControlaAudio.instancia.PlayOneShot(somDeDano);

        if (statusDoJogador.vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        scriptControlaInterface.GameOver();
    }

    public void CurarVida(int quantidadeDeCura)
    {
        statusDoJogador.vida += quantidadeDeCura;

        if (statusDoJogador.vida > statusDoJogador.vidaInicial)
        {
            statusDoJogador.vida = statusDoJogador.vidaInicial;
        }

        scriptControlaInterface.AtualizarSliderDeVida();
        ControlaAudio.instancia.PlayOneShot(somAoPegarKit);
    }
}