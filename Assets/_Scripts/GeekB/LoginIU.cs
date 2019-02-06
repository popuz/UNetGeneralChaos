using UnityEngine;
using UnityEngine.Networking;

public class LoginIU : MonoBehaviour
{
    [SerializeField] private GameObject loginPanel;

    private void Start()
    {
        if ((NetworkManager.singleton as MyNetworkManager).serverMode)
            loginPanel.SetActive(false);
    }

    public void Login()
    {
        NetworkManager.singleton.StartClient();
    }
}