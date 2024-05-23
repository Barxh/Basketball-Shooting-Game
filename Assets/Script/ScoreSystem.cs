using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;
public class ScoreSystem: MonoBehaviour
{
    // Start is called before the first frame update
    

    [SerializeField]
    private GameObject ballPrefab;
    [SerializeField]
    private SpawnArea spawnArea;
    [SerializeField]
    private TMP_Text scoreText;

    private GameObject currentBall;

    private SceneSystem sceneSystem=new SceneSystem();


    private int score = 0;

    private void Awake(){
         sceneSystem.init();
    }

    public void scored(){
        score+=1;
        scoreText.SetText(score.ToString());
    }

    public void startGame(){
        
        
       

        spawnBall();

    }

    public Scene getScenePrediction(){
        return sceneSystem.getScenePrediction();
    }
     public PhysicsScene2D getScenePredictionPhysics(){
        return sceneSystem.getScenePredictionPhysics();
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

 

    void FixedUpdate(){

        sceneSystem.FixedUpdate();
       
    
    }

}
