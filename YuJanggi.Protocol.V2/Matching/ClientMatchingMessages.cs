namespace YuJanggi.Protocol.V2.Matching
{
    public enum MatchingFormation
    {
        HEHE, EHEH, EHHE, HEEH
    }

    /// <summary>매칭 대기열 참가 요청입니다. 플레이어 식별은 서버 세션을 사용합니다.</summary>
    public sealed record MatchingRequest
    {
    }

    /// <summary>현재 매칭 대기열에서 나가기 위한 요청입니다.</summary>
    public sealed record MatchingCancelRequest
    {
    }

    public sealed record FormationSubmit
    {
        public MatchingFormation Formation { get; init; }
    }
}
