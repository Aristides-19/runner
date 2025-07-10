using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para reiniciar o cambiar de escena

public class ColisionPeligro : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si el objeto con el que colisionamos tiene la etiqueta "Peligro"
        if (collision.gameObject.CompareTag("Peligro"))
        {
            TerminarJuego();
        }
    }

    // También puedes usar OnTriggerEnter si tus colisiones son triggers
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Peligro"))
        {
            TerminarJuego();
        }
    }

    void TerminarJuego()
    {
        Debug.Log("¡Juego terminado! Has chocado con un objeto peligroso.");
        SceneManager.LoadScene("GameOverScene");

    }
}