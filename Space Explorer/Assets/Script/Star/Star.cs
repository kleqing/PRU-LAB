using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [Header("Star Settings")] [SerializeField]
    private float lifetime;

    [SerializeField] private float speed;

    [Header("Score")] [SerializeField] public int scoreValue;

    private Rigidbody2D rb;

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
        if (other.gameObject.CompareTag("Spaceship"))
        {
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + scoreValue);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}

