using System.Collections;
using UnityEngine;

public class Disparador : MonoBehaviour
{
    public GameObject objetoADisparar1;
    public GameObject objetoADisparar2;
    public float delayInicial = 1f;
    public float intervaloEntreDisparos = 2f;
    private Transform jugador;

    [SerializeField] float timepoParaDestruir = 2;
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(DispararConDelayInicial());
    }

    IEnumerator DispararConDelayInicial()
    {
        yield return new WaitForSeconds(delayInicial);

        while (true)
        {
            DispararHaciaJugador(objetoADisparar1);
            yield return new WaitForSeconds(0.1f);
            DispararHaciaJugador(objetoADisparar2);

            yield return new WaitForSeconds(intervaloEntreDisparos);
        }
    }

    void DispararHaciaJugador(GameObject objeto)
    {
        // Calculamos la dirección hacia el jugador
        Vector3 direccion = (jugador.position - transform.position).normalized;
        print(direccion);

        // Rotamos el objeto para que apunte hacia el jugador (opcional)
        Quaternion rotacion = this.transform.rotation;

        // Instanciamos el objeto a disparar en la posición actual del Disparador
        GameObject disparo = Instantiate(objeto, transform.position, rotacion);

        Destroy(disparo,timepoParaDestruir);
        // Movemos el objeto en la dirección calculada
        disparo.GetComponent<Rigidbody2D>().AddForce(direccion * 1000f); // Ajusta la velocidad según necesites
    }
}
