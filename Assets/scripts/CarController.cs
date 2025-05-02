using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float maxSpeed = 20f;
    public float acceleration = 5f;
    public float turnSpeed = 100;
    public Transform firePoint;
    public float brakeForce = 10f; // Добавьте это
    private float currentSpeed = 0f;
    public CarDoorTrigger doorTrigger;
    private Rigidbody2D rb;
    private float moveInput;
    private float turnInput;
    private Vector2 preservedMomentum = Vector2.zero;
    private bool isPreservingMomentum = false;
    void Start()
    {
        enabled = false;
        rb = GetComponent<Rigidbody2D>(); // Машина неактивна, пока игрок не зашел
        
    }

    void Update()
    {
        // Движение машины (управление аналогично машинному)
        moveInput = Input.GetAxis("Vertical"); // W/S или Up/Down
        turnInput = Input.GetAxis("Horizontal"); // A/D или Left/Right

        // Выход из машины (по кнопке E)
        if (Input.GetKeyDown(KeyCode.E))
        {
            ExitCar();
        }
    }

    void FixedUpdate()
    {
        float speed = rb.velocity.magnitude;
        Vector2 forward = transform.up;

        if (Input.GetKey(KeyCode.Space))
        {
            // Применение тормозной силы
            Vector2 brakeForceVector = -rb.velocity.normalized * brakeForce;
            rb.AddForce(brakeForceVector);
        }
        else
        {
            // Движение (вперёд/назад)
            if (Mathf.Abs(speed) < maxSpeed)
            {
                rb.AddForce(forward * moveInput * acceleration);
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (!isPreservingMomentum)
            {
                preservedMomentum = rb.velocity.normalized;
                isPreservingMomentum = true;
            }

            // Сохраняем импульс: машина будет скользить в сохранённом направлении
            float preservedSpeed = rb.velocity.magnitude;
            rb.velocity = Vector2.Lerp(rb.velocity, preservedMomentum * preservedSpeed, 1.0f);
        }
        else
        {
            isPreservingMomentum = false;
        }

        // Поворот
        if (speed > 0.1f)
        {
            float turnMultiplier;
            float lowSpeedThreshold = 3f; // порог для уменьшения поворота
            float highSpeedThreshold = 20f; // порог для облегчения поворота при высокой скорости
            float minTurnMultiplier = 0.2f; // минимальный множитель для поворота

            // Проверяем, если ручник нажат и скорость выше 20, делаем поворот как при низкой скорости
            if (Input.GetKey(KeyCode.Space) && speed > highSpeedThreshold)
            {
                // Имитация поведения низкой скорости
                turnMultiplier = Mathf.Lerp(minTurnMultiplier, 1f, speed / highSpeedThreshold);
                // или просто задаем минимальный для высокой скорости, чтобы поворот был менее чувствительным
                turnMultiplier = minTurnMultiplier;
            }
            else
            {
                // обычное поведение
                if (speed < lowSpeedThreshold)
                {
                    turnMultiplier = Mathf.Lerp(minTurnMultiplier, 1f, speed / lowSpeedThreshold);
                }
                else if (speed < highSpeedThreshold)
                {
                    float t = (speed - lowSpeedThreshold) / (highSpeedThreshold - lowSpeedThreshold);
                    turnMultiplier = Mathf.Lerp(1f, minTurnMultiplier, t);
                }
                else
                {
                    turnMultiplier = minTurnMultiplier;
                }
            }

            float rotation = -turnInput * turnSpeed * Time.fixedDeltaTime * turnMultiplier;
            rb.MoveRotation(rb.rotation + rotation);
        }

        // БОКОВОЕ трение (для уменьшения скольжения вбок)
        if (!Input.GetKey(KeyCode.Space))
        {
            Vector2 lateralVelocity = Vector2.Dot(rb.velocity, transform.right) * transform.right;
            rb.velocity -= lateralVelocity * 0.3f; // убираем боковое скольжение
        }

        // ПЕРЕНАПРАВЛЕНИЕ при малой скорости
        if (speed < 2f)
        {
            // Просто жёстко перенаправляем velocity в сторону взгляда
            rb.velocity = Vector2.Lerp(rb.velocity, forward * speed * Mathf.Sign(moveInput), 0.5f);
        }
    }

    void ExitCar()
    {
        // Возвращаем игрока обратно на землю рядом с машиной
        doorTrigger.ExitCar();
    }
}