using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{


 public int maxHealth = 8000;
 public int currentHealth;
 public HealthBar healthBar;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
     CheckGameOver();
    }


   public void TakeDamage(int damage) {


       if(currentHealth > 0) {
       currentHealth -= damage;
       healthBar.SetHealth(currentHealth);
       Debug.Log("Taking Damage. Current health: " + currentHealth);
    }
  }

  public void RestoreHealth(int health) {


      if(currentHealth < maxHealth ) {
       currentHealth += health;
       healthBar.SetHealth(currentHealth);
       Debug.Log("Restored health. Current health: " + currentHealth);
    }
} 


public void CheckGameOver() {
  
   if(currentHealth == 0) {
      
       Debug.Log("You Died, Game Over!");
       SceneManager.LoadScene("GameOver");  // loads gameova
       //healthBar.gameObject.SetActive(false);
     }
          
  } 

}
