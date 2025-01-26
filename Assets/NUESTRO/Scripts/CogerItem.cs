using UnityEngine;
using UnityEngine.UI;

public class CogerItem : MonoBehaviour
{
     Inventario inventario;
    public Image imagen;
    void Update()
    {
        inventario = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventario>();
    }
    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Player")
        {
            inventario.Cantidad = inventario.Cantidad + 1;
            Destroy(gameObject);
        }
    }
}