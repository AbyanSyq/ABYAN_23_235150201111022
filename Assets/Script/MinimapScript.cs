using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    public Transform player;
    private void LateUpdate() {
        transform.position = new Vector3(player.position.x, player.position.y,transform.position.z);//so the z position is not change
    }
}
