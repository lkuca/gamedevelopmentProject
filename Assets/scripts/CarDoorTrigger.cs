using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDoorTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject car;
    public GameObject door; // Дверь, которая будет открываться
    public Transform seatPoint; // Позиция в машине для игрока
    public float openDoorDistance = 2f; // Расстояние для открытия двери
    private bool isPlayerNear = false;
    [SerializeField] private AudioSource carMusic;
    private bool isDoorOpen = false;
    private void Update()
    {
        float distanceToCar = Vector3.Distance(player.transform.position, door.transform.position);

        if (distanceToCar <= openDoorDistance && !car.GetComponent<CarController>().enabled)
        {
            // Открыть дверь только один раз
            if (!isDoorOpen)
            {
                OpenDoor();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                EnterCar();
            }
        }
        else
        {
            // Закрыть дверь, если она открыта и игрок ушёл
            if (isDoorOpen)
            {
                CloseDoor();
            }
        }
    }

    void OpenDoor()
    {
        door.transform.Rotate(0, 0, -63); // Открываем на 90 градусов
        isDoorOpen = true;
    }

    void CloseDoor()
    {
        // Закрытие двери, если игрок далеко
        door.transform.Rotate(0, 0, 63); // Закрываем обратно
        isDoorOpen = false;
    }

    void EnterCar()
    {
        // Если игрок вблизи, скрываем его и активируем управление машиной
        player.SetActive(false);
        car.GetComponent<CarController>().enabled = true;
        player.transform.position = seatPoint.position; // Перемещаем игрока внутрь машины
        if (carMusic != null)
        {
            // Включаем компонент
            carMusic.enabled = true;

            // Запускаем музыку
            carMusic.Play();
        }
        else
        {
            
            Debug.LogWarning("AudioSource для музыки не назначен!");
        }

    }

    public void ExitCar()
    {
        // Выход из машины по кнопке E
        car.GetComponent<CarController>().enabled = false;
        player.SetActive(true);
        carMusic.Stop();
        
        player.transform.position = car.transform.position + Vector3.left; // Перемещаем на позицию рядом с машиной
    }
}

