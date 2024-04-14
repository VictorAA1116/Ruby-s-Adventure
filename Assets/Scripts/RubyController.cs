using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;

    public int maxHealth = 5;
    public float timeInvincible = 2.0f;

    public int health {get{return currentHealth;}}
    int currentHealth;

    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    public GameObject projectilePrefab;

    AudioSource audioSource;
    public AudioClip throwSound;
    public AudioClip hitSound;

    public ParticleSystem hitPrefab;
    public ParticleSystem healthPrefab;

    public int score = 0;
    public GameObject scoreText;
    TextMeshProUGUI scoreTextText;

    public GameObject gameOver;
    TextMeshProUGUI gameOverText;
    bool gameOverBool = false;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        scoreTextText = scoreText.GetComponent<TextMeshProUGUI>();
        gameOverText = gameOver.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        
        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }

        scoreTextText.text = "Robots Fixed: " + score.ToString();

        if (score == 2)
        {
            gameOverBool = true;
            gameOver.SetActive(true);
            gameOverText.text = "You win! Game created by Group 5";
            speed = 0.0f;
        }
        if (currentHealth == 0)
        {
            gameOverBool = true;
            gameOver.SetActive(true);
            gameOverText.text = "You lost! Press R to restart!";
            speed = 0.0f;
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (gameOverBool == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
            ParticleSystem particleObject = Instantiate(hitPrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.Euler(90.0f, 0.0f, 0.0f));

            PlaySound(hitSound);
        }

        if (amount > 0)
        {
            ParticleSystem particleObject = Instantiate(healthPrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.Euler(90.0f, 0.0f, 0.0f));
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth/(float)maxHealth);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
        
        PlaySound(throwSound);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    public void ChangeScore(int scoreAmount)
    {
        if (scoreAmount > 0)
        {
            score = score + scoreAmount;
        }
    }
}
