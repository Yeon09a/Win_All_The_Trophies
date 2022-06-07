using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI를 사용하기 위해 필요

// Gameclear 화면

public class GameClear : MonoBehaviour
{
    public Text textComp; //  트로피의 개수를 나타내기 위한 text로 Text UI 컴포넌트를 받기 위해 public으로 설정한다.

    // Update is called once per frame
    void Update()
    {
        textComp.text = (ObjectCollision4.getTrophyCount()).ToString(); // ObjectCollision4(마지막 스테이지의 오브젝트 충돌 스크립트) 스크립트의 getTrophyCount함수를 통해 트로피의 개수를 가져온다. 트로피의 개수는 int형이므로 ToString을 사용하여 string형으로 바꾼 후 textComp.text에 넣어 UI의 text를 트로피의 개수로 바꿔준다.
    }
}
