using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public string curWeaponType;
    public bool inTrigger = false;
    public Transform firePoint; // Точка появления пули
    public GameObject projectilePrefab; // Префаб пули
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        WeaponManager();
        HandleShooting();
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

    public void dropWeapon(string weapon)
    {
        if (curWeaponType != "Null")
        {
            Vector3 newpose = new Vector3(transform.position.x, transform.position.y, 0);
            Instantiate(Resources.Load("Prefabs/Items/" + weapon), newpose, Quaternion.identity);
            if (!inTrigger)
                curWeaponType = "Null";
        }
        else
        {
            Debug.Log("weapon is null");
        }
    }

    public void setcurweapon(string weapon)
    {
        curWeaponType = weapon;
    }

    public void playAnimwhenWeaponishold()
    {
        animator.SetBool("isarmedanim", true);
        animator.CrossFade("armed", 0.2f, 0);
    }
}
