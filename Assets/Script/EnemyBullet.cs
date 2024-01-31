using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Rigidbody2D bulletRB;
    public GameObject DestroyAnim;
    public float bulletSpeed;
    public float bulletDamage;
    public SpriteRenderer bulletSprite;
    public Sprite body;

    private void Awake() {
        bulletRB = GetComponent<Rigidbody2D>();
        bulletSprite.sprite = body;
    }
    private void Start() {
        bulletRB.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        Invoke("DestroyCondition", 3);
    } 
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player"))
        {   
            FindAnyObjectByType<PlayerScript>().SetHealth(-bulletDamage);
        }
        DestroyCondition();
    }
    
    void DestroyCondition(){
        GameObject Anim = Instantiate(DestroyAnim, transform.position, Quaternion.Euler(0,0,transform.rotation.z));
        Destroy(Anim, 0.5f);
        Destroy(gameObject);
    }
}
