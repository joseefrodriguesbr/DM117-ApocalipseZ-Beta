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

        // Adiciona uma força para o projétil se mover para frente
        rb.linearVelocity = transform.forward * velocidade;

        // Destroi o projetil após o tempo de vida
        Destroy(gameObject, tempoDeVida);
    }

    void OnTriggerEnter(Collider other)
    {
        // Se a bala atingir um objeto com a tag "Adversario"
        if (other.gameObject.CompareTag("Adversario"))
        {            
            Debug.Log("Você atingiu o adversário!");
            explosion.Play();
            rb.linearVelocity = Vector3.zero;

            // Destrói o objeto após a animação de explosão
            Destroy(gameObject, 2);
        }
    }
}
