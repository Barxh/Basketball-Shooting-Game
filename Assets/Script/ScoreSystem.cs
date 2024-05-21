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
    private int score = 0;

    public void scored(){
        score+=1;
        scoreText.SetText(score.ToString());
    }

    public void startGame(){
        GameObject ball=Instantiate(ballPrefab);
        spawnArea.spawnBall(ball.transform);
    }

}
