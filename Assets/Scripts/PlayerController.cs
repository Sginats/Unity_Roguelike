using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("UI Elementi")]
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text healthText;
    public GameObject gameOverPanel;

    [Header("Spēles Iestatījumi")]
    public int maxHealth = 3;
    private int currentHealth;
    private int score = 0;
    private float timer = 0f;
    private bool isGameActive = true;

    [Header("Skaņas (Indeksi no SFX saraksta)")]
    public int catchSoundIndex = 0;
    public int hitSoundIndex = 1; 
    public int deathSoundIndex = 4;

    SFX_Script sfx;

    void Start()
    {
        sfx = FindFirstObjectByType<SFX_Script>();
        currentHealth = maxHealth;
        UpdateUI();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (isGameActive)
        {
            timer += Time.deltaTime;
            if (timerText != null)
                timerText.text = "Laiks: " + timer.ToString("F2") + "s";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isGameActive) return;

        if (collision.CompareTag("Good"))
        {
            score++;
            if (sfx != null) sfx.PlaySFX(catchSoundIndex);
            Destroy(collision.gameObject);
        }

        else if (collision.CompareTag("Bad"))
        {
            Destroy(collision.gameObject); 
            TakeDamage();
        }

        UpdateUI();
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

        if (sfx != null) sfx.PlaySFX(deathSoundIndex);

        Time.timeScale = 0;

        if (healthText != null) healthText.text = "Dzīvības: 0";
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "Punkti: " + score;
        if (healthText != null) healthText.text = "Dzīvības: " + currentHealth;
    }
}