using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text scoreText;
    private int score = 0;

    public void scored(){
        score+=1;
        scoreText.SetText(score.ToString());
    }



}
