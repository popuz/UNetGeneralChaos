﻿using System.Collections;
using System.Collections.Generic;
using DatabaseControl;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MyNetworkManager : NetworkManager
{
    public bool serverMode;

    public delegate void ResponseDelegate(string response);

    public ResponseDelegate loginResponseDelegate;
    public ResponseDelegate registerResponseDelegate;

    void Start()
    {
        if (serverMode)
        {
            StartServer();
            NetworkServer.UnregisterHandler(MsgType.Connect);
            NetworkServer.RegisterHandler(MsgType.Connect, OnServerConnectCustom);
            NetworkServer.RegisterHandler(MsgType.Highest + (short) NetMsgType.Login, OnServerLogin);
            NetworkServer.RegisterHandler(MsgType.Highest + (short) NetMsgType.Register, OnServerRegister);
        }
    }

    // методы, вызываемые UI для отправки запроса
    public void Login(string login, string pass)
    {
        ClientConnect();
        StartCoroutine(SendLogin(login, pass));
    }

    public void Register(string login, string pass)
    {
        ClientConnect();
        StartCoroutine(SendRegister(login, pass));
    }

    IEnumerator SendLogin(string login, string pass)
    {
        while (!client.isConnected) yield return null;
        Debug.Log("client login");
        client.connection.Send(MsgType.Highest + (short) NetMsgType.Login, new UserMessage(login, pass));
    }

    IEnumerator SendRegister(string login, string pass)
    {
        while (!client.isConnected) yield return null;
        Debug.Log("client register");
        client.connection.Send(MsgType.Highest + (short) NetMsgType.Register, new UserMessage(login, pass));
    }


    IEnumerator LoginUser(NetworkMessage netMsg)
    {
        UserMessage msg = netMsg.ReadMessage<UserMessage>();
        IEnumerator e = DCF.Login(msg.login, msg.pass);
        while (e.MoveNext())
            yield return e.Current;

        string response = e.Current as string;
        if (response == "Success")
        {
            Debug.Log("server login success");
            netMsg.conn.Send(MsgType.Scene, new StringMessage(SceneManager.GetActiveScene().name));
        }
        else
        {
            Debug.Log("server login fail");
            netMsg.conn.Send(MsgType.Highest + (short) NetMsgType.Login, new StringMessage(response));
        }
    }

    IEnumerator RegisterUser(NetworkMessage netMsg)
    {
        UserMessage msg = netMsg.ReadMessage<UserMessage>();
        IEnumerator e = DCF.RegisterUser(msg.login, msg.pass, "");
        while (e.MoveNext())
            yield return e.Current;

        string response = e.Current as string;
        Debug.Log("server register done");
        netMsg.conn.Send(MsgType.Highest + (short) NetMsgType.Register, new StringMessage(response));
    }

    void OnServerConnectCustom(NetworkMessage netMsg)
    {
        if (LogFilter.logDebug)
        {
            Debug.Log("NetworkManager:OnServerConnectCustom");
        }

        netMsg.conn.SetMaxDelay(maxDelay);
        OnServerConnect(netMsg.conn);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        NetworkServer.DestroyPlayersForConnection(conn);
        if (conn.lastError != NetworkError.Ok)
        {
            if (LogFilter.logError)
            {
                Debug.LogError("ServerDisconnected due to error: " +
                               conn.lastError);
            }
        }

        Debug.Log("A client disconnected from the server: " + conn);
    }


    void ClientConnect()
    {
        NetworkClient client = this.client;
        if (client == null)
        {
            client = StartClient();
            client.RegisterHandler(MsgType.Highest + (short) NetMsgType.Login, OnClientLogin);
            client.RegisterHandler(MsgType.Highest + (short) NetMsgType.Register, OnClientRegister);
        }
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        NetworkServer.SetClientReady(conn);
        Debug.Log("Client is set to the ready state (ready to receive state updates): " + conn);
    }

    // Server callbacks
    public override void OnServerAddPlayer(NetworkConnection conn, short
        playerControllerId)
    {
        GameObject player = Instantiate(playerPrefab, Vector3.zero,
            Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        Debug.Log("Client has requested to get his player added to the game");
    }

    public override void OnServerRemovePlayer(NetworkConnection conn,
        UnityEngine.Networking.PlayerController player)
    {
        if (player.gameObject != null)
            NetworkServer.Destroy(player.gameObject);
    }


    public override void OnClientDisconnect(NetworkConnection conn)
    {
        StopClient();
        if (conn.lastError != NetworkError.Ok)
        {
            if (LogFilter.logError)
            {
                Debug.LogError("ClientDisconnected due to error: " +
                               conn.lastError);
            }
        }

        Debug.Log("Client disconnected from server: " + conn);
    }

    void OnServerLogin(NetworkMessage netMsg)
    {
        StartCoroutine(LoginUser(netMsg));
    }

    void OnServerRegister(NetworkMessage netMsg)
    {
        StartCoroutine(RegisterUser(netMsg));
    }


    void OnClientLogin(NetworkMessage netMsg)
    {
        loginResponseDelegate.Invoke(netMsg.reader.ReadString());
    }

    void OnClientRegister(NetworkMessage netMsg)
    {
        registerResponseDelegate.Invoke(netMsg.reader.ReadString());
    }
}

public class UserMessage : MessageBase
{
    public string login;
    public string pass;

    // конструктор, обязательный для работы Unet с наследником
    public UserMessage()
    {
    }

    public UserMessage(string login, string pass)
    {
        this.login = login;
        this.pass = pass;
    }

    public override void Deserialize(NetworkReader reader)
    {
        login = reader.ReadString();
        pass = reader.ReadString();
    }

    public override void Serialize(NetworkWriter writer)
    {
        writer.Write(login);
        writer.Write(pass);
    }
}

public enum NetMsgType
{
    Login,
    Register
}