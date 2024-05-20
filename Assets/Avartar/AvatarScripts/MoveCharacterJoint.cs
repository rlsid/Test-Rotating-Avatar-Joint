using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class MoveCharacterJoint : MonoBehaviour
{
    private string fullpth = "Assets/Avartar/JointTextFile2/jabCross.txt";
    public int textCount = 0; //행 개수

    StreamReader sr;    //스트림 리더
    string[] textValue; //텍스트 파일 각 행 저장 배열
    string[] jointXYZ;  //텍스트 파일에서 조인트 x,y,z값 저장하는 배열
    Vector3[] realJoint; // 텍스트 파일에서 읽어온 x,y,z로 캐릭터 실제 position값 저장

    public Animator anim;
    StoreJointData storeData;
    MoveAvatar moveAvatar;

   
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        storeData = new StoreJointData(anim);
        moveAvatar = new MoveAvatar();

        FileInfo fileInfo = new FileInfo(fullpth);

        if (fileInfo.Exists)
        {
            Debug.Log("파일 존재");
            sr = new StreamReader(fullpth);
            textValue = File.ReadAllLines(fullpth); //텍스트 파일의 모든 행 읽어들이기
            textCount = textValue.Length;

        }
        else
        {
            Debug.Log("파일 경로에 파일이 없습니다. 경로가 잘못되었는지 확인하세요.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        realJoint = new Vector3[13];
        string line = null;

        try {
            if (textCount > 0) //textCount가 0일때 line변수에 Null값이 들어가게 됨
            {
                for (int i = 0; i < 13; i++)
                {
                    line = sr.ReadLine(); //파일의 한줄씩 받아오기 \n까지
                    jointXYZ = line.Split(' '); //3개의 값을 ' ' 을 기준으로 나눠 배열에 저장

                    //string으로 저장되어 있는 값을 float형으로 변환 후 저장
                    realJoint[i].x = float.Parse(jointXYZ[0]);
                    realJoint[i].y = float.Parse(jointXYZ[1]);
                    realJoint[i].z = float.Parse(jointXYZ[2]);

                    textCount--;
                }
            }
            else
            {
                sr.Close(); // streamReader 닫음
                Debug.Log("스트림 리더 닫음");
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
        catch (NullReferenceException e) {
            print("오류 사항 : " + e);
            Debug.Log(line);
        }

        // 파일에서 받은 데이터 저장
        storeData.SetTrackJointData(realJoint);
       
        // 아바타의 팔다리, 몸통 관절데이터 저장
        storeData.Store();

        //움직이기 위한 관절 데이터 전달
        moveAvatar.SetRequiredData(storeData.limbsJointData, storeData.torsoJointData);
        
        //아바타 팔다리, 몸통 움직이는 함수
        moveAvatar.MoveLimbs();
        moveAvatar.MoveTorso();
  
        // 데이터 청소
        storeData.ClearAllData();
        moveAvatar.ClearAllData();
    }

}



