using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    public float force = 100f;
    public int maxTrajectoryIteration = 50;
    public GameObject ballPrediction;
    private Vector2 defaultBallPosition;
    private Vector2 startPosition;

    private Rigidbody2D physics;
    private Scene sceneMain;
    private PhysicsScene2D sceneMainPhysiscs;

    private Scene scenePrediction;
     private PhysicsScene2D scenePredictionPhysiscs;
    void Awake(){

        physics = GetComponent<Rigidbody2D>();


    }
        void Start()
    {

        Physics2D.simulationMode = SimulationMode2D.Script;

        physics.isKinematic = true;
        defaultBallPosition = physics.position;

        createMainScene();
        createScenePrediction();

    }

    private void createScenePrediction()
    {
        CreateSceneParameters sceneParameters = new CreateSceneParameters(LocalPhysicsMode.Physics2D);


        scenePrediction = SceneManager.CreateScene("PredictionScene", sceneParameters);
        scenePredictionPhysiscs = scenePrediction.GetPhysicsScene2D();
    }

    private void createMainScene()
    {
        sceneMain = SceneManager.CreateScene("MainScene");
        sceneMainPhysiscs = sceneMain.GetPhysicsScene2D();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0)){
            startPosition = getMousePosition();
        
        }

        if(Input.GetMouseButton(0))
        {
            GameObject newBallPrediction = spawnBallPrediction();
            throwBall(newBallPrediction.GetComponent<Rigidbody2D>());
            createTrajectory(newBallPrediction);
            Destroy(newBallPrediction);
        }


        if (Input.GetMouseButtonUp(0))
        {
            GetComponent<LineRenderer>().positionCount = 0;

            physics.isKinematic = false;
            throwBall(physics);

        }

    }

    private void createTrajectory(GameObject newBallPrediction)
    {
        LineRenderer ballLine = GetComponent<LineRenderer>();
        ballLine.positionCount = maxTrajectoryIteration;

        for (int i = 0; i < maxTrajectoryIteration; i++)
        {
            scenePredictionPhysiscs.Simulate(Time.fixedDeltaTime);
            ballLine.SetPosition(i, new Vector3(newBallPrediction.transform.position.x, newBallPrediction.transform.position.y, 0));
        }
    }

    private void throwBall( Rigidbody2D physics)
    {
        physics.AddForce(getThrowPower(startPosition, getMousePosition()) * force, ForceMode2D.Force);
    }

    private GameObject spawnBallPrediction()
    {
        GameObject newBallPrediction = GameObject.Instantiate(ballPrediction);
        SceneManager.MoveGameObjectToScene(newBallPrediction, scenePrediction);
        newBallPrediction.transform.position = transform.position;
        return newBallPrediction;
    }

    private Vector2 getThrowPower(Vector2 startPosition, Vector2 endPosition)
    {
        return startPosition - endPosition;
    }

    void FixedUpdate(){
        if(!sceneMainPhysiscs.IsValid()) return;
        sceneMainPhysiscs.Simulate(Time.fixedDeltaTime);
    
    }


    void OnCollisionEnter2D(Collision2D collision){
        if(!collision.gameObject.tag.Equals("ground")) return;
        physics.isKinematic = true;
        transform.position = defaultBallPosition;
        physics.velocity = Vector2.zero;
        physics.angularVelocity = 0f;
    }
    private Vector2 getMousePosition(){

        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
