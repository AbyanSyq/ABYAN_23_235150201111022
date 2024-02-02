using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    
    public Rigidbody2D playerRB;
    public Transform bulletSpawnPoint;
    public GameObject DeathAnim;


    public GameObject bullet;//Customable
    public GameObject lightBullet;
    public GameObject SniperBullet;
    public GameObject RoketBullet;


    public float timer;
    public float fireRate;//Customable
    public float health;//Customable
    public float moveSpeed;//Customable
    public float tankRotationSpeed;//Customable
    public float damage;
    public float headRotationSpeed;
    
    public Slider slider;
    public Image healthImage;
    public Gradient colorGradient;

    public Slider fireSlider;
    public Image fireImage;
    
    Vector2 movement;
    private void Start() {
        SetMaxHealth(health);
        fireSlider.maxValue = fireRate;
    }


    private void Update() {
        timer += Time.deltaTime;
        InputManager();
        FireRateBar();
    }
    private void FixedUpdate() {
            playerRB.velocity = (Vector2)transform.up * movement.y * moveSpeed * Time.fixedDeltaTime;
            playerRB.MoveRotation(transform.rotation * Quaternion.Euler(0,0, -movement.x * tankRotationSpeed *  Time.fixedDeltaTime));
        }
    private void InputManager(){
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if(Input.GetButton("Fire1")){
            if (timer >= fireRate)
            {
                Shoot();
                timer = 0;
            }
        }
    }
    void Shoot(){
        GameObject bulletOBject = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
    
    public void SetMaxHealth(float health){// add max health to slider and color
        slider.maxValue = health;
        slider.value = health;
        healthImage.color = colorGradient.Evaluate(1f);
    }
    public void SetHealth(float magnitude){// edit health
        Debug.Log(magnitude);
        health += magnitude;
        if (health >= slider.maxValue)
        {
            health = slider.maxValue;
        }
        slider.value = health;
        if(health <= 0){
            destroy();
        }
        healthImage.color = colorGradient.Evaluate(slider.normalizedValue);
    }
    public void FireRateBar(){
        fireSlider.value = timer;
    }
    public void destroy(){
        Debug.Log("MATIII");
        FindAnyObjectByType<MenuManager>().LoseCondiditon();
        GameObject Anim = Instantiate(DeathAnim, transform.position, Quaternion.Euler(0,0,transform.rotation.z));
        Destroy(Anim, 0.5f);
        Destroy(gameObject);
    }

    public void TankPowerSetUp(int magnitude){
        // FindAnyObjectByType<PlayerBullet>().BulletSetUp(magnitude); gak bisa soalnya in prefabs 
        switch (magnitude)
        {
            case 1:
                bullet = lightBullet;
                fireRate =  0.1f;
                health = 600f;
                moveSpeed = 600f;
                tankRotationSpeed = 400f;
                headRotationSpeed = 400f;
                damage = 100;
                break;
            case 2:
                bullet = SniperBullet;
                fireRate =  0.8f;
                health = 400f;
                moveSpeed = 400;
                tankRotationSpeed = 300f;
                headRotationSpeed = 500f;
                damage = 1000;
                break;
            case 3:
                bullet = RoketBullet;
                fireRate =  0.6f;
                health = 1500f;
                moveSpeed = 150f;
                tankRotationSpeed = 200f;
                headRotationSpeed = 100f;
                damage = 500;
                break;
            default:break;
        }
        FindAnyObjectByType<HeadMov>().headRotationSetUp(headRotationSpeed);
    }
}
