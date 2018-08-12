using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.API;
using System.Linq;
using LeagueSandbox.GameServer;
using System.Collections.Generic;

namespace Spells
{
    public class KatarinaQ : GameScript
    {
        bool _listenerAdded = false;
        float _targetNr = 4;
        static AttackableUnit _qUnit1;
        static AttackableUnit _qUnit2;
        static AttackableUnit _qUnit3;
        static AttackableUnit _qUnit4;
        private static Particle _mark1;
        private static Particle _mark2;
        private static Particle _mark3;
        private static Particle _mark4;
        private static Champion _owner;
        private static Spell _spell;

        public void OnActivate(Champion owner)
        {
        }
        
        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {

            if (!_listenerAdded)
            {
                ApiEventManager.OnHitUnit.AddListener(this, owner, OnProc);
                _listenerAdded = true;
            }

            _owner = owner;

            _spell = spell;

        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            _targetNr = 4;
            spell.AddProjectileTarget("KatarinaQ",target);
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            var ap = owner.GetStats().AbilityPower.Total * 0.45f;

            target.TakeDamageBySpell(owner, 40 + 25 * spell.Level + ap, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false, spell);
            
            if (_targetNr == 4)
            {
                OnQProc(target,false);
                _qUnit1 = target;
                _mark1 = ApiFunctionManager.AddParticleTarget(owner, "katarina_daggered.troy", _qUnit1);
                ApiFunctionManager.CreateTimer(4.0f, () => {
                    _qUnit1 = null;
                    ApiFunctionManager.RemoveParticle(_mark1);
                });
                Bounce1(owner, target, spell, projectile);
            }
            else if (_targetNr == 3)
            {
                OnQProc(target, false);
                _qUnit2 = target;
                _mark2 = ApiFunctionManager.AddParticleTarget(owner, "katarina_daggered.troy", _qUnit2);
                ApiFunctionManager.CreateTimer(4.0f, () => {
                    _qUnit2 = null;
                    ApiFunctionManager.RemoveParticle(_mark2);
                });
                Bounce2(owner, target, spell, projectile);
            }
            else if (_targetNr == 2)
            {
                OnQProc(target, false);
                _qUnit3 = target;
                _mark3 = ApiFunctionManager.AddParticleTarget(owner, "katarina_daggered.troy", _qUnit3);
                ApiFunctionManager.CreateTimer(4.0f, () => {
                    _qUnit3 = null;
                    ApiFunctionManager.RemoveParticle(_mark3);
                });
                Bounce3(owner, target, spell, projectile);
            }
            else if (_targetNr == 1)
            {
                OnQProc(target, false);
                _qUnit4 = target;
                _mark4 = ApiFunctionManager.AddParticleTarget(owner, "katarina_daggered.troy", _qUnit4);
                ApiFunctionManager.CreateTimer(4.0f, () => {
                    _qUnit4 = null;
                    ApiFunctionManager.RemoveParticle(_mark4);
                });
            }
            else
            {
                projectile.setToRemove();
            }
            
            _targetNr -= 1;

            

        }

