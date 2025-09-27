using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.StandaloneInputModule;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]

    [SerializeField] float moveSpeed;
    [SerializeField] float yawSpeed;
    [SerializeField] float jumpForce;

    Rigidbody rb;

    Animator animator;

    PlayerInput playerInput;

    InputAction moveAction;
    InputAction shootAction; // Adicione uma a��o para o tiro
    InputAction jumpAction;  // Mec�nica  de pulo implementada, mas n�o relevante para o jogo

    Vector2 inputMove;

    InputAction attackAction;

    // Componentes para o sistema de tiro
    public GameObject prefabProjetil;
    public Transform pontoDeTiro;
    public float atrasoEntreTiros = 0.1f;

    // Vari�veis privadas para o tiro
    private float proximoTiro;


    [Header("Health")]
    public int saudeMaxima = 100;
    private int saudeAtual;
    public Slider sliderDeSaude; // Barra de UI

    // Painel de Game Over
    [Header("UI")]
    public GameObject painelGameOver;

    [Header("Fase Conclu�da")]
    public GameObject painelFaseConcluida;

    // --- Vari�veis de Contagem
    private int zumbisEliminados = 0;
    private int metaEliminacao = 10;
    public TextMeshProUGUI contadorZumbisTexto;

    private bool estaNoChao = true;     // Mec�nica  de pulo implementada, mas n�o relevante para o jogo
    public float fallMultiplier = 2.5f; // Multiplicador para a gravidade na queda

    [Header("Audio")] //
    public AudioClip somDeTiro;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        // Inicializa o AudioSource
        audioSource = GetComponent<AudioSource>();

        moveAction = playerInput.actions["Move"];
        shootAction = playerInput.actions["Attack"]; // Assine a a��o "Shoot"
        jumpAction = playerInput.actions["Jump"];   // Mec�nica  de pulo implementada, mas n�o relevante para o jogo

        // Inicializa a sa�de ao iniciar o jogo
        saudeAtual = saudeMaxima;

        if (sliderDeSaude != null)
        {
            sliderDeSaude.maxValue = saudeMaxima;
            sliderDeSaude.value = saudeAtual;
        }

        // Oculta o painel no in�cio
        if (painelGameOver != null)
        {
            painelGameOver.SetActive(false);
        }
        AtualizarContador();
    }

    // O OnEnable � chamado quando o objeto � ativado
    private void OnEnable()
    {
        Debug.Log("OnEnable chamado. Assinando o evento de Pulo.");
        //jumpAction.performed += ctx => Pular();
    }

    // O OnDisable � chamado quando o objeto � desativado
    private void OnDisable()
    {
        Debug.Log("OnDisable chamado. Assinando o evento de Pulo.");
        //jumpAction.performed -= ctx => Pular();
    }

    private void Pular()
    {
        // Mec�nica  de pulo implementada, mas n�o relevante para o jogo
        if (estaNoChao)
        {
            Debug.Log("Tentando pular!"); 
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            estaNoChao = false;
        }
        else
        {
            Debug.Log("N�o pode pular, n�o est� no ch�o."); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colis�o detectada com: " + collision.gameObject.name);

        // Verifica se o objeto colidido � um terreno
        if (collision.gameObject.name.Equals("Terrain")) //  (collision.gameObject.GetComponent<TerrainCollider>() != null)
        {
            estaNoChao = true;
        }
    }

    public void AdicionarZumbiEliminado()
    {
        zumbisEliminados++;
        AtualizarContador();
    }

    private void AtualizarContador()
    {
        // Atualiza o texto do contador
        contadorZumbisTexto.text = "Zumbis: " + zumbisEliminados.ToString();
    }


    // M�TODOS DE SA�DE 
    public void ReceberDano(int dano)
    {
        saudeAtual -= dano;

        animator.SetTrigger("Hit");

        // Atualiza a UI da barra de sa�de
        if (sliderDeSaude != null)
        {
            sliderDeSaude.value = saudeAtual;
        }

        if (saudeAtual <= 0)
        {
            Morrer();
        }
    }

    private void Morrer()
    {
        Debug.Log("O jogador morreu!");

        animator.SetBool("IsDead", true);

        // L�gica de Game Over

        GetComponent<PlayerController>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;


        if (painelGameOver != null)
        {
            painelGameOver.SetActive(true);
        }
    }

    // M�TODOS DE GAME OVER
    public void ReiniciarJogo()
    {
        // Pega o nome da cena atual e a carrega novamente
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VoltarParaMenuPrincipal()
    {
        // Carrega a cena do menu principal
        SceneManager.LoadScene("MainMenu");
    }

    // M�todo que verifica o contato do jogador com outros objetos (Trigger)
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entrou na zona de sa�da!");
        // Verifica se o objeto com o qual o jogador colidiu tem a tag "ZonaDeSaida"
        if (other.gameObject.CompareTag("ZonaDeSaida"))
        {
            // Checa se o n�mero de zumbis eliminados � maior que a meta de eliminacao
            if (zumbisEliminados >= metaEliminacao)
            {
                Debug.Log("Fase concluida!");
                // Se as duas condi��es forem verdadeiras, mostra o painel de "Fase Conclu�da"
                painelFaseConcluida.SetActive(true);

                //Pausar o jogo
                //Time.timeScale = 0;
            }
        }
    }

    void Atirar()
    {
        Debug.Log("Bot�o de tiro pressionado!");

        if (Time.time > proximoTiro)
        {
            proximoTiro = Time.time + atrasoEntreTiros;

            // Cria��o de um vetor de rota��o que use apenas a rota��o Y do personagem.
            // Isso garante que a bala se mova horizontalmente.
            Quaternion direcaoTiro = Quaternion.Euler(0, transform.eulerAngles.y, 0);

            // Instancia do proj�til na posi��o do ponto de tiro
            Instantiate(prefabProjetil, pontoDeTiro.position, direcaoTiro);

            // Toca o som do tiro
            if (audioSource != null && somDeTiro != null)
            {
                audioSource.PlayOneShot(somDeTiro);
            }
        }
    }

    void Update()
    {
        inputMove = moveAction.ReadValue<Vector2>();

        if (shootAction.IsPressed())
        {
            animator.SetBool("IsShooting", true);
            Atirar();
        }
        else
        {
            animator.SetBool("IsShooting", false);
        }


        if (jumpAction.IsPressed())
        {
            Pular();
        }
        

        if (rb.linearVelocity.y < 0)
        {
            // Aplica uma for�a extra de gravidade
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        Move();
        Yaw();
        MoveAnimation();
    }

    void MoveAnimation()
    {
        animator.SetFloat("linearVelocity", rb.linearVelocity.magnitude);
    }

    void Move()
    {
        rb.linearVelocity = inputMove.y * transform.forward * moveSpeed;
    }

    void Yaw()
    {
        rb.angularVelocity = new Vector3(
            0f,
            inputMove.x * yawSpeed,
            0f
        );

    }
}
