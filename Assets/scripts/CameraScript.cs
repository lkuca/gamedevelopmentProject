using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Transform player;               // Ссылка на игрока
    Vector3 offset;                 // Смещение камеры относительно игрока
    bool followCursor = false;      // Переменная для отслеживания, нужно ли следовать за курсором

    Vector3 lastCursorPosition;     // Хранение последней позиции курсора
    Vector3 cameraOffsetOnDetach;   // Смещение камеры относительно игрока при отсоединении

    Camera cam;                     // Ссылка на основную камеру

    void Start()
    {
        player = FindObjectOfType<charactermovment>().transform;  // Найдем объект игрока
        offset = transform.position - player.position;  // Сохраняем изначальное смещение камеры
        lastCursorPosition = Input.mousePosition;  // Инициализируем последнюю позицию курсора
        cameraOffsetOnDetach = Vector3.zero;  // Изначально камера будет следовать за игроком
        cam = Camera.main;  // Получаем ссылку на основную камеру
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        if (Input.GetKey(KeyCode.LeftShift))  // Если зажата клавиша LeftShift
        {
            followCursor = true;
            // Камера должна появиться на последней позиции игрока, но больше не следовать за ним
            if (cameraOffsetOnDetach == Vector3.zero)
            {
                transform.position = player.position + offset;
                cameraOffsetOnDetach = transform.position - new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            }
        }
        else
        {
            followCursor = false;
        }

        if (followCursor)
        {
            MoveCameraWithMouse();  // Камера отсоединена и следит за курсором
        }
        else
        {
            FollowPlayer();  // Камера снова следует за игроком
        }
    }

    // Камера следует за игроком
    void FollowPlayer()
    {
        // Камера возвращается на позицию игрока, добавляем смещение
        transform.position = player.position + offset;
    }

    // Камера отсоединена и двигается по позиции курсора
    void MoveCameraWithMouse()
    {
        // Получаем мировые координаты курсора
        Vector3 cursorWorldPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));

        // Камера теперь может изменять свою позицию по оси Z, так как больше нет фиксирования Z
        // Перемещаем камеру на позицию курсора
        transform.position = Vector3.Lerp(transform.position, cursorWorldPos, Time.deltaTime * 5f);  // Перемещаем камеру с плавной анимацией
    }
}
