namespace YuJanggi.Protocol.V2.Matching
{
    public enum MatchingResult
    {
        Accepted            = 0,
        AlreadyMatching     = 1,
        AlreadyMatched      = 2,
        HandshakeRequired   = 3,
        ServerError         = 4
    }
    /// <summary>
    /// 신청 요청의 RequestId를 유지하는 응답입니다.
    /// Accepted는 대기열 접수이며 매칭 완료를 의미하지 않습니다.
    /// </summary>
    public sealed record MatchingResponse
    {
        public MatchingResult   Result { get; init; }
        public string?          Message { get; init; }
    }

    public enum MatchingCancelResult
    {
        Cancelled           = 0,
        NotMatching         = 1,
        AlreadyMatched      = 2,
        HandshakeRequired   = 3,
        ServerError         = 4
    }

    /// <summary>취소 요청의 RequestId를 유지하는 응답입니다.</summary>
    public sealed record MatchingCancelResponse
    {
        public MatchingCancelResult Result { get; init; }
        public string?              Message { get; init; }
    }

    /// <summary>매칭된 플레이어의 식별 정보입니다. 진영은 이를 담는 필드로 구분합니다.</summary>
    public sealed record MatchingPlayer
    {
        public string PlayerId   { get; init; } 
            = string.Empty;
        public string PlayerName { get; init; } 
            = string.Empty;
    }

    /// <summary>
    /// 매칭 완료 시 양쪽 클라이언트에 보내는 서버 이벤트입니다(RequestId는 null).
    /// 서버가 MatchId와 초·한 플레이어 정보를 채워 전송합니다.
    /// 대국 시작 및 초기 보드 데이터는 별도 게임 메시지에서 처리합니다.
    /// </summary>
    public sealed record MatchingFound
    {
        public string           MatchId     { get; init; } 
            = string.Empty;
        public MatchingPlayer   ChoPlayer   { get; init; } 
            = new MatchingPlayer();
        public MatchingPlayer   HanPlayer   { get; init; } 
            = new MatchingPlayer();
    }
}

