using System.Collections;
using Mapbox.Unity.MeshGeneration.Modifiers;
using UnityEngine;

public class ApplyColliderLayerOverrides : MonoBehaviour
{
    public string includeLayerName = "Buildings";
    public int overridePriority = 0;
    public Transform rootOfBuildings;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2.5f);

        int includeLayer = LayerMask.NameToLayer(includeLayerName);
        if (includeLayer == -1)
        {
            Debug.LogError($"Слой '{includeLayerName}' не найден! Добавь его в Project Settings > Tags and Layers.");
            yield break;
        }

        int includeMask = 1 << includeLayer;

        foreach (Transform child in rootOfBuildings.GetComponentsInChildren<Transform>())
        {
            var collider = child.GetComponent<MeshCollider>();
            if (collider != null)
            {
#if UNITY_2022_2_OR_NEWER
                collider.includeLayers = includeMask;
                collider.excludeLayers = 0;
                collider.layerOverridePriority = overridePriority;
                Debug.Log($"✔ Обновлён MeshCollider у: {child.name}");
#else
                Debug.LogWarning("Layer overrides требуют Unity 2022.2 или выше.");
#endif
            }
        }
    }
}

