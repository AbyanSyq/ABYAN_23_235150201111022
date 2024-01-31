using UnityEngine;
using Pathfinding;
using UnityEngine.AI;
using Unity.Mathematics;
using UnityEngine.UI;
using Unity.VisualScripting;


public class EnemyAI : MonoBehaviour
{
    Transform target;
    public Object healthBar;
    public float speed = 200f;
    public float nextWaitPointDistance = 3f;//jarak character ke waypoint sebelum gerak ke point selanjutnya

    Path path;//path charakter ke target
    int currentWayPoint = 0; 
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    public Rigidbody2D head;
    public float headRotationSpeed;


    public Slider slider;
    public float currenthealth;
    public float playerDamage;
    public Image healthImage;
    public Gradient colorGradient;

    private void Awake() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.GetComponent<Transform>();
        }
        FindAnyObjectByType<GameManager>().EnemyCount(1);
    }
    private void Start() {
        MaxHealth(currenthealth);   
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, .5f);//update path every 0.5 second
    }
    private void Update() {
        head.position = rb.position;
        HeadRotation();
    }
    void UpdatePath(){
        if(seeker.IsDone()){
        seeker.StartPath(rb.position, target.position, OnPathComplete);//searching a path form character position to target
        }
    }

    void OnPathComplete(Path p){
        if(!p.error){
            path = p;
            currentWayPoint = 0;
        }
    }

    private void FixedUpdate() {
        if(path ==  null){//check there a path or no
            return;
        }

        if( currentWayPoint >= path.vectorPath.Count){//check character is raech the end of path or no,   
            reachedEndOfPath = true;
            return;
        }else{
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;//dont get it
        Vector2 force = direction * speed * Time.fixedDeltaTime;

        rb.AddForce(force);

        float distance =  Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if(distance < nextWaitPointDistance){
            currentWayPoint++;
        }
    }
    private void HeadRotation(){
        Vector2 Direction = (Vector2)target.position - rb.position;
        float angle = math.atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90f;
        float rotationStep = headRotationSpeed * Time.deltaTime;

        Quaternion currentRotation = Quaternion.Euler(0, 0, head.rotation);
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        head.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotationStep).eulerAngles.z;
    }

    public void MaxHealth(float health){
        slider.maxValue = health;
        slider.value = health;
        healthImage.color = colorGradient.Evaluate(1f);
    }

    public void SetHealth(float magnitude){
        Debug.Log(magnitude);
        currenthealth += magnitude;
        slider.value = currenthealth;
        if(currenthealth <= 0){
            Destroy();
        }
        healthImage.color = colorGradient.Evaluate(slider.normalizedValue);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("PlayerBullet")){
            SetHealth(-playerDamage);
        }
    }

    public void Destroy(){
        FindAnyObjectByType<GameManager>().EnemyCount(-1);
        Destroy(gameObject);
    }
}
