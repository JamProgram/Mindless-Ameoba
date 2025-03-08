using System.Collections;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    [SerializeField] public float health = 100f;
    public Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
        } else if (!IsDead)
            {
	       animator.SetBool("death", false);
            }
    }

    public bool IsDead
    {
        get
        {
            return health <= 0f;
        }
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;

        if (health <= 0f)
        {
            health = 0f;

            if (IsDead)
            {
                // Enable the animator and trigger the death animation
                animator.SetBool("death", true);

                // Start the coroutine to handle destruction after the animation
                StartCoroutine(DestroyAfterAnimation());
            }
            
        }
    }
//new code!
    private IEnumerator DestroyAfterAnimation()
    {
        // Wait for the duration of the death animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Destroy the game object after the animation completes
        //Destroy(gameObject,2f); 

       gameObject.SetActive(false);
      
    }
}
