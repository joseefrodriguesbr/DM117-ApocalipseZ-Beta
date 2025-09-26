using UnityEngine;

public class ProjetilController : MonoBehaviour
{
    public float velocidade = 20f;
    public float tempoDeVida = 5f;

    [SerializeField] ParticleSystem flame;
    [SerializeField] ParticleSystem explosion;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Adiciona uma for�a para o proj�til se mover para frente
        rb.linearVelocity = transform.forward * velocidade;

        // Destroi o projetil ap�s o tempo de vida
        Destroy(gameObject, tempoDeVida);
    }

    void OnTriggerEnter(Collider other)
    {
        // Se a bala atingir um objeto com a tag "Adversario"
        if (other.gameObject.CompareTag("Adversario"))
        {            
            Debug.Log("Voc� atingiu o advers�rio!");
            explosion.Play();
            rb.linearVelocity = Vector3.zero;

            // Destr�i o objeto ap�s a anima��o de explos�o
            Destroy(gameObject, 2);
        }
    }
}
