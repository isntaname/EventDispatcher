using EventDispatcher;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button), typeof(Image))]
public class ColorMessagePublisher : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(SendColorMessage);
    }

    private void SendColorMessage()
    {
        var color = GetComponent<Image>().color;
        var message = new ColorMessage(color);
        EventMessageDispatcher.Dispatch(message);
    }
}