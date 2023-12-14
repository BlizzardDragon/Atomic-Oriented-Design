using Atomic;

namespace AtomicOrientedDesign.Shooter
{
    public interface ISendMessageComponent
    {
        void SendMessage(string message);
    }

    public class SendMessageComponent : ISendMessageComponent
    {
        private AtomicEvent<string> _messageReceived;

        public SendMessageComponent(AtomicEvent<string> messageReceived)
        {
            _messageReceived = messageReceived;
        }

        public void SendMessage(string message) => _messageReceived?.Invoke(message);
    }
}