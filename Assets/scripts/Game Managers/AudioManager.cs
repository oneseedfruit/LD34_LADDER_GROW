using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class AudioManager : MonoBehaviour {
    public AudioMixer mainAudio;
    public AudioClip ladderClimbAudio;
    public AudioClip ladderScaryAudio;
    public AudioClip ladderScaryShortAudio;
    public AudioClip ladderHorrorAudio;
    public AudioClip ladderHorrorShortAudio;
    public AudioClip ladderCatAppearAudio;
    public AudioClip ladderJoyAudio;
    [HideInInspector]
    public bool spawnCatFlag1 = false;

    private AudioSource ladderClimbAS;
    private AudioSource ladderScaryAS;
    private AudioSource ladderHorrorAS;
    private static AudioSource ladderCatAppearAS;
    private static AudioSource ladderJoyAS;
    private GameStateManager gameStateManager;

    void Awake () {
        ladderClimbAS = this.gameObject.AddComponent<AudioSource>();
        ladderScaryAS = this.gameObject.AddComponent<AudioSource>();
        ladderHorrorAS = this.gameObject.AddComponent<AudioSource>();
        ladderCatAppearAS = this.gameObject.AddComponent<AudioSource>();
        ladderJoyAS = this.gameObject.AddComponent<AudioSource>();

        gameStateManager = GetComponent<GameStateManager>();
                
        ladderClimbAS.outputAudioMixerGroup = mainAudio.outputAudioMixerGroup;
        ladderScaryAS.outputAudioMixerGroup = mainAudio.outputAudioMixerGroup;
        ladderHorrorAS.outputAudioMixerGroup = mainAudio.outputAudioMixerGroup;
        ladderCatAppearAS.outputAudioMixerGroup = mainAudio.outputAudioMixerGroup;
        ladderJoyAS.outputAudioMixerGroup = mainAudio.outputAudioMixerGroup;

        ladderClimbAS.clip = ladderClimbAudio;
        ladderScaryAS.clip = ladderScaryAudio;
        ladderHorrorAS.clip = ladderHorrorAudio;
        ladderCatAppearAS.clip = ladderCatAppearAudio;
        ladderJoyAS.clip = ladderJoyAudio;

        ladderClimbAS.Stop();
        ladderScaryAS.Stop();
        ladderHorrorAS.Stop();
        ladderCatAppearAS.Stop();
        ladderJoyAS.Stop();
    }

    // Use this for initialization
    private void Start () {
	
	}
	
	// Update is called once per frame
	private void Update () {
	    if (GameStateManager.CurrentGameState == GameStates.Play) {
            PlayClimbingAudio();
            PlayHorrorAudio();

            if (ladderScaryAS.isPlaying) {
                if ((ladderScaryAS.time >= 12f && ladderScaryAS.time < 12.1f) ||
                    (ladderScaryAS.time >= 8f && ladderScaryAS.time < 8.1f) ||
                    (ladderScaryAS.time >= 5f && ladderScaryAS.time < 5.1f) ||
                    (ladderScaryAS.time >= 1f && ladderScaryAS.time < 1.1f)) {
                    spawnCatFlag1 = true;
                } else {
                    spawnCatFlag1 = false;
                }
            } else {
                spawnCatFlag1 = false;
            }
        }

        PlayScaryAudio();        
    }
    
    private void PlayClimbingAudio () {
        if (gameStateManager.stickFigureInstance.State == EnumStickFigureStates.Climbing && !ladderClimbAS.isPlaying) {
            ladderClimbAS.Play();
        }
        if (gameStateManager.stickFigureInstance.State == EnumStickFigureStates.Idling) {
            ladderClimbAS.Stop();
        }
    }

    private void PlayScaryAudio () {
        if (GameStateManager.CurrentGameState == GameStates.Play) {

            if (GameStateManager.hasGameStarted && gameStateManager.stickFigureInstance.State == EnumStickFigureStates.Climbing) {
                if (ladderScaryAS.clip != ladderScaryAudio) {
                    ladderScaryAS.clip = ladderScaryAudio;
                    ladderScaryAS.PlayDelayed(1.5f);
                    ladderScaryAS.loop = true;
                } else {
                    ladderScaryAS.UnPause();
                }
            } else if (gameStateManager.stickFigureInstance.State == EnumStickFigureStates.Idling) {
                if (ladderScaryAS.clip == ladderScaryAudio) {
                    ladderScaryAS.Stop();
                }
                if (!ladderScaryAS.isPlaying && ladderScaryAS.clip == ladderScaryAudio) {
                    ladderScaryAS.clip = ladderScaryShortAudio;
                    ladderScaryAS.PlayDelayed(0.01f);
                    ladderScaryAS.loop = false;
                }
            }

        } else if (GameStateManager.CurrentGameState == GameStates.GameOver) {

            ladderScaryAS.Stop();            

        } 
    }    

    private void PlayHorrorAudio () {
        if (GameStateManager.CurrentGameState == GameStates.Play) {
            if (Cat.isCatPresent) {
                ladderHorrorAS.loop = false;
                ladderHorrorAS.Play();
            } else {
                ladderHorrorAS.Stop();
            }
        }
    }
   
    public static void PlayJoyAudio () {
        if (ladderJoyAS != null && !ladderJoyAS.isPlaying) {
            ladderJoyAS.Play();
            ladderJoyAS.volume = 0.7f;
            ladderJoyAS.loop = false;
        }
    }

    public static void PlayCatAppearSFX () {
        if (!ladderCatAppearAS.isPlaying) {
            ladderCatAppearAS.Play();
            ladderCatAppearAS.volume = 2.5f;
            ladderCatAppearAS.loop = false;
        }
    }
}
