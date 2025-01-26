using UnityEngine;
using UnityEngine.UI;

public class CogerItem : MonoBehaviour
{
    public Transform puntoRecogida; // Punto al que se teletransportará el objeto
    public Image imagen;
    public ArmaBurbuja arma;

    private void Start()
    {
        arma = FindAnyObjectByType<ArmaBurbuja>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TeletransportarObjeto();
        }
    }

    void TeletransportarObjeto()
    {
        transform.position = puntoRecogida.position;
        transform.parent = puntoRecogida; // Opcional: hacer que el objeto sea hijo del punto de recogida
        arma.EquiparArma();
    }
}
