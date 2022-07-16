using UnityEngine;

namespace Gameplay.Entities
{
    // This can hit other entites. think minecraft mob.
    public class CombatEntity : MovableEntity
    {
        [SerializeField] protected int damage = 1;
        [SerializeField] protected float knockbackStrength = 1.5f;
        
        public virtual void HitOther(Entity entity)
        {
            Vector2 knockbackDirection = GetTargetLookDirection() * knockbackStrength;
            entity.TakeHit(damage, knockbackDirection);
        }
    }
}