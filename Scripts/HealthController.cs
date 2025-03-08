
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    
    [SerializeField] public float health =100f;
   
    // Start is called before the first frame update


   public void ApplyDamage(float damage){
    	health -= damage;

	if(health <= 0f) {
           health = 0f;
	   Destroy(gameObject);
           
      }

   }
}


