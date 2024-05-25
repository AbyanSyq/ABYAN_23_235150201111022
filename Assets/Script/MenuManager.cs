using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    private bool gameStart = false;
    public GameObject menuPanel;
    public GameObject player;
    public GameObject healthBarCanvas;
    public GameObject tankSetup;
    public GameObject tankInfo;
    public GameObject gameOverPanel;
    public GameObject VictoryPanel;
    public GameObject stagePanel;
    public Text stageText;
    
    public GameObject camera;
    public Slider hitPoint;
    public Slider damage;
    public Slider fireRate;
    public Slider aiming;
    public Slider movement;
    public Slider handling;
    public int tankNumber = 0;


    public Text tankTipeText;

    private void Start() {
        TankSetUpButton(0);
        tankNumber = PlayerPrefs.GetInt("TankNumber");
        Debug.Log(PlayerPrefs.GetInt("TankNumber"));
    }

    //--------------------------------------------------Pause Button Script------------------------------------------------
    public void pauseMenuPanel(bool magnitude){
        menuPanel.SetActive(magnitude);
    }
    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackToMainMenu(){

        SceneManager.LoadScene(0);
    }
    public void ExitGame(){
        Application.Quit();
    }
    public void Resume(){
        FindAnyObjectByType<GameManager>().playPause();
    }

    //----------------------------------------------------------------------------------------------------------------------
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

            PlayerPrefs.SetInt("TankNumber", tankNumber);
            Debug.Log(PlayerPrefs.GetInt("TankNumber"));
            FindAnyObjectByType<TankSetUpScript>().TankSetUp(tankNumber);

            if (tankNumber == 1 || tankNumber == 2)
            {
                tankTipeText.text = "RUN & GUN";
            }else if(tankNumber >= 3 && tankNumber <= 5){
                tankTipeText.text = "SNIPER";
            }else{
                tankTipeText.text = "ROKET LAUNCER";
            }
        }
        hitPoint.value = player.GetComponent<PlayerScript>().health;
        damage.value = player.GetComponent<PlayerScript>().damage;
        fireRate.value = fireRate.maxValue - player.GetComponent<PlayerScript>().fireRate;
        aiming.value = player.GetComponent<PlayerScript>().headRotationSpeed;
        movement.value = player.GetComponent<PlayerScript>().moveSpeed;
        handling.value = player.GetComponent<PlayerScript>().tankRotationSpeed;
    }
    public void LoseCondiditon(){
        healthBarCanvas.SetActive(false);
        gameOverPanel.SetActive(true);
        gameStart = false;
    }
    public void VictoryCondition(){
        healthBarCanvas.SetActive(false);
        VictoryPanel.SetActive(true);
        gameStart = false;
    }

    public void stagePanelManager(int stageNum){
        stagePanel.SetActive(true);
        stageText.text = stageNum.ToString();
    }
    // 

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
