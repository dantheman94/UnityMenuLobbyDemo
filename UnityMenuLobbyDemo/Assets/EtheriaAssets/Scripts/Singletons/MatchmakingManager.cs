using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MatchmakingManager : MonoBehaviour {

    //**************************************************************************************************************
    //                                                                                                             *
    //      INSPECTOR                                                                                              *
    //                                                                                                             *
    //**************************************************************************************************************
    
    //**************************************************************************************************************
    //                                                                                                             *
    //      VARIABLES                                                                                              *
    //                                                                                                             *
    //**************************************************************************************************************
    
    public static MatchmakingManager Instance;

    public enum ELevels { Midgard, Grasslands, Forest, Spire, Longshore, ENUMCOUNT }
    public enum EDifficulties { Easy, Normal, Hard, Mythic, ENUMCOUNT }
    public enum EModifiers { GenerationRate, DoubleDamage, IronForge, ENUMCOUNT }

    public class SModifierState {

        // Constructor
        public SModifierState(EModifiers mod) {

            _modifier = mod;
            _active = false;
        }

        public EModifiers _modifier;
        public bool _active;
    }

    private ELevels _CurrentLevel = ELevels.ENUMCOUNT;
    private EDifficulties _CurrentDifficulty = EDifficulties.ENUMCOUNT;
    private List<SModifierState> _Modifiers;

    //**************************************************************************************************************
    //                                                                                                             * 
    //      FUNCTIONS                                                                                              * 
    //                                                                                                             * 
    //**************************************************************************************************************

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Called when the script instance is being loaded.
    /// </summary>
    private void Awake() {

        // Initialize singleton
        if (Instance != null && Instance != this) {

            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Called when the script instance is being loaded.
    /// </summary>
    private void Start() {

        _Modifiers = new List<SModifierState>();
        for (int i = 0; i < (int)EModifiers.ENUMCOUNT - 1; i++)
            _Modifiers.Add(new SModifierState((EModifiers)i));
        
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Sets the text string of the specified Text component being passed through
    /// </summary>
    // <param name="titleText"></param>
    /// <returns>
    //  void
    /// </returns>
    public void SetMatchTitleText(Text titleText) {

        // Sanity check
        if (titleText) {

            // Set difficulty text
            string diff;
            if (_CurrentDifficulty != EDifficulties.ENUMCOUNT)
                diff = _CurrentDifficulty.ToString();

            else
                diff = "Select Difficulty";

            // Set level text
            string level;
            if (_CurrentLevel != ELevels.ENUMCOUNT)
                level = _CurrentLevel.ToString();

            else
                level = "Select Level";

            titleText.text = string.Concat(level + " on " + diff);

            // Update start match button interactability
            WidgetManager.Instance.PrivateLobby_StartMatchButton.interactable = _CurrentDifficulty != EDifficulties.ENUMCOUNT && _CurrentLevel != ELevels.ENUMCOUNT;
        }
        else
            Debug.LogWarning(titleText + "is null!");
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  
    /// </summary>
    //  <param name="modifer"></param>
    public void ActivateModifier(int modifer) {

        // Find the iterator in the modifier list
        for (int i = 0; i < (int)EModifiers.ENUMCOUNT; i++) {


        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  
    /// </summary>
    //  <param name="modifier"></param>
    public void DeactivateModifier(int modifier) {

    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Starts the gameplay match sequence.
    /// </summary>
    public void StartMatch() {

        // Sanity Check
        if (_CurrentDifficulty != EDifficulties.ENUMCOUNT && _CurrentLevel != ELevels.ENUMCOUNT) {

            // Fade in screen to black
            WidgetManager.Instance.FadeScreen(new Color(0, 0, 0, 0), Color.black, 3, false,  () => ASyncLoad.Instance.LoadLevelByIndex(2, true));
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Starts the tutorial level sequence.
    /// </summary>
    public void StartTutorial() {
        
        // Fade in screen to black
        WidgetManager.Instance.FadeScreen(new Color(0, 0, 0, 0), Color.black, 3, false);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //**************************************************************************************************************
    //                                                                                                             * 
    //      COROUTINES                                                                                             * 
    //                                                                                                             * 
    //**************************************************************************************************************

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  
    /// </summary>
    // <param name="delay"></param>
    /// <returns>
    //  IEnumerator
    /// </returns>
    private IEnumerator DelayedFunctionCall(float delay, Action method) {

        // Delay
        float normalizedTime = 0f;
        while (normalizedTime <= 1f) {

            normalizedTime += Time.deltaTime / delay;
            yield return null;
        }

        // Call function
        method();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //**************************************************************************************************************
    //                                                                                                             * 
    //      GETTERS & SETTERS                                                                                      * 
    //                                                                                                             * 
    //**************************************************************************************************************

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Sets reference to the current level to play based on the current level enumerator.
    /// </summary>
    /// <returns>
    //  void
    /// </returns>
    public void SetLevel(int level) { _CurrentLevel = (ELevels)level; }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Sets reference to the current difficulty to play based on the current difficulty enumerator.
    /// </summary>
    /// <returns>
    //  void
    /// </returns>
    public void SetDifficulty(int difficulty) { _CurrentDifficulty = (EDifficulties)difficulty; }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

}
