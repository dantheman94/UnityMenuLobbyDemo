using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//******************************
//
//  Created by: Daniel Marton
//
//  Last edited by: Daniel Marton
//  Last edited on: 13/07/2019
//
//******************************

public class ASyncLoad : MonoBehaviour {

    //**************************************************************************************************************
    //                                                                                                             *
    //      VARIABLES                                                                                              *
    //                                                                                                             *
    //**************************************************************************************************************

    public AsyncOperation Async { get; private set; }
    public static ASyncLoad Instance;

    //**************************************************************************************************************
    //                                                                                                             * 
    //      FUNCTIONS                                                                                              * 
    //                                                                                                             * 
    //**************************************************************************************************************

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  This is called before Startup().
    /// </summary>
    public void Awake() {

        // If a singleton instance of this class already exists
        if (Instance != null && Instance != this) {

            // Destroy the new class
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Starts loading a scene (by index) in the background.
    /// </summary>
    // <param name="index"></param>
    public void LoadLevelByIndex(int index, bool activateSceneOnLoad = false) {

        Async = SceneManager.LoadSceneAsync(index);
        Async.allowSceneActivation = activateSceneOnLoad;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //**************************************************************************************************************
    //                                                                                                             * 
    //      GETTERS & SETTERS                                                                                      * 
    //                                                                                                             * 
    //**************************************************************************************************************

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Activates the level to become the new current scene (must be loaded completely first)
    /// </summary>
    public void ActivateLevel() { Async.allowSceneActivation = true; }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Returns whether the level loading is complete.
    /// </summary>
    /// <returns>
    //  bool
    /// </returns>
    public bool LoadComplete() { return Async.isDone; }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Returns the progress percentage of the current scene being loaded.
    /// </summary>
    /// <returns>
    //  float
    /// </returns>
    public float GetSceneLoadProgress() { return Async.progress; }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

}
