using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
    public int Escena;
    private bool juegoPausado = false;
    public GameObject MenuPausa;

    // Update se llama una vez por fotograma
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (juegoPausado)
            {
                ContinuarJuego();
            }
            else
            {
                PausarJuego();
            }
        }
    }

    public void PausarJuego()
    {
        MenuPausa.SetActive(true);
        Time.timeScale = 0f;
        juegoPausado = true;
        // Aquí puedes activar la UI de pausa
    }

    public void ContinuarJuego()
    {
        MenuPausa.SetActive(false);
        Time.timeScale = 1f;
        juegoPausado = false;
        // Aquí puedes desactivar la UI de pausa
    }
    public void Salir()
    {
        SceneManager.LoadScene(Escena);
    }
}
