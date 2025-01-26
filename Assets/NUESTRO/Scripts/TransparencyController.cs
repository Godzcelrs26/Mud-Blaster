using UnityEngine;
using System.Collections.Generic;

public class TransparencyController : MonoBehaviour
{
    public Transform player; // Arrastra aquí el jugador desde el inspector
    public LayerMask obstacleLayer; // Define la capa de los objetos que pueden obstruir
    public Material transparentMaterial; // Asigna el material transparente desde el inspector
    public float radioDeteccion = 10.0f; // Radio de detección para los obstáculos

    private List<Renderer> renderersOriginales = new List<Renderer>();
    private Dictionary<Renderer, Material> materialesOriginales = new Dictionary<Renderer, Material>();

    void Update()
    {
        // Restaurar materiales originales
        RestaurarMaterialesOriginales();

        // Detectar obstáculos en el radio de detección
        Collider[] colliders = Physics.OverlapSphere(transform.position, radioDeteccion, obstacleLayer);
        foreach (Collider collider in colliders)
        {
            Renderer renderer = collider.GetComponent<Renderer>();
            if (renderer != null && !materialesOriginales.ContainsKey(renderer))
            {
                // Guardar el material original y aplicar el material transparente
                materialesOriginales[renderer] = renderer.material;
                renderer.material = transparentMaterial;
                renderersOriginales.Add(renderer);
            }
        }
    }

    private void RestaurarMaterialesOriginales()
    {
        foreach (Renderer renderer in renderersOriginales)
        {
            if (renderer != null && materialesOriginales.ContainsKey(renderer))
            {
                renderer.material = materialesOriginales[renderer];
            }
        }
        renderersOriginales.Clear();
        materialesOriginales.Clear();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDeteccion);
    }
}
