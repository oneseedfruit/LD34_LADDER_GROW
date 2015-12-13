using UnityEngine;
using System.Collections;

public class LadderStick : MonoBehaviour {

    [HideInInspector]
    public SpriteRenderer ladderStickSR; 
    public bool isPlaced = false;           // is this ladderStick placed?

    private void Awake () {
        ladderStickSR = GetComponent<SpriteRenderer>();
    }

    private void Update () {
        if (isPlaced) { // make ladderStick visible when placed
            ladderStickSR.color = new Color(ladderStickSR.color.r, ladderStickSR.color.g, ladderStickSR.color.b, 1);
        } else {        // make ladderStick invisiable when not placed
            ladderStickSR.color = new Color(ladderStickSR.color.r, ladderStickSR.color.g, ladderStickSR.color.b, 0);
        }
    }
}
