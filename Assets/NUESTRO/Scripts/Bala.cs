using UnityEngine;

public class Bala : MonoBehaviour
{
    public float tiempoVida = 10f; // Tiempo de vida de la bala si no colisiona con nada

    void Start()
    {
        // Destruir la bala después de tiempoVida segundos si no colisiona con nada
        Destroy(gameObject, tiempoVida);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Destruir la bala al colisionar con cualquier objeto
        Destroy(gameObject);
    }
}