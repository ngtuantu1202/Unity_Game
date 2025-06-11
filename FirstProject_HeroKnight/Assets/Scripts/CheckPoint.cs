using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private GameManager gameManager;
    public Transform respawnPoint;

    SpriteRenderer spriteRenderer;
    public Sprite passive, active;
    Collider2D coll;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.UpdateCheckPoint(respawnPoint.position);
            spriteRenderer.sprite = active;
            coll.enabled = false;
        }
    }
}