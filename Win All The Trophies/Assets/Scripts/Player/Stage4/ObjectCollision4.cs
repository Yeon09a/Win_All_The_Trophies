using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI를 사용하기 위해 필요
using UnityEngine.SceneManagement; // Scene 전환을 위해 필요

// Stage4 플레이어 충돌 스크립트

public class ObjectCollision4 : MonoBehaviour
{
    static int trophyCount = ObjectCollision3.getTrophyCount(); // 트로피의 개수를 나타낸 변수. 다음 스테이지에 값을 전달하기 위해(다음 스테이지까지 트로피의 개수를 유지하기 위해) static으로 선언. 전 스테이지의 ObjectCollision3의 getTrophyCount함수를 통해 전 스테이지의 트로피의 개수를 가져옴.
    static int stage4trophy = 0; // 현재 스테이지에서만 얻은 트로피의 개수
    public Text textComp; // 트로피의 개수를 나타내기 위한 text로 Text UI 컴포넌트를 받기 위해 public으로 설정한다.
    public Image playerHP; // 플레이어의 체력 게이지로 Image UI의 컴포넌트를 받을 변수로 받기 위해 public으로 설정한다.

    public AudioClip trophyClip; // 트로피를 얻었을 때(트로피와 충돌했을 때)의 사운드. 오디오 소스를 넣을 수 있는 공간 마련. 음원 소스를 넣을 수 있도록 public으로 설정한다.
    public AudioClip itemClip; // 아이템를 얻었을 때(아이템와 충돌했을 때)의 사운드. 오디오 소스를 넣을 수 있는 공간 마련. 음원 소스를 넣을 수 있도록 public으로 설정한다.
    public AudioClip attackedClip; // 적에게 공격을 받았을 때(적와 충돌했을 때)의 사운드. 오디오 소스를 넣을 수 있는 공간 마련. 음원 소스를 넣을 수 있도록 public으로 설정한다.
    public AudioClip clearClip; // 스테이지를 클리어했을 때(EndPoint와 충돌했을 때)의 사운드. 오디오 소스를 넣을 수 있는 공간 마련. 음원 소스를 넣을 수 있도록 public으로 설정한다.

    AudioSource playerSrc; // 실제로 음원을 출력할 수 있는 오디오 소스 변수

    // Start is called before the first frame update
    void Start()
    {
        textComp.text = trophyCount.ToString(); // 처음에 trophyCount를 ToString를 사용하여 string형으로 변환한 후 textComp.text에 넣어 UI의 text를 트로피의 개수를 보여준다.(이전 스테이지에서 얻은 트로피의 개수)
        playerSrc = GetComponent<AudioSource>(); // AudioSource 컴포넌트를 얻어 playerSrc에 넣는다.
    }

    private void OnTriggerEnter2D(Collider2D collision) // Trigger 허용, 충돌한 순간
    {
        // 적과 충돌
        if (collision.name.Contains("Enemy")) //  Enemy(적)와 충돌하였을 때
        {
            playerHP.fillAmount -= 0.10f; // 플레이어의 체력 게이지를 10씩 줄인다.
            playerSrc.PlayOneShot(attackedClip, 0.2f); // 공격을 받았을 때의 사운드(attackedClip)를 0.2 볼륨으로 출력한다.
        }

        // 트로피 얻음
        if (collision.name.Contains("Trophy")) //  Trophy와 충돌하였을 때
        {
            Animator TrophyAnimator = collision.GetComponent<Animator>(); // 충돌한 Trophy의 Animator 컴포넌트를 얻어와 TrophyAnimator에 넣는다.
            TrophyAnimator.SetTrigger("DisappearTrigger"); // DisappearTrigger로 바꿔주어 TrophyDisappear 애니메이션(Trophy가 사라지는 애니메이션)이 출력되도록 한다.
            Destroy(collision.gameObject, 0.28f); // 0.28초 뒤에 Trophy를 제거한다.
            trophyCount++; // trophyCount를 1 추가한다.(트로피의 개수를 하나 추가한다.)
            stage4trophy++; // stage4trophy를 1 추가한다.(트로피의 개수를 하나 추가한다.)
            textComp.text = trophyCount.ToString(); // trophyCount를 ToString를 사용하여 string형으로 변환한 후 textComp.text에 넣어 UI의 text를 트로피의 개수를 보여준다.
            playerSrc.PlayOneShot(trophyClip, 0.2f); // 트로피를 얻었을 때의 사운드(trophyClip)를 0.2 볼륨으로 출력한다.
        }

        // 아이템 얻음
        if (collision.name.Contains("item")) //  item와 충돌하였을 때
        {
            playerHP.fillAmount += 0.20f; // 플레이어의 체력 게이지를 20 늘린다.
            Destroy(collision.gameObject);  // item을 제거한다.
            playerSrc.PlayOneShot(itemClip, 0.2f); // 아이템를 얻었을 때의 사운드(itemClip)를 0.2 볼륨으로 출력한다.
        }

        // 클리어
        if (collision.name.Contains("EndPoint")) // EndPoint와 충돌하였을 때
        {
            SceneManager.LoadScene("GameClear"); // "GameClear" 씬으로 이동한다.
            playerSrc.PlayOneShot(clearClip, 0.2f); // 스테이지를 클리어했을 때의 사운드(clearClip)를 0.2 볼륨으로 출력한다.
        }
    }

    // trophyCount를 반환하는 getter
    static public int getTrophyCount() // 다른 스크립트에서 사용하기 위해 static으로 선언
    {
        return trophyCount; // trophyCount을 반환한다.
    }

    // stage4trophy를 반환하는 getter
    static public int getStage4trophy() // 다른 스크립트에서 사용하기 위해 static으로 선언
    {
        return stage4trophy; // stage4trophy를 반환한다.
    }
}
