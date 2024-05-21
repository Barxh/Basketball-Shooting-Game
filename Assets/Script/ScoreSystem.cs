using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    

    [SerializeField]
    private GameObject ballPrefab;
    [SerializeField]
    private SpawnArea spawnArea;
    [SerializeField]
    private TMP_Text scoreText;

    private GameObject currentBall;



    private int score = 0;

    public void scored(){
        score+=1;
        scoreText.SetText(score.ToString());
    }

    public void startGame(){

        spawnBall();

    }

    private void respawnBall(){
        destroyBall();
        spawnBall();

    }

    private void spawnBall(){
        currentBall=Instantiate(ballPrefab);
        Ball ballComand=currentBall.GetComponent<Ball>();

        
        ballComand.scoredEvent+=scored;
        ballComand.onGroundEvent+=respawnBall;

        spawnArea.spawnBall(currentBall.transform);

    }

    private void destroyBall(){
        if(!currentBall) return;

        Ball ballComand=currentBall.GetComponent<Ball>();

        
        ballComand.scoredEvent-=scored;
        ballComand.onGroundEvent-=respawnBall;

        Destroy(currentBall);
        currentBall=null;

    }


}
