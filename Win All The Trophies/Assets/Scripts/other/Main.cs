using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Scene 전환을 위해 필요

// Main 화면

public class Main : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Stage1으로 이동
        if (Input.GetKeyDown(KeyCode.Space)) // 마우스 스페이스 바를 눌렀을 때
        {
            SceneManager.LoadScene("Stage1"); // "Stage1" 씬으로 이동한다.
        }
    }
}
