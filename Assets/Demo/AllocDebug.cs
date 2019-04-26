using EventDispatcher;
using UnityEngine;
using UnityEngine.UI;

public class AllocDebug : MonoBehaviour
{
    private long lastFrameAlloc;
    [SerializeField]
    private long nowDiff;

    [SerializeField]
    private int listenersCount = 100;

    private TestListener[] listeners;

    private void Awake()
    {
        listeners = new TestListener[listenersCount];
        for (var i = 0; i < listenersCount; i++)
        {
            listeners[i] = new TestListener();
        }
    }

    private void LateUpdate()
    {
        var nowAlloc = System.GC.GetTotalMemory(false);
        nowDiff = (nowAlloc - lastFrameAlloc) / 1024;
        lastFrameAlloc = nowAlloc;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            for (var i = 0; i <= 1000; i++)
            {
                var m1 = new AllocTestMessage() {value = "test"};
                FastMessagesPipe.SendMessage(m1);
            }
        }

        if (Input.GetKey(KeyCode.Return))
        {
            for (var i = 0; i <= 1000; i++)
            {
                var m1 = new AllocTestMessageSlow() {value = "test"};
                EventMessageDispatcher.Dispatch(m1);
            }
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(), nowDiff + "kb", new GUIStyle() {fontSize = 48});
    }

    public struct AllocTestMessage : IFastMessage
    {
        public string value;
    }

    public class AllocTestMessageSlow : EventMessage
    {
        public string value;
    }

    public class TestListener : EventMessageReceiver<AllocTestMessageSlow>, IFastMessageListener<AllocTestMessage>
    {
        private string val;

        public TestListener()
        {
            FastMessagesPipe.AddListener(this);
        }

        protected override void ReadMessage(AllocTestMessageSlow message)
        {
            val = message.value;
        }

        public void OnMessageReceived(AllocTestMessage message)
        {
            val = message.value;
        }
    }
}