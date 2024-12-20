using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;


public class InventoryRequestHandler : MonoBehaviour
{
    private BackpackManager _backpackManager;

    [Inject]
    public void Construct(BackpackManager backpackManager)
    {
        _backpackManager = backpackManager;
    }

    private void OnEnable()
    {
        foreach (var backPack in _backpackManager.Backpacks)
        {
            SubscribeToBackpack(backPack);
        }
    }

    private void SubscribeToBackpack(BackPack backPack)
    {
        backPack.OnItemAdded.AddListener(HandleItemAdded);
        backPack.OnItemRemoved.AddListener(HandleItemRemoved);
    }

    private void HandleItemAdded(IItem item)
    {
        StartCoroutine(SendInventoryData(item.ID, "added"));
    }

    private void HandleItemRemoved(IItem item)
    {
        StartCoroutine(SendInventoryData(item.ID, "removed"));
    }

    private IEnumerator SendInventoryData(string itemID, string action)
    {
        UnityWebRequest request = new UnityWebRequest("https://wadahub.manerai.com/api/inventory/status", "POST");
        string jsonData = $"{{\"item_id\": \"{itemID}\", \"action\": \"{action}\"}}";
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer kPERnYcWAY46xaSy8CEzanosAgsWM84Nx7SKM4QBSqPq6c7StWfGxzhxPfDh8MaP");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"Data sent successfully: {request.downloadHandler.text}");
        }
        else
        {
            Debug.LogError($"Error: {request.error}");
        }
    }
}


