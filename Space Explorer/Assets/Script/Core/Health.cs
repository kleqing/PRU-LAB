using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float originHealth;
    [SerializeField] private GameObject explosionPrefab;

    public float maxHealth { get; private set; }
    
    private bool isDead;
    
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private bool isBlinking;
    
    private void Awake()
    {
        maxHealth = originHealth;
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (gameObject.CompareTag("Spaceship"))
        {
            PlayerPrefs.SetInt("Health", (int)maxHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        maxHealth = Mathf.Clamp(maxHealth - damage, 0, originHealth);
        if (gameObject.CompareTag("Spaceship"))
        {
            PlayerPrefs.SetInt("Health", (int)maxHealth);
        }
        if (maxHealth > 0)
        {
            if (!isBlinking)
            {
                StartCoroutine(Blink());
            }
        }
        else
        {
            if (!isDead)
            {
                isDead = true;
                Movement movement = GetComponent<Movement>();
                if (movement != null)
                {
                    movement.enabled = false;
                }
                Die();
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
    
    public IEnumerator Blink()
    {
        isBlinking = true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
        isBlinking = false;
    }
}
