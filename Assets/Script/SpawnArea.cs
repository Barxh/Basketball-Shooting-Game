using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SpawnArea : MonoBehaviour
{
    [SerializeField]
    private Vector2 size; //anja
private float left;
private float right;
private float top;
private float bottom;

public void spawnBall(Transform ballTransform){
float x= Random.Range(left,right);
float y= Random.Range(top,bottom);

ballTransform.position = new Vector3(x,y);
}

    private void OnDrawGizmos(){

        Gizmos.color=Color.red;
         left= transform.position.x - size.x /2;
         right= transform.position.x + size.x /2;
         top= transform.position.y + size.y /2;
         bottom= transform.position.y - size.y /2;
        Gizmos.DrawLine(new Vector3(left, top), new Vector3(right, top));
        Gizmos.DrawLine(new Vector3(left, bottom), new Vector3(right, bottom));
        
        Gizmos.DrawLine(new Vector3(left, top), new Vector3(left, bottom));
        Gizmos.DrawLine(new Vector3(right, top), new Vector3(right, bottom));

        

    }
}