        public void Bounce1(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            

            var each = 0;
            bool sett = false;

            foreach (var enemyTarget in ApiFunctionManager.GetUnitsInRange(target, 675, true))
            {
                
                if (enemyTarget != null)
                {
                    if (enemyTarget.Team == CustomConvert.GetEnemyTeam(owner.Team))
                    {
                        if (enemyTarget != target)
                        {
                            if (enemyTarget != owner)
                            {
                                if (target.GetDistanceTo(enemyTarget) < 675)
                                {
                                    if (!ApiFunctionManager.UnitIsTurret(enemyTarget))
                                    {
                                        if (enemyTarget != _qUnit1)
                                        {
                                            if (sett == false)
                                            {
                                                projectile.DashToTarget(enemyTarget, 1500, 700, 300, 0.5f);
                                                sett = true;
                                            }
                                            
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                

                each++;

            }

            if (sett == false)
            {
                projectile.setToRemove();
            }
            

        }

        public void Bounce2(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            

            var each = 0;
            bool sett = false;

            foreach (var enemyTarget in ApiFunctionManager.GetUnitsInRange(target, 675, true))
            {

                if (enemyTarget != null)
                {
                    if (enemyTarget.Team == CustomConvert.GetEnemyTeam(owner.Team))
                    {
                        if (enemyTarget != target)
                        {
                            if (enemyTarget != owner)
                            {
                                if (target.GetDistanceTo(enemyTarget) < 675)
                                {
                                    if (!ApiFunctionManager.UnitIsTurret(enemyTarget))
                                    {
                                        if (enemyTarget != _qUnit1)
                                        {
                                            if (enemyTarget != _qUnit2)
                                            {
                                                if (sett == false)
                                                {
                                                projectile.DashToTarget(enemyTarget, 1500, 700, 300, 0.5f);
                                                sett = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


                each++;

            }

            if (sett == false)
            {
                projectile.setToRemove();
            }
            

        }

        public void Bounce3(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            

            var each = 0;
            bool sett = false;

            foreach (var enemyTarget in ApiFunctionManager.GetUnitsInRange(target, 675, true))
            {

                if (enemyTarget != null)
                {
                    if (enemyTarget.Team == CustomConvert.GetEnemyTeam(owner.Team))
                    {
                        if (enemyTarget != target)
                        {
                            if (enemyTarget != owner)
                            {
                                if (target.GetDistanceTo(enemyTarget) < 675)
                                {
                                    if (!ApiFunctionManager.UnitIsTurret(enemyTarget))
                                    {
                                        if (enemyTarget != _qUnit1)
                                        {
                                            if (enemyTarget != _qUnit2)
                                            {
                                                if (enemyTarget != _qUnit3)
                                                {
                                                    if (sett == false)
                                                    {
                                                        projectile.DashToTarget(enemyTarget, 1500, 700, 300, 0.5f);
                                                        sett = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


                each++;

            }

            if (sett == false)
            {
                projectile.setToRemove();
            }
            

        }



        public void OnUpdate(double diff)
        {
        }

        public void OnQProc(AttackableUnit target, bool isCrit)
        {

            if (target == _qUnit1)
            {
                target.TakeDamageBySpell(_owner, 15 * _spell.Level + _owner.GetStats().AbilityPower.Total * 0.015f , DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false, _spell);
            }
            else if (target == _qUnit2)
            {
                target.TakeDamageBySpell(_owner, 15 * _spell.Level + _owner.GetStats().AbilityPower.Total * 0.015f, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false, _spell);
            }
            else if (target == _qUnit3)
            {
                target.TakeDamageBySpell(_owner, 15 * _spell.Level + _owner.GetStats().AbilityPower.Total * 0.015f, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false, _spell);
            }
            else if (target == _qUnit4)
            {
                target.TakeDamageBySpell(_owner, 15 * _spell.Level + _owner.GetStats().AbilityPower.Total * 0.015f, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false, _spell);
            }
            
        }

            public static void OnProc(AttackableUnit target, bool isCrit)
        {
            

            if (target == _qUnit1)
            {
                target.TakeDamageBySpell(_owner, 15 * _spell.Level + _owner.GetStats().AbilityPower.Total * 0.015f, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false, _spell);
                _qUnit1 = null;
                ApiFunctionManager.RemoveParticle(_mark1);
            }
            else if (target == _qUnit2)
            {
                target.TakeDamageBySpell(_owner, 15 * _spell.Level + _owner.GetStats().AbilityPower.Total * 0.015f, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false, _spell);
                _qUnit2 = null;
                ApiFunctionManager.RemoveParticle(_mark2);
            }
            else if (target == _qUnit3)
            {
                target.TakeDamageBySpell(_owner, 15 * _spell.Level + _owner.GetStats().AbilityPower.Total * 0.015f, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false, _spell);
                _qUnit3 = null;
                ApiFunctionManager.RemoveParticle(_mark3);
            }
            else if (target == _qUnit4)
            {
                target.TakeDamageBySpell(_owner, 15 * _spell.Level + _owner.GetStats().AbilityPower.Total * 0.015f, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false, _spell);
                _qUnit4 = null;
                ApiFunctionManager.RemoveParticle(_mark4);
            }
            

        }

    }
}

        
        
