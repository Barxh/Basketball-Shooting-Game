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

    private Scene sceneMain;
    private PhysicsScene2D sceneMainPhysics;

   private Scene scenePrediction; //provjeri da li se ovo brise
    private PhysicsScene2D scenePredictionPhysics;



    private int score = 0;

    private void Awake(){
          Physics2D.simulationMode = SimulationMode2D.Script;
    }

    public void scored(){
        score+=1;
        scoreText.SetText(score.ToString());
    }

    public void startGame(){
      
        
        createMainScene();
        createScenePrediction();

        spawnBall();

    }

    public Scene getScenePrediction(){
        return scenePrediction;
    }
     public PhysicsScene2D getScenePredictionPhysics(){
        return scenePredictionPhysics;
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

    private void createScenePrediction()
    {
        CreateSceneParameters sceneParameters = new CreateSceneParameters(LocalPhysicsMode.Physics2D);


        scenePrediction = SceneManager.CreateScene("PredictionScene", sceneParameters);
        scenePredictionPhysics = scenePrediction.GetPhysicsScene2D();
    }

    private void createMainScene()
    {
        sceneMain = SceneManager.CreateScene("MainScene");
        sceneMainPhysics = sceneMain.GetPhysicsScene2D();
    }

    void FixedUpdate(){
        if(!sceneMainPhysics.IsValid()) return;
        sceneMainPhysics.Simulate(Time.fixedDeltaTime);
    
    }

}
