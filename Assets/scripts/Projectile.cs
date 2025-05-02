using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private PauseMenuManager pauseMenu;
    public float speed = 10f;

    void Update()
    {
        if (PauseMenuManager.IsGamePaused)
            return;

        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        NPCController npc = other.GetComponent<NPCController>();
        if (npc != null)
        {
            npc.Kill();
            Destroy(gameObject); // Уничтожаем пулю
        }
    }
}
