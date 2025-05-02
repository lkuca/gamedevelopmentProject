using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public int money = 100;
    public float wanderSpeed = 1f;
    private Vector2 direction;
    private Rigidbody2D rb;

    private bool isTalking = false;
    private bool isPanicking = false;
    private float panicTime = 5f;

    private bool hasTalked = false; // ✅ Новый флаг
    public Sprite deadSprite; // Спрайт мертвого тела
    public GameObject moneyDropPrefab; // Префаб дропа денег
    private bool isDead = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating(nameof(ChangeDirection), 0, 3f);
    }

    void FixedUpdate()
    {
        if (isDead) return;

        if (!isTalking && !isPanicking)
        {
            rb.velocity = direction * wanderSpeed;
        }
    }

    void ChangeDirection()
    {
        direction = Random.insideUnitCircle.normalized;
    }

    public void Talk()
    {
        if (isDead) return;

        if (hasTalked)
        {
            DialogueUI.instance.ShowDialogue("Я уже всё сказал.");
            return;
        }

        isTalking = true;
        rb.velocity = Vector2.zero;

        int chance = Random.Range(0, 100);
        if (chance < 10 && money > 0)
        {
            DialogueUI.instance.ShowDialogue("На, держи немного денег.");
            MoneyManager.instance.AddMoney(10);
            money -= 10;
        }
        else
        {
            DialogueUI.instance.ShowDialogue("У меня нет денег!");
        }

        hasTalked = true; // ✅ Даже если не дал — считается, что попытка была
        Invoke(nameof(EndTalk), 2f);
    }

    public void Steal()
    {
        if (isDead) return;

        if (money > 0)
        {
            MoneyManager.instance.AddMoney(money);
            DialogueUI.instance.ShowDialogue("Ты меня обокрал!");
            money = 0;
        }
        else
        {
            DialogueUI.instance.ShowDialogue("У меня уже ничего нет!");
        }

        StartCoroutine(Panic());
    }

    public void Kill()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true; // Отключаем влияние физики
        rb.simulated = false;  // Полностью исключаем из симуляции
        wanderSpeed = 0f;
        if (isDead) return; // Чтобы не убивать дважды
        isDead = true;

        // Прекращаем движение
        rb.velocity = Vector2.zero;
        CancelInvoke(nameof(ChangeDirection));
        StopAllCoroutines();

        // Меняем спрайт
        GetComponent<SpriteRenderer>().sprite = deadSprite;

        // Отключаем коллайдер или меняем слой
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast"); // или свой отдельный слой

        // Дропаем деньги
        if (money > 0 && moneyDropPrefab != null)
        {
            Instantiate(moneyDropPrefab, transform.position, Quaternion.identity);
        }

        money = 0;

        // Удаляем NPC через 5 секунд
        Destroy(gameObject, 5f);
    }

    IEnumerator Panic()
    {
        isPanicking = true;
        for (float t = 0; t < panicTime; t += Time.deltaTime)
        {
            rb.velocity = Random.insideUnitCircle.normalized * 2f;
            yield return null;
        }
        isPanicking = false;
    }

    void EndTalk()
    {
        isTalking = false;
    }
}

