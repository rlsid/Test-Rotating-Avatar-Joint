﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// 저장한 관절 정보 가지고 움직이는 클래스 하나
class MoveAvatar
{
    private List<AvatarData> limbsJointData;
    private Dictionary<string, AvatarData> torsoJointData;

    const float LimbsAmount = 0.3f;
    const float Speed = 10f;

    public void SetRequiredData(List<AvatarData> limbsJointData, Dictionary<string, AvatarData> torsoJointData)
    {
        this.limbsJointData = limbsJointData;
        this.torsoJointData = torsoJointData;
    }

    //모든 관절 데이터 지우는 함수
    public void ClearAllData()
    {
        limbsJointData.Clear();
        torsoJointData.Clear();
    }

    //팔다리 관절 움직이는 함수
    public void MoveLimbs()
    {
        foreach (var i in limbsJointData)
        {
            Quaternion changeRot = Quaternion.FromToRotation(i.initialDir, Vector3.Slerp(i.initialDir, i.CurrentDirection, LimbsAmount));
            i.parent.rotation = changeRot * i.initialRotation;
        }
    }

    //일정 비율만큼 몸통에 있는 관절 회전시키는 함수
    public void RotateTorso(AvatarData avatarData, float amount)
    {
        Quaternion targetRotation;

        targetRotation = Quaternion.FromToRotation(avatarData.initialDir, Vector3.Slerp(avatarData.initialDir, avatarData.CurrentDirection, amount));
        targetRotation *= avatarData.initialRotation;
        avatarData.parent.rotation = Quaternion.Lerp(avatarData.parent.rotation, targetRotation, Time.deltaTime * Speed);
    }

    //몸통 움직이는 함수
    public void MoveTorso()
    {
        RotateTorso(torsoJointData["rightHip"], 0.5f);
        RotateTorso(torsoJointData["leftHip"], 0.5f);
        RotateTorso(torsoJointData["neckTwist"], 0.3f);
        RotateTorso(torsoJointData["rightShoulder"], 0.3f);
        RotateTorso(torsoJointData["leftShoulder"], 0.3f);
    }

}