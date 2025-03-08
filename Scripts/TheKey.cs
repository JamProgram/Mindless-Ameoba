using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheKey : MonoBehaviour
{
     public Component doorcolliderhere;
     public GameObject KeyGone;
     public GameObject showUI = null;

void OnTriggerStay(Collider other) {
if(other.CompareTag("Player") )

doorcolliderhere.GetComponent<BoxCollider>().enabled = true;
showUI.SetActive(true);

if(other.CompareTag("Player") )
KeyGone.SetActive(false);
showUI.SetActive(false);
}
}
