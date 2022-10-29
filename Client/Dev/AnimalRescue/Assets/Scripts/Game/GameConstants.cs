using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConstants 
{
    public const int RequiredExperience = 100; // 레벨업 할떄 필요한 경험치 양
    public const int EnemyExperience = 3;      // 몬스터가 주는 경험치양
    public const int StatpowerUpPrice = 100;   // 능력치 강화할때마다 필요한 골드가격 level * 100
    public const float EnemySpawnTime = 1f;   // 몬스터 스폰시간
    public const int SpawnEnemyCount = 100;   // 1웨이브당 스폰 몬스터 숫자
    public const float WaveTime = 120f;       // 1웨이브 진행시간

}
