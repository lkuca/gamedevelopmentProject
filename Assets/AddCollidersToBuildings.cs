using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AddCollidersToBuildings : MonoBehaviour
{
    public Transform rootOfBuildings;         // Сюда перетащи объект типа CitySimulatorMap
    public string targetLayerName = "Buildings";
    public float delay = 2.5f;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);

        int layer = LayerMask.NameToLayer(targetLayerName);
        if (layer == -1)
        {
            Debug.LogError($"Слой '{targetLayerName}' не найден. Добавь его в Project Settings > Tags and Layers.");
            yield break;
        }

        foreach (Transform child in rootOfBuildings.GetComponentsInChildren<Transform>())
        {
            if (child.GetComponent<MeshFilter>() && child.GetComponent<MeshRenderer>())
            {
                if (child.GetComponent<Collider>() == null)
                {
                    var collider = child.gameObject.AddComponent<MeshCollider>();
                    collider.convex = false;
                    child.gameObject.layer = layer;

                    Debug.Log($"✅ Коллайдер добавлен к: {child.name}");
                }
            }
        }

        Debug.Log("🎉 Завершено: все здания теперь имеют MeshCollider и находятся на нужном слое.");
    }
}
