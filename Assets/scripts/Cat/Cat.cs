using UnityEngine;
using System.Collections;

public class Cat : MonoBehaviour {
    public static bool isCatPresent = false;
    public EnumCatStates State = EnumCatStates.Idling;

    public float stamina = 150f;

    private Transform[] cats = new Transform[2];
    private Transform attackingCat;
    private Transform climbingCat;
    private Animator attackingCatAnim;
    private Animator climbingCatAnim;
    private bool isAdrenalineGiven = false;

    private void Awake () {
        isCatPresent = true;

        cats = GetComponentsInChildren<Transform>();
        for (int i = 0; i < cats.Length; i++) {
            if (cats[i].name == "attackingCat") {
                attackingCat = cats[i];
            } else if (cats[i].name == "climbingCat") {
                climbingCat = cats[i];
            }
        }

        attackingCatAnim = attackingCat.gameObject.GetComponent<Animator>();
        climbingCatAnim = climbingCat.gameObject.GetComponent<Animator>();

    }

    private void Start () {
        if (GameStateManager.CurrentGameState == GameStates.Play) {
            AudioManager.PlayCatAppearSFX();
        }
    }
    
    private void Update () {
        if (GameStateManager.CurrentGameState == GameStates.Play || GameStateManager.CurrentGameState == GameStates.GameOver) {
            switch (State) {
                case EnumCatStates.Idling:
                    attackingCat.gameObject.SetActive(false);
                    attackingCatAnim.speed = 0;
                    climbingCat.gameObject.SetActive(true);
                    climbingCatAnim.speed = 0;
                    break;
                case EnumCatStates.Attacking:
                    attackingCat.gameObject.SetActive(true);
                    attackingCatAnim.speed = 1;
                    climbingCat.gameObject.SetActive(false);
                    climbingCatAnim.speed = 0;
                    break;
                case EnumCatStates.Climbing:
                    attackingCat.gameObject.SetActive(false);
                    attackingCatAnim.speed = 0;
                    climbingCat.gameObject.SetActive(true);
                    climbingCatAnim.speed = 1;
                    break;
                case EnumCatStates.GivingUp:
                    attackingCatAnim.speed = 0;
                    climbingCatAnim.speed = 0;
                    break;
            }
        }

        if (State == EnumCatStates.Attacking && GameStateManager.CurrentGameState == GameStates.Play) {
            if (transform.position.y - GameStateManager.stickFigureInstanceGlobal.transform.position.y <= 10f) {
                transform.localScale = new Vector3(transform.localScale.x - 0.5f * Time.deltaTime, transform.localScale.y - 0.5f * Time.deltaTime, transform.localScale.z - 0.5f * Time.deltaTime);
                transform.position = transform.position - Vector3.up * 0.5f * Time.deltaTime;
            }
        } else if (State == EnumCatStates.Climbing) {
            Climb();
        }        

        DrainStamina();
        GiveAdrenaline(GameStateManager.spawnCatFlag1 && GameStateManager.spawnCatFlag2);

        CleanUp();
    }
    
    private void OnDestroy () {
        isCatPresent = false;
        if (State != EnumCatStates.Attacking) {
            AudioManager.PlayJoyAudio();
            GameStateManager.catLost++;
        }
    }

    private void Climb () {
        if (transform.position.y <= GameStateManager.stickFigureInstanceGlobal.transform.position.y) {
            transform.position = new Vector3(transform.position.x,
                                             transform.position.y + stamina * GameStateManager.stickFigureInstanceGlobal.climbingSpeed * Time.deltaTime,
                                             0);

        }
    }

    private void DrainStamina () {
        if (stamina > 0) {
            stamina -= 0.025f * Time.deltaTime;
        } else {
            this.gameObject.AddComponent<Rigidbody2D>();
            transform.Rotate(Quaternion.Euler(Vector3.forward * (Random.Range(-1, 1) * 50f) * Time.deltaTime).eulerAngles);
        }
        
    }

    private void GiveAdrenaline(bool adrenaline) {
        if (adrenaline && !isAdrenalineGiven) {
            stamina += Random.Range(5f, 10f) * Time.deltaTime;
            isAdrenalineGiven = true;
            Debug.Log("Adrenaline given!");            
        }
    }

    private void CleanUp() {
        if (Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.1f, 0)).y - transform.position.y >= 20f) {
            Destroy(this.gameObject);
        }

        if (transform.localScale.x <= 0.2f) {
            Destroy(this.gameObject);
        }
    }    
}
