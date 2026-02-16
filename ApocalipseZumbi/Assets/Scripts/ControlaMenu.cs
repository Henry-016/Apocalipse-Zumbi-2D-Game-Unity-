using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaMenu : MonoBehaviour
{
    [SerializeField] private GameObject painelDosControles;

    private void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void JogarJogo()
    {
        SceneManager.LoadScene("Fase1_Scene");
    }

    public void SairDoJogo()
    {
        Application.Quit();
    }

    public void MostrarControle()
    {
        painelDosControles.SetActive(true);
    }

    public void FecharPainelDeControles()
    {
        painelDosControles.SetActive(false);
    }
}
