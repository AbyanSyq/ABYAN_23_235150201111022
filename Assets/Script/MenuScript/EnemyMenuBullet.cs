using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMenuBullet : MonoBehaviour
{
    public Rigidbody2D bulletRB;
    public GameObject DestroyAnim;
    public float bulletSpeed;
    public float bulletDamage;

    private void Start() {
        bulletRB.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        Invoke("DestroyCondition", 3);
    } 
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Enemy")){
        collision.gameObject.GetComponent<EnemyMenuAI>().SetHealth(-bulletDamage);
        }
        DestroyCondition();
    }
    
    void DestroyCondition(){
        GameObject Anim = Instantiate(DestroyAnim, transform.position, Quaternion.Euler(0,0,transform.rotation.z));
        Destroy(Anim, 0.5f);
        Destroy(gameObject);
    }
}
