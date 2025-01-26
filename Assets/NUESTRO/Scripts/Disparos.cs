using UnityEngine;

public class Disparos : MonoBehaviour
{
    public GameObject proyectilPrefab; 
    public Transform puntoDisparo;
    public float fuerzaDisparo = 20f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Disparar();
        }
    }

    void Disparar()
    {
        GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);

        Rigidbody rb = proyectil.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(puntoDisparo.forward * fuerzaDisparo , ForceMode.Impulse);
        }
        Destroy(proyectil, 5f);
    }
}