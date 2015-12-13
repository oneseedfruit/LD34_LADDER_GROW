using UnityEngine;
using System.Collections;

public class StickFigure : MonoBehaviour {

    public EnumStickFigureStates State = EnumStickFigureStates.Idling;
    [HideInInspector]
    public float climbingSpeed = 10f;

    private Animator stickFigureAnimator;
    private Rigidbody2D stickFigureRb2D;

    private void Awake() {
        stickFigureAnimator = GetComponent<Animator>();
        stickFigureRb2D = GetComponent<Rigidbody2D>();
        stickFigureRb2D.gravityScale = 0;
    }

    private void Update() {

        if (GameStateManager.CurrentGameState == GameStates.Play) {
            if (transform.position.y < GameStateManager.ladderTotalHeight) {
                transform.position = new Vector3(transform.position.x, transform.position.y + climbingSpeed * Time.deltaTime, transform.position.z);
                State = EnumStickFigureStates.Climbing;
            } else {
                State = EnumStickFigureStates.Idling;
            }

            switch (State) {
                case EnumStickFigureStates.Idling:
                    stickFigureAnimator.speed = 0;
                    break;
                case EnumStickFigureStates.Climbing:
                    stickFigureAnimator.speed = 1;
                    break;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col) {
        if (col.tag == "Cat") {
            GameStateManager.CurrentGameState = GameStates.GameOver;
            stickFigureRb2D.gravityScale += 10 * Time.deltaTime;
            stickFigureRb2D.AddTorque(Random.Range(-100f, 100f));
            col.gameObject.GetComponent<Cat>().State = EnumCatStates.Attacking;
        }
    }
}
