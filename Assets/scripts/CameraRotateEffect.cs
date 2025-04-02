using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateEffect : MonoBehaviour
{
    charactermovment pm;

    float mod = 1f;  // Уменьшаем шаг изменения угла для медленного качания
    float zVal = 0.0f;
    float rotationSpeed = 1f;  // Меньшая скорость вращения

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<charactermovment>();
    }

    // Update is called once per frame
    void Update()
    {
        // Проверяем, двигается ли персонаж
        float moveDirection = pm.GetComponent<Rigidbody2D>().velocity.x;  // Получаем скорость по оси X

        // Если персонаж не двигается, не будем менять угол камеры
        if (moveDirection == 0)
        {
            return;  // Выход из метода Update, камера не будет качаться
        }

        if (pm.canMove == true)
        {
            // Если персонаж движется вправо или влево, определяем направление вращения
            if (moveDirection > 0) // Если движется вправо
            {
                mod = rotationSpeed;  // Камера качается вправо
            }
            else if (moveDirection < 0) // Если движется влево
            {
                mod = -rotationSpeed;  // Камера качается влево
            }

            // Качаем камеру медленно
            zVal += mod * Time.deltaTime;  // Используем Time.deltaTime для плавности

            // Плавно меняем направление вращения при достижении определенного угла
            if (transform.eulerAngles.z >= 5.0f && transform.eulerAngles.z < 10.0f)
            {
                mod = -rotationSpeed;  // Если камера качнулась вправо, начинаем качать влево
            }
            else if (transform.eulerAngles.z < 355.0f && transform.eulerAngles.z > 350.0f)
            {
                mod = rotationSpeed;  // Если камера качнулась влево, начинаем качать вправо
            }

            // Применяем поворот камеры
            Vector3 rot = new Vector3(0, 0, zVal);
            this.transform.eulerAngles = rot;
        }
    }
}
