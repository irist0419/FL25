using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class HealthDisplay : MonoBehaviour
{
    private int health = 5;
    public Text healthText;

    void Update()
    {

        healthText.text = "HEALTH : " + health;
        //healthText.text = health.ToString();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            health--;
        }

    }
}
