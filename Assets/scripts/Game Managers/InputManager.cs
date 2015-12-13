using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class InputManager : MonoBehaviour {

    private static bool buttonL = false;
    private static bool buttonR = false;

    private static bool isTouchScreen = false;
    private static bool onButtonL = false;
    private static bool onButtonR = false;

    public static bool ButtonL {    
        get {
            if (!isTouchScreen && (GameStateManager.CurrentGameState == GameStates.Play &&
                Input.GetAxisRaw("Horizontal") < 0 || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Mouse0))) {
                buttonL = true;
                if (GameStateManager.hasGameStarted == false) {
                    GameStateManager.hasGameStarted = true;
                }
                return buttonL;
            }

            if (isTouchScreen && onButtonL) {                
                isTouchScreen = false;
                onButtonL = false;
                Debug.Log("L");
                return true;
            }

            buttonL = false;           
            
            return buttonL;
        }
    }

    public static bool ButtonR {
        get {
            if (!isTouchScreen && (GameStateManager.CurrentGameState == GameStates.Play &&
                Input.GetAxisRaw("Horizontal") > 0 || Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Mouse1))) {
                buttonR = true;
                if (GameStateManager.hasGameStarted == false) {
                    GameStateManager.hasGameStarted = true;
                }
                return buttonR;
            }

            if (isTouchScreen && onButtonR) {
                isTouchScreen = false;
                onButtonR = false;
                Debug.Log("R");
                return true;                
            }

            buttonR = false;            
            
            return buttonR;
        }
    }

	public void OnScreenButtonL () {
        isTouchScreen = true;
        onButtonL = true;
    }

    public void OnScreenButtonR () {
        isTouchScreen = true;
        onButtonR = true;        
    }

    public void Update () {
        
    }
}
