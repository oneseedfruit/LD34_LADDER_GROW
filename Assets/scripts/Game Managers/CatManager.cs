using UnityEngine;
using System.Collections;

public class CatManager : MonoBehaviour {

    public Cat cat;
    public Transform catEntrance;

    private GameStateManager gameStateManager;
    private LadderManager ladderManager;
    private Cat spawnedCat;
    private Transform spawnedCatEntrance;

    private void Awake () {
        gameStateManager = GetComponent<GameStateManager>();
        ladderManager = GetComponent<LadderManager>();
    }
	private void Start () {
	
	}
	
	private void Update () {
        Ladder latestLadder = ladderManager.ladders[ladderManager.ladders.Count - 1];
        float ladderTotalHeight = ladderManager.ladderTotalHeight;

        if (GameStateManager.CurrentGameState == GameStates.Play && gameStateManager.stickFigureInstance.transform.position.y >= 4f) {
            SpawnCat();
        }
	}

    private void SpawnCat () {
        if (!Cat.isCatPresent) {
            if (GameStateManager.spawnCatFlag1) {
                spawnedCat = GameObject.Instantiate<Cat>(cat);
                spawnedCat.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.05f, 0));
                spawnedCat.State = EnumCatStates.Climbing;
            }

            if (GameStateManager.spawnCatFlag2) {
                spawnedCatEntrance = GameObject.Instantiate<Transform>(catEntrance);
                spawnedCatEntrance.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1.1f, 10));
                Cat.isCatPresent = true;
            }
        }
    }
}
