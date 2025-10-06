using Unity.Netcode;
using UnityEngine;

public class ServerSetup : MonoBehaviour
{
    public void startServer()
    {
        NetworkManager.Singleton.StartServer();
    }

    public void startHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void startClient()
    {
        NetworkManager.Singleton.StartClient();
    }
}
