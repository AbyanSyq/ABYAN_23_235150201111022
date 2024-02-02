using UnityEngine;
using Pathfinding;
using UnityEngine.AI;
using UnityEngine.UI;
using Unity.VisualScripting;
using Unity.Mathematics;


public class EnemyMenuAI : MonoBehaviour
{
    public Transform target;

    public GameObject DeathAnim;
    
    public Object healthBar;
    public float speed = 200f;
    public float nextWaitPointDistance = 3f;//jarak character ke waypoint sebelum gerak ke point selanjutnya

    Path path;//path charakter ke target
    int currentWayPoint = 0; 
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    public Rigidbody2D head;
    public Rigidbody2D BodyTank;
    public float headRotationSpeed;

    private int spawnCode;
    public Transform topRight;
    public Transform topLeft;
    public Transform Left;
    public Transform BottomLeft;
    public Transform Bottom;
    public Transform BottomRight;


    public Slider slider;
    public float currenthealth;
    public float playerDamage;
    public Image healthImage;
    public Gradient colorGradient;

    private void Start() {
        MaxHealth(currenthealth);  
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, .5f);//update path every 0.5 second
    }
    private void Update() {
        head.position = rb.position;
        BodyTank.position = rb.position;
        HeadRotation();
    }
    void UpdatePath(){//searching a path form character position to target
        if(seeker.IsDone()){
        seeker.StartPath(rb.position, target.position, OnPathComplete);
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

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;// i dont get it

        Vector2 force = direction * speed * Time.fixedDeltaTime;
        rb.AddForce(force);

        float distance =  Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if(distance < nextWaitPointDistance){
            currentWayPoint++;
        }


        float angle = math.atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        float rotationStep = headRotationSpeed * Time.deltaTime;

        Quaternion currentRotation = Quaternion.Euler(0, 0, BodyTank.rotation);
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        BodyTank.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotationStep).eulerAngles.z;
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
            SpawnEnemy();
        }
        healthImage.color = colorGradient.Evaluate(slider.normalizedValue);
    }
    public void SpawnEnemy(){//spawn enemy in random place
        GameObject Anim = Instantiate(DeathAnim, transform.position, Quaternion.Euler(0,0,transform.rotation.z));
        Destroy(Anim, 0.5f);
        spawnCode++;
        Debug.Log("spawncode = " + spawnCode);
        if (spawnCode > 6)
        {
            spawnCode = 0;
        }
        Transform spawnpoint = Bottom;
        switch (spawnCode){
            case 1: spawnpoint = topRight;      break;
            case 2: spawnpoint = topLeft;       break;
            case 3: spawnpoint = Left;          break;
            case 4: spawnpoint = BottomLeft;    break;
            case 5: spawnpoint = Bottom;        break;
            case 6: spawnpoint = BottomRight;   break;
            default:break;
        }

        gameObject.transform.position = new Vector2(spawnpoint.position.x , spawnpoint.position.y);
        currenthealth = slider.maxValue;
        slider.value = currenthealth;
    }
}
