using UnityEngine;

public class BalaMovimento : MonoBehaviour
{
    [SerializeField] private AudioClip somDeMorte;

    private Rigidbody fisicaDaBala;

    private float velocidadeDaBala = 20;

    private void Awake()
    {
        fisicaDaBala = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        fisicaDaBala.MovePosition(fisicaDaBala.transform.position + transform.forward * velocidadeDaBala * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider objetoDeColisao)
    {
        Quaternion rotacaoOpostaABala = Quaternion.LookRotation(-transform.forward);

        switch (objetoDeColisao.tag)
        {
            case "Inimigo":
            {
                ControlaInimigo inimigo = objetoDeColisao.GetComponent<ControlaInimigo>();
                inimigo.TomarDano(1);
                inimigo.SoltarParticulaSangue(transform.position, rotacaoOpostaABala);
                break;
            }

            case "ChefeDeFase":
            {
                ControlaChefe inimigoChefe = objetoDeColisao.GetComponent<ControlaChefe>();
                inimigoChefe.TomarDano(1);
                inimigoChefe.SoltarParticulaSangue(transform.position, rotacaoOpostaABala);
                break;
            }
        }

        Destroy(gameObject);
    }
}
