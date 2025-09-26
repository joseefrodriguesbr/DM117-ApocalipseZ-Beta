using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    // Variáveis de Saúde e UI
    [SerializeField] int healthPoints = 100;
    [SerializeField] Slider healthBar;
    Animator animator;

    // --- Variáveis para suporte à perseguição pelo zumbi ---
    public Transform player;
    public float visionRadius = 10f;
    public float wanderRadius = 5f;
    public float chaseSpeed = 3.5f;
    public float wanderSpeed = 1.5f;

    // Variáveis para ataque e empurrão
    public float attackRadius = 0.8f;
    public float knockbackForce = 10f;
    private float attackCooldown = 1.5f;
    private float nextAttackTime;

    // Comportamento inicial do zumbi
    public bool iniciaEmMovimento = true;

    private Rigidbody rb;
    private Vector3 wanderTarget;
    private bool isIdle = false; // Estado para controle interno

    [Header("Ataque")]
    public int dano = 10;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (healthBar != null)
        {
            healthBar.maxValue = healthPoints;
            healthBar.value = healthPoints;
        }

        animator.SetInteger("healthPoints", healthPoints);

        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Lógica para iniciar em movimento ou em Idle
        if (iniciaEmMovimento)
        {
            GetNewWanderTarget();
            isIdle = false;
        }
        else
        {
            isIdle = true;
        }
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        Vector3 moveDirection;
        float currentSpeed;
        Vector3 targetPosition;

        // Transição do estado Idle para perseguição
        if (isIdle)
        {
            moveDirection = Vector3.zero;
            currentSpeed = 0f;
            targetPosition = transform.position;

            if (distanceToPlayer <= visionRadius)
            {
                isIdle = false; // Sai do estado Idle
            }
        }
        // Lógica de ataque ou perseguição/perambulação
        else if (distanceToPlayer <= attackRadius)
        {
            moveDirection = Vector3.zero;
            currentSpeed = 0f;
            animator.SetBool("IsAttacking", true);
            targetPosition = player.position;

            if (Time.time >= nextAttackTime)
            {
                Debug.Log("Zumbi atacando!");
                Rigidbody playerRb = player.GetComponent<Rigidbody>();
                if (playerRb != null)
                {
                    Debug.Log("Empurrão do player!");
                    Vector3 knockbackDirection = (player.position - transform.position).normalized;
                    playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);

                    PlayerController playerController = playerRb.GetComponent<PlayerController>();

                    if (playerController != null)
                    {
                        // Causa dano ao jogador
                        Debug.Log("Incrementa dano!");
                        playerController.ReceberDano(dano);
                    }

                }
                nextAttackTime = Time.time + attackCooldown;
            }
        }
        else if (distanceToPlayer <= visionRadius)
        {
            animator.SetBool("IsAttacking", false);
            targetPosition = player.position;
            moveDirection = (targetPosition - rb.position).normalized;
            currentSpeed = chaseSpeed;
        }
        else
        {
            animator.SetBool("IsAttacking", false);
            targetPosition = wanderTarget;
            moveDirection = (targetPosition - rb.position).normalized;
            currentSpeed = wanderSpeed;

            if (Vector3.Distance(transform.position, wanderTarget) < 1f)
            {
                GetNewWanderTarget();
            }
        }

        Vector3 lookTarget = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        transform.LookAt(lookTarget);

        moveDirection.y = 0;
        rb.linearVelocity = moveDirection * currentSpeed;

        float movementSpeed = rb.linearVelocity.magnitude;
        animator.SetFloat("Speed", movementSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisão física detectada!");
    }

    void TakeDamage(int damage)
    {
        healthPoints -= damage;

        if (healthBar != null)
        {
            healthBar.value = healthPoints;
        }

        animator.SetInteger("healthPoints", healthPoints);

        if (healthPoints <= 0)
        {
            
            Invoke(nameof(SelfDestroy), 2);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projetil"))
        {
            Debug.Log($"{gameObject.name} colisão realizada.");
            TakeDamage(30);
        }
    }

    void SelfDestroy()
    {
        Destroy(gameObject);

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Pega a instância do PlayerController
            PlayerController playerController = player.GetComponent<PlayerController>();

            // Chama o método para adicionar na contagem de zumbis eliminados
            if (playerController != null)
            {               
                playerController.AdicionarZumbiEliminado();
            }
        }
    }

    private void GetNewWanderTarget()
    {
        // Determinar perambulação
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        wanderTarget = transform.position + randomDirection;
    }
}