using UnityEngine;
using Unity.Netcode;

public class ColourTrigger : NetworkBehaviour
{
    public NetworkVariable<Color> netColor = new NetworkVariable<Color>(Color.white);
    private Material instanceMat;

    public override void OnNetworkSpawn()
    {
        netColor.OnValueChanged += OnColorChanged;
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null )
        {
            instanceMat = new Material(meshRenderer.material);
            meshRenderer.material = instanceMat;
            UpdateMaterialColor(netColor.Value);
        }
    }

    public override void OnNetworkDespawn()
    {
        netColor.OnValueChanged -= OnColorChanged;
    }

    private void OnColorChanged(Color oldColor, Color newColor)
    {
        UpdateMaterialColor(newColor);
    }

    private void UpdateMaterialColor(Color newColor)
    {
        if (instanceMat != null)
        {
            instanceMat.SetColor("_BaseColor", newColor);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        NetworkObject networkObject = other.GetComponent<NetworkObject>();
        if (IsClient && networkObject != null && networkObject.IsOwner)
        {
            ChangeColorServerRpc(networkObject.OwnerClientId);
        }
    }

    [Rpc(SendTo.Server)]
    private void ChangeColorServerRpc(ulong playerId)
    {
        Color newColor =
            (playerId % 2 == 0) ? new Color(0, 1, 0.5f, 0.5f) : new Color(1, 0, 0.5f, 0.5f);

        netColor.Value = newColor;
    }
}
