using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnterTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject car;
    public Transform seatPoint; // точка, куда переместится игрок в машине

    private bool isPlayerInside = false;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == player && Input.GetKeyDown(KeyCode.E))
        {
            EnterCar();
        }
    }

    void EnterCar()
    {
        player.SetActive(false); // можно заменить на скрытие
        car.GetComponent<CarController>().enabled = true;
        isPlayerInside = true;
    }

    void ExitCar()
    {
        car.GetComponent<CarController>().enabled = false;
        player.transform.position = car.transform.position + Vector3.left; // респавн рядом
        player.SetActive(true);
        isPlayerInside = false;
    }
}

