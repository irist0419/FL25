using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using TMPro;
using UnityEngine;

public class NPCcontrol : MonoBehaviour
{
    [SerializeField] private string chatName;



    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                Say();
            }
        }
    }


    void Say()
    {
        Flowchart flowChart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        if (flowChart.HasBlock(chatName))
        {
            flowChart.ExecuteBlock(chatName);
        }
    }
}  