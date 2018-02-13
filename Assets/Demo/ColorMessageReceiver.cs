using UnityEngine;

public class ColorMessageReceiver : MonoBehaviourEventMessageReceiver<ColorMessage>
{
    [SerializeField]
    private Renderer targetRenderer;

    protected override void ReadMessage(ColorMessage message)
    {
        var mat = Instantiate(targetRenderer.material);
        mat.color = message.color;
        targetRenderer.material = mat;
    }
}