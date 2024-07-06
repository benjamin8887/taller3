using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public float jumpSpeedMultiplier = 1.5f;
    public int extraJumps = 1;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Slider slider;
    public GameObject inventoryManagerPrefab;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private int jumpsLeft;
    public int maxHealth = 5; // Cambio aquí: Ahora maxHealth es pública
    private int currentHealth;
    [SerializeField] float waitToTransition = 1;
    [SerializeField] bool died = false;
    private bool facingRight = true;
    private Vector3 respawnPoint;
    public Animator anim;
    private InventoryManager inventoryManager;

    public float interactionRange = 2f;
    public LayerMask mercaderLayer;

    // Enemy effect variables
    private bool isAffectedByEnemy = false;
    private float enemySlowDownFactor;
    private float enemyDamagePerSecond;
    private float damageTimer = 1f; // Tiempo entre cada aplicación de daño
    private float damageTimerCounter = 0f; // Contador para controlar el tiempo entre aplicaciones de daño

    [Header("SHOOT")]
    bool canShoot = false;
    bool canShootS = false;
    public GameObject projectilePrefab; // Asigna el prefab del proyectil en el Inspector
    public GameObject projectilePrefabEscopeta; // Asigna el prefab del proyectil en el Inspector
    public GameObject shootPosition;
    [SerializeField] float shootSpeed;

    // Enum para el estado de la vida
    public enum HealthState
    {
        SinVida,
        Vida1,
        Vida2,
        Vida3,
        Vida4,
        Vida5
        
    }

    [Header("Health View")]
    [SerializeField] List<Sprite> healthStateSprite;
    [SerializeField] Image health;


    public HealthState healthState; // Variable para almacenar el estado actual de la vida

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsLeft = extraJumps;
        currentHealth = maxHealth;
        SetMaxHealth(maxHealth);
        respawnPoint = transform.position;

        if (inventoryManagerPrefab != null)
        {
            inventoryManager = FindObjectOfType<InventoryManager>();
            if (inventoryManager == null)
            {
                GameObject manager = Instantiate(inventoryManagerPrefab);
                inventoryManager = manager.GetComponent<InventoryManager>();
            }
        }
    }

    private void Update()
    {
        if (died) return;
        HandleInput();

        if (currentHealth <= 0)
        {
            Die();
        }

        // Control del temporizador para el daño por segundo del enemigo
        if (isAffectedByEnemy)
        {
            damageTimerCounter += Time.deltaTime;
            if (damageTimerCounter >= damageTimer)
            {
                TakeDamage(Mathf.RoundToInt(enemyDamagePerSecond));
                damageTimerCounter = 0f;
            }
        }
        if (canShoot)
        {
            PistolaShoot();
        }
        if (canShootS)
        {
            EscopetaShoot();
        }
    }

    private void PistolaShoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Obtener la posición actual del jugador
            Vector3 playerPos = shootPosition.transform.position;

            // Instanciar el proyectil en la posición del jugador
            GameObject projectile = Instantiate(projectilePrefab, playerPos, Quaternion.identity);

            // Configurar la velocidad del proyectil (ejemplo: 10 unidades por segundo hacia la derecha)
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(shootSpeed * (transform.localScale.x < 0 ? -1 : 1), 0f); // Puedes ajustar la velocidad según tus necesidades
        }
    }

    private void EscopetaShoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Obtener la posición actual del jugador
            Vector3 playerPos = shootPosition.transform.position;

            // Instanciar el proyectil en la posición del jugador
            GameObject projectile = Instantiate(projectilePrefabEscopeta, playerPos, Quaternion.identity);

            // Configurar la velocidad del proyectil (ejemplo: 10 unidades por segundo hacia la derecha)
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(shootSpeed * (transform.localScale.x < 0 ? -1 : 1), 0f); // Puedes ajustar la velocidad según tus necesidades
            canShootS = false;
            Invoke("FreeEscopeta", 1);
            anim.SetBool("EscopetaShoot", true);

        }
    }

    public void FreeEscopeta()
    {
        canShootS = true;
    }

    private void FixedUpdate()
    {
        if (died) return;

        HandleMovement();

        HandleAnimation();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
        {
            Jump();
            jumpsLeft--;
        }

        if (Input.GetKeyDown(KeyCode.I) && inventoryManager != null && !inventoryManager.inventoryOpen)
        {
            inventoryManager.ToggleInventory();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractWithMerchant();
        }
    }

    private void HandleMovement()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (isGrounded)
        {
            jumpsLeft = extraJumps;
        }

        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if ((moveInput > 0 && !facingRight) || (moveInput < 0 && facingRight))
        {
            Flip();
        }
    }

    public void Pistola()
    {
        anim.SetBool("Pistola", true);
        anim.SetBool("Escopeta", false);
        canShoot = true;
        canShootS = false;
    }

    public void Escopeta()
    {
        anim.SetBool("Pistola", false);
        anim.SetBool("Escopeta", true);
        canShootS = true;
        canShoot = false;
    }

    private void HandleAnimation()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            anim.SetBool("Pistola", false);
            anim.SetBool("Escopeta", false);
            canShoot = false;

            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }

        if (rb.velocity.y >= 0)
        {
            if (rb.velocity.y > 0)
            {
                canShoot = false;
                anim.SetBool("Pistola", false);
                anim.SetBool("Escopeta", false);
            }
            anim.SetBool("JumpEnd", false);
        }
        else if (rb.velocity.y < 0)
        {
            canShoot = false;
            anim.SetBool("Pistola", false);
            anim.SetBool("Escopeta", false);
            anim.SetBool("JumpEnd", true);
        }
    }

    private void Jump()
    {
        anim.SetBool("Jump", true);
        rb.velocity = new Vector2(rb.velocity.x * jumpSpeedMultiplier, jumpForce);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        SetHealth(currentHealth);


        switch (currentHealth)
        {
            case (int)HealthState.Vida1:
                health.sprite = healthStateSprite[1];
                break;
            case (int)HealthState.Vida2:
                health.sprite = healthStateSprite[2];
                break;
            case (int)HealthState.Vida3:
                health.sprite = healthStateSprite[3];
                break;
            case (int)HealthState.Vida4:
                health.sprite = healthStateSprite[4];
                break;
            case (int)HealthState.Vida5:
                health.sprite = healthStateSprite[5];
                break;
            case (int)HealthState.SinVida:
                health.sprite = healthStateSprite[0];
                break;
            default:
                break;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void HealPlayer(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        SetHealth(currentHealth);
    }

    private void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    private void SetHealth(int health)
    {
        slider.value = health;
    }

    private void Die()
    {
        died = true;
        Debug.Log("DIE");
        anim.SetBool("Die", true); 

        Invoke("ReloadScene", waitToTransition);
    }

    private void ReloadScene()
    {
        currentHealth = maxHealth;
        SetHealth(currentHealth);

        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
    }

    private void InteractWithMerchant()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRange, mercaderLayer);

        foreach (Collider2D collider in colliders)
        {
            MerchantScript merchant = collider.GetComponent<MerchantScript>();
            if (merchant != null)
            {
                merchant.OpenShop();
                break;
            }
        }
    }

    // Enemy effect methods
    public void ApplyEffect(float slowDownFactor, float damagePerSecond)
    {
        isAffectedByEnemy = true;
        enemySlowDownFactor = slowDownFactor;
        enemyDamagePerSecond = damagePerSecond;
    }

    public void RemoveEffect()
    {
        isAffectedByEnemy = false;
        damageTimerCounter = 0f; // Reinicia el temporizador cuando se elimina el efecto del enemigo
    }
}


