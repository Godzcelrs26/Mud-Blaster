using UnityEngine;

public class ArmaBurbuja : MonoBehaviour
{
    public GameObject balaPrefab; // Prefab de la bala
    public Transform bocaPistola; // Punto desde donde se disparan las balas
    public float velocidadBala = 20f; // Velocidad de la bala
    public float cargaMaxima = 100f; // Carga m�xima de la pistola
    public float consumoPorBala = 10f; // Porcentaje de carga consumido por cada bala
    public float tiempoRecarga = 2f; // Tiempo que tarda en recargar

    private float cargaActual;
    private bool recargando = false;
    private bool pistolaActiva = true;

    // Start se llama una vez antes de la primera ejecuci�n de Update despu�s de que se crea el MonoBehaviour
    void Start()
    {
        cargaActual = cargaMaxima;
    }

    // Update se llama una vez por fotograma
    void Update()
    {
        if (pistolaActiva)
        {
            if (Input.GetButtonDown("Fire1") && cargaActual >= consumoPorBala && !recargando)
            {
                Disparar();
            }

            if (Input.GetKeyDown(KeyCode.R) && !recargando)
            {
                StartCoroutine(Recargar());
            }
        }
    }

    void Disparar()
    {
        GameObject bala = Instantiate(balaPrefab, bocaPistola.position, bocaPistola.rotation);
        Rigidbody rb = bala.GetComponent<Rigidbody>();
        rb.linearVelocity = bocaPistola.forward * velocidadBala;

        cargaActual -= consumoPorBala;
    }

    System.Collections.IEnumerator Recargar()
    {
        recargando = true;
        yield return new WaitForSeconds(tiempoRecarga);
        cargaActual = cargaMaxima;
        recargando = false;
    }

    public void ActivarPistola()
    {
        pistolaActiva = true;
        gameObject.SetActive(true);
    }

    public void DesactivarPistola()
    {
        pistolaActiva = false;
        gameObject.SetActive(false);
    }
}
