
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class FollowAI : MonoBehaviour
{

public enum States {Follow, Attack}
private NavMeshAgent agent;
public Transform target;
//new code!!
public float attackDistance =1f;
public States currentState;
public bool inSight;
private Vector3 directionToTarget;
 public Animator animate;
public ZombieHealth MyZombieHealth;
public Player myplayer;
public AudioSource audioSource;


    // Start is called before the first frame update
     private void Start()
    {
    
	if (agent == null){

        agent = GetComponent<NavMeshAgent>();
	
       }
         
        animate = MyZombieHealth.GetComponent<Animator>();
        animate.SetBool("attack", false);
        
     
    }

    // Update is called once per frame
    private void Update()
{
  UpdateStates();
  CheckForPlayer();
  CheckforDeath();

}

private void UpdateStates() {

switch(currentState) {
      case States.Follow:
      Follow();
      break;
 
      case States.Attack:
      Attack();
      break;
  }

}



private void CheckForPlayer() {

 directionToTarget = target.position - transform.position;
 RaycastHit hitInfo;

if(Physics.Raycast(transform.position, directionToTarget.normalized, out hitInfo)) {

   inSight = hitInfo.transform.CompareTag("Player");
}

}


private void Follow() {

if(agent.remainingDistance <= attackDistance && inSight) {

agent.ResetPath();
currentState = States.Attack;
}
else
{

if (target !=null)
    {
        agent.SetDestination(target.position);
  if(!inSight) {
	myplayer.RestoreHealth(1); 
         }
      }
  }
}


private void Attack() {
  if(!inSight) {

   currentState = States.Follow;
   animate.SetBool("attack", false);
   audioSource.Play();
   
}
else {
    LookAtTarget();
    animate.SetBool("attack", true);
    myplayer.TakeDamage(1);
   }  
}

private void LookAtTarget(){

Vector3 lookDirection = directionToTarget;
lookDirection.y = 0f;
Quaternion lookRotation = Quaternion.LookRotation(lookDirection);

transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * agent.angularSpeed);

}

public void CheckforDeath() {
 
if (MyZombieHealth.health ==0)
            {
                currentState = States.Follow; 
            }
   }

}