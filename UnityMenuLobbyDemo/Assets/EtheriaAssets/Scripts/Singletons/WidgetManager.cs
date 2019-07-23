using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//******************************
//
//  Created by: Daniel Marton
//
//  Last edited by: Daniel Marton
//  Last edited on: 13/7/2019
//
//******************************

public class WidgetManager : MonoBehaviour {

    //**************************************************************************************************************
    //                                                                                                             *
    //      INSPECTOR                                                                                              *
    //                                                                                                             *
    //**************************************************************************************************************

    [Space]
    [Header("-----------------------------------")]
    [Header(" Main Menu Widgets ")]
    [Space]
    public GameObject Widget_MainMenu;
    public Animator AnimController_MainMenu;
    public AnimationClip ExitAnimationClip_MainMenu;
    [Space]
    public GameObject Widget_Matchmaking;
    public Animator AnimController_Matchmaking;
    public AnimationClip ExitAnimationClip_Matchmaking;
    public Text Matchmaking_MatchTitle;
    [Space]
    public GameObject Widget_HostPrivateLobby;
    public Animator AnimController_HostPrivateLobby;
    public AnimationClip ExitAnimationClip_PrivateLobby;
    public Text PrivateLobby_MatchTitle;
    public Button PrivateLobby_StartMatchButton;
    [Space]
    public GameObject Widget_DifficultyPlaylist;
    public GameObject Widget_Leaderboards;
    public GameObject Widget_Showcase;
    public GameObject Widget_Credits;
    public GameObject Widget_StartTutorial;
    public GameObject Widget_ConnectIP;
    public GameObject Widget_ConfirmQuit;
    public GameObject Widget_LevelsList;
    public GameObject Widget_DifficultiesList;
    public GameObject Widget_ModifierList;
    public GameObject Widget_Fade;

    //**************************************************************************************************************
    //                                                                                                             *
    //      VARIABLES                                                                                              *
    //                                                                                                             *
    //**************************************************************************************************************

    public enum ECurrentMenuState { Mainmenu, Matchmaking, HostPrivateLobby, DifficultyPlaylists, StartTutorial, ConnectIP, ConfirmQuit, SelectLevel, SelectDifficulty, SelectModifiers }
    public static WidgetManager Instance;

    private ECurrentMenuState _CurrentMenuState = ECurrentMenuState.Mainmenu;

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
    //  
    /// </summary>
    // <param name="delay"></param>
    public void GoToMainMenu(float delay) {

        // Current widget exit animation
        switch (_CurrentMenuState) {

            case ECurrentMenuState.Matchmaking:
                AnimController_Matchmaking.SetBool("PlayEnter", false);
                AnimController_Matchmaking.SetBool("PlayExit", true);
                break;

            case ECurrentMenuState.HostPrivateLobby:
                AnimController_HostPrivateLobby.SetBool("PlayEnter", false);
                AnimController_HostPrivateLobby.SetBool("PlayExit", true);
                break;

            default: break;
        }

        // Delayed widget swap
        StartCoroutine(DelayedWidgetSwap(delay, ECurrentMenuState.Mainmenu, GetCurrentMenuWidget(), Widget_MainMenu));
        StartCoroutine(DelayedFunctionCall(delay, () => AnimController_MainMenu.SetBool("PlayEnter", true)));
        StartCoroutine(DelayedFunctionCall(delay, () => AnimController_MainMenu.SetBool("PlayExit", false)));

        // Reset animation trigger variables
        StartCoroutine(DelayedFunctionCall(delay + ExitAnimationClip_MainMenu.length + 0.1f, () => AnimController_MainMenu.SetBool("PlayEnter", false)));
    }

