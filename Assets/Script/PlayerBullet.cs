using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Rigidbody2D bulletRB;
    public GameObject DestroyAnim;
    public float bulletSpeed;
    // public float bulletDamage;
    public SpriteRenderer bulletSprite;
    public Sprite ligthBullet;
    public Sprite sniperBullet;
    public Sprite rocketBullet;

    private void Awake() {
        bulletRB = GetComponent<Rigidbody2D>();
    }
    public void BulletSetUp(int magnitude){
        switch (magnitude)
        {
            case 1: 
                bulletSpeed = 30;
                bulletSprite.sprite = ligthBullet;
                break;
            case 2: 
                bulletSpeed = 55;
                bulletSprite.sprite = sniperBullet;
                break;
            case 3: 
                bulletSpeed = 18;
                bulletSprite.sprite = rocketBullet;
                break;
            default:break;
        }
    }
    private void Start() {
        bulletRB.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        Invoke("DestroyCondition", 3);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        DestroyCondition();
    }
    
    void DestroyCondition(){
        GameObject Anim = Instantiate(DestroyAnim, transform.position, Quaternion.Euler(0,0,transform.rotation.z));
        Destroy(Anim, 0.5f);
        Destroy(gameObject);
    }
}
