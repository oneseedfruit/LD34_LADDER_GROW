using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {

    // ladder sticks that make up a ladder segment
    [HideInInspector]
    public LadderStick ladderStickL;
    [HideInInspector]
    public LadderStick ladderStickR;
    [HideInInspector]
    public LadderStick ladderStickM;    
    public bool isPlaced = false;

    private LadderStick[] ladderSticks = new LadderStick[3];

    private void Awake () {

        ladderSticks = GetComponentsInChildren<LadderStick>();

    }

    private void Start () {

        for (int i = 0; i < ladderSticks.Length; i++) {
            // if ladderSticks are instantiated, don't let them be visible yet
            ladderSticks[i].isPlaced = false;
                        
            // assign the ladder sticks to ladderStickL, R, M respectively
            switch (i) {
                case 0:
                    ladderStickL = ladderSticks[i];
                    break;
                case 1:
                    ladderStickR = ladderSticks[i];
                    break;
                case 2:
                    ladderStickM = ladderSticks[i];
                    break;
            }        
        }

    }
    
    private void Update () {
        if (ladderStickL.isPlaced && ladderStickR.isPlaced) {
            isPlaced = true;
        } else {
            isPlaced = false;
        }

        if (Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.1f, 0)).y - transform.position.y >= 100f) {
            Destroy(this.gameObject);
        }
    }

}
