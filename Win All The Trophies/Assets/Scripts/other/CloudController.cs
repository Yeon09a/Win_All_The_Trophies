using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 구름 움직임(스크롤링)

public class  CloudController : MonoBehaviour
{
    GameObject[] cloud = new GameObject[2]; // Cloud1, Cloud2 오브젝트를 넣을 배열
    GameObject Mcamera; // Main Camera 오브젝트를 넣을 변수
    int cloud1 = 0; // 배열의 인덱스로 순서를 나타냄. 시작할 때에는 Cloud1이 앞에 위치하므로 0으로 둔다
    int cloud2 = 1; // 배열의 인덱스로 순서를 나타냄. 시작할 때에는 Cloud2이 뒤에 위치하므로 1로 둔다.

    // Start is called before the first frame update
    void Start()
    {
        cloud[cloud1] = GameObject.Find("Cloud1"); // cloud 배열의 0번째 인덱스(앞서 위치한 구름)에 "Cloud1" 라는 이름의 오브젝트(구름 오브젝트)를 찾아 넣어준다.
        cloud[cloud2] = GameObject.Find("Cloud2"); // cloud 배열의 1번째 인덱스(뒤에 위치한 구름)에 "Cloud2" 라는 이름의 오브젝트(구름 오브젝트)를 찾아 넣어준다.
        Mcamera = GameObject.Find("Main Camera"); // "Main Camera"라는 이름의 오브젝트(Main Camera 오브젝트)를 찾아 Mcamera에 넣어준다.
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 firstPos = cloud[cloud1].transform.position; // cloud[cloud1]에 있는 오브젝트(앞쪽에 위치한 Cloud 오브젝트)의 위치를 firstPos에 넣어준다.
        Vector3 McamPos = Mcamera.transform.position; // Mcamera의 위치를 McamPos에 넣어준다.
        float Xpos = McamPos.x - firstPos.x; // Mcamera의 x좌표(McamPos.x)에서 cloud[first]의 x좌표(firstPos.x)를 뺀 값을 Xpos에 넣어준다.

        // 구름의 이동
        transform.Translate(Vector2.left * Time.deltaTime); // 왼쪽으로 1만큼 움직인다.(Vector2.left (-1, 0)을 의미한다.) Time.deltaTime을 사용하여 이동거리를 보정한다.(Time.deltaTime은 컴퓨텅의 성능과 상관없이 이동거리가 같도록 보정해준다.)

        // 구름의 순서 바꾸기(무한반복)
        if (Xpos > 55) // Xpos(Main Camera와 cloud[first]의 거리)가 55를 넘으면(cloud[cloud1]가 화면을 벗어나면)
        {
            cloud[cloud1].transform.position = new Vector3(McamPos.x + 50, firstPos.y, firstPos.z); // cloud[cloud1]를 뒤쪽으로 위치시킨다.

            // cloud[cloud1]가 뒤로 이동을 하면 cloud[cloud2]가 앞에 위치하기 때문에 cloud1과 cloud2의 값을 바꾸어 서로 순서를 바꾼다.
            int tem = cloud1;
            cloud1 = cloud2;
            cloud2 = tem;
        }
    }
}
