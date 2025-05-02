using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charactermovment : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    Animator animator;
    float x;
    float y;
    private PauseMenuManager pausedMenu;
    public bool canMove = true;

    Vector3 mousePosition;
    Vector3 direct;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenuManager.IsGamePaused)
            return;

        InputManager();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            MovementManager();
        }
        RotationCharacter();
        
    }

    void InputManager()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        animator.SetBool("iswalking", x !=0 || y !=0);
        if(x ==0 && y ==0 && !animator.GetBool("isarmedanim")) {
            animator.CrossFade("idle_animation", 0.2f, 0);
        }
        else if (x == 0 && y == 0 && animator.GetBool("isarmedanim"))
        {
            animator.CrossFade("armed", 0.2f, 0);
        }


    }

    void MovementManager()
    {
        rb.velocity = new Vector2(x *speed, y * speed);
    }

    void RotationCharacter()
    {
        mousePosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - cam.transform.position.z));
        rb.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((mousePosition.y - transform.position.y), (mousePosition.x - transform.position.x)) * Mathf.Rad2Deg);
    }
}
