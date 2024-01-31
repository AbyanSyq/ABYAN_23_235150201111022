using UnityEngine;

public class EnemyShootMechanism : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bullet;
    public LayerMask targetLayerMask;
    public bool inRange = false;
    public float fireRate;
    public float fireRange;

    private void Start() {
        InvokeRepeating("Shoot",0f,fireRate);
    }
    private void Update() {
        inRange = Physics2D.OverlapCircle(transform.position, fireRange,  targetLayerMask);
    }
    void Shoot(){
        if (inRange){
            GameObject bulletOBject = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, fireRange);
    }
}