using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//******************************
//
//  Created by: Daniel Marton
//
//  Last edited by: Daniel Marton
//  Last edited on: 13/7/2019
//
//******************************

public class Player : NetworkBehaviour {

    //**************************************************************************************************************
    //                                                                                                             *
    //      INSPECTOR                                                                                              *
    //                                                                                                             *
    //**************************************************************************************************************

    [Space]
    [Header("----------------------------------------------------------------------------------------------------")]
    [Header("*** PLAYER CLASS *** ")]
    [Header(" Menu Setup ")]
    [Space]
    public GameObject MenuCardPrefab = null;

    //**************************************************************************************************************
    //                                                                                                             *
    //      VARIABLES                                                                                              *
    //                                                                                                             *
    //**************************************************************************************************************

    private GameObject _PlayerCardGO;

    //**************************************************************************************************************
    //                                                                                                             * 
    //      FUNCTIONS                                                                                              * 
    //                                                                                                             * 
    //**************************************************************************************************************

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Initializes the player.
    /// </summary>
    [Command]
    public void CmdInit(int playerIndex) { RpcInit(playerIndex); }

    /// <summary>
    //  Initializes the player.
    /// </summary>
    [ClientRpc]
    public void RpcInit(int playerIndex) {
        
        // Set gameObject.name
        if (isLocalPlayer)
            gameObject.name = "Player_" + playerIndex + "_Local";
        else
            gameObject.name = "Player_" + playerIndex;

    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Spawns the menu playercard gameObject.
    /// </summary>
    [Command]
    public void CmdCreateCard() { RpcCreateCard(); }

    /// <summary>
    //  Spawns the menu playercard gameObject.
    /// </summary>
    [ClientRpc]
    public void RpcCreateCard() {

        // Spawn player card object
        _PlayerCardGO = NetworkPool.ServerSpawn(MenuCardPrefab, Vector3.zero, Quaternion.identity);
        _PlayerCardGO.transform.SetParent(NetworkingMenuManager.Instance.FolderPlayerCards);

        // Set position in 2D space
        RectTransform rect = _PlayerCardGO.GetComponent<RectTransform>();
        rect.localPosition = new Vector2(0f, 0f);
        rect.anchoredPosition = new Vector2(0, -40 - (NetworkingMenuManager.Instance.numPlayers - 1) * 80);
        rect.sizeDelta = new Vector2(0, 80);

        //rect.rect.Set(0f, 0f, rect.rect.width, rect.rect.height);
        //rect.rect.position.Set(rect.rect.position.x, -40 - (NetworkingMenuManager.Instance.numPlayers - 1) * 80);        
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //**************************************************************************************************************
    //                                                                                                             * 
    //      COROUTINES                                                                                             * 
    //                                                                                                             * 
    //**************************************************************************************************************

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //**************************************************************************************************************
    //                                                                                                             * 
    //      GETTERS & SETTERS                                                                                      * 
    //                                                                                                             * 
    //**************************************************************************************************************

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

}
