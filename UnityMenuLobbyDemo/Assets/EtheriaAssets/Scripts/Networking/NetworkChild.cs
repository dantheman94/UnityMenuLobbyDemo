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
// Base component for network child objects.
/// </summary>
public abstract class NetworkChild : NetworkBehaviour {
    private NetworkIdentity m_networkParent;

    [SyncVar(hook = "OnNetParentChanged")]
    private NetworkInstanceId m_networkParentId;

    public NetworkIdentity NetworkParent {
        get { return m_networkParent; }
    }

    #region Server methods
    public override void OnStartServer() {
        UpdateParent();
        base.OnStartServer();
    }

    [ServerCallback]
    public void RefreshParent() {
        UpdateParent();
        GetComponent<NetworkIdentity>().RebuildObservers(false);
    }

    void UpdateParent() {
        NetworkIdentity parent = transform.parent != null ? transform.parent.GetComponentInParent<NetworkIdentity>() : null;
        m_networkParent = parent;
        m_networkParentId = parent != null ? parent.netId : NetworkInstanceId.Invalid;
    }

    public override bool OnCheckObserver(NetworkConnection conn) {
        // Parent id might not be set yet (but parent is)
        m_networkParentId = m_networkParent != null ? m_networkParent.netId : NetworkInstanceId.Invalid;

        if (m_networkParent != null && m_networkParent.observers != null) {
            // Visible only when parent is visible
            return m_networkParent.observers.Contains(conn);
        }
        return false;
    }

    public override bool OnRebuildObservers(HashSet<NetworkConnection> observers, bool initialize) {
        // Parent id might not be set yet (but parent is)
        m_networkParentId = m_networkParent != null ? m_networkParent.netId : NetworkInstanceId.Invalid;

        if (m_networkParent != null && m_networkParent.observers != null) {
            // Who sees parent will see child too
            foreach (var parentObserver in m_networkParent.observers) {
                observers.Add(parentObserver);
            }
        }
        return true;
    }
    #endregion

    #region Client Methods
    public override void OnStartClient() {
        base.OnStartClient();
        FindParent();
    }

    void OnNetParentChanged(NetworkInstanceId newNetParentId) {
        if (m_networkParentId != newNetParentId) {
            m_networkParentId = newNetParentId;
            FindParent();
            OnParentChanged();
        }
    }

    /// <summary>
    /// Called on client when server sends new parent
    /// </summary>
    protected virtual void OnParentChanged() {
    }

    private void FindParent() {
        if (NetworkServer.localClientActive) {
            // Both server and client, NetworkParent already set
            return;
        }

        if (!ClientScene.objects.TryGetValue(m_networkParentId, out m_networkParent)) {
            Debug.AssertFormat(false, "NetworkChild, parent object {0} not found", m_networkParentId);
        }
    }
    #endregion
}