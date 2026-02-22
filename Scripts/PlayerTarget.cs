using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerTarget : MonoBehaviour
{
    private float health;
    public float startHealth;
    public Image healthBar;


    void start()
    {
        health = startHealth;
    }

    public void PlayerTakesDamage(float damageAmount)
    {
        health -= damageAmount;


        if (health <= 0f)
        {
            Death();
        }

    }

    void Death()
    {
        SceneManager.LoadScene("MainMenu");
    }


}