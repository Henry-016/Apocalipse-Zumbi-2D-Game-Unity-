using UnityEngine;
using System.Collections;

public class ControlaArma : MonoBehaviour
{
    [SerializeField] private GameObject bala;
    [SerializeField] private GameObject posicaoDoCanoDaArma;
    [SerializeField] private AudioClip somDeDisparo;
    [SerializeField] private AudioClip somSemMunicao;
    [SerializeField] private AudioClip somDeRecarregar;
    [SerializeField] private Texture2D miraTexture;

    private ControlaInterface scriptControlaInterface;
    private int quantidadeMaximaPermitidaDeBalas = 10;
    private int quantidadeDeBalasAtualNoPente;
    private bool estaRecarregando = false;

    private void Start()
    {
        ColocarCursorBonito();

        quantidadeDeBalasAtualNoPente = quantidadeMaximaPermitidaDeBalas;
        scriptControlaInterface = GameObject.FindFirstObjectByType(typeof(ControlaInterface)) as ControlaInterface;
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            ColocarCursorPadrao();
            return;
        }

        if (estaRecarregando == true) return;

        if (Input.GetButtonDown("Fire1"))
        {
            if (quantidadeDeBalasAtualNoPente > 0)
            {
                Atirar();
            }

            else
            {
                ControlaAudio.instancia.PlayOneShot(somSemMunicao);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (quantidadeDeBalasAtualNoPente < quantidadeMaximaPermitidaDeBalas)
            {
                StartCoroutine(RecarregarArma());
            }
        }
    }

    public void ColocarCursorBonito()
    {
        Vector2 hotspot = new Vector2(miraTexture.width / 2, miraTexture.height / 2);

        Cursor.SetCursor(miraTexture, hotspot, CursorMode.Auto);
    }

    public void ColocarCursorPadrao()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Atirar()
    {
        Instantiate(bala, posicaoDoCanoDaArma.transform.position, posicaoDoCanoDaArma.transform.rotation);
        ControlaAudio.instancia.PlayOneShot(somDeDisparo);
        quantidadeDeBalasAtualNoPente--;
        scriptControlaInterface.AtualizarQuantidadeDeBalasAtuais(quantidadeDeBalasAtualNoPente);
    }

    private IEnumerator RecarregarArma()
    {
        estaRecarregando = true;

        ControlaAudio.instancia.PlayOneShot(somDeRecarregar);

        yield return new WaitForSeconds(somDeRecarregar.length);

        quantidadeDeBalasAtualNoPente = quantidadeMaximaPermitidaDeBalas;

        scriptControlaInterface.AtualizarQuantidadeDeBalasAtuais(quantidadeDeBalasAtualNoPente);
        estaRecarregando = false;
    }
}
