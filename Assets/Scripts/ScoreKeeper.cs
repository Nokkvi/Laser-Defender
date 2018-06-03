using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public static int score = 0;

    private Text text;

    void Start()
    {
       text = GetComponent<Text>();
        Reset();
    }

	// Use this for initialization
	public void Score (int points) {
        score += points;
        text.text = score.ToString();
	}
	
	// Update is called once per frame
	public static void Reset ()
    {
        score = 0;
    }
}
