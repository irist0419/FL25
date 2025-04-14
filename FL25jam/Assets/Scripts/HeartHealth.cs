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

    private List<Image> hearts = new List<Image>();

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
            int heartHealth = (currentHealth - (i * 2));
            if (heartHealth >= 2)
                hearts[i].sprite = fullHeart;
            else if (heartHealth == 1)
                hearts[i].sprite = halfHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        UpdateHearts();
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UpdateHearts();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            TakeDamage(1); 
        }

        if (Input.GetKeyDown(KeyCode.H)) {
            Heal(1);
        }
    }
}