using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UITextClimbed : MonoBehaviour {

    private Text climbedText;

    private void Awake () {
        climbedText = GetComponent<Text>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        climbedText.text = "Ladder Grown: " + GameStateManager.ladderSegmentsCount.ToString() + "\n";
        climbedText.text = climbedText.text + "Escapes: " + GameStateManager.catLost.ToString() + "\n";

        if (GameStateManager.CurrentGameState == GameStates.GameOver) {
            climbedText.text = climbedText.text + "\nGame over! You did well.";
        }
	}
}
