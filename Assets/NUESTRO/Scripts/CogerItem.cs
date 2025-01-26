using UnityEngine;

public class CogerItem : MonoBehaviour
{
     Inventario inventario;
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
