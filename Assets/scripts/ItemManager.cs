using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item item;

    private PlayerWeaponManager pw;
    private bool isPlayerInTrigger = false;
    private PauseMenuManager pausemenu;
    private SpriteRenderer sr;
    private Color originalColor;

    private Vector3 originalScale;
    public float pulseSpeed = 2f;
    public float pulseAmount = 0.05f;
    public UIHintManager ui;
    void Start()
    {
        pw = FindObjectOfType<PlayerWeaponManager>();
        sr = GetComponent<SpriteRenderer>();
        ui = FindObjectOfType<UIHintManager>();

        if (sr != null)
        {
            originalColor = sr.color;
        }

        originalScale = transform.localScale;
    }

    void Update()
    {
        // Пульсация
        float scaleOffset = Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = originalScale + Vector3.one * scaleOffset;

        // Подбор оружия
        if (isPlayerInTrigger && Input.GetMouseButtonDown(1))
        {
            StartCoroutine(WaitAndPickup());
        }
        if (PauseMenuManager.IsGamePaused)
            return;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            pw.inTrigger = true;
            if (pw != null && ui != null)
            {
                ui.ShowHint("Press RMB to pick up item");
            }
            if (sr != null)
            {
                sr.color = Color.green; // Подсветка
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            pw.inTrigger = false;
            ui?.HideHint();
            if (sr != null)
            {
                sr.color = originalColor; // Возврат цвета
            }
        }
    }

    IEnumerator WaitAndPickup()
    {
        if (pw.curWeaponType != "Null")
        {
            
            pw.dropWeapon(pw.curWeaponType);
        }

        yield return new WaitForSeconds(0.05f);

        pw.curWeaponType = item.weaponType.ToString();
        pw.playAnimwhenWeaponishold();
        Destroy(gameObject);
        if (ui != null)
        {
            ui.ShowHint("Press LMB to shoot\nPress RMB to drop item");
        }
    }
}
