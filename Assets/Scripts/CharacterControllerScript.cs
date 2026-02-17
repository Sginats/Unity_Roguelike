using UnityEngine;
using UnityEngine.InputSystem; // Tavai kustībai
using TMPro;                   // Punktiem un tekstiem
using UnityEngine.SceneManagement; // Restartēšanai

public class CharacterControllerScript : MonoBehaviour
{
    [Header("Kustība un Animācija")]
    public float moveSpeed = 1f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float moveInput;

    [Header("UI Elementi")]
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text healthText;
    public GameObject gameOverPanel;

    [Header("Spēles Statistika")]
    public int maxHealth = 3;
    private int currentHealth;
    private int score = 0;
    private float timer = 0f;
    private bool isGameActive = true;

    [Header("Izmēra Loģika")]
    public float sizeChangeAmount = 0.5f; // Cik daudz palielināt/samazināt
    public float minSize = 0.5f;          // Minimālais izmērs

    [Header("Skaņas (Indeksi)")]
    public int catchSoundIndex = 0;
    public int hitSoundIndex = 1; 
    public int deathSoundIndex = 4;
    SFX_Script sfx;

    void Start()
    {
        // Kustības komponentes
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // Spēles loģikas sākums
        sfx = FindFirstObjectByType<SFX_Script>();
        currentHealth = maxHealth;
        UpdateUI();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    void Update()
    {
        // Ja spēle beigusies, neko nedarām
        if (!isGameActive) return;

        // 1. KUSTĪBAS LOĢIKA (Tavs kods)
        moveInput = 0;
        
        // Pārbauda vai Keyboard eksistē, lai nemestu kļūdas
        if (Keyboard.current != null)
        {
            if (Keyboard.current.leftArrowKey.isPressed)
            {
                moveInput = -1;
            }
            else if (Keyboard.current.rightArrowKey.isPressed)
            {
                moveInput = 1;
            }
        }

        if (animator != null)
            animator.SetBool("isWalking", moveInput != 0);

        if (spriteRenderer != null)
        {
            if (moveInput > 0)
                spriteRenderer.flipX = false;
            else if (moveInput < 0)
                spriteRenderer.flipX = true;
        }

        // 2. TAIMERA LOĢIKA
        timer += Time.deltaTime;
        if (timerText != null)
            timerText.text = "Laiks: " + timer.ToString("F2") + "s";
    }

    private void FixedUpdate()
    {
        if (isGameActive && rb != null)
        {
            rb.MovePosition(rb.position + new Vector2(moveInput * moveSpeed * Time.deltaTime, 0));
        }
    }

    // --- ĒŠANAS UN SADURSMI LOĢIKA ---
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isGameActive) return;

        // "Labie" objekti (Good)
        if (collision.CompareTag("Good"))
        {
            score++;
            transform.localScale += new Vector3(sizeChangeAmount, sizeChangeAmount, 0); // Palielina izmēru
            
            if (sfx != null) sfx.PlaySFX(catchSoundIndex);
            Destroy(collision.gameObject);
            UpdateUI();
        }
        // "Sliktie" objekti (Bad)
        else if (collision.CompareTag("Bad"))
        {
            score--;
            if (score < 0) score = 0;

            // Samazina izmēru (bet neļauj pazust pavisam)
            if (transform.localScale.x > minSize)
            {
                transform.localScale -= new Vector3(sizeChangeAmount, sizeChangeAmount, 0);
            }

            Destroy(collision.gameObject);
            TakeDamage(); // Noņem dzīvību
            UpdateUI();
        }
    }

    void TakeDamage()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            GameOver();
        }
        else
        {
            if (sfx != null) sfx.PlaySFX(hitSoundIndex);
        }
    }

    void GameOver()
    {
        isGameActive = false;
        if (animator != null) animator.SetBool("isWalking", false); // Apstādina animāciju

        if (sfx != null) sfx.PlaySFX(deathSoundIndex);

        Time.timeScale = 0; // Apstādina laiku

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Punkti: " + score;
        
        if (healthText != null)
            healthText.text = "Dzīvības: " + currentHealth;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}