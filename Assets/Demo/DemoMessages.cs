using EventDispatcher;
using UnityEngine;

public class RotationMessage : EventMessage
{
    public Vector3 eulerAnglesRotation;
    public float time;

    public RotationMessage(Vector3 eulerAnglesRotation, float time)
    {
        this.eulerAnglesRotation = eulerAnglesRotation;
        this.time = time;
    }
}

public class ColorMessage : EventMessage
{
    public Color color;

    public ColorMessage(Color color)
    {
        this.color = color;
    }
}