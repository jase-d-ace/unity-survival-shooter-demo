using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    //using keyword "static" in the declaration of a variable attaches the variable to the CLASS, not the instance. That means that if this script is attached to multiple gameObjects, it will update the same score across all gameObjects.
    public static int score;


    Text text;


    void Awake ()
    {
        text = GetComponent <Text> ();
        score = 0;
    }


    void Update ()
    {
        text.text = "Score: " + score;
    }
}
