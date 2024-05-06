using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //���� ����� ���� ��� ����

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;
    private string fullpth = "Assets/Avartar/JointTextFile2/"; //���� ��� ����
    StreamWriter sw;
    HumanBodyBones[] myBone;

    private void Start()
    {
        anim = GetComponent<Animator>();
        myBone = new HumanBodyBones[13];
        
        //���� �ٲ�!!
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

        if (false == File.Exists(fullpth + "jabCross.txt")) //��ο� test1.txt ���� ������ ���� ����
        {
            var file = File.CreateText(fullpth + "jabCross.txt");
            file.Close();  //�����ϰ� �ݱ�
        }
        sw = new StreamWriter(fullpth + "jabCross.txt"); //���� ��� ����
    }

    private void Update()
    {   
        // position�� ���� ��ǥ��
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
            UnityEditor.EditorApplication.isPlaying = false; //����Ƽ ������ ���� ���߱�
        }
    }

}

