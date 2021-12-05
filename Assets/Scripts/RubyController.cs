using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RubyController : MonoBehaviour
{

    public float speed = 3.0f;
    private float boostTimer;
    private bool boosting;


    public int maxHealth = 5;
    public float timeInvincible = 2.0f;

    public GameObject lightningEffectPrefab;

    public AudioClip collectedClip;

    public AudioClip talkingClip;

    public GameObject projectilePrefab;

    public ParticleSystem hitEffectPrefab;

    public AudioClip throwSound;
    public AudioClip hitSound;

    public AudioSource musicSource;
    public AudioClip winSong;
    public AudioClip deathSong;

    //public Text fixedText;
    //public Text winText;

    //private int count;

    //private bool counting = false;

    //private float health = 0f;

    int currentHealth;
    public int health { get { return currentHealth; }}
    
    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal; 
    float vertical;

    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        audioSource= GetComponent<AudioSource>();

        boostTimer = 0;
        boosting = false;
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
                PlaySound(talkingClip);
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (boosting)
        {
            boostTimer += Time.deltaTime;
            if(boostTimer >= 3)
            {
                speed = 3;
                boostTimer = 0;
                boosting = false;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "SpeedBoost")
        {
            boosting = true;
            speed = 6;
            GameObject lightningEffectObject = Instantiate(lightningEffectPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);

            PlaySound(collectedClip);
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
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            hitEffectPrefab.Play();

            animator.SetTrigger("Hit");
            
            if (currentHealth <= 0f)
            {
                PlayerDied();
            }

            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;

            PlaySound(hitSound);
        }
        
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    private void PlayerDied()
    {
        musicSource.clip = deathSong;
        musicSource.Play();
        LevelManager.instance.GameOver();
        gameObject.SetActive(false);
    }

    private void GameWin()
    {
        LevelManager.instance.WinGame();
        gameObject.SetActive(false);
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

}
