using System.Collections;
using System.Collections.Generic;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using UnityEngine.WSA;

using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{
    Sprite[] walking, legsSpr, armedWalking; // Добавлено для вооружённого состояния
    int counter = 0, legCount = 0;
    charactermovment pm;
    float timer = 0.05f, legTimer = 0.05f;
    public SpriteRenderer torso, legs;
    SpriteContainer sc;
    public bool isArmed = false; // Флаг: вооружён ли игрок
    Animator animator;
    void Start()
    {
        pm = this.GetComponent<charactermovment>();
        sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpriteContainer>();
        walking = sc.getPlayerUnarmedWalk(); // Анимация ходьбы без оружия
        armedWalking = sc.getPlayerArmedWalk(); // Анимация ходьбы с оружием
        legsSpr = sc.getPlayerLegs(); // Анимация для ног
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        animateLegs();
        
        animateTorso();
    }

    void animateTorso()
    {
        if (pm.canMove)
        {
            // Выбираем правильный набор спрайтов: с оружием или без
            Sprite[] currentWalking = isArmed ? armedWalking : walking;

            torso.sprite = currentWalking[counter];
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                if (counter < currentWalking.Length - 1)
                {
                    counter++;
                }
                else
                {
                    counter = 0;
                }
                timer = 0.1f;
            }
        }
    }

    void animateLegs()
    {
        if (pm.canMove)
        {
            legs.sprite = legsSpr[legCount];
            legTimer -= Time.deltaTime;

            if (legTimer <= 0)
            {
                if (legCount < legsSpr.Length - 1)
                {
                    legCount++;
                }
                else
                {
                    legCount = 0;
                }
                legTimer = 0.05f;
            }
        }
    }
}
