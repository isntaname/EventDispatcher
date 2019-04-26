using System.Collections;
using EventDispatcher;
using UnityEngine;

public class RotationMessageReceiver : MonoBehaviourEventMessageReceiver<RotationMessage>
{
    private IEnumerator rotateEnumerator;

    protected override void ReadMessage(RotationMessage message)
    {
        if (rotateEnumerator != null)
        {
            StopCoroutine(rotateEnumerator);
        }
        rotateEnumerator = Rotate(message.eulerAnglesRotation, message.time);
        StartCoroutine(rotateEnumerator);
    }

    private IEnumerator Rotate(Vector3 angle, float time)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + angle);
        for (var t = 0f; t < 1; t += Time.deltaTime / time)
        {
            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
    }
}