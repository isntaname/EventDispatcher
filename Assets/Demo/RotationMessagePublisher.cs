using UnityEngine;
using UnityEngine.UI;

public class RotationMessagePublisher : MonoBehaviour
{
    [SerializeField]
    private InputField time;
    [SerializeField]
    private InputField rotationX;
    [SerializeField]
    private InputField rotationY;
    [SerializeField]
    private InputField rotationZ;
    [SerializeField]
    private Button applyButton;

    private void Awake()
    {
        applyButton.onClick.AddListener(SendRotationMessage);
    }

    private void SendRotationMessage()
    {
        float x, y, z, t;
        float.TryParse(rotationX.text, out x);
        float.TryParse(rotationY.text, out y);
        float.TryParse(rotationZ.text, out z);
        float.TryParse(time.text, out t);
        var v3 = new Vector3(x, y, z);
        var message = new RotationMessage(v3, t <= 0 ? 1 : t);
        EventMessageDispatcher.Dispatch(message);
        
    }
}