using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    // singleton
    public static PlayerHealthController instance;

    private void Awake()
    {
        instance = this;
    }

    public int currentHealth, maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer()
    {
        currentHealth--;

        // If player is dead
        if (currentHealth <= 0)
        {
            currentHealth = 0; // If for some reason player health is -, set to 0 for display
            gameObject.SetActive(false);    // Deactivate player
        }
    }
}
