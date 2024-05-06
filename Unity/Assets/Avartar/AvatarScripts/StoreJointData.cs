using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// 상하체 관절 저장하는 클래스 하나
class StoreJointData
{

    private Vector3[] trackJoint = new Vector3[13];
    public List<AvatarData> limbsJointData;
    public Dictionary<string, AvatarData> torsoJointData;
    public Animator anim;

    Vector3 virtualNeck;
    Vector3 virtualHips;
    Vector3 virtualUpperChest;

    public StoreJointData(Animator animator)
    {
        anim = animator;
        limbsJointData = new List<AvatarData>();
        torsoJointData = new Dictionary<string, AvatarData>();
    }

    public void ClearAllData()
    {
        limbsJointData.Clear();
        torsoJointData.Clear();
    }

    // 가상의 관절 만들기, 관절 위치 재조정
    public void MakeVirtualData()
    {
        // 가상의 목 관절의 위치 구하기
        virtualNeck = (trackJoint[1] + trackJoint[2]) / 2.0f;
        virtualNeck.y += 0.05f;

        // 가상의 힙 관절의 위치 구하기
        virtualHips = (trackJoint[7] + trackJoint[8]) / 2.0f;
        virtualHips.y += 0.075f;

        //가상의 UpperChest 관절 위치 구하기
        virtualUpperChest = (trackJoint[1] + trackJoint[2]) / 2.0f;
        virtualUpperChest.y -= 0.1f;


        anim.GetBoneTransform(HumanBodyBones.Hips).position = virtualHips;
    }

    public void SetTrackJointData(Vector3[] realJoint)
    {
        trackJoint = realJoint;
    }

    public void AddLimbsJointData(HumanBodyBones parent, HumanBodyBones child, Vector3 trackParent, Vector3 trackChild)
    {
        limbsJointData.Add(new AvatarData(anim.GetBoneTransform(parent), anim.GetBoneTransform(child), trackParent, trackChild));
    }

    public void AddTorsoJointData(string name, HumanBodyBones parent, HumanBodyBones child, Vector3 trackParent, Vector3 trackChild)
    {
        torsoJointData.Add(name, new AvatarData(anim.GetBoneTransform(parent), anim.GetBoneTransform(child), trackParent, trackChild));
    }

    public void Store()
    {
        // 가상 관절 데이터 만들어서 저장 -> 트래킹하지 않는 관절이 있음
        MakeVirtualData();

        //팔다리 관절 데이터 저장
        AddLimbsJointData(HumanBodyBones.RightUpperArm, HumanBodyBones.RightLowerArm, trackJoint[2], trackJoint[4]);
        AddLimbsJointData(HumanBodyBones.RightLowerArm, HumanBodyBones.RightHand, trackJoint[4], trackJoint[6]);

        AddLimbsJointData(HumanBodyBones.LeftUpperArm, HumanBodyBones.LeftLowerArm, trackJoint[1], trackJoint[3]);
        AddLimbsJointData(HumanBodyBones.LeftLowerArm, HumanBodyBones.LeftHand, trackJoint[3], trackJoint[5]);

        AddLimbsJointData(HumanBodyBones.RightUpperLeg, HumanBodyBones.RightLowerLeg, trackJoint[8], trackJoint[10]);
        AddLimbsJointData(HumanBodyBones.RightLowerLeg, HumanBodyBones.RightFoot, trackJoint[10], trackJoint[12]);

        AddLimbsJointData(HumanBodyBones.LeftUpperLeg, HumanBodyBones.LeftLowerLeg, trackJoint[7], trackJoint[9]);
        AddLimbsJointData(HumanBodyBones.LeftLowerLeg, HumanBodyBones.LeftFoot, trackJoint[9], trackJoint[11]);


        // 몸통 데이터 저장
        AddTorsoJointData("rightHip", HumanBodyBones.Hips, HumanBodyBones.RightUpperLeg, virtualHips, trackJoint[8]);
        AddTorsoJointData("leftHip", HumanBodyBones.Hips, HumanBodyBones.LeftUpperLeg, virtualHips, trackJoint[7]);
        AddTorsoJointData("neckTwist", HumanBodyBones.Neck, HumanBodyBones.Head, virtualNeck, trackJoint[0]);
        AddTorsoJointData("rightShoulder", HumanBodyBones.UpperChest, HumanBodyBones.RightUpperArm, virtualUpperChest, trackJoint[2]);
        AddTorsoJointData("leftShoulder", HumanBodyBones.UpperChest, HumanBodyBones.LeftUpperArm, virtualUpperChest, trackJoint[1]);
    }
}