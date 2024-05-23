//using System;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events; //anja 


public class Ball : MonoBehaviour
{
   


    // Start is called before the first frame update
     [SerializeField] //anja
    private float force = 100f; //anja
     [SerializeField]
    private int maxTrajectoryIteration = 50; //anja
    public GameObject ballPrediction;

    public event UnityAction scoredEvent;

    public event UnityAction onGroundEvent;

    private Vector2 defaultBallPosition;
    private Vector2 startPosition;



    private Rigidbody2D physics;

   

    private float ballScorePosition; //anja


    private ScoreSystem scoreSystem;
    private Scene scenePrediction; //dodato sa mjesta gdje se ono brise
     private PhysicsScene2D scenePredictionPhysics;


    void Awake(){
        scoreSystem=GameObject.Find("ScoreSystem").GetComponent<ScoreSystem>();
        scenePrediction=scoreSystem.getScenePrediction();
        scenePredictionPhysics=scoreSystem.getScenePredictionPhysics();
        physics = GetComponent<Rigidbody2D>();


    }
        void Start()
    {

        

        physics.isKinematic = true;
        defaultBallPosition = transform.position;


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
            scenePredictionPhysics.Simulate(Time.fixedDeltaTime);
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

    
 void OnCollisionEnter2D(Collision2D collision){
        checkGroundContact(collision);
    }

    void OnTriggerEnter2D(Collider2D collider){
        ballScorePosition = transform.position.y;
    }
  
  void OnTriggerExit2D(Collider2D collider){
    if(transform.position.y < ballScorePosition){
        Debug.Log("Scored");
        scoredEvent.Invoke();
    }
  }


    private Vector2 getMousePosition(){

        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


     private void checkGroundContact(Collision2D collision){

        if(!collision.gameObject.tag.Equals("ground")) return;
       
        onGroundEvent.Invoke();
}
}
