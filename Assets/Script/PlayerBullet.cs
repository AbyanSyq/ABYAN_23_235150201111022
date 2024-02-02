using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Rigidbody2D bulletRB;
    public GameObject DestroyAnim;
    public float damage;
    public float bulletSpeed;
    public int BulletTipe;
    public float radius = 0;

    private void Awake() {
        bulletRB = GetComponent<Rigidbody2D>();
    }
    public void SetDamage(float magnitude){
        damage = magnitude;
    }
 
    private void Start() {
        bulletRB.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        Invoke("DestroyCondition", 3);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if( collision.gameObject.CompareTag("Enemy")){
            collision.gameObject.GetComponent<EnemyAI>().SetHealth(-damage);
        }
        BulletEffect(BulletTipe);
        DestroyCondition();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if( collision.gameObject.CompareTag("Enemy")){
            collision.gameObject.GetComponent<EnemyAI>().SetHealth(-damage);
            BulletEffect(BulletTipe);
        }

    }

        public void BulletEffect(int tipe){  
            switch (tipe)
            {
                case 1: break;
                case 2: break;
                case 3: 

                Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, radius);
                foreach (Collider2D target in targets)
                {  
                    if (target.GetComponent<EnemyAI>() != null)
                    {
                        target.gameObject.GetComponent<EnemyAI>().SetHealth(-damage);
                    }
                    
                } 

                break;
                default:break;
            }
        }

    
    void DestroyCondition(){
        Debug.Log("bulletDestroy");
        GameObject Anim = Instantiate(DestroyAnim, transform.position, Quaternion.Euler(0,0,transform.rotation.z));
        Destroy(Anim, 0.5f);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
