using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyBase : MonoBehaviour
{

  [Header("Enemy Parts")]
   public GameObject sword;
   public GameObject bomb;
   public GameObject hand;
   public GameObject flash;
   public GameObject rifle;
   public GameObject rifle_arm;

   [Header("Enemy Variables")]
   public float speed;
   public bool hasBomb;
   public bool isShooting;
   private bool isAnimPlaying;

   [Header("Prefabs")]
   public GameObject bullet;
   public GameObject bomb_prefab;


   [Header("Animators")]
   public Animator bomb_anim;
   public Animator hand_anim;
   private Animator animator;

   private EnemyPatrol enemyPatrol;
   private void Start()
   {
       animator = GetComponent<Animator>();
               enemyPatrol = GetComponentInParent<EnemyPatrol>();

   }


  
    //Handle Animations
   void Update()
   {
     animator.SetFloat("speed",speed);
     if(isShooting  && !isAnimPlaying)
    {
      StartCoroutine(Shoot());
    }else if(hasBomb && !isAnimPlaying )
     {

         animator.SetBool("isBomb",true);
         bomb.SetActive(true);
         hand.SetActive(true);
         bomb_anim.SetFloat("speed",speed);
         hand_anim.SetFloat("speed",speed);
     }
     else if(!isAnimPlaying)
    {
      animator.SetBool("isBomb",false);
        bomb.SetActive(false);
        hand.SetActive(false);

    } 
   }
    

    public void Melee()
    {
        if(isAnimPlaying) return;
        StartCoroutine(Attack());
    }


    //Bomb Attack
    public void DropBomb()
    {
      if(!hasBomb) return;
     bomb newBomb =  Instantiate(bomb_prefab,bomb.transform.position,Quaternion.identity).GetComponent<bomb>();
      newBomb.movingLeft = enemyPatrol.movingLeft;
      bomb.SetActive(false);
      hasBomb = false;
      animator.SetBool("isBomb",false);
      animator.SetFloat("speed",0);

    }

  //Sword Attack
  private  IEnumerator Attack()
   {
       isAnimPlaying = true;
       animator.SetBool("isBomb",false);
       animator.SetFloat("speed",0);
       hand.SetActive(true);
       hand_anim.SetTrigger("melee");
       hasBomb = false;
       bomb.SetActive(false);
       sword.SetActive(true);
       animator.SetTrigger("melee");
       yield return new WaitForSeconds(0.5f);
       sword.SetActive(false);
       isAnimPlaying = false;
       
   }
    //Rifle Attack
   private IEnumerator Shoot()
   {
     animator.SetBool("isBomb",false);
     animator.SetFloat("speed",0);
     animator.Play("idle_side", 0, 0);
     animator.speed = 0;
     speed = 0;
     Hide();
     isAnimPlaying = true;
     rifle_arm.SetActive(true);
     rifle.SetActive(true);
     flash.SetActive(true);
     bullet Bullet  = Instantiate(bullet,flash.transform.position, Quaternion.identity).GetComponent<bullet>();
    Bullet.movingLeft = enemyPatrol.movingLeft;
     yield return new WaitForSeconds(0.3f);
     isAnimPlaying = false;
     rifle_arm.SetActive(false);
     rifle.SetActive(false);
     flash.SetActive(false);
     animator.speed = 1;
   }

   public IEnumerator DropAndResetBomb()
   {
     DropBomb();
     yield return new WaitForSeconds(2f);
     hasBomb = true;
   }


  public IEnumerator ShootForSeconds(int seconds)
  {
    isShooting = true;
    yield return new WaitForSeconds(seconds);
    isShooting = false;
  }


   private void Hide()
   {
     foreach(Transform child in transform)
     {
       child.gameObject.SetActive(false);
     }
   }

  


   

}
