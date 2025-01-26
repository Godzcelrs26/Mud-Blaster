using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float amplitude = 0.5f; // Altura de la oscilación
    public float frequency = 1f; // Frecuencia de la oscilación

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + amplitude * Mathf.Sin(Time.time * frequency);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}