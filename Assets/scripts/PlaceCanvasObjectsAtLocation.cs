using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CesiumForUnity;
using Unity.Mathematics;
public class PlaceObjectsOnLocation : MonoBehaviour
{
    public GameObject canvasParent;

    // Координаты района Õismäe, Таллин
    public double latitude = 59.4133;
    public double longitude = 24.6550;
    public double height = 50.0; // можно увеличить, если внутри земли

    void Start()
    {
        if (canvasParent == null)
        {
            Debug.LogError("Canvas Parent is not assigned.");
            return;
        }

        foreach (Transform child in canvasParent.transform)
        {
            CesiumGlobeAnchor anchor = child.GetComponent<CesiumGlobeAnchor>();
            if (anchor == null)
            {
                anchor = child.gameObject.AddComponent<CesiumGlobeAnchor>();
            }

            anchor.longitudeLatitudeHeight = new double3(longitude, latitude, height);
        }
    }
}
