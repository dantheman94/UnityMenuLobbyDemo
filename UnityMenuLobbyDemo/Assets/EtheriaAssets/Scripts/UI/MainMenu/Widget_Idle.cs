using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

//******************************
//
//  Created by: Daniel Marton
//
//  Last edited by: Daniel Marton
//  Last edited on: 31/1/2019
//
//******************************

public class Widget_Idle : MonoBehaviour {

    //**************************************************************************************************************
    //                                                                                                             *
    //      INSPECTOR                                                                                              *
    //                                                                                                             *
    //**************************************************************************************************************
    
    [Header(" Widget Idle ")]
    [Space]
    public float IdleWaitThreshold = 5f;
    [Space]
    public GameObject GO_HeaderTitleImage;
    public GameObject GO_HeaderTitleText;
    public GameObject GO_ButtonWrapper;

    //**************************************************************************************************************
    //                                                                                                             *
    //      VARIABLES                                                                                              *
    //                                                                                                             *
    //**************************************************************************************************************
    
    private IEnumerator _EntryCoroutine;
    private IEnumerator _ExitCoroutine;

    private Animator _AnimationController;

    //**************************************************************************************************************
    //                                                                                                             * 
    //      FUNCTIONS                                                                                              * 
    //                                                                                                             * 
    //**************************************************************************************************************

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Called just before Update() for the first time.
    /// </summary>
    private void Start() {

        _AnimationController = GetComponent<Animator>();

        _EntryCoroutine = IdleEntryCheck();
        StartCoroutine(_EntryCoroutine);
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
    //  <param name="message"></param>
    /// <returns>
    //  IEnumerator
    /// </returns>
    private IEnumerator IdleEntryCheck() {
        
        // Delay
        float normalizedTime = 0f;
        while (normalizedTime <= 1f) {

            // Reset the idle timer if there's any input
            if (Input.anyKey)
                normalizedTime = 0f;

            normalizedTime += Time.deltaTime / IdleWaitThreshold;
            yield return null;
        }
        
        _AnimationController.SetBool("PlayReverse", false);
        _AnimationController.SetBool("PlayForward", true);

        _ExitCoroutine = IdleExitCheck();
        StartCoroutine(_ExitCoroutine);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  
    /// </summary>
    //  <param name="message"></param>
    /// <returns>
    //  IEnumerator
    /// </returns>
    private IEnumerator IdleExitCheck() {

        // Wait until theres some input from the user
        yield return new WaitUntil(() => Input.anyKey);
        
        _AnimationController.SetBool("PlayForward", false);
        _AnimationController.SetBool("PlayReverse", true);

        StartCoroutine(IdleEntryCheck());
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

}
