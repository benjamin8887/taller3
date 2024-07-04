using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverCubo : MonoBehaviour
{
    public float velocidadMovimiento = 5f; // Velocidad de movimiento del cubo
    public float fuerzaSalto = 10f; // Fuerza de salto del cubo
    public Rigidbody rb; // Componente Rigidbody del cubo

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtenemos el Rigidbody del cubo
    }

    void FixedUpdate()
    {
        // Obtener la entrada de movimiento del jugador
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Calcular el vector de movimiento basado en la entrada del jugador
        Vector3 movimiento = new Vector3(movimientoHorizontal, 0f, movimientoVertical) * velocidadMovimiento * Time.deltaTime;

        // Aplicar el movimiento al cubo
        rb.MovePosition(transform.position + movimiento);

        // Saltar cuando se presiona la tecla de espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }
}
