using System;
using System.Collections.Generic;
using UnityEngine;

namespace EventDispatcher
{
    public static class EventMessageDispatcher
    {
        private static readonly Dictionary<Type, HashSet<IEventMessageReceiver>> receivers =
            new Dictionary<Type, HashSet<IEventMessageReceiver>>();

        static EventMessageDispatcher()
        {
        }

        /// <summary>
        /// Sends message to all receivers which can receive message of that type.
        /// </summary>
        /// <param name="message">Message to be sent</param>
        public static void Dispatch(IEventMessage message)
        {
            var messageType = message.GetType();
            if (!receivers.ContainsKey(messageType)) return;
            foreach (var receiver in receivers[messageType])
            {
                receiver.ApplyMessage(message);
            }
        }

        /// <summary>
        /// Adds receiver to the list of receivers.
        /// </summary>
        /// <param name="receiver"></param>
        public static void AddReceiver(IEventMessageReceiver receiver)
        {
            var receiverMessageType = GetRecieverMessageType(receiver);
            if (!receivers.ContainsKey(receiverMessageType))
            {
                receivers.Add(receiverMessageType, new HashSet<IEventMessageReceiver>());
            }
            receivers[receiverMessageType].Add(receiver);
        }

        /// <summary>
        /// Removes receiver from the list of receivers.
        /// </summary>
        /// <param name="receiver"></param>
        public static void RemoveReceiver(IEventMessageReceiver receiver)
        {
            var receiverMessageType = GetRecieverMessageType(receiver);
            if (receivers.ContainsKey(receiverMessageType) && receivers[receiverMessageType] != null)
            {
                receivers[receiverMessageType].Remove(receiver);
            }
        }

        private static Type GetRecieverMessageType(IEventMessageReceiver receiver)
        {
            var recieverType = receiver.GetType().BaseType;
            if (recieverType != null)
            {
                var genArgs = recieverType.GetGenericArguments();
                if (genArgs.Length > 0)
                    return genArgs[0];
            }
            Debug.LogError("Can't find base MessageReceiver for this type");
            return null;
        }
    }
}