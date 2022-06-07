using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI를 사용하기 위해 필요

// Gameover 화면

public class GameOver : MonoBehaviour
{
    public Text textComp; //  트로피의 개수를 나타내기 위한 text로 Text UI 컴포넌트를 받기 위해 public으로 설정한다.

    // Update is called once per frame
    void Update()
    {
        // 각 스테이지에서 얻은 트로피의 개수를 갖고 있는 스크립트(ObjectCollision1, ObjectCollision2, ObjectCollision3, ObjectCollision4)의 각 스테이지에서 얻은 트로피의 개수를 가져오는 함수를 사용하여 트로피의 개수를 다 더한 후 ToString을 사용하여 string형으로 바꾸어 textComp.text에 넣어 UI의 text를 트로피의 개수로 바꿔준다.
        textComp.text = (ObjectCollision1.getTrophyCount() + ObjectCollision2.getStage2trophy() + ObjectCollision3.getStage3trophy() + ObjectCollision4.getStage4trophy()).ToString(); 
    }
}
