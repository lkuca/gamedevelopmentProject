using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class MoneyPickup : MonoBehaviour
{
    public int amount = 10;

    private bool playerInRange = false;

    public void SetAmount(int value)
    {
        amount = value;
    }

    void Update()
    {
        if (playerInRange && Input.GetMouseButtonDown(1))
        {
            MoneyManager.instance.AddMoney(amount);
            UIHintManager hint = FindObjectOfType<UIHintManager>();
            hint?.HideHint();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
        UIHintManager hint = FindObjectOfType<UIHintManager>();
        hint?.ShowHint("ПКМ — подобрать деньги");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
        UIHintManager hint = FindObjectOfType<UIHintManager>();
        hint?.HideHint();
    }
}
