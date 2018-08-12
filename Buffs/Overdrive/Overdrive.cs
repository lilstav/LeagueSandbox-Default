using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Logic.GameObjects.Spells;
using LeagueSandbox.GameServer.Logic.GameObjects.Stats;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Overdrive
{
    internal class Overdrive : IBuffGameScript
    {
        private StatsModifier _statMod;

        public void OnActivate(ObjAiBase unit, Spell ownerSpell)
        {
            _statMod = new StatsModifier();
            _statMod.MoveSpeed.PercentBonus = _statMod.MoveSpeed.PercentBonus + (12f + ownerSpell.Level * 4) / 100f;
            _statMod.AttackSpeed.PercentBonus = _statMod.AttackSpeed.PercentBonus + (22f + 8f * ownerSpell.Level) / 100f;
            unit.AddStatModifier(_statMod);
        }

        public void OnDeactivate(ObjAiBase unit)
        {
            unit.RemoveStatModifier(_statMod);
        }

        public void OnUpdate(double diff)
        {

        }
    }
}

