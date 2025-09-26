using System.Collections;
using UnityEngine;

public class PainelIntroScript : MonoBehaviour
{
    [SerializeField]
    private float duracao = 5f; // Duração em segundos que o painel ficará ativo

    private CanvasGroup canvasGroup;

    void Awake()
    {
        // Certifica-se de que o painel está ativo no início
        // para que o Awake possa ser chamado
        gameObject.SetActive(true);
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        // Inicia a rotina para mostrar o painel
        StartCoroutine(MostrarPainel());
    }

    private IEnumerator MostrarPainel()
    {
        // Ativa o painel no início da cena
        gameObject.SetActive(true);

        if (canvasGroup != null)
        {
            // Efeito de fade-in
            float tempoPassado = 0;
            while (tempoPassado < 1)
            {
                canvasGroup.alpha = tempoPassado;
                tempoPassado += Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = 1;
        }

        // Espera pelo tempo de duração
        yield return new WaitForSeconds(duracao);

        if (canvasGroup != null)
        {
            // Efeito de fade-out
            float tempoPassado = 0;
            while (tempoPassado < 1)
            {
                canvasGroup.alpha = 1 - tempoPassado;
                tempoPassado += Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = 0;
        }

        // Desativa o painel no final da rotina
        gameObject.SetActive(false);
    }
}