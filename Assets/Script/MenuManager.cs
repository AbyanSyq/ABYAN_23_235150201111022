using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using Mono.Cecil.Cil;
using Unity.VisualScripting;

public class MenuManager : MonoBehaviour
{
    private bool gameStart = false;
    public GameObject menuPanel;
    public GameObject player;
    public GameObject healthBarCanvas;
    public GameObject tankSetup;
    public GameObject tankInfo;
    
    public GameObject camera;
    public Slider hitPoint;
    public Slider damage;
    public Slider fireRate;
    public Slider aiming;
    public Slider movement;
    public Slider handling;
    public int tankNumber = 1;

    public void PlayPausePanel(bool magnitude){
        menuPanel.SetActive(magnitude);
    }
    public void TankSetUpButton(int magnitude){
        if (!gameStart)
        {
            tankNumber += magnitude;
            if (tankNumber > 8)
            {
                tankNumber = 1;
            }else if(tankNumber <= 0 ){
                tankNumber = 8;
            }

            FindAnyObjectByType<TankSetUpScript>().TankSetUp(tankNumber);

            hitPoint.value = player.GetComponent<PlayerScript>().health;
            // damage.value = player.GetComponent<PlayerScript>().;
            fireRate.value = fireRate.maxValue - player.GetComponent<PlayerScript>().fireRate;
            // aiming.value = player.GetComponent<PlayerScript>().;
            movement.value = player.GetComponent<PlayerScript>().moveSpeed;
            handling.value = player.GetComponent<PlayerScript>().tankRotationSpeed;
        }
    }

    public void StartButton(){
        gameStart = true;

        tankSetup.SetActive(false);// remove tank set up ui
        tankInfo.SetActive(false);

        healthBarCanvas.SetActive(true);
        camera.SetActive(true);
        player.GetComponent<CircleCollider2D>().enabled = true;

        player.transform.position = default;
        player.transform.localScale = new Vector3(1.5f,1.5f,1.5f);

        player.GetComponent<PlayerScript>().enabled = true;
        FindAnyObjectByType<GameManager>().GameStart();
        
    }
}
