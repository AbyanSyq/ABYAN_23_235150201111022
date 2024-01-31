using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    
    public Rigidbody2D playerRB;
    public Transform bulletSpawnPoint;
    public GameObject bullet;//Customable
    public float timer;
    public float fireRate;//Customable
    public float health;//Customable
    public float moveSpeed;//Customable
    public float tankRotationSpeed;//Customable
    
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
        Destroy(gameObject);
        FindAnyObjectByType<GameManager>().LoseCondiditon();
    }

    public void TankPowerSetUp(int magnitude){
        // FindAnyObjectByType<PlayerBullet>().BulletSetUp(magnitude); gak bisa soalnya in prefabs 
        switch (magnitude)
        {
            case 1:
                fireRate =  0.1f;
                health = 500f;
                moveSpeed = 600f;
                tankRotationSpeed = 400f;
                FindAnyObjectByType<HeadMov>().headRotationSetUp(400);
                break;
            case 2:
                fireRate =  1.2f;
                health = 300f;
                moveSpeed = 400;
                tankRotationSpeed = 300f;
                FindAnyObjectByType<HeadMov>().headRotationSetUp(200);
                break;
            case 3:
                fireRate =  1.2f;
                health = 800f;
                moveSpeed = 150f;
                tankRotationSpeed = 100f;
                FindAnyObjectByType<HeadMov>().headRotationSetUp(100);
                break;
            default:break;
        }
    }
}
