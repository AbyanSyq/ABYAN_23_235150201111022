using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSetUpScript : MonoBehaviour
{

    public SpriteRenderer body;
    public SpriteRenderer gun;
    public SpriteRenderer gunConnector;
    public SpriteRenderer tower;

    public Sprite body01;
    public Sprite body02;
    public Sprite body03;
    public Sprite body04;
    public Sprite body05;
    public Sprite body06;
    public Sprite body07;
    public Sprite body08;


    public Sprite gun01;
    public Sprite gun02;
    public Sprite gun03;
    public Sprite gun04;
    public Sprite gun05;
    public Sprite gun06;
    public Sprite gun07;
    public Sprite gun08;



    public Sprite gunConnector01;
    public Sprite gunConnector02;
    public Sprite gunConnector03;
    public Sprite gunConnector04;
    public Sprite gunConnector05;
    public Sprite gunConnector06;
    public Sprite gunConnector07;
    public Sprite gunConnector08;

    public Sprite tower01;
    public Sprite tower02;
    public Sprite tower03;
    public Sprite tower04;
    public Sprite tower05;
    public Sprite tower06;
    public Sprite tower07;
    public Sprite tower08;

    public void TankSetUp(int magnitude){
        switch (magnitude)
        {   
            //LIGHT TANK(RUN AND GUN)
            case 1: 
                body.sprite         = body01;
                gun.sprite          = gun01;
                gunConnector.sprite = gunConnector01;
                tower.sprite        = tower01;
                FindAnyObjectByType<PlayerScript>().TankPowerSetUp(1);
                break;
            case 2: 
                body.sprite         = body02;
                gun.sprite          = gun02;
                gunConnector.sprite = gunConnector02;
                tower.sprite        = tower02;
                FindAnyObjectByType<PlayerScript>().TankPowerSetUp(1);
                break;
            //MEDIUM TANK(SNIPER)    
            case 3: 
                body.sprite         = body03;
                gun.sprite          = gun03;
                gunConnector.sprite = gunConnector03;
                tower.sprite        = tower03;
                FindAnyObjectByType<PlayerScript>().TankPowerSetUp(2);
                break;
            case 4: 
                body.sprite         = body04;
                gun.sprite          = gun04;
                gunConnector.sprite = gunConnector04;
                tower.sprite        = tower04;
                FindAnyObjectByType<PlayerScript>().TankPowerSetUp(2);
                break;
            case 5: 
                body.sprite         = body05;
                gun.sprite          = gun05;
                gunConnector.sprite = gunConnector05;
                tower.sprite        = tower05;
                FindAnyObjectByType<PlayerScript>().TankPowerSetUp(2);
                break;
            //HEAVY TANK(BOM LAUNCER)COMING SOON
            case 6: 
                body.sprite         = body06;
                gun.sprite          = gun06;
                gunConnector.sprite = gunConnector06;
                tower.sprite        = tower06;
                FindAnyObjectByType<PlayerScript>().TankPowerSetUp(3);
                break;
            case 7: 
                body.sprite         = body07;
                gun.sprite          = gun07;
                gunConnector.sprite = gunConnector07;
                tower.sprite        = tower07;
                FindAnyObjectByType<PlayerScript>().TankPowerSetUp(3);
                break;
            case 8: 
                body.sprite         = body08;
                gun.sprite          = gun08;
                gunConnector.sprite = gunConnector08;
                tower.sprite        = tower08;
                FindAnyObjectByType<PlayerScript>().TankPowerSetUp(3);
                break;
            default:
                break;
        }
    }

}
