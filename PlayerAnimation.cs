using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //파일 입출력 위한 헤더 파일

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;
    private string fullpth = "Assets/Avartar/JointTextFile2/"; //파일 경로 정의
    StreamWriter sw;
    HumanBodyBones[] myBone;

    private void Start()
    {
        anim = GetComponent<Animator>();
        myBone = new HumanBodyBones[13];
        
        //순서 바뀜!!
        myBone[0] = HumanBodyBones.Head;
        myBone[1] = HumanBodyBones.LeftUpperArm;
        myBone[2] = HumanBodyBones.RightUpperArm;
        myBone[3] = HumanBodyBones.LeftLowerArm;
        myBone[4] = HumanBodyBones.RightLowerArm;
        myBone[5] = HumanBodyBones.LeftHand;
        myBone[6] = HumanBodyBones.RightHand;
        myBone[7] = HumanBodyBones.LeftUpperLeg;
        myBone[8] = HumanBodyBones.RightUpperLeg;
        myBone[9] = HumanBodyBones.LeftLowerLeg;
        myBone[10] = HumanBodyBones.RightLowerLeg;
        myBone[11] = HumanBodyBones.LeftFoot;
        myBone[12] = HumanBodyBones.RightFoot;

        if (false == File.Exists(fullpth + "jabCross.txt")) //경로에 test1.txt 파일 없으면 새로 생성
        {
            var file = File.CreateText(fullpth + "jabCross.txt");
            file.Close();  //생성하고 닫기
        }
        sw = new StreamWriter(fullpth + "jabCross.txt"); //파일 열어서 쓰기
    }

    private void Update()
    {   
        // position은 절대 좌표임
        for (int i = 0; i < 13; i++)
        {
            sw.Write(anim.GetBoneTransform(myBone[i]).position.x + " ");
            sw.Write(anim.GetBoneTransform(myBone[i]).position.y + " ");
            sw.WriteLine(anim.GetBoneTransform(myBone[i]).position.z);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            sw.Flush();
            sw.Close();
            UnityEditor.EditorApplication.isPlaying = false; //유니티 에디터 실행 멈추기
        }
    }

}

