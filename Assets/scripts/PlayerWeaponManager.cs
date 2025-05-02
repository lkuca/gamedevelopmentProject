using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    private PauseMenuManager pausemenu;
    public string curWeaponType;
    public bool inTrigger = false;
    public Transform firePoint; // Точка появления пули
    public Transform shell;
    public GameObject projectilePrefab; // Префаб пули
    public GameObject shelly;
    Animator animator;
    private UIHintManager ui;
    void Start()
    {
        animator = GetComponent<Animator>();
        ui = FindObjectOfType<UIHintManager>();
    }

    public void Update()
    {
        if (PauseMenuManager.IsGamePaused)
            return;

        WeaponManager();
        HandleShooting();
        HandleWeaponHints();


    }

    void HandleWeaponHints()
    {
        //if (curWeaponType != "Null")
        //{
        //    // Если оружие в руках, показываем подсказки для стрельбы и сброса
        //    ui?.ShowHint("Press LMB to shoot\nPress RMB to drop item");
        //}
        //else
        //{
        //    // Если оружия нет, скрываем подсказки
        //    ui?.ShowHint("Press RMB to pick up item");
        //}
    }

    void WeaponManager()
    {
        if (Input.GetMouseButtonDown(1) && !inTrigger)
        {
            dropWeapon(curWeaponType);
            animator.SetBool("isarmedanim", false);
            animator.CrossFade("idle_animation", 0.2f, 0);
        }
    }

    void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0) && curWeaponType != "Null")
        {
            Shoot();
            ammo();
        }
    }

    void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            Debug.LogWarning("Projectile Prefab или FirePoint не установлены!");
        }
    }
    void ammo()
    {
        if (shelly != null && shell != null)
        {
            Instantiate(shelly, shell.position, shell.rotation);
        }
        else
        {
            Debug.LogWarning("Projectile Prefab или FirePoint не установлены!");
        }
    }

    public void dropWeapon(string weapon)
    {
        if (curWeaponType != "Null")
        {
            Vector3 newpose = new Vector3(transform.position.x, transform.position.y, 0);
            Instantiate(Resources.Load("Prefabs/Items/" + weapon), newpose, Quaternion.identity);
            if (!inTrigger)
            {
                curWeaponType = "Null";
                // Скрываем подсказки при сбросе оружия
                ui?.HideHint();
            }
        }
        else
        {
            Debug.Log("weapon is null");
        }
    }

    public void setcurweapon(string weapon)
    {
        curWeaponType = weapon;
        if (ui != null)
        {
            ui.ShowHint("Press LMB to shoot or Press RMB to drop item");
        }
    }

    public void playAnimwhenWeaponishold()
    {
        
        animator.SetBool("isarmedanim", true);
        animator.CrossFade("armed", 0.2f, 0);
    }
}
