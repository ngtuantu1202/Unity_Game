using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpointHandler : MonoBehaviour
{
    private GameManager gameManager;
    private Vector2 startPos;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager != null)
        {
            gameManager.playerTransform = this.transform;
            startPos = transform.position;
            gameManager.UpdateCheckPoint(startPos);
        }
    }

    public void Respawn()
    {
        if (gameManager != null)
        {
            transform.position = gameManager.checkpointPos;
            gameObject.SetActive(true);

            var rb = GetComponent<Rigidbody2D>();
            if (rb != null) rb.velocity = Vector2.zero;
        }
    }
}