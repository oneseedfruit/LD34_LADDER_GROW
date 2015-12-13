using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour {

    public static GameStates CurrentGameState = GameStates.Play;
    public static bool hasGameStarted = false;
    public static bool spawnCatFlag1 = false;
    public static bool spawnCatFlag2 = false;
    public static int ladderSegmentsCount = 0;
    public static float ladderTotalHeight = 0;
    public static int catLost = 0;
    public static float maxDistanceFromStickFigure = 5f;
    public static float stickFigureClimbingSpeed = 10f;
    public static StickFigure stickFigureInstanceGlobal;

    public StickFigure stickFigure;
    [HideInInspector]
    public StickFigure stickFigureInstance;

    private LadderManager ladderManager;
    private AudioManager audioManager;
    
    private void Awake () {
        ladderManager = GetComponent<LadderManager>();
        audioManager = GetComponent<AudioManager>();
    }

    private void Start () {

        // spawn the stick figure if it is not there
        if (GameObject.FindObjectOfType<StickFigure>() == null) {
            stickFigureInstance = GameObject.Instantiate<StickFigure>(stickFigure);
            stickFigureInstanceGlobal = stickFigureInstance;
            stickFigureInstance.transform.position = ladderManager.ladders[0].transform.position + Vector3.up * 0.4f;
        }
    }

    private void Update () {

        // set the values
        ladderTotalHeight = ladderManager.ladderTotalHeight;
        ladderManager.maxDistanceFromStickFigure = maxDistanceFromStickFigure;
        stickFigureInstance.climbingSpeed = stickFigureClimbingSpeed;

        // get the values
        ladderSegmentsCount = ladderManager.ladderSegmentsCount;
        spawnCatFlag1 = audioManager.spawnCatFlag1;
        spawnCatFlag2 = ladderManager.spawnCatFlag2;

        // game is reset
        if (CurrentGameState == GameStates.GameOver || CurrentGameState == GameStates.GameWon) {
            hasGameStarted = false;
        }
    }
}
