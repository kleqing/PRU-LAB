using System;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [Header("Meteor Settings")]
    [SerializeField] private float lifetime;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    
    private Rigidbody2D rb;
    
    [Header("Score")]
    public int scoreValue;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        rb.linearVelocity = new Vector2(0, speed * -1);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //* Debug.Log($"Meteor collided with: {other.gameObject.name}, Tag: {other.gameObject.tag}");
        if (other.gameObject.CompareTag("Spaceship"))
        {
            Health health = other.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
                StartCoroutine(health.Blink());
                //PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + scoreValue);
                Destroy(gameObject);
            }
        }
        
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + scoreValue);
            Destroy(gameObject);
        }
    }
}