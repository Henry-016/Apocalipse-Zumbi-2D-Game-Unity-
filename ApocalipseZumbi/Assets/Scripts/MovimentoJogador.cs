using UnityEngine;

public class MovimentoJogador : Movimentacao
{
    public void RotacaoJogador(LayerMask mascaraDoChao)
    {
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit impacto;

        if (Physics.Raycast(raio, out impacto, 100, mascaraDoChao))
        {
            Vector3 posicaoMiraDoJogador = impacto.point - transform.position;

            posicaoMiraDoJogador.y = 0;

            if (posicaoMiraDoJogador.sqrMagnitude > 0.1f)
            {
                Rotacionar(posicaoMiraDoJogador);
            }
        }
    }
}
