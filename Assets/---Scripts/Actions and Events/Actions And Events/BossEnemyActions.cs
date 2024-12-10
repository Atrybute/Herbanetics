using System;

public class BossEnemyActions
{
   public  event Action OnBossDefeat;

   public void BossDefeatedEvent()
   {
        OnBossDefeat?.Invoke();
   }
}
