using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public string curWeaponType; 
    public bool inTrigger = false;
    Animator animator;
    //public static bool isarmedAnim;


    void Start () {
        animator = GetComponent<Animator>();

    }


    public void Update () {
        WeaponManager();
    }

    void WeaponManager()
    {
        if(Input.GetMouseButtonDown(1) && !inTrigger)
        {
            dropWeapon(curWeaponType);
            animator.SetBool("isarmedanim", false);
            animator.CrossFade("idle_animation", 0.2f, 0);
        }
        
    }
    public void dropWeapon(string weapon)
    {
        if(curWeaponType != "Null")
        {
            //isarmedAnim = true;
            //Debug.Log("net naturala otvet");
            //animator.SetBool("isarmedanim", true);
            //animator.CrossFade("armed", 0.2f, 0);
            Vector3 newpose= new Vector3(transform.position.x, transform.position.y, 0);
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

        curWeaponType= weapon;
       

        
    }

    public void playAnimwhenWeaponishold()
    {
        Debug.Log("oi");
        animator.SetBool("isarmedanim", true);
        animator.CrossFade("armed", 0.2f, 0);

    }
}
