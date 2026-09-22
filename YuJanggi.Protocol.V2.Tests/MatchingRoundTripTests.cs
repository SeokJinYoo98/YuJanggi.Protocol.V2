using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YuJanggi.Protocol.V2.Framing;
using YuJanggi.Protocol.V2.Matching;
using YuJanggi.Protocol.V2.Messages;
using YuJanggi.Protocol.V2.Messages.MessageFactory;
using YuJanggi.Protocol.V2.Serialization;

namespace YuJanggi.Protocol.V2.Tests
{
    [TestClass]
    public class MatchingRoundTripTests
    {
        [TestMethod]
        public void MatchingRequest_RoundTrip_PreservesRequestIdAndPayload()
        {
            var message = ClientMessageFactory.Create(
                ClientMessageType.MatchingRequest, new MatchingRequest());

            var received = RoundTrip(message);

            Assert.AreEqual(ClientMessageType.MatchingRequest, received.Type);
            Assert.IsFalse(string.IsNullOrWhiteSpace(received.RequestId));
            Assert.AreEqual(message.RequestId, received.RequestId);
            Assert.IsNotNull(received.GetPayload<MatchingRequest>());
        }

        [TestMethod]
        public void MatchingCancelRequest_RoundTrip_PreservesRequestIdAndPayload()
        {
            var message = ClientMessageFactory.Create(
                ClientMessageType.MatchingCancelRequest, new MatchingCancelRequest());

            var received = RoundTrip(message);

            Assert.AreEqual(ClientMessageType.MatchingCancelRequest, received.Type);
            Assert.IsFalse(string.IsNullOrWhiteSpace(received.RequestId));
            Assert.AreEqual(message.RequestId, received.RequestId);
            Assert.IsNotNull(received.GetPayload<MatchingCancelRequest>());
        }

        [TestMethod]
        [DataRow(MatchingResult.Accepted, null)]
        [DataRow(MatchingResult.AlreadyMatching, "이미 매칭 중입니다.")]
        [DataRow(MatchingResult.AlreadyMatched, "매칭이 완료되었습니다.")]
        [DataRow(MatchingResult.HandshakeRequired, "핸드셰이크가 필요합니다.")]
        [DataRow(MatchingResult.ServerError, "서버 오류")]
        public void MatchingResponse_RoundTrip_PreservesResultAndRequestId(
            MatchingResult result, string? detail)
        {
            var request = ClientMessageFactory.Create(
                ClientMessageType.MatchingRequest, new MatchingRequest());
            var response = new MatchingResponse { Result = result, Message = detail };
            var message = ServerMessageFactory.CreateResponse(
                ServerMessageType.MatchingResponse, request.RequestId!, response);

            var received = RoundTrip(message);
            var payload = received.GetPayload<MatchingResponse>();

            Assert.AreEqual(ServerMessageType.MatchingResponse, received.Type);
            Assert.AreEqual(request.RequestId, received.RequestId);
            Assert.AreEqual(result, payload.Result);
            Assert.AreEqual(detail, payload.Message);
        }

        [TestMethod]
        [DataRow(MatchingCancelResult.Cancelled, null)]
        [DataRow(MatchingCancelResult.NotMatching, "매칭 대기 중이 아닙니다.")]
        [DataRow(MatchingCancelResult.AlreadyMatched, "매칭이 이미 완료되었습니다.")]
        [DataRow(MatchingCancelResult.HandshakeRequired, "핸드셰이크가 필요합니다.")]
        [DataRow(MatchingCancelResult.ServerError, "서버 오류")]
        public void MatchingCancelResponse_RoundTrip_PreservesResultAndRequestId(
            MatchingCancelResult result, string? detail)
        {
            var request = ClientMessageFactory.Create(
                ClientMessageType.MatchingCancelRequest, new MatchingCancelRequest());
            var response = new MatchingCancelResponse { Result = result, Message = detail };
            var message = ServerMessageFactory.CreateResponse(
                ServerMessageType.MatchingCancelResponse, request.RequestId!, response);

            var received = RoundTrip(message);
            var payload = received.GetPayload<MatchingCancelResponse>();

            Assert.AreEqual(ServerMessageType.MatchingCancelResponse, received.Type);
            Assert.AreEqual(request.RequestId, received.RequestId);
            Assert.AreEqual(result, payload.Result);
            Assert.AreEqual(detail, payload.Message);
        }

        [TestMethod]
        public void MatchingFound_RoundTrip_PreservesPlayersAndHasNoRequestId()
        {
            var match = new MatchingFound
            {
                MatchId = "match-001",
                ChoPlayer = new MatchingPlayer { PlayerId = "player-cho", PlayerName = "초 플레이어" },
                HanPlayer = new MatchingPlayer { PlayerId = "player-han", PlayerName = "한 플레이어" }
            };
            var message = ServerMessageFactory.CreateEvent(ServerMessageType.MatchingFound, match);

            var received = RoundTrip(message);
            var payload = received.GetPayload<MatchingFound>();

            Assert.AreEqual(ServerMessageType.MatchingFound, received.Type);
            Assert.IsNull(received.RequestId);
            Assert.AreEqual(match.MatchId, payload.MatchId);
            Assert.AreEqual(match.ChoPlayer.PlayerId, payload.ChoPlayer.PlayerId);
            Assert.AreEqual(match.ChoPlayer.PlayerName, payload.ChoPlayer.PlayerName);
            Assert.AreEqual(match.HanPlayer.PlayerId, payload.HanPlayer.PlayerId);
            Assert.AreEqual(match.HanPlayer.PlayerName, payload.HanPlayer.PlayerName);
        }

        private static TMessage RoundTrip<TMessage>(TMessage message)
        {
            byte[] body = MessageSerializer.Serialize(message);
            byte[] packet = MessageFramer.Encode(body);
            int bodyLength = MessageFramer.DecodeBodyLength(
                packet.AsSpan(0, MessageFramer.HeaderSize));

            Assert.AreEqual(body.Length, bodyLength);
            return MessageSerializer.Deserialize<TMessage>(
                packet.AsSpan(MessageFramer.HeaderSize, bodyLength));
        }
    }
}
