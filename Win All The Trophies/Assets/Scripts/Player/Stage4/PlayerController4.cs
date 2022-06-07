using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI를 사용하기 위해 필요
using UnityEngine.SceneManagement; // Scene 전환을 위해 필요

// Stage4 Player 컨트롤러

public class PlayerController4 : MonoBehaviour
{
    Rigidbody2D playerRigid2D; // Player의 Rigidbody2D 컴포넌트가 들어갈 변수
    Animator playerAnimator; // Animator 컴포넌트가 들어올 변수
    float jumpForce = 1000.0f; // Player가 점프하는 힘
    float walkForce = 70.0f; // Player가 이동하는 힘
    float maxWalkSpeed = 2.0f; // Player가 갈수있는 최대 속도
    int jumpCount = 0; // Player의 점프 횟수(점프 횟수를 제한하기 위해)

    float attackedForce = 120.0f; // Player가 공격받았을 때 받는 힘
    public Image playerHP; // 플레이어의 체력 게이지로 Image UI의 컴포넌트를 받을 변수로 받기 위해 public으로 설정한다.

    public AudioClip jumpClip; // Player가 점프할 때의 사운드. 오디오 소스를 넣을 수 있는 공간 마련. 음원 소스를 넣을 수 있도록 public으로 설정한다.
    public AudioClip attackedClip; // Player가 공격받았을 때의 사운드. 오디오 소스를 넣을 수 있는 공간 마련. 음원 소스를 넣을 수 있도록 public으로 설정한다.
    AudioSource playerSrc; // 실제로 음원을 출력할 수 있는 오디오 소스 변수

    // Start is called before the first frame update
    void Start()
    {
        playerRigid2D = GetComponent<Rigidbody2D>(); // player의 Rigidbody2D 컴포넌트를 얻어 playerRigid2D에 넣는다.
        playerAnimator = GetComponent<Animator>(); // player의 Animator 컴포넌트를 얻어 playerAnimator에 넣는다.
        playerSrc = GetComponent<AudioSource>(); // AudioSource 컴포넌트를 얻어 playerSrc에 넣는다.

    }

    // Update is called once per frame
    void Update()
    {

        // 점프
        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스 바를 눌렀을 때
        {
            if (jumpCount < 2) // 점프 횟수(jumpCount)가 2보다 작으면
            {
                if (jumpCount == 0) // 점프 횟수(jumpCount)가 0일 때
                {
                    playerAnimator.SetTrigger("JumpTrigger"); // JumpTrigger로 바꿔주어 Jump 애니메이션(player가 점프하는 애니메이션)이 출력되도록 한다.
                    playerRigid2D.AddForce(transform.up * jumpForce); // 위쪽으로 jumpForce 만큼 점프한다.
                    playerSrc.PlayOneShot(jumpClip, 0.2f); // 점프할 때의 사운드(jumpClip)를 0.2 볼륨으로 출력한다.
                }
                else if (jumpCount == 1) // 점프 횟수(jumpCount)가 1일 때
                {
                    playerAnimator.SetTrigger("DoubleJumpTrigger"); // DoubleJumpTrigger로 바꿔주어 DoubleJump 애니메이션(player가 더블점프하는 애니메이션)이 출력되도록 한다.
                    playerRigid2D.AddForce(transform.up * jumpForce * 0.7f); // 위쪽으로 jumpForce * 0.7 만큼 점프한다.
                    playerSrc.PlayOneShot(jumpClip, 0.2f); // 점프할 때의 사운드(jumpClip)를 0.2 볼륨으로 출력한다.
                }

                jumpCount++; // 점프 횟수(jumpCount)를 +1 한다.
            }
        }

        // 좌우이동
        if (Input.GetKey(KeyCode.RightArrow)) // 우측 방향키를 누르고 있을 때
        {
            playerRigid2D.AddForce(transform.right * walkForce); // 오른쪽으로 walkForce 만큼 힘을 가해 이동한다.
            transform.localScale = new Vector3(5, 5, 1); // Player의 이미지를 반전하여 오른쪽을 바라보도록 한다.
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) // 좌측 방향키를 누르고 있을 때
        {
            playerRigid2D.AddForce(transform.right * (-1) * walkForce); // 왼쪽으로 walkForce 만큼 힘을 가해 이동한다.
            transform.localScale = new Vector3(-5, 5, 1); // Player의 이미지를 반전하여 왼쪽을 바라보도록 한다.
        }

        // 속도 제한
        if (playerRigid2D.velocity.x > maxWalkSpeed) // Player의 수평방향의 속도가 maxWalkSpeed 보다 커지면(오른쪽 이동)
        {
            playerRigid2D.velocity = new Vector2(maxWalkSpeed, playerRigid2D.velocity.y); // Player의 수평방향의 속도를 maxWalkSpeed로 하고 수직방향의 속도는 그대로 둔다.
        }
        else if (playerRigid2D.velocity.x < (-1) * maxWalkSpeed && playerRigid2D.velocity.x != 0) // Player의 수평방향의 속도가 (-1) * maxWalkSpeed 작고 0이 아니면(왼쪽 이동)
        {
            playerRigid2D.velocity = new Vector2((-1) * maxWalkSpeed, playerRigid2D.velocity.y); //  Player의 수평방향의 (-1) * 속도를 maxWalkSpeed로 하고 수직방향의 속도는 그대로 둔다.
        }

        // 걷기 애니메이션
        if (playerRigid2D.velocity.x != 0) // Player가 이동할 때(Player의 수평방향의 속도가 0이 아닐 때)
        {
            playerAnimator.SetBool("WalkBool", true); // WalkBool을 true로 하여 Walk 애니메이션(player가 걷는하는 애니메이션)을 출력되도록 한다.
        }
        else  // Player가 제자리에 있을 때(Player의 수평방향의 속도가 0일 때)
        {
            playerAnimator.SetBool("WalkBool", false); // WalkBool을 false로 하여 Walk 애니메이션(player가 걷는하는 애니메이션)을 출력되지 않도록 한다.(Walk 애니메이션에서 Idle 애니메이션이 출력되도록 한다.)
        }

        // Gameover
        if (playerHP.fillAmount == 0 || transform.position.y < -6.0f) // 주인공(플레이어)의 체력 게이지가 0 이거나 Player의 y좌표가 -6.0보다 밑에 있을 때
        {
            SceneManager.LoadScene("GameOver"); // "GameOver" 씬으로 이동한다.
        }
    }

