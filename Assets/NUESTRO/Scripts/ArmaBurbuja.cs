using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class ArmaBurbuja : MonoBehaviour
{
    public TextMeshProUGUI CargaBala; // Texto para mostrar la carga de balas
    public GameObject balaPrefab; // Prefab de la bala
    public Transform bocaPistola; // Punto desde donde se disparan las balas
    public float velocidadBala = 10f; // Velocidad de la bala
    public float cargaMaxima = 100f; // Carga máxima de la pistola
    public float consumoPorBala = 10f; // Porcentaje de carga consumido por cada bala
    public float tiempoRecarga = 2f; // Tiempo que tarda en recargar

    private float cargaActual;
    private bool recargando = false;
    private bool pistolaActiva = true;
    public bool equipada = false; // Indica si el arma está equipada
    public Animator anim;

    // Start se llama una vez antes de la primera ejecución de Update después de que se crea el MonoBehaviour
    void Start()
    {
        cargaActual = cargaMaxima;
    }

    // Update se llama una vez por fotograma
    void Update()
    {
        if (pistolaActiva && equipada)
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
        Textos();
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

    public void EquiparArma()
    {
        equipada = true;
        anim.SetBool("Pistola", equipada);
        ActivarPistola();
    }

    public void DesequiparArma()
    {
        anim.SetBool("Pistola", equipada);
        equipada = false;
        DesactivarPistola();
    }

    void Textos()
    {
        CargaBala.text = "Cantidad de Balas: " + cargaActual;
    }
}

