using UnityEngine;

public class ShootMechanism : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bullet;
    public LayerMask enemyLayerMask;

    public float BulletSpeed;
    public float fireRange;
    public bool inRange = false;


    private void Update() {
        inRange = Physics2D.OverlapCircle(transform.position, fireRange,  enemyLayerMask);
    }
    private void InputManager(){
        if(Input.GetButtonDown("Fire1") && inRange){
            Shoot();
        }
    }
    void Shoot(){
        GameObject bulletOBject = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
}
