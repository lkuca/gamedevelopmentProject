using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CesiumForUnity;
using Unity.Mathematics;
public class SpawnToOismae : MonoBehaviour
{
    public GameObject canvasContainer; // Canvas(1)

    void Start()
    {
        // Создаём временный объект с GlobeAnchor
        GameObject temp = new GameObject("GeoTemp");
        var anchor = temp.AddComponent<CesiumGlobeAnchor>();

        // Устанавливаем координаты Õismäe, Tallinn (долгота, широта, высота)
        anchor.longitudeLatitudeHeight = new double3(24.6456, 59.3967, 50.0);

        // Спавним всех детей Canvas в эту точку
        foreach (Transform child in canvasContainer.transform)
        {
            child.position = temp.transform.position;
        }

        // Удаляем временный объект
        Destroy(temp);
    }
}