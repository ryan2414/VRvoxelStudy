using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//사용자가 마우스를 클릭한 지점에 복셀을 1개 만들고 싶다.
//필요 속성 : 복셀 공장
public class VoxelMaker : MonoBehaviour
{
    //복셀 공장
    public GameObject voxelFactory;

    //오브젝트 풀의 크기
    public int voxelPoolSize = 20;

    //오브젝트 풀
    public static List<GameObject> voxelPool = new List<GameObject>();

    //생성 시간
    public float creatTime = 0.1f;

    //경과 시간
    private float currentTime = 0;

    private void Start()
    {
        //오브젝트 풀에 비활성화된 복셀을 담고 싶다.
        for (int i = 0; i < voxelPoolSize; i++)
        {
            //1. 복셀 공장에서 복셀 생성하기
            GameObject voxel = Instantiate(voxelFactory);
            //2. 복셀 비활성화하기
            voxel.SetActive(false);
            //3. 복셀을 오브젝트 풀에 담고 싶다.
            voxelPool.Add(voxel);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ARAVRInput.Get(ARAVRInput.Button.One))
        {
            //일정 시간마다 복셀을 만들고 싶다.
            //1. 경과 시간이 흐른다.
            currentTime += Time.deltaTime;
            //2. 경과 시간이 생성 시간을 초과했다면
            if (currentTime > creatTime)
            {
                // //사용자가 마우스를 클릭한 지점에 복셀을 1개 만들고 싶다.
                // //1. 사용자가 마우스를 클릭했다면
                // if (Input.GetButtonDown("Fire1"))
                //2. 마우스의 위치가 바닥 위에 위치해 있다면
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //2) 컨트롤러가 향하는 방향으로 시선 만들기
                Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
                RaycastHit hitInfo = new RaycastHit();
                if (Physics.Raycast(ray, out hitInfo))
                {
                    //복셀 오브젝트 풀 이용하기
                    //1. 만약 오브젝트 풀에 복셀이 있다면
                    if (voxelPool.Count > 0)
                    {
                        //복셀을 생성했을 때만 경과 시간을 초기화해준다.
                        creatTime = 0;
                    
                        //2. 오브젝트 풀에서 복셀을 하나 가져온다.
                        GameObject voxel = voxelPool[0];
                        //3. 복셀을 활성화 한다.
                        voxel.SetActive(true);
                        //4. 복셀을 배치하고 싶다.
                        voxel.transform.position = hitInfo.point;
                        //5. 오브젝트 풀에서 복셀을 제거한다.
                        voxelPool.RemoveAt(0);
                    }
                }
            } 
        }
        
    }
}