using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    protected Rigidbody meuRigidBody;
    private void Awake()
    {
        meuRigidBody = GetComponent<Rigidbody>();
    }

    public void Movimentar(Vector3 direcao, float velocidade)
    {
        meuRigidBody.linearVelocity = new Vector3(direcao.x * velocidade, meuRigidBody.linearVelocity.y, direcao.z * velocidade);
    }

    public void Rotacionar(Vector3 direcao)
    {
        direcao.y = 0;

        if (direcao != Vector3.zero)
        {
            Quaternion novaRotacao = Quaternion.LookRotation(direcao);
            meuRigidBody.MoveRotation(novaRotacao);
        }
    }

    public void Morrer()
    {
        meuRigidBody.constraints = RigidbodyConstraints.None;
        meuRigidBody.linearVelocity = Vector3.zero;
        meuRigidBody.isKinematic = false;
        GetComponent<Collider>().enabled = false;
    }
}
