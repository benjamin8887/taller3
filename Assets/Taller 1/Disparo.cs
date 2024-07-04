using UnityEngine;

public class Disparo : MonoBehaviour
{
    public GameObject proyectilPrefab; // Prefab del proyectil
    public Transform puntoDeDisparo; // Punto desde donde se dispara el proyectil
    public float fuerzaDeDisparo = 10f; // Fuerza del disparo
    public float velocidadDeDisparo = 0.5f; // Velocidad de disparo en segundos
    public float distanciaMaxima = 10f; // Distancia máxima que puede recorrer la bala

    private float tiempoUltimoDisparo; // Tiempo del último disparo

    void Update()
    {
        // Detectar la dirección del mouse
        Vector3 direccionDelDisparo = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        // Rotar el personaje hacia la dirección del disparo
        float angulo = Mathf.Atan2(direccionDelDisparo.y, direccionDelDisparo.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angulo - 90, Vector3.forward);

        // Disparar al hacer clic izquierdo del ratón y si ha pasado el tiempo de disparo
        if (Input.GetMouseButtonDown(0) && Time.time > tiempoUltimoDisparo + velocidadDeDisparo)
        {
            Disparar();
            tiempoUltimoDisparo = Time.time;
        }
    }

    void Disparar()
    {
        // Instanciar el proyectil
        GameObject proyectil = Instantiate(proyectilPrefab, puntoDeDisparo.position, transform.rotation);

        // Aplicar fuerza al proyectil
        Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(transform.up * fuerzaDeDisparo, ForceMode2D.Impulse);
        }

        // Destruir la bala después de recorrer una cierta distancia
        Destroy(proyectil, distanciaMaxima / fuerzaDeDisparo);
    }

    // Detectar colisiones con otros objetos
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") // Si la bala no colisiona con el personaje
        {
            Destroy(gameObject); // Destruir la bala
        }
    }
}
