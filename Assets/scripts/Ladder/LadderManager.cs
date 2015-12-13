using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LadderManager : MonoBehaviour {

    public Ladder ladderSegment;
    public int ladderSegmentsCount = 0;
    public float ladderTotalHeight = 0;
    [HideInInspector]
    public float maxDistanceFromStickFigure = 0;
    [HideInInspector]
    public List<Ladder> ladders = new List<Ladder>();
    [HideInInspector]
    public bool spawnCatFlag2 = false;    

    private GameStateManager gameStateManager;
    
	private void Awake () {

        // if the list of ladders is empty add the first ladder found in the scene to the list
	    if (ladders.Count == 0) {
            ladders.Add(GameObject.FindObjectOfType<Ladder>());
        }

        gameStateManager = GetComponent<GameStateManager>();

	}
	
    private void Start () {
        
    }
    
	private void Update () {

        float distanceFromStickFigure = ladderTotalHeight - gameStateManager.stickFigureInstance.transform.position.y;
        float random = Random.Range(0, distanceFromStickFigure);
        float r = Mathf.Abs(Mathf.RoundToInt(distanceFromStickFigure) - Mathf.RoundToInt(random));

        if (Mathf.Abs(Mathf.RoundToInt(distanceFromStickFigure) - Mathf.RoundToInt(random)) >= 5.5f) {
            spawnCatFlag2 = true;
        } else {
            spawnCatFlag2 = false;
        }

        if (GameStateManager.CurrentGameState == GameStates.Play && distanceFromStickFigure <= maxDistanceFromStickFigure) {
            SpawnLadderSegment();            
        }        
	}

    private void SpawnLadderSegment () {
        Ladder prevLadder = ladders[ladders.Count - 1];

        // if the most recent instantiated ladderSegment is complete
        if (IsLadderSegmentComplete(ladders[ladders.Count - 1])) {

            Ladder newLadder = GameObject.Instantiate<Ladder>(ladderSegment);
            float prevLadderHeight = prevLadder.ladderStickL.ladderStickSR.bounds.max.y - prevLadder.ladderStickL.ladderStickSR.bounds.min.y;

            ladders.Add(newLadder);
            newLadder.transform.position = prevLadder.transform.position + Vector3.up * (prevLadderHeight);
            ladderSegmentsCount++;

            ladderTotalHeight = prevLadder.transform.position.y;

        }
            
    }

    private bool IsLadderSegmentComplete (Ladder ladder) {      
         
        // if both ladderStickL and ladderStickR of the current ladder are placed, and a button is pressed
        if ((InputManager.ButtonL || InputManager.ButtonR) && ladder.ladderStickL.isPlaced && ladder.ladderStickR.isPlaced) {
            ladder.ladderStickM.isPlaced = true; // place the middle ladderStickM to complete the current ladder segment
            return true; // return true, ladder segment is complete
        } else {
            if (InputManager.ButtonL && !InputManager.ButtonR) {            // if only ButtonL is pressed
                ladder.ladderStickL.isPlaced = true; // place the left ladderStickL
                return false; // return false, ladder segment is not complete yet
            } else if (InputManager.ButtonR && !InputManager.ButtonL) {     // if only ButtonR is pressed
                ladder.ladderStickR.isPlaced = true; // place the right ladderStickR
                return false; // return false, ladder segment is not complete yet
            }
        }

        return false; // return false, ladder segment is not complete yet

    }
}
