using System;
using System.Collections.Generic;
using UnityEngine;

namespace EventDispatcher
{
    public static class FastMessagesPipe
    {
        private static readonly Dictionary<Type, IFastMessagePublisher> publishers;

        static FastMessagesPipe()
        {
            publishers = new Dictionary<Type, IFastMessagePublisher>();
        }

        private static void AddPublisher<T>(T observer) where T : IFastMessagePublisher
        {
            publishers.Add(typeof(T), observer);
        }

        private static FastMessagePublisher<T> GetPublisherForMessage<T>() where T : struct, IFastMessage
        {
            if (publishers.TryGetValue(typeof(FastMessagePublisher<T>), out var val))
            {
                return val as FastMessagePublisher<T>;
            }

            var newPublisher = new FastMessagePublisher<T>();
            AddPublisher(newPublisher);
            return newPublisher;
        }

        public static void SendMessage<T>(T message) where T : struct, IFastMessage
        {
            var publisher = GetPublisherForMessage<T>();
            publisher.SendMessage(message);
        }

        public static void AddListener<T>(IFastMessageListener<T> listener) where T : struct, IFastMessage
        {
            var publisher = GetPublisherForMessage<T>();
            publisher.AddListener(listener);
        }

        public static void RemoveListener<T, T1>(T1 listener)
            where T : struct, IFastMessage where T1 : IFastMessageListener<T>
        {
            var publisher = GetPublisherForMessage<T>();
            publisher.RemoveListener(listener);
        }
    }
}