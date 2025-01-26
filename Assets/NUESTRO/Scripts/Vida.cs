using UnityEngine;

public class Vida : MonoBehaviour
{
    public float saludMaxima = 100f;      
    public float saludActual;           
    public float regeneracion = 0f;      
    public float escudo = 0f;            
    public bool estaVivo = true;        
    public bool inmune = false;         

    public float tiempoInmunidad = 0f;
    private float contadorInmunidad = 0f;

    private void Start()
    {
        saludActual = saludMaxima;
    }

    private void Update()
    {
        if (estaVivo && regeneracion > 0)
        {
            RegenerarSalud(Time.deltaTime * regeneracion);
        }

        if (inmune)
        {
            contadorInmunidad -= Time.deltaTime;
            if (contadorInmunidad <= 0f)
            {
                inmune = false;
            }
        }
    }

    public void RecibirDanio(float cantidad)
    {
        if (!estaVivo || inmune) return;

        float danioReal = Mathf.Max(0, cantidad - escudo); 
        saludActual -= danioReal;

        if (saludActual <= 0)
        {
            saludActual = 0;
            estaVivo = false;
            Muerte();
        }

        if (tiempoInmunidad > 0)
        {
            inmune = true;
            contadorInmunidad = tiempoInmunidad;
        }
    }

    public void Curar(float cantidad)
    {
        if (!estaVivo) return;

        saludActual += cantidad;
        saludActual = Mathf.Min(saludActual, saludMaxima);
    }
    private void RegenerarSalud(float cantidad)
    {
        saludActual += cantidad;
        saludActual = Mathf.Min(saludActual, saludMaxima);
    }

    // Lógica para manejar la muerte de la entidad
    private void Muerte()
    {
        Debug.Log(gameObject.name + " ha muerto.");
    }

    public void RestablecerVida()
    {
        saludActual = saludMaxima;
        estaVivo = true;
        inmune = false;
    }
}
