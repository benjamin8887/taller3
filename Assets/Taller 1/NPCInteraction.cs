using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject interactionIcon;
    public Transform player;
    public float interactionDistance = 2f;

    void Start()
    {
        interactionIcon.SetActive(false); // Aseguramos que al inicio esté desactivado
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= interactionDistance)
        {
            interactionIcon.SetActive(true);
        }
        else
        {
            interactionIcon.SetActive(false);
        }
    }
}
