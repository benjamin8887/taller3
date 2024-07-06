using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class NPC : MonoBehaviour
{
    public string dialogueText = "¡Hola! Soy un NPC.";
    public GameObject dialogueBubblePrefab; // Asigna el Prefab de la burbuja de texto en el Inspector.
    public Transform playerTransform; // Referencia al transform del jugador

    private bool playerInRange = false;
    private GameObject dialogueBubbleInstance;
    public UnityEvent onStartDialog;
    public UnityEvent onEndDialog;

    [SerializeField] Collider2D col;
    bool isToucingInteractrion;
    void OnTriggerEnter2D(Collider2D other)
    {
        isToucingInteractrion = other.IsTouching(col);

        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerTransform = other.transform; // Guarda la referencia al transform del jugador
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isToucingInteractrion = other.IsTouching(col);
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            HideDialogue(); // Oculta la burbuja de diálogo cuando el jugador sale del rango
        }
    }

    void Update()
    {
        
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && isToucingInteractrion) // Define la tecla que el jugador debe presionar para interactuar
        {
            StartEndDialog();
        }

        // Voltear el NPC hacia el jugador
        if (playerInRange)
        {
            FlipTowardsPlayer();
        }
    }

    public void StartEndDialog()
    {
        if (dialogueBubbleInstance == null)
        {
            onStartDialog.Invoke();
            ShowDialogue();
        }
        else
        {
            onEndDialog.Invoke();
            HideDialogue();
        }
    }

    void ShowDialogue()
    {
        Debug.Log(dialogueText); // Puedes quitar esto si ya no lo necesitas.
        if (dialogueBubblePrefab != null && dialogueBubbleInstance == null)
        {
            dialogueBubbleInstance = Instantiate(dialogueBubblePrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
            dialogueBubbleInstance.GetComponentInChildren<TextMeshPro>().text = dialogueText;
        }
    }

    void HideDialogue()
    {
        if (dialogueBubbleInstance != null)
        {
            Destroy(dialogueBubbleInstance);
            dialogueBubbleInstance = null;
        }
    }

    void FlipTowardsPlayer()
    {
        if (playerTransform.position.x < transform.position.x)
        {
            // Jugador está a la izquierda del NPC, volteamos hacia la izquierda
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            // Jugador está a la derecha del NPC, volteamos hacia la derecha
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
