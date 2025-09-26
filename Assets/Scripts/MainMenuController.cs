using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject painelCreditos;

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void AbrirCreditos()
    {
        // Ativa o painel de cr�ditos
        painelCreditos.SetActive(true);
    }

    public void FecharCreditos()
    {
        // Desativa o painel de cr�ditos
        painelCreditos.SetActive(false);
    }
}
