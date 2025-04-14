using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeartHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int currentHealth;

    [SerializeField] private Image heartPrefab;
    [SerializeField] private Transform heartContainer;

    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;

    public List<Image> hearts = new List<Image>();

    void Start()
    {
        currentHealth = maxHealth;
        DrawHearts();
    }

    void DrawHearts()
    {
        // Clear existing
        foreach (Transform child in heartContainer)
        {
            Destroy(child.gameObject);
        }
        hearts.Clear();

        // Draw new
        int totalHearts = maxHealth / 2;
        for (int i = 0; i < totalHearts; i++)
        {
            Image heart = Instantiate(heartPrefab, heartContainer);
            hearts.Add(heart);
        }

        UpdateHearts();
    }

    public void UpdateHearts()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            int heartValue = Mathf.Clamp(currentHealth - (i * 2), 0, 2);

            switch (heartValue)
            {
                case 2:
                    hearts[i].sprite = fullHeart;
                    break;
                case 1:
                    hearts[i].sprite = halfHeart;
                    break;
                default:
                    hearts[i].sprite = emptyHeart;
                    break;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        //Debug.Log("Took damage: " + amount + ", currentHealth: " + currentHealth);
        UpdateHearts();
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UpdateHearts();
    }
    
}