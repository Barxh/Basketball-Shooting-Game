using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class SceneSystem{

     private Scene sceneMain;
    private PhysicsScene2D sceneMainPhysics;

   private Scene scenePrediction; //provjeri da li se ovo brise
    private PhysicsScene2D scenePredictionPhysics;


    public void init(){
          Physics2D.simulationMode = SimulationMode2D.Script;
        createMainScene();
        createScenePrediction();
    }

 private void createMainScene()
    {
        sceneMain = SceneManager.CreateScene("MainScene");
        sceneMainPhysics = sceneMain.GetPhysicsScene2D();
    }

   private void createScenePrediction()
    {
        CreateSceneParameters sceneParameters = new CreateSceneParameters(LocalPhysicsMode.Physics2D);


        scenePrediction = SceneManager.CreateScene("PredictionScene", sceneParameters);
        scenePredictionPhysics = scenePrediction.GetPhysicsScene2D();
    }

     public Scene getScenePrediction(){
        return scenePrediction;
    }
     public PhysicsScene2D getScenePredictionPhysics(){
        return scenePredictionPhysics;
    }

    public void FixedUpdate(){
         if(!sceneMainPhysics.IsValid()) return;
        sceneMainPhysics.Simulate(Time.fixedDeltaTime);
    }

}