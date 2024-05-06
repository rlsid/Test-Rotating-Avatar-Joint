### 유니티 3D 휴머노이드의 관절을 움직여 동작을 구현해보는 테스트
-------------------------------------------------------------
![image](https://github.com/rlsid/Test-Rotating-Avatar-Joint/assets/122157081/b79966b6-080a-4ac1-8585-2dcffca0947d)
1. `Player Animation` Script를 적용하여 관절의 위치를 텍스트 파일에 먼저 기록한다.
   아바타 컴포넌트에 Animator을 설정하여 유니티 에디터를 실행하게 되면 유니티 애니메이션이 적용된다.
   애니메이션이 실행되는 동안 `Player Animation` C# Script는 캐릭터의 관절 위치(x, y, z)를 텍스트 파일에 기록한다.
   

![image](https://github.com/rlsid/Test-Rotating-Avatar-Joint/assets/122157081/724a5689-9cd8-4161-9790-39769c28b850)

2. `Player Animation` Script는 비활성화 해두고 `Move Character Joint` Script를 적용한다.
   `Move Character Joint` C# Script는 아까 기록한 텍스트 파일의 데이터를 읽어와 관절의 x, y, z 위치값을 이용해 관절의 회전을 구현한다.
   이전에 실행했던 애니메이션의 움직임 그대로 아바타가 똑같이 동작하는 것을 확인할 수 있다.
   

자세한 내용은 블로그에서 확인할 수 있다.

[](https://velog.io/@one_two_three/series/Unity-Project)
