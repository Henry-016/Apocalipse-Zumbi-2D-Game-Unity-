using UnityEngine;

public class ComportamentoMenu : MonoBehaviour
{
    [SerializeField] private float tempoMinimo = 5f;
    [SerializeField] private float tempoMaximo = 10f;

    [SerializeField] private string[] acoesAleatorias;

    private Animator animator;
    private float temporizador;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        ResetarTemporizador();
    }

    private void Update()
    {
        temporizador -= Time.deltaTime;

        if (temporizador <= 0)
        {
            ExecutarAcao();
            ResetarTemporizador();
        }
    }

    private void ExecutarAcao()
    {
        if (acoesAleatorias.Length == 0) return;

        int indiceSorteado = Random.Range(0, acoesAleatorias.Length);

        string triggerSorteado = acoesAleatorias[indiceSorteado];

        animator.SetTrigger(triggerSorteado);
    }

    private void ResetarTemporizador()
    {
        temporizador = Random.Range(tempoMinimo, tempoMaximo);
    }
}
