using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 2f; // Расстояние взаимодействия
    private NPCController nearestNPC;

    void Update()
    {
        FindNearestNPC();

        if (nearestNPC != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                nearestNPC.Talk();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                nearestNPC.Steal();
            }
        }
    }

    void FindNearestNPC()
    {
        NPCController[] allNPCs = FindObjectsOfType<NPCController>();
        float minDist = interactRange;
        nearestNPC = null;

        foreach (var npc in allNPCs)
        {
            float dist = Vector2.Distance(transform.position, npc.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearestNPC = npc;
            }
        }
    }
}
