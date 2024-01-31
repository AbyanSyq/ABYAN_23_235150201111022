using System.Collections;
using System.Collections.Generic;
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
    }

    public void playPause(){
        if (gameStart)
        {
            if(Time.timeScale == 1){
                Time.timeScale = 0;
            }else{
                Time.timeScale = 1;
            }
        }
        
    }
    private void Update() {
        InputManager();
        timer += Time.deltaTime;

        if (stageNow < 4 && gameStart)
        {   
            Debug.Log("IF SATU");
            if (!stage01Active && !stage02Active && enemyCount <= 0 && !stage03Active)
            {   
            enemySpawnCount = 0;
            stageManager();
            timer = 0; 
            }
        }
        

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
        Debug.Log("STAGE");
        switch (stageNow)
        {
            case 1: stage01Active = true;break;
            case 2: stage02Active = true;break;
            case 3: stage03Active = true;break;
            default: playPause();break;
        }
    }
    public void InputManager(){
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playPause();
        }
    }
    public void stage01(){
        if (timer >= stage01Interval)       
        {
            if (enemySpawnCount < stage01MaxEnemy)
            {
                SpawnEnemy();
                enemySpawnCount++;
            }else{
                stage01Active = false;
            }
            timer = 0;
        }
    }
    public void stage02(){
        if (timer >= stage02Interval)
        {
            if (enemySpawnCount < stage02MaxEnemy)
            {
                SpawnEnemy();
                enemySpawnCount++;
            }else{
                stage02Active = false;
            }
            timer = 0;
        }
    }
    public void stage03(){
        if (timer >= stage03Interval)
        {
            if (enemySpawnCount < stage03MaxEnemy)
            {
                SpawnEnemy();
                enemySpawnCount++;
            }else{
                stage03Active = false;
            }
            timer = 0;
        }
    }

    public void EnemyCount(int magnitude){
        enemyCount += magnitude;
    }
    public void SpawnEnemy(){
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
    public void LoseCondiditon(){
        playPause();
    }

}
