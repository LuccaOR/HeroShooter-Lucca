using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public CharacterStats PlayerStats;
    public CharacterStats myStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amout)
    {
        currentHealth -= amout;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    // Update is called once per frame
    void Die()
    {
        PlayerStats.AddKill();
        myStats.AddDeath();
        gameObject.SetActive(false);
    }
}
