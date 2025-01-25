using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Jugador : MonoBehaviour
{
    // Velocidad de movimiento del jugador
    public float velocidad = 5.0f;
    private Rigidbody rb;
    private Animator animator;
    public float Normalizado;

    // Start se llama una vez antes de la primera ejecución de Update después de que se crea el MonoBehaviour
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update se llama una vez por fotograma
    void Update()
    {
        // Manejar la entrada del usuario
        Vector3 movimiento = ObtenerEntradaUsuario();

        // Aplicar movimiento al objeto
        MoverJugador(movimiento);

        // Actualizar animaciones
        ActualizarAnimaciones(movimiento);

        // Rotar en la dirección del movimiento
        RotarJugador(movimiento);

        // Calcular la magnitud del movimiento y normalizarla
        Normalizado = movimiento.magnitude;
    }

    // Método para obtener la entrada del usuario
    Vector3 ObtenerEntradaUsuario()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        return new Vector3(movimientoHorizontal, 0.0f, movimientoVertical);
    }

    // Método para mover al jugador
    void MoverJugador(Vector3 movimiento)
    {
        if (movimiento != Vector3.zero)
        {
            rb.MovePosition(transform.position + movimiento * velocidad * Time.deltaTime);
        }
    }

    // Método para actualizar las animaciones
    void ActualizarAnimaciones(Vector3 movimiento)
    {
        animator.SetFloat("Velocidad", Normalizado);
    }

    // Método para rotar al jugador en la dirección del movimiento
    void RotarJugador(Vector3 movimiento)
    {
        if (movimiento != Vector3.zero)
        {
            Quaternion nuevaRotacion = Quaternion.LookRotation(movimiento);
            rb.MoveRotation(nuevaRotacion);
        }
    }
}
