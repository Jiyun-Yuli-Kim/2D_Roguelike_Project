# 2D_Roguelike_Project
 ### 플레이어

- PlayerController
- 애니메이션
    - 기본 8방향 애니메이션 적용 → 구현 완료 했으나, 고민중
    - 좌우 애니메이션만 갖도록 구성(flipx 활용 가능)
- 이동
    - 키보드 W,S,A,D로 이동
    - 캐릭터 기준 마우스 위치로 공격 방향을 지정
    - 마우스 클릭 등으로 공격
- 스킬
    - [SerializedField] public GameObject CurSkill; → 스킬 프리팹을 붙임

### PlayerController

- float playerSpeed
- Animator player Animator
- BulletLauncher launcher
- FixedUpdate()
    - input 받아서 이동
- OnTriggerEnter(”Skill”)
    - launcher.curSkill = other : 스킬 변경
    - launcher.SetBullet();
- OnTriggerEnter(”Bullet”)
    - playerHP--;
    - launcher.bullet.Deactivate();

### BulletLauncher

- BulletPool bulletPool
- Transform 스폰위치
- Transform 타겟위치
- Skill curSkill 현재스킬
- SetBullet() : 현재 스킬에 따른 불렛 설정
- Shoot() : 인풋 직접 받아 불렛 발사

### BulletPool

- int poolSize;
- GameObject bulletPrefab;

### Bullet

- float bulletSpeed;
- float bulletDamage;

### abstract class Skill

추상클래스 사용 이유 : 자식클래스에서 구현을 강제(activate)할건데, 자기 자신도 매서드를 구현(deactivate)하게 하려고

- string skillName;
- float bulletFrequency; // 스폰 주기
- float bulletSpeed;
- float bulletDamage;
- GameObject skillBullet; // 스킬에 따라 다른 프리팹
- public abstract void Activate();
- public virtual void Deactivate()

### PowerUp : Skill

- Activate()
    - PlayerController-AttackDmg++;
    - 스폰빈도++;

### Bender : Skill

- Activate()
    - PlayerBullets 어쩌구~~;
    
    // 총알이 존재하는 동안 타겟의 트랜스폼 따오도록
    

### Room

노드 내부에 생성되는 방에 대한 정보

- RectInt roomRect : 시작 좌표와 크기 정보
- int[ , ] spawnArea : 방안의 아이템 스폰 가능 영역을 2차원 좌표로 확인

### 싱글턴

- GameManager
    - SceneChanger
    - 현재 스테이지에 대한 SO(?)
- SoundManager
    - bgm 관리
    - sfx 관리
- PlayerStats
    - 플레이어 xp
    - 플레이어 레벨
    - 코인

### 공격로직

- 기본 공격 : 정면 방향으로 bullet 발사(아이작 st.)
- 추가스킬 : 맵에서 랜덤으로 스폰됨.
    - powerUp : 데미지 up, 연사 느려짐
    - bender : 공격이 적에게 유도됨. 타겟팅의 정확도가 떨어져도 적을 향해 bullet 발사됨
- 피격판정 trigger

### 몬스터 구조

- base
    - 이동 공격 판단조건
        - 일정 반경 내에서 랜덤 이동(how? 코루틴?)
        - 트리거 안에 플레이어가 들어오면 돌진 및 공격
    - abstract 이동
    - 공격
    - 이동속도
    - 공격주기
- children : base
    - 이미지
    - 애니메이션
    - 추상함수 구현

### 스테이지

스크립터블 오브젝트

- 스테이지 명
- 발자국 소리
- 룰타일
- bgm
- 몬스터 총량
- 몬스터 리스트

### 길찾기

- 목적지까지 가는 경로 띄워주기(스킬, n초간 발동)
- 혹은 이지모드로 설정시 미니맵상에 띄워주기
- 스테이지 종료조건
    - 모든 몬스터 퇴치
    - 열쇠 n개 획득시 목적지 잠금해제
    - 목적지 도달
        - 시작지점과 도착지점이 일정거리 이상이 되도록 설정… 할 필요가 있을까? 어차피 맵 전체를 돌면서 아이템 찾아야하는데

### UI-인게임 캔버스

- 팝업
    - 스테이지 정보
- 좌측 상단
    - hp
    - 코인
    - 버프 남은시간(?) → 슬라이더로 나타낼지, 종료전 깜빡이는걸로 나타낼지
- 좌측 하단
    - 남은 몬스터 수
    - 남은 열쇠 수
    - 남은 코인 수
- 우측 상단
    - 미니맵
        - How? 라인렌더러?
    - 일시정지버튼
        - 볼륨설정
        - 나가기
- 우측 하단
    - 현재스킬 아이콘