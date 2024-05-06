using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class MoveCharacterJoint : MonoBehaviour
{
    private string fullpth = "Assets/Avartar/JointTextFile2/jabCross.txt";
    public int textCount = 0; //�� ����

    StreamReader sr;    //��Ʈ�� ����
    string[] textValue; //�ؽ�Ʈ ���� �� �� ���� �迭
    string[] jointXYZ;  //�ؽ�Ʈ ���Ͽ��� ����Ʈ x,y,z�� �����ϴ� �迭
    Vector3[] realJoint; // �ؽ�Ʈ ���Ͽ��� �о�� x,y,z�� ĳ���� ���� position�� ����

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
            Debug.Log("���� ����");
            sr = new StreamReader(fullpth);
            textValue = File.ReadAllLines(fullpth); //�ؽ�Ʈ ������ ��� �� �о���̱�
            textCount = textValue.Length;

        }
        else
        {
            Debug.Log("���� ��ο� ������ �����ϴ�. ��ΰ� �߸��Ǿ����� Ȯ���ϼ���.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        realJoint = new Vector3[13];
        string line = null;

        try {
            if (textCount > 0) //textCount�� 0�϶� line������ Null���� ���� ��
            {
                for (int i = 0; i < 13; i++)
                {
                    line = sr.ReadLine(); //������ ���پ� �޾ƿ��� \n����
                    jointXYZ = line.Split(' '); //3���� ���� ' ' �� �������� ���� �迭�� ����

                    //string���� ����Ǿ� �ִ� ���� float������ ��ȯ �� ����
                    realJoint[i].x = float.Parse(jointXYZ[0]);
                    realJoint[i].y = float.Parse(jointXYZ[1]);
                    realJoint[i].z = float.Parse(jointXYZ[2]);

                    textCount--;
                }
            }
            else
            {
                sr.Close(); // streamReader ����
                Debug.Log("��Ʈ�� ���� ����");
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
        catch (NullReferenceException e) {
            print("���� ���� : " + e);
            Debug.Log(line);
        }

        // ���Ͽ��� ���� ������ ����
        storeData.SetTrackJointData(realJoint);
       
        // �ƹ�Ÿ�� �ȴٸ�, ���� ���������� ����
        storeData.Store();

        //�����̱� ���� ���� ������ ����
        moveAvatar.SetRequiredData(storeData.limbsJointData, storeData.torsoJointData);
        
        //�ƹ�Ÿ �ȴٸ�, ���� �����̴� �Լ�
        moveAvatar.MoveLimbs();
        moveAvatar.MoveTorso();
  
        // ������ û��
        storeData.ClearAllData();
        moveAvatar.ClearAllData();
    }

}



