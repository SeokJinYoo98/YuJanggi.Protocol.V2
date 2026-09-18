using System;
using System.Text.Json;
using YuJanggi.Protocol.V2.Messages;

namespace YuJanggi.Protocol.V2.Messages.MessageFactory
{
    public static class ClientMessageFactory
    {
        public static ClientMessage Create<TPayload>(
            ClientMessageType   type,
            TPayload            payload)
        {
            if (payload is null)
                throw new ArgumentNullException(nameof(payload));

            return new ClientMessage
            {
                Type        = type,
                RequestId   = Guid.NewGuid().ToString(),
                Payload     = JsonSerializer.SerializeToElement(payload)
            };
        }
    }
}