    internal void FadeScreen(Color color, Color black, int v1, bool v2, object v3) {
        throw new NotImplementedException();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  
    /// </summary>
    // <param name="delay"></param>
    public void GoToMatchmaking(float delay) {

        // Current widget exit animation
        switch (_CurrentMenuState) {

            case ECurrentMenuState.Mainmenu:
                AnimController_MainMenu.SetBool("PlayEnter", false);
                AnimController_MainMenu.SetBool("PlayExit", true);
                break;

            case ECurrentMenuState.HostPrivateLobby:
                AnimController_HostPrivateLobby.SetBool("PlayEnter", false);
                AnimController_HostPrivateLobby.SetBool("PlayExit", true);
                break;

            default: break;
        }

        // Delayed widget swap
        StartCoroutine(DelayedWidgetSwap(delay, ECurrentMenuState.Matchmaking, GetCurrentMenuWidget(), Widget_Matchmaking));
        StartCoroutine(DelayedFunctionCall(delay, () => AnimController_Matchmaking.SetBool("PlayEnter", true)));
        StartCoroutine(DelayedFunctionCall(delay, () => AnimController_Matchmaking.SetBool("PlayExit", false)));

        // Reset animation trigger variables
        StartCoroutine(DelayedFunctionCall(delay + ExitAnimationClip_Matchmaking.length + 0.1f, () => AnimController_Matchmaking.SetBool("PlayEnter", false)));
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  
    /// </summary>
    // <param name="delay"></param>
    public void GoToHostPrivateLobby(float delay) {

        // Current widget exit animation
        switch (_CurrentMenuState) {

            case ECurrentMenuState.Mainmenu:
                AnimController_MainMenu.SetBool("PlayEnter", false);
                AnimController_MainMenu.SetBool("PlayExit", true);
                break;

            case ECurrentMenuState.Matchmaking:
                AnimController_Matchmaking.SetBool("PlayEnter", false);
                AnimController_Matchmaking.SetBool("PlayExit", true);
                break;

            default: break;
        }

        // Delayed widget swap
        StartCoroutine(DelayedWidgetSwap(delay, ECurrentMenuState.HostPrivateLobby, GetCurrentMenuWidget(), Widget_HostPrivateLobby));
        StartCoroutine(DelayedFunctionCall(delay, () => AnimController_HostPrivateLobby.SetBool("PlayExit", false)));
        StartCoroutine(DelayedFunctionCall(delay, () => AnimController_HostPrivateLobby.SetBool("PlayEnter", true)));

        // Reset animation trigger variables
        StartCoroutine(DelayedFunctionCall(delay + ExitAnimationClip_PrivateLobby.length + 0.1f, () => AnimController_HostPrivateLobby.SetBool("PlayEnter", false)));
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  
    /// </summary>
    // <param name="delay"></param>
    public void GoToDifficultyPlaylists(float delay) {

        // Delayed widget swap
        StartCoroutine(DelayedWidgetSwap(delay, ECurrentMenuState.DifficultyPlaylists, null, Widget_DifficultyPlaylist));
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  
    /// </summary>
    // <param name="delay"></param>
    public void GoToTutorial(float delay) {

        // Delayed widget swap
        StartCoroutine(DelayedWidgetSwap(delay, ECurrentMenuState.StartTutorial, null, Widget_StartTutorial));
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  
    /// </summary>
    // <param name="delay"></param>
    public void GoToConnectIP(float delay) {

        // Delayed widget swap
        StartCoroutine(DelayedWidgetSwap(delay, ECurrentMenuState.ConnectIP, null, Widget_ConnectIP));
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  
    /// </summary>
    // <param name="delay"></param>
    public void GoToConfirmQuit(float delay) {

        // Delayed widget swap
        StartCoroutine(DelayedWidgetSwap(delay, ECurrentMenuState.ConfirmQuit, null, Widget_ConfirmQuit));
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  
    /// </summary>
    // <param name="delay"></param>
    public void GoToSelectLevel(float delay) {

        // Delayed widget swap
        StartCoroutine(DelayedWidgetSwap(delay, ECurrentMenuState.SelectLevel, null, Widget_LevelsList));
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  
    /// </summary>
    // <param name="delay"></param>
    public void GoToSelectDifficulty(float delay) {

        // Delayed widget swap
        StartCoroutine(DelayedWidgetSwap(delay, ECurrentMenuState.SelectDifficulty, null, Widget_DifficultiesList));
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  
    /// </summary>
    // <param name="delay"></param>
    public void GoToSelectModifiers(float delay) {

        // Delayed widget swap
        StartCoroutine(DelayedWidgetSwap(delay, ECurrentMenuState.SelectModifiers, null, Widget_ModifierList));
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  
    /// </summary>
    // <param name="startCol"></param>
    // <param name="endCol"></param>
    /// <returns>
    //  
    /// </returns>
    public void FadeScreen(Color startCol, Color endCol, float speed, bool hideWidgetWhenComplete, Action methodWhenComplete = null) {

        if (Widget_Fade) {

            Widget_Fade.SetActive(true);
            StartCoroutine(FadeScreenCoroutine(startCol, endCol, speed, hideWidgetWhenComplete, methodWhenComplete));
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //**************************************************************************************************************
    //                                                                                                             * 
    //      COROUTINES                                                                                             * 
    //                                                                                                             * 
    //**************************************************************************************************************

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Coroutine that waits by the amount specified before disabling & enabling widget gameObjects.
    /// </summary>
    // <param name="delay"></param>
    // <param name="widgetHide"></param>
    // <param name="WidgetShow"></param>
    /// <returns>
    //  IEnumerator
    /// </returns>
    private IEnumerator DelayedWidgetSwap(float delay, ECurrentMenuState newState, GameObject widgetHide, GameObject WidgetShow) {

        // Delay
        float normalizedTime = 0f;
        while (normalizedTime <= 1f) {

            normalizedTime += Time.deltaTime / delay;
            yield return null;
        }

        // Hide widget gameObject
        if (widgetHide != null)
            widgetHide.SetActive(false);

        // Set new menu state
        _CurrentMenuState = newState;

        // Show widget gameObject
        if (WidgetShow != null)
            WidgetShow.SetActive(true);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  
    /// </summary>
    // <param name="delay"></param>
    /// <returns>
    //  IEnumerator
    /// </returns>
    public IEnumerator DelayedFunctionCall(float delay, Action method) {

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

    /// <summary>
    //  
    /// </summary>
    //  <param name="startCol"></param>
    //  <param name="endCol"></param>
    /// <returns>
    //  
    /// </returns>
    private IEnumerator FadeScreenCoroutine(Color startCol, Color endCol, float speed, bool hideWidgetWhenComplete, Action methodWhenComplete = null) {

        Color currentColor = startCol;
        float time = 0f;

        // Sanity check        
        if (Widget_Fade) {

            Image background = Widget_Fade.GetComponentInChildren<Image>();
            while (time <= 1f) {

                currentColor = Color.Lerp(startCol, endCol, time);
                background.color = currentColor;
                time += Time.deltaTime / speed;
                yield return null;
            }

            // On fade complete
            Widget_Fade.SetActive(!hideWidgetWhenComplete);
            if (methodWhenComplete != null)
                methodWhenComplete();
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //**************************************************************************************************************
    //                                                                                                             * 
    //      GETTERS & SETTERS                                                                                      * 
    //                                                                                                             * 
    //**************************************************************************************************************

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Returns reference to the current widget gameObject based on the current menu state enumerator.
    /// </summary>
    /// <returns>
    //  GameObject
    /// </returns>
    private GameObject GetCurrentMenuWidget() {

        GameObject widget = null;
        switch (_CurrentMenuState) {

            case ECurrentMenuState.Mainmenu:
                widget = Widget_MainMenu;
                break;

            case ECurrentMenuState.Matchmaking:
                widget = Widget_Matchmaking;
                break;

            case ECurrentMenuState.HostPrivateLobby:
                widget = Widget_HostPrivateLobby;
                break;

            case ECurrentMenuState.DifficultyPlaylists:
                widget = Widget_DifficultyPlaylist;
                break;

            case ECurrentMenuState.StartTutorial:
                widget = Widget_StartTutorial;
                break;

            case ECurrentMenuState.ConnectIP:
                widget = Widget_ConnectIP;
                break;

            case ECurrentMenuState.ConfirmQuit:
                widget = Widget_ConfirmQuit;
                break;

            case ECurrentMenuState.SelectLevel:
                widget = Widget_LevelsList;
                break;

            case ECurrentMenuState.SelectDifficulty:
                widget = Widget_DifficultiesList;
                break;

            case ECurrentMenuState.SelectModifiers:
                widget = Widget_ModifierList;
                break;

            default: break;
        }
        return widget;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

}
