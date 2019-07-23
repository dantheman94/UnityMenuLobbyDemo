using System;
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

public class NetworkNotifyConnection : NetworkConnection {
    public MultiplayerGame Game;

    public override void Initialize(string networkAddress, int networkHostId, int networkConnectionId, HostTopology hostTopology) {
        base.Initialize(networkAddress, networkHostId, networkConnectionId, hostTopology);
        Game = NetworkManager.singleton.GetComponent<MultiplayerGame>();
    }

    public override bool SendByChannel(short msgType, MessageBase msg, int channelId) {
        Prefilter(msgType, msg, channelId);
        if (base.SendByChannel(msgType, msg, channelId)) {
            Postfilter(msgType, msg, channelId);
            return true;
        }
        return false;
    }

    private void Prefilter(short msgType, MessageBase msg, int channelId) {
    }

    private void Postfilter(short msgType, MessageBase msg, int channelId) {
        if (msgType == MsgType.ObjectSpawn || msgType == MsgType.ObjectSpawnScene) {
            // NetworkExtensions.GetObjectSpawnNetId uses reflection to extract private 'netId' field
            ///Game.OnObjectSpawn(NetworkExtensions.GetObjectSpawnNetId(msg), this);
        }
        else if (msgType == MsgType.ObjectDestroy) {
            // NetworkExtensions.GetObjectDestroyNetId uses reflection to extract private 'netId' field
            ///Game.OnObjectDestroy(NetworkExtensions.GetObjectDestroyNetId(msg), this);
        }
    }
}