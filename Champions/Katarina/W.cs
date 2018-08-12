using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.API;
using System.Linq;
using LeagueSandbox.GameServer;

namespace Spells
{
    public class KatarinaW : GameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            spell.spellAnimation("SPELL2", owner);
            ApiFunctionManager.AddParticleTarget(owner, "katarina_W_cas.troy", owner,1, "C_BUFFBONE_GLB_CHEST_LOC");
            ApiFunctionManager.AddParticleTarget(owner, "katarina_w_mis.troy", owner);
            //ApiFunctionManager.AddParticleTarget(owner, "katarina_w_tar_sand.troy", owner);   enemy
            

        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var damage = new[] { 40, 75, 110, 145, 180 }[spell.Level - 1] + (owner.GetStats().AbilityPower.Total * 0.6f) + (owner.GetStats().AttackDamage.Total * 0.25f);

            foreach (var enemyTarget in ApiFunctionManager.GetUnitsInRange(target, 375, true))
            {
                if (enemyTarget != owner && owner.GetDistanceTo(enemyTarget) < 375 && !ApiFunctionManager.UnitIsTurret(enemyTarget) && !ApiFunctionManager.UnitIsChampion(enemyTarget) && enemyTarget.Team == CustomConvert.GetEnemyTeam(owner.Team))
                {
                    enemyTarget.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                    ApiFunctionManager.AddParticleTarget(owner, "katarina_w_tar.troy", enemyTarget);

                    KatarinaQ.OnProc(enemyTarget, false);
                }
                else if(ApiFunctionManager.UnitIsChampion(enemyTarget) && enemyTarget != owner && owner.GetDistanceTo(enemyTarget) < 375 && !ApiFunctionManager.UnitIsTurret(enemyTarget) && enemyTarget.Team == CustomConvert.GetEnemyTeam(owner.Team))
                {
                    enemyTarget.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                    ApiFunctionManager.AddParticleTarget(owner, "katarina_w_tar.troy", enemyTarget);
                    var buff = ((ObjAIBase)target).AddBuffGameScript("KatarinaWBuff", "KatarinaWBuff", spell, -1, true);
                    ApiFunctionManager.AddBuffHUDVisual("KatarinaW", 1.0f, 1, owner, 1.0f);
                    
                    KatarinaQ.OnProc(enemyTarget, false);

                    ApiFunctionManager.CreateTimer(1.0f, () =>
                    {
                        owner.RemoveBuffGameScript(buff);
                    });
                }
            }
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
