# YuJanggi.Protocol.V2

Unity 클라이언트와 .NET 서버가 공유하는 네트워크 프로토콜 라이브러리입니다.

## 지원 환경

- .NET 10
- .NET Standard 2.1

## 포함 내용

- Client / Server 메시지 계약
- DTO
- JSON 직렬화
- Length Prefix 프레이밍
- Protocol Handshake
- 버전 관리

## 제외 내용

- UnityEngine
- TCP 연결 구현
- 게임 규칙
- 서버 런타임 로직

## 빌드
```powershell
dotnet build -c Release
빌드 결과:
bin/Release/net10.0/
bin/Release/netstandard2.1/