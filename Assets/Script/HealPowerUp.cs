using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPowerUp : MonoBehaviour
{
    public GameObject player;
    private int HealChange;
    private void Start() {
        HealChange = 1;
    }

    private void OnTriggerEnter2D(Collider2D collison) {
        if (collison.gameObject.CompareTag("Player"))
        {
            if (HealChange > 0)
            {
                player.GetComponent<PlayerScript>().SetHealth(+2000);
                HealChange--;
            }
        }
    }
}
