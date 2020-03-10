using System;
using UnityEngine;

namespace Data
{
  [Serializable]
   public class GameSettings
   {
      public int playerHealth;
      public float playerSpeed;
      public float startStepDelay;
      public float stepDelayMultiplier;
      public int blocksCount;
      public int blocksHealth;
      public float playerBulletSpeed;
      public float enemyBulletSpeed;
      public float enemyMovingSpeed;
   }
}
