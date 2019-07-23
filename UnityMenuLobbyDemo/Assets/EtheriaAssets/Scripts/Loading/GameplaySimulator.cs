using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//******************************
//
//  Created by: Daniel Marton
//
//  Last edited by: Daniel Marton
//  Last edited on: 13/7/2019
//
//******************************

public class GameplaySimulator : MonoBehaviour {


    //**************************************************************************************************************
    //                                                                                                             *
    //      INSPECTOR                                                                                              *
    //                                                                                                             *
    //**************************************************************************************************************

    [Space]
    [Header("-----------------------------------")]
    [Header(" Gameplay Simulator ")]
    [Space]
    public Text SimulationText;
    public Slider SimulationSlider;

    //**************************************************************************************************************
    //                                                                                                             *
    //      VARIABLES                                                                                              *
    //                                                                                                             *
    //**************************************************************************************************************

    private float _SliderThreshold_LoadComplete = 0.25f;
    private float _LoadingMatchPauseTime = 1f;
    private float _LoadingMatchSliderRate = 1f;

    private float _SliderThreshold_Gameplay = 0.75f;
    private float _GameplayPauseTime = 2f;
    private float _GameplaySliderRate = 2f;

    private float _CurrentSlidePosition = 0f;
    private float _SliderSpeed = 1f;
    private bool _SliderMove = false;
   

    //**************************************************************************************************************
    //                                                                                                             * 
    //      FUNCTIONS                                                                                              * 
    //                                                                                                             * 
    //**************************************************************************************************************

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Called when the script instance is being loaded.
    /// </summary>
    private void Start() {

        // Fade in screen from black
        WidgetManager.Instance.Widget_Fade.SetActive(true);
        Image img = WidgetManager.Instance.Widget_Fade.GetComponentInChildren<Image>();
        if (img)
            img.color = Color.black;

        StartCoroutine(WidgetManager.Instance.DelayedFunctionCall(1, () => WidgetManager.Instance.FadeScreen(Color.black, new Color(0, 0, 0, 0), 3, true)));
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Called once each frame.
    /// </summary>
    private void Update() {

        // Sanity check
        if (SimulationSlider) {

            if (_SliderMove)
                _CurrentSlidePosition += Time.deltaTime * _SliderSpeed;

            SimulationSlider.value = _CurrentSlidePosition;
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Starts simulating the gameplay sequence.
    /// </summary>
    private void SimulateMatch() {

        // Sanity check
        if (SimulationSlider) {

            StartCoroutine(LoadingMatch());
        }
    }

    //**************************************************************************************************************
    //                                                                                                             * 
    //      COROUTINES                                                                                             * 
    //                                                                                                             * 
    //**************************************************************************************************************

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  
    /// </summary>
    /// <returns>
    //  IEnumerator
    /// </returns>
    private IEnumerator LoadingMatch() {

        // Start Loading level
        while (_CurrentSlidePosition < _SliderThreshold_LoadComplete) {  

            _SliderSpeed = _LoadingMatchSliderRate;
            _SliderMove = true;
            yield return null;
        }

        yield return new WaitForSeconds(_LoadingMatchPauseTime);

        StartCoroutine(SimulateGameplay());
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  
    /// </summary>
    /// <returns>
    //  IEnumerator
    /// </returns>
    private IEnumerator SimulateGameplay() {

        // Start simulating gameplay
        if (_CurrentSlidePosition >= _SliderThreshold_LoadComplete && _CurrentSlidePosition < _SliderThreshold_Gameplay) {

            _SliderSpeed = _GameplaySliderRate;
            _SliderMove = true;
        }
        yield return null;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  
    /// </summary>
    /// <returns>
    //  IEnumerator
    /// </returns>
    private IEnumerator SubmittingResults() {

        yield return null;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //**************************************************************************************************************
    //                                                                                                             * 
    //      GETTERS & SETTERS                                                                                      * 
    //                                                                                                             * 
    //**************************************************************************************************************

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

}
