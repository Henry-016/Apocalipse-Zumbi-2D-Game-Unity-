using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControlaInterface : MonoBehaviour
{
    [SerializeField] private Slider sliderDaVidaDoJogador;
    [SerializeField] private GameObject painelDeGameOver;
    [SerializeField] private Text textoDoTempoDeSobrevivencia;
    [SerializeField] private Text textoDoTempoDeSobrevivenciaRecorde;
    [SerializeField] private Text textoDaQuantidadeDeZumbisMortos;
    [SerializeField] public Text textoChefeApareceu;
    [SerializeField] public Text textoDaMunicao;
    [SerializeField] private AudioClip somDeGameOver;
    [SerializeField] private GameObject telaDePause;

    private ControlaJogador scriptControlaJogador;
    private float tempoPontuacaoSalva;
    private int quantidadeDeZumbisMortos;
    private ControlaArma scriptControlaArma;

    private void Start()
    {
        Time.timeScale = 1;
        scriptControlaArma = GameObject.FindAnyObjectByType<ControlaArma>();
        scriptControlaJogador = GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>();
        sliderDaVidaDoJogador.maxValue = scriptControlaJogador.statusDoJogador.vida;
        AtualizarSliderDeVida();

        tempoPontuacaoSalva = PlayerPrefs.GetFloat("Recorde");
        AtualizarTextoRecorde(tempoPontuacaoSalva);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PausarJogo();
        }
    }

    public void PausarJogo()
    {
        Time.timeScale = 0;
        telaDePause.SetActive(true);
        ControlaAudio.instancia.PausarTrilhaSonora(false);
    }

    public void DespausarJogo()
    {
        Time.timeScale = 1;
        telaDePause.SetActive(false);
        ControlaAudio.instancia.PausarTrilhaSonora(true);

        scriptControlaArma.ColocarCursorBonito();
    }

    public void FecharJogo()
    {
        Application.Quit();
    }

    public void AtualizarSliderDeVida()
    {
        sliderDaVidaDoJogador.value = scriptControlaJogador.statusDoJogador.vida;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        painelDeGameOver.SetActive(true);

        ControlaAudio.instancia.TocarSomGameOver(somDeGameOver);

        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        int segundos = (int)(Time.timeSinceLevelLoad % 60);

        textoDoTempoDeSobrevivencia.text = $"Você Sobreviveu por {minutos}min e {segundos}s";

        AjustarPontuacaoMaxima();
    }

    public void ReiniciarJogo()
    {
        string nomeDaCenaAtual = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nomeDaCenaAtual);
    }

    private void AjustarPontuacaoMaxima()
    {
        if (Time.timeSinceLevelLoad > tempoPontuacaoSalva)
        {
            tempoPontuacaoSalva = Time.timeSinceLevelLoad;
            PlayerPrefs.SetFloat("Recorde", tempoPontuacaoSalva);
            AtualizarTextoRecorde(tempoPontuacaoSalva);
        }
    }
    private void AtualizarTextoRecorde(float tempo)
    {
        int min = (int)(tempo / 60);
        int seg = (int)(tempo % 60);
        textoDoTempoDeSobrevivenciaRecorde.text = $"Seu Melhor Recorde foi: {min}min e {seg}s";
    }

    public void AtualizarQuantidadeDeZumbiMortos()
    {
        quantidadeDeZumbisMortos++;
        textoDaQuantidadeDeZumbisMortos.text = $" x {quantidadeDeZumbisMortos}";
    }

    public void AtualizarQuantidadeDeBalasAtuais(int municaoAtual)
    {
        textoDaMunicao.text = $"X {municaoAtual}/10";
    }


    public void AvisoQueChefeApareceu()
    {
        StartCoroutine(DesaparecerTexto(1, textoChefeApareceu));
    }

    public IEnumerator DesaparecerTexto(float tempoDeDuracao, Text textoASumir)
    {
        textoChefeApareceu.gameObject.SetActive(true);
        Color corTexto = textoASumir.color;
        corTexto.a = 1;
        textoASumir.color = corTexto;

        yield return new WaitForSeconds(1);

        float contador = 0;

        while (textoASumir.color.a > 0)
        {
            contador += Time.deltaTime / tempoDeDuracao;
            corTexto.a = Mathf.Lerp(1, 0, contador);
            textoASumir.color = corTexto;

            if (textoASumir.color.a <= 0)
            {
                textoASumir.gameObject.SetActive(false);
            }

            yield return null;
        }
    }
}
