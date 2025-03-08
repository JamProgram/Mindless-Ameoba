using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Weapon : MonoBehaviour
{

 private Animator anim;
 private AudioSource audioSource;
	
   public float range = 100f;
   public int bulletsPerMag=30;
   public int bulletsLeft=200;   

   public int currentBullets;

   public enum ShootMode{Auto, Semi}
   public ShootMode shootingMode;

   public Text ammoText;

   public Transform shootPoint;
   public GameObject hitParticles;
   public GameObject bulletImpact;
   public ParticleSystem muzzleFlash;
   public AudioClip shootSound;
	

   public float fireRate =0.1f;
   public float damage = 20f;
   float fireTimer;
   private bool isReloading;
   private bool shootInput;

private Vector3 originalPosition;
public Vector3 aimPosition;
public float aodSpeed =8f;

    // Start is called before the first frame update
    void Start()
    {

	anim = GetComponent<Animator>();
	audioSource = GetComponent<AudioSource>();

       currentBullets = bulletsPerMag;
       originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

      switch(shootingMode){
        case ShootMode.Auto:
          shootInput =Input.GetButton("Fire1");
        break;
            
        case ShootMode.Semi:
          shootInput =Input.GetButtonDown("Fire1");
        break;
      }
        
 if (shootInput)  
        {
		if(currentBullets>0)
			Fire();
		else if(bulletsLeft >0)
			DoReload();
	}

	if(Input.GetKeyDown(KeyCode.R)) {
		if(currentBullets < bulletsPerMag && bulletsLeft > 0)
		DoReload();
}

	if (fireTimer < fireRate)
          fireTimer += Time.deltaTime;

        AimDownSights();  
    }

	void FixedUpdate()
{
  	AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

	if(info.IsName("Fire")) anim.SetBool("Fire", false);

      isReloading = info.IsName("Reload");
} 

private void AimDownSights(){
  if(Input.GetButton("Fire2") && !isReloading){
    
    transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition,Time.deltaTime * aodSpeed);
  }
  else{
    transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition,Time.deltaTime * aodSpeed);
  }
}
    private void Fire() 
	{

		if(fireTimer < fireRate || currentBullets<=0 || isReloading) return;
            
		
		RaycastHit hit;
           
            if(Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range))
{

		Debug.Log(hit.transform.name + "Found!");

	 GameObject hitParticleEffect = Instantiate(hitParticles, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal) );
   GameObject bulletHole = Instantiate(bulletImpact, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal) );

   	Destroy(hitParticleEffect, 2f);
	Destroy(bulletHole, 2f);

    if(hit.transform.GetComponent<HealthController>()){
        hit.transform.GetComponent<HealthController>().ApplyDamage(damage);
     } else if(hit.transform.GetComponent<ZombieHealth>()){
        hit.transform.GetComponent<ZombieHealth>().ApplyDamage(damage);
     }

		}

		
		anim.CrossFadeInFixedTime("Fire", 0.01f);
	      muzzleFlash.Play();
            PlayShootSound();

          currentBullets--;
          UpdateAmmoText();
          fireTimer=0.0f;  //resets timer

    }

  public void Reload()
{
     if(bulletsLeft <=0) return;
	
	int bulletsToload =bulletsPerMag - currentBullets;
	int bulletsToDeduct =(bulletsLeft >= bulletsToload) ? bulletsToload : bulletsLeft; 

	bulletsLeft -= bulletsToDeduct;
	currentBullets += bulletsToDeduct;	

        UpdateAmmoText();
}

  private void DoReload(){

	AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

	if(isReloading) return;
	
	anim.CrossFadeInFixedTime("Reload", 0.01f);

}

   private void PlayShootSound() 
{

   audioSource.PlayOneShot(shootSound);
   //audioSource.clip = shootSound;
   //audioSource.Play();
}

   private void UpdateAmmoText() {

  ammoText.text = currentBullets + " / " + bulletsLeft;

  }
}