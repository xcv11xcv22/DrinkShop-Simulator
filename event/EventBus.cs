// EventBus.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DrinkShop.Events
{
    public class EventBus
    {
        private static readonly EventBus _instance = new EventBus();
        public static EventBus Instance => _instance;

        private bool isProcessing = false;
        private readonly Dictionary<Type, List<Action<IEvent>>> _subscribers = new();
        private readonly string _logPath = "event_log.txt";
        private readonly Queue<string> _logQueue = new();
        private readonly object _logLock = new();

        private EventBus()
        {
            Subscribe<DrinkCompletedEvent>(LogDrinkCompleted);
            Subscribe<CustomerEnterEvent>(LogCustomerEnter);
        }

        public void Subscribe<T>(Action<T> handler) where T : IEvent
        {
            var type = typeof(T);
            if (!_subscribers.ContainsKey(type))
            {
                _subscribers[type] = new List<Action<IEvent>>();
            }
            _subscribers[type].Add(e => handler((T)e));
        }

        public void Publish<T>(T eventObj) where T : IEvent
        {
            var type = typeof(T);

            if (!isProcessing)
            {
                _ = RunLogAsync();
                isProcessing = true;
            }

            if (_subscribers.TryGetValue(type, out var handlers))
            {
                foreach (var handler in handlers)
                {
                    handler.Invoke(eventObj);
                }
            }
        }

        private void LogDrinkCompleted(DrinkCompletedEvent e)
        {
            var logEntry = $"[DrinkCompleted] {DateTime.Now:HH:mm:ss} {e.Customer.name} received drink {e.Drink.Name}\n";
            lock (_logLock)
            {
                _logQueue.Enqueue(logEntry);
            }
        }

        private void LogCustomerEnter(CustomerEnterEvent e)
        {
            var logEntry = $"[CustomerEnter] {DateTime.Now:HH:mm:ss} Accepted customer {e.Customer.name} for drink {e.Customer.Order.DrinkName}\n";
            lock (_logLock)
            {
                _logQueue.Enqueue(logEntry);
            }
        }

        private async Task RunLogAsync()
        {
            while (true)
            {
                string? logEntry = null;
                lock (_logLock)
                {
                    if (_logQueue.Count > 0)
                    {
                        logEntry = _logQueue.Dequeue();
                    }
                }

                if (logEntry != null)
                {
                    await File.AppendAllTextAsync(_logPath, logEntry, Encoding.UTF8);
                }
                else
                {
                    await Task.Delay(500);
                }
            }
        }
    }
}
