using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public Transform[] puntosPatrulla;
    public float velocidadPatrulla = 2.0f;
    public float velocidadPersecucion = 4.0f;
    public float distanciaDeteccion = 10.0f;
    public float distanciaAtaque = 2.0f;
    public string tagObjetivo = "Player";

    private int puntoActual = 0;
    private Transform objetivo;
    private Vector3 posicionInicial;
    private enum Estado { Patrullando, Persiguiendo, Atacando, Volviendo }
    private Estado estadoActual;

    // Start se llama una vez antes de la primera ejecuci�n de Update despu�s de que se crea el MonoBehaviour
    void Start()
    {
        estadoActual = Estado.Patrullando;
        posicionInicial = transform.position;
    }

    // Update se llama una vez por fotograma
    void Update()
    {
        switch (estadoActual)
        {
            case Estado.Patrullando:
                Patrullar();
                break;
            case Estado.Persiguiendo:
                Perseguir();
                break;
            case Estado.Atacando:
                Atacar();
                break;
            case Estado.Volviendo:
                Volver();
                break;
        }
    }

    void Patrullar()
    {
        if (puntosPatrulla.Length == 0) return;

        Transform puntoDestino = puntosPatrulla[puntoActual];
        MoverHacia(puntoDestino.position, velocidadPatrulla);

        if (Vector3.Distance(transform.position, puntoDestino.position) < 0.2f)
        {
            puntoActual = (puntoActual + 1) % puntosPatrulla.Length;
        }

        DetectarObjetivo();
    }

    void Perseguir()
    {
        if (objetivo == null)
        {
            estadoActual = Estado.Volviendo;
            return;
        }

        MoverHacia(objetivo.position, velocidadPersecucion);

        if (Vector3.Distance(transform.position, objetivo.position) < distanciaAtaque)
        {
            estadoActual = Estado.Atacando;
        }
        else if (Vector3.Distance(transform.position, objetivo.position) > distanciaDeteccion)
        {
            estadoActual = Estado.Volviendo;
        }
    }

    void Atacar()
    {
        if (objetivo == null)
        {
            estadoActual = Estado.Volviendo;
            return;
        }

        // Implementar l�gica de ataque aqu�

        if (Vector3.Distance(transform.position, objetivo.position) > distanciaAtaque)
        {
            estadoActual = Estado.Persiguiendo;
        }
    }

    void Volver()
    {
        MoverHacia(posicionInicial, velocidadPatrulla);

        if (Vector3.Distance(transform.position, posicionInicial) < 0.2f)
        {
            estadoActual = Estado.Patrullando;
        }

        DetectarObjetivo();
    }

    void DetectarObjetivo()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, distanciaDeteccion);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(tagObjetivo))
            {
                objetivo = collider.transform;
                estadoActual = Estado.Persiguiendo;
                break;
            }
        }
    }

    void MoverHacia(Vector3 destino, float velocidad)
    {
        Vector3 direccion = (destino - transform.position).normalized;
        transform.position += direccion * velocidad * Time.deltaTime;

        // Solo rotar en el eje Y
        Vector3 direccionY = new Vector3(direccion.x, 0, direccion.z);
        if (direccionY != Vector3.zero)
        {
            Quaternion rotacion = Quaternion.LookRotation(direccionY);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, Time.deltaTime * velocidad);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaDeteccion);
    }
}
