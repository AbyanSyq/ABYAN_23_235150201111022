using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameStart = false;
    public GameObject enemyTiktok;
    public GameObject playerObject;
    public GameObject menuManagerObject;
    public Transform topRight;
    public Transform topLeft;
    public Transform Left;
    public Transform BottomLeft;
    public Transform Bottom;
    public Transform BottomRight;
    public float timer;
    public int enemyCount;
    public int enemySpawnCount;
    public int stageNow = 0;

    //stage01
    public float stage01Interval;
    public int stage01MaxEnemy;
    bool stage01Active = false;

    //stage02
    public float stage02Interval;
    public int stage02MaxEnemy;
    bool stage02Active = false;

    //stage03
    public float stage03Interval;
    public int stage03MaxEnemy;
    bool stage03Active = false;

    public void GameStart(){
        gameStart = true;
        Time.timeScale = 1;
        stageManager();
    }

    public void playPause(){

        if(Time.timeScale == 1){
            Time.timeScale = 0;
            menuManagerObject.GetComponent<MenuManager>().pauseMenuPanel(true);
        }else{
            Time.timeScale = 1;
            menuManagerObject.GetComponent<MenuManager>().pauseMenuPanel(false);
        }

        
    }
    private void Update() {
        InputManager();
        timer += Time.deltaTime;

        if (stage01Active)
        {
            stage01();
        }
        if(stage02Active){
            stage02();  
        }
        if(stage03Active){
            stage03();
        }

    }
    public void stageManager(){
        stageNow++; 
        
        switch (stageNow)
        {
            case 1: Invoke("stage01",2f);break;
            case 2: Invoke("stage02",2f);break;
            case 3: Invoke("stage03",2f);break;
            default: ;break;
        }
        if (stageNow < 4)
        {
            menuManagerObject.GetComponent<MenuManager>().stagePanelManager(stageNow);
        }
    }
    public void InputManager(){
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            playPause();
        }
    }
    public void stage01(){
        Debug.Log("stage 01 called");
        stage01Active = true;
        if (timer >= stage01Interval)       
        {
            if (enemySpawnCount < stage01MaxEnemy)
            {
                SpawnEnemy();
                enemySpawnCount++;
            }else if(enemyCount <= 0){
                enemySpawnCount = 0;
                stage01Active = false;
                stageManager();

            }
            timer = 0;
        }
    }
    public void stage02(){
        Debug.Log("stage 02 called");
        stage02Active = true;
        if (timer >= stage02Interval)
        {
            if (enemySpawnCount < stage02MaxEnemy)
            {
                SpawnEnemy();
                enemySpawnCount++;
            }else if(enemyCount <= 0){
                enemySpawnCount = 0;
                stage02Active = false;
                stageManager();
            }
            timer = 0;
        }
    }
    public void stage03(){
        Debug.Log("stage 03 called");
        stage03Active = true;
        if (timer >= stage03Interval)
        {
            if (enemySpawnCount < stage03MaxEnemy)
            {
                SpawnEnemy();
                enemySpawnCount++;
            }else if(enemyCount <= 0){
                enemySpawnCount = 0;
                stage03Active = false;
                stageManager();
                menuManagerObject.GetComponent<MenuManager>().VictoryCondition();
            }
            timer = 0;
        }
    }

    public void EnemyCount(int magnitude){
        enemyCount += magnitude;
    }
    public void SpawnEnemy(){//spawn enemy in random place
        int random = Random.Range(1,7);
        Transform spawnpoint = Bottom;
        switch (random){
            case 1: spawnpoint = topRight;      break;
            case 2: spawnpoint = topLeft;       break;
            case 3: spawnpoint = Left;          break;
            case 4: spawnpoint = BottomLeft;    break;
            case 5: spawnpoint = Bottom;        break;
            case 6: spawnpoint = BottomRight;   break;
            default:break;
        }
        GameObject tiktok = Instantiate(enemyTiktok, spawnpoint.position, Quaternion.identity);
    }
}
