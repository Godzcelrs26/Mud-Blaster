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

    // Start se llama una vez antes de la primera ejecuci�n de Update despu�s de que se crea el MonoBehaviour
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
        ActualizarAnimaciones();

        // Rotar en la direcci�n del movimiento
        RotarJugador(movimiento);
    }

    // M�todo para obtener la entrada del usuario
    Vector3 ObtenerEntradaUsuario()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        return new Vector3(movimientoHorizontal, 0.0f, movimientoVertical);
    }

    // M�todo para mover al jugador
    void MoverJugador(Vector3 movimiento)
    {
        if (movimiento != Vector3.zero)
        {
            rb.MovePosition(transform.position + movimiento * velocidad * Time.deltaTime);
        }
    }

    // M�todo para actualizar las animaciones
    void ActualizarAnimaciones()
    {

    }

    // M�todo para rotar al jugador en la direcci�n del movimiento
    void RotarJugador(Vector3 movimiento)
    {
        if (movimiento != Vector3.zero)
        {
            Quaternion nuevaRotacion = Quaternion.LookRotation(movimiento);
            rb.MoveRotation(nuevaRotacion);
        }
    }
}