    // 강체 간의 충돌 검사
    private void OnCollisionEnter2D(Collision2D collision) // 충돌한 순간
    {
        if (collision.gameObject.name.Contains("Tilemap")) // Tilemap(맵)과 충돌하였을 때(맵에 닿았을 때)
        {
            jumpCount = 0; // jumpCount를 0으로 초기화한다.
        }

        if (collision.gameObject.name.Contains("Saw")) // Saw(톱 날)과 충돌하였을 때
        {
            float direction = transform.position.x - collision.gameObject.transform.position.x; // 적과 충돌한 방향을 알아내기 위해 변수로 Player의 x좌표에서 Enemy의 x좌표를 뺀 값을 구한다. 값이 0보다 작을 경우 오른쪽에서 공격을 받은 것이고, 0보다 클 경우 왼쪽에서 공격을 받은 것이다.

            if (direction <= 0) // 오른쪽에서 공격을 받은 경우(direction이 0이하일 때)
            {
                transform.localScale = new Vector3(5, 5, 1); // Player의 이미지를 반전하여 오른쪽을 바라보도록 한다.
            }
            else // 왼쪽에서 공격을 받은 경우(direction이 0보다 클 때)
            {
                transform.localScale = new Vector3(-5, 5, 1); // Player의 이미지를 반전하여 왼쪽을 바라보도록 한다.
            }

            playerAnimator.SetTrigger("AttackedTrigger"); // AttackedTrigger로 바꿔주어 BeAttacked 애니메이션(player가 공격받는 애니메이션)이 출력되도록 한다.
            playerRigid2D.AddForce(new Vector2(0, attackedForce)); // attackedForce만큼 위로 힘을 가한다.

            playerHP.fillAmount -= 0.10f; // 플레이어의 체력 게이지를 10씩 줄인다.
            playerSrc.PlayOneShot(attackedClip, 0.2f); // 공격을 받았을 때의 사운드(attackedClip)를 0.2 볼륨으로 출력한다.
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) // 충돌한 순간, Trigger 모드
    {
        if (collision.name.Contains("Enemy")) // Enemy(적)과 충돌하였을 때
        {
            float direction = transform.position.x - collision.gameObject.transform.position.x; // 적과 충돌한 방향을 알아내기 위해 변수로 Player의 x좌표에서 Enemy의 x좌표를 뺀 값을 구한다. 값이 0보다 작을 경우 오른쪽에서 공격을 받은 것이고, 0보다 클 경우 왼쪽에서 공격을 받은 것이다.

            if (direction <= 0) // 오른쪽에서 공격을 받은 경우(direction이 0이하일 때)
            {
                transform.localScale = new Vector3(5, 5, 1); // Player의 이미지를 반전하여 오른쪽을 바라보도록 한다.
            }
            else // 왼쪽에서 공격을 받은 경우(direction이 0보다 클 때)
            {
                transform.localScale = new Vector3(-5, 5, 1); // Player의 이미지를 반전하여 왼쪽을 바라보도록 한다.
            }

            playerAnimator.SetTrigger("AttackedTrigger"); // AttackedTrigger로 바꿔주어 BeAttacked 애니메이션(player가 공격받는 애니메이션)이 출력되도록 한다.
            playerRigid2D.AddForce(new Vector2(0, attackedForce)); // attackedForce만큼 위로 힘을 가한다.
        }
    }
}
