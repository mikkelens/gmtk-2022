using Abilities.Base;
using Abilities.Data;
using Management;
using Tools;

namespace Abilities.Spells
{
	// can modify stats
	public class Focus : Spell
	{
		public Optional<ImpactData> selfImpact;

		protected override void Use()
		{
			if (selfImpact.Enabled)
				ImpactEntity(selfImpact.Value, SourceEntity, SourceEntity.transform.forward.WorldToPlane());
			
			if (selfImpact.Value.effects.Enabled) 
				SourceEntity.ApplyModifierCollectionToObject(selfImpact.Value.effects.Value);
		}
	}
}