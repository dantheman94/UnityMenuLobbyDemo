using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//******************************
//
//  Created by: Daniel Marton
//
//  Last edited by: Daniel Marton
//  Last edited on: 17/1/2019
//
//******************************

/// <summary>
//  Resources used:
//  - https://stackoverflow.com/questions/45908172/synchronizing-complex-gameobjects-in-unity-networking-unet
/// </summary>

/// <summary>
//  Simple component which starts multiplayer game right on start.
/// </summary>
public class MultiplayerGame : MonoBehaviour {
    HashSet<NetworkIdentity> m_dirtyObj = new HashSet<NetworkIdentity>();

    private void Start() {
        var net = NetworkManager.singleton;

        var host = net.StartHost();
        if (host != null) {
            NetworkServer.SetNetworkConnectionClass<NetworkNotifyConnection>();
        }
    }

    /// <summary>
    /// Reliable callback called on server when client receives new object.
    /// </summary>
    public void OnObjectSpawn(NetworkInstanceId objectId, NetworkConnection conn) {
        var obj = NetworkServer.FindLocalObject(objectId);
        RefreshChildren(obj.transform);
    }

    /// <summary>
    /// Reliable callback called on server when client loses object.
    /// </summary>
    public void OnObjectDestroy(NetworkInstanceId objectId, NetworkConnection conn) {
    }

    void RefreshChildren(Transform obj) {
        ///foreach (var child in obj.GetChildren()) {
        ///    NetworkIdentity netId;
        ///    if (child.TryGetComponent(out netId)) {
        ///        m_dirtyObj.Add(netId);
        ///    }
        ///    else {
        ///        RefreshChildren(child);
        ///    }
        ///}
    }

    private void Update() {
        ///NetworkIdentity netId;
        ///while (m_dirtyObj.RemoveFirst(out netId)) {
        ///    if (netId != null) {
        ///        netId.RebuildObservers(false);
        ///    }
        ///}
    }
}