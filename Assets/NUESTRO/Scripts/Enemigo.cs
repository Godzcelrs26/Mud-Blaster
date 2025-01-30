using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float vida = 1;
    public Transform[] puntosPatrulla;
    public float velocidadPatrulla = 2.0f;
    public float velocidadPersecucion = 4.0f;
    public float distanciaDeteccion = 10.0f;
    public float distanciaAtaque = 2.0f;
    public float daño = 10.0f; // Daño infligido al objetivo
    public float tiempoEntreAtaques = 1.5f; // Tiempo de espera entre ataques
    public string tagObjetivo = "Player";

    private int puntoActual = 0;
    private Transform objetivo;
    private Vector3 posicionInicial;
    private bool puedeAtacar = true; // Control para limitar la frecuencia de ataque
    private enum Estado { Patrullando, Persiguiendo, Atacando, Volviendo }
    private Estado estadoActual;

    void Start()
    {
        estadoActual = Estado.Patrullando;
        posicionInicial = transform.position;
    }

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
        if (vida <= 0)
        {
            Destroy(gameObject);
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

        if (puedeAtacar)
        {
            puedeAtacar = false; // Evita que ataque de inmediato otra vez

            // Verifica si el objetivo tiene el componente Vida
            Vida vidaJugador = objetivo.GetComponent<Vida>();
            if (vidaJugador != null)
            {
                vidaJugador.RecibirDanio(daño); // Inflige daño al objetivo
            }

            Invoke(nameof(ResetAtaque), tiempoEntreAtaques); // Espera antes del próximo ataque
        }

        if (Vector3.Distance(transform.position, objetivo.position) > distanciaAtaque)
        {
            estadoActual = Estado.Persiguiendo;
        }
    }

    void ResetAtaque()
    {
        puedeAtacar = true; // Permite atacar de nuevo
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bala"))
        {
            vida = -vida; 
        }
    }
}
