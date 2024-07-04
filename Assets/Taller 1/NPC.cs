using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class NPC : MonoBehaviour
{
    public string dialogueText = "¡Hola! Soy un NPC.";
    public GameObject dialogueBubblePrefab; // Asigna el Prefab de la burbuja de texto en el Inspector.

    private bool playerInRange = false;
    private GameObject dialogueBubbleInstance;
    public UnityEvent onStartDialog;
    public UnityEvent onEndDialog;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            HideDialogue(); // Oculta la burbuja de diálogo cuando el jugador sale del rango
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E)) // Define la tecla que el jugador debe presionar para interactuar
        {
            StartEndDialog();
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
}
