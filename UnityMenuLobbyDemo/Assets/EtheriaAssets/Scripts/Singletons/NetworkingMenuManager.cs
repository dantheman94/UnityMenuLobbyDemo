using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

//******************************
//
//  Created by: Daniel Marton
//
//  Last edited by: Daniel Marton
//  Last edited on: 13/7/2019
//
//******************************

public class NetworkingMenuManager : NetworkManager {

    //**************************************************************************************************************
    //                                                                                                             *
    //      INSPECTOR                                                                                              *
    //                                                                                                             *
    //**************************************************************************************************************

    [Space]
    [Header("----------------------------------------------------------------------------------------------------")]
    [Header("*** GAME MANAGER CLASS *** ")]
    [Header(" Parenting Folders ")]
    [Space]
    public Transform FolderPlayers;
    public Transform FolderPlayerCards;
    [Header(" Lobby Header ")]
    [Space]
    public Text LobbyCountText;

    //**************************************************************************************************************
    //                                                                                                             *
    //      VARIABLES                                                                                              *
    //                                                                                                             *
    //**************************************************************************************************************

    public static NetworkingMenuManager Instance;

    public static readonly int MaxPlayerCount = 2;
    public static int NumConnectedPlayers = 0;

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

        // Initialize singleton
        if (Instance != null && Instance != this) {

            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Fade in screen to black
        WidgetManager.Instance.FadeScreen(Color.black, new Color(0, 0, 0, 0), 3, true);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Called on the server when a client adds a new player with ClientScene.AddPlayer.
    /// </summary>
    // <param name="conn"></param>
    // <param name="playerControllerId"></param>
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {

        // Create the player prefab instance
        var obj = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity, FolderPlayers);
        NetworkServer.AddPlayerForConnection(conn, obj, playerControllerId);

        // Initialize the player
        Player player = obj.GetComponent<Player>();
        player.CmdInit(numPlayers - 1);
        player.CmdCreateCard();
        player.gameObject.SetActive(true);

        LobbyCountText.text = string.Concat(numPlayers.ToString() + " / " + maxConnections.ToString());
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Called on the server when a client removes a player.
    /// </summary>
    // <param name="conn"></param>
    // <param name="player"></param>
    public override void OnServerRemovePlayer(NetworkConnection conn, PlayerController player) {

    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Called on the client when connected to a server.
    /// </summary>
    //  <param name="conn"></param>
    public override void OnClientConnect(NetworkConnection conn) {
        base.OnClientConnect(conn);

        ///NumConnectedPlayers += 1;

        // Enable any NetworkIdentityGameObjects that are meant to be active from startup
        NetworkServer.SpawnObjects();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Called on the server when a client connects.
    /// </summary>
    //  <param name="conn"></param>
    public override void OnServerConnect(NetworkConnection conn) {
        base.OnServerConnect(conn);

        NumConnectedPlayers += 1;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    //  Called on the client when disconnected to a server.
    /// </summary>
    //  <param name="conn"></param>
    public override void OnClientDisconnect(NetworkConnection conn) {
        base.OnClientDisconnect(conn);

        NumConnectedPlayers -= 1;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //**************************************************************************************************************
    //                                                                                                             * 
    //      COROUTINES                                                                                             * 
    //                                                                                                             * 
    //**************************************************************************************************************

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //**************************************************************************************************************
    //                                                                                                             * 
    //      GETTERS & SETTERS                                                                                      * 
    //                                                                                                             * 
    //**************************************************************************************************************

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

}
