using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item item;

    private PlayerWeaponManager pw;
    private bool isPlayerInTrigger = false; // Флаг для проверки, находится ли игрок в триггере
    SpriteRenderer sr;

    void Start()
    {
        pw = FindObjectOfType<PlayerWeaponManager>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Проверяем нажатие правой кнопки мыши и наличие игрока в триггере
        if (isPlayerInTrigger && Input.GetMouseButtonDown(1))
        {
            StartCoroutine(WaitAndPickup());
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.name == "Player")
        {
            isPlayerInTrigger = true;
            col.GetComponent<PlayerWeaponManager>().inTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.name == "Player")
        {
            isPlayerInTrigger = false;
            col.GetComponent<PlayerWeaponManager>().inTrigger = false;
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
        Destroy(gameObject);
        pw.playAnimwhenWeaponishold();
        
    }
}
