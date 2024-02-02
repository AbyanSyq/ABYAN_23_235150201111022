using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyAI>().SetHealth(-99999);
        }else if(collision.gameObject.CompareTag("Player")){
            collision.gameObject.GetComponent<PlayerScript>().SetHealth(-9999);
        }
    }
}
