# Win_All_The_Trophies
2021년 서울여자대학교 디지털미디어학과 CG프로그래밍 기말 프로젝트<br/>

2D 픽셀 어드벤처 PC 게임<br/>
플레이어가 몬스터와 싸우면서 맵 내의 트로피를 수집하는 2D PC 게임
* 본 프로젝트는 서울여자대학교 디지털미디어학과 CG프로그래밍 수업의 기말 프로젝트 결과물입니다.
* 프로젝트 내에 에셋을 사용하였으므로 현재 Repository에는 에셋을 제외한 코드만 존재합니다.
* 본 프로젝트는 Unity를 처음 다뤄본 프로젝트입니다. Unity의 기본적인 학습을 목표로 한 프로젝트 입니다.

## 게임 소개
'Win All The Trophies'는 플레이어를 조작하여 몬스터를 퇴치하고 맵에 숨겨진 트로피를 수집하는 게임입니다.<br/>
몬스터를 퇴치하는 것 뿐만 아니라 맵의 이곳저곳을 탐험하며 보물을 찾는 듯한 느낌을 주는 것을 목적으로 하고 있습니다.<br/>

### 게임 특징
* 귀여운 캐릭터와 다양한 몬스터의 전투
  * 귀여운 캐릭터를 조작하면서 여러 종류의 몬스터와 전투를 즐길 수 있습니다.
  <br/><img width="50%" src="https://github.com/user-attachments/assets/e6188484-fb74-4000-8fa2-54f491866f95"/><img width="50%" src="https://github.com/user-attachments/assets/2a8fa887-1928-47d8-973e-2864d147f16b"/>
* 맵 이곳저곳에 숨겨진 트로피
  * 맵에 숨겨진 트로피를 찾으면서 맵을 즐길 수 있습니다.
  <br/><img width="50%" src="https://github.com/user-attachments/assets/c9163176-c719-4c2f-aa98-760b5f6e9d24"/><img width="50%" src="https://github.com/user-attachments/assets/7dd78448-97a8-4db5-928f-26c9da270514"/>
## 프로젝트 개요
🔗자세한 내용은 Notion에서 확인하실 수 있으십니다.    [<img src="https://img.shields.io/badge/Notion-000000?style=flat-round&logo=Notion&logoColor=white"/>](https://www.notion.so/Win_All_The_Trophies-178b66b96b778005ad0ed3344085cfed?pvs=4)
### 개발 기간
* 2021.05 - 2021.06 (약 1개월)
### 개발 환경
* Unity 2019.1.10
### 수행업무
개인 프로젝트로 다음과 같은 부분을 수행했습니다.<br/>

씬 이동 제작
* LoadScene() 활용한 씬 이동 제작
 
스테이지 제작
* 난이도에 따른 스테이지 4개 제작
* 스테이지에 따른 카메라 컨트롤 제작
  * 카메라 좌표를 활용한 카메라 이동 제한

플레이어 조작 제작
* 충돌처리를 통한 아이템 및 트로피 획득, 피격, 게임 클리어 제작
* Input을 활용한 키보드 입력에 따른 플레이어 이동, 점프, 2단 점프, 공격 제작
* 플레이어 애니메이션 적용

적 및 장애물의 이동, 피격, 공격 제작
* Trigger 충돌처리를 통한 적 이동 및 피격, 공격 제작
* 적 애니메이션 적용

게임 사운드 적용
* udioSource를 활용한 게임 사운드 적용

트로피 개수 데이터 관리
* static을 사용한 획득한 트로피 개수 관리

구름 이동 제작
* Background Scrolling을 활용한 배경 구름 이동 제작
## 프로젝트 성과
* 서울여자대학교 CG프로그래밍 수업 기말 프로젝트 성적 만점
