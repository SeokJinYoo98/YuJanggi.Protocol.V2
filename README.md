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
dotnet test -c Release
dotnet pack YuJanggi.Protocol.V2/YuJanggi.Protocol.V2.csproj -c Release -o artifacts/nuget
```

빌드 결과:

- `YuJanggi.Protocol.V2/bin/Release/net10.0/`
- `YuJanggi.Protocol.V2/bin/Release/netstandard2.1/`
- `artifacts/nuget/YuJanggi.Protocol.V2.0.1.0.nupkg`

## Unity UPM

원본 C# 소스와 .NET 프로젝트 위치는 유지합니다. UPM 소스는 다음 명령으로 생성합니다
(.NET SDK 10 및 PowerShell 필요).

```powershell
./scripts/Prepare-Upm.ps1
```

```text
YuJanggi.Protocol.V2/       # 유일한 소스 원본, 기존 .NET 프로젝트
YuJanggi.Protocol.V2.Tests/ # 기존 테스트
scripts/Prepare-Upm.ps1    # csproj Compile 항목에서 UPM 소스 생성
upm/
  package.json
  README.md
  Runtime/
    YuJanggi.Protocol.V2.asmdef
    Generated/             # 자동 생성, Git 제외, 직접 수정 금지
```

Unity 6에서 System.Text.Json 8.0.5와 전이 의존성을 별도로 제공한 뒤 생성된
`upm/package.json`을 로컬 패키지로 설치합니다. Git URL 직접 설치는 지원하지 않습니다.
설치와 배포 방법, 의존성 선택지 및 IL2CPP 검증 제한은 [UPM 안내](upm/README.md)를 참고하세요.
