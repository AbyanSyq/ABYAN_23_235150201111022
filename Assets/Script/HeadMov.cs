using Unity.Mathematics;
using UnityEngine;

public class HeadMov : MonoBehaviour
{
    public Rigidbody2D head;
    public Camera cam;
    public float headRotationSpeed;
    
    Vector2 mousePos;
    public void headRotationSetUp(float magnitude){
        headRotationSpeed = magnitude;
    }

    private void Update() {
        HeadRotation();
        transform.localPosition = new Vector2(0,0);
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void HeadRotation(){
        Vector2 Direction = mousePos - head.position;
        float angle = math.atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90f;
        float rotationStep = headRotationSpeed * Time.deltaTime;

        Quaternion currentRotation = Quaternion.Euler(0, 0, head.rotation);
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        head.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotationStep).eulerAngles.z;
    }
}
