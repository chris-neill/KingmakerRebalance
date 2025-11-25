using CallOfTheWild.NewMechanics;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Root;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Pathfinding.Voxels;
using UnityEngine;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace CallOfTheWild
{
    public class Stalker
    {
        public static BlueprintCharacterClass stalker_class;

        public static BlueprintProgression stalker_progression;

        public static BlueprintFeature stalker_proficiencies;

        public static BlueprintFeature animus_pool;

        public static BlueprintAbilityResource animus_resource;

        public static BlueprintAbilityResource strike_resource;

        public static BlueprintFeature certain_strike;

        public static BlueprintFeature strike_recovery;

        public static BlueprintFeature subtle_intent;

        public static BlueprintFeature defensive_reflexes;

        public static BlueprintFeature blending;

        public static BlueprintFeature blending_imp;

        public static BlueprintFeature enhance_perception;

        public static BlueprintFeature critical_focus;

        public static BlueprintFeature sixth_sense;

        public static BlueprintBuff deadly_strike_buff;

        public static Conditional deadly_strike_cond;

        public static BlueprintFeature deadly_strike_apply;

        public static BlueprintFeature deadly_strike;

        public static BlueprintFeature deadly_insight;

        public static BlueprintFeature deadly_ambush;

        public static BlueprintFeature master_strike;

        public static BlueprintFeatureSelection stalker_art;

        public static BlueprintFeature critical_edge;

        public static BlueprintFeature critical_training;

        public static BlueprintFeature killers_implements;

        public static BlueprintFeature blindsight;

        public static BlueprintFeature counterstrike;

        public static BlueprintFeature shadow_strike;

        public static BlueprintFeature blade_dash;

        public static BlueprintFeatureSelection martial_strikes;

        private static BlueprintUnitProperty casting_stat_property;

        private static LibraryScriptableObject library => Main.library;

        private static BlueprintCharacterClass[] getStalkerArray()
        {
            return new BlueprintCharacterClass[1] { stalker_class };
        }

        public static void createClass()
        {
            BlueprintCharacterClass rogue =
                ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>(
                    "299aa766dee3cbf4790da4efb8c72484"
                );
            BlueprintCharacterClass monk =
                ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>(
                    "e8f21e5b58e0569468e420ebea456124"
                );
            ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>(
                "cda0615668a6df14eb36ba19ee881af6"
            );
            stalker_class = Helpers.Create<BlueprintCharacterClass>();
            stalker_class.name = "StalkerClass";
            library.AddAsset(stalker_class, "");
            stalker_class.LocalizedName = Helpers.CreateString("Stalker.Name", "Stalker");
            stalker_class.LocalizedDescription = Helpers.CreateString(
                "Stalker.Description",
                "An effective warrior wielding both skill and stealth, the stalker is a martial disciple who battles in the deep shadows and the hidden underworld of night. Through rigorous training and deep, intuitive instincts, the stalker is a trained killer whose very art is considered illegal in some places. Part mystic, part warrior, and part assassin, the stalker’s arts are varied, but always deadly."
            );
            stalker_class.m_Icon = rogue.Icon;
            stalker_class.SkillPoints = rogue.SkillPoints - 1;
            stalker_class.HitDie = rogue.HitDie;
            stalker_class.BaseAttackBonus = rogue.BaseAttackBonus;
            stalker_class.FortitudeSave = library.Get<BlueprintStatProgression>(
                "dc0c7c1aba755c54f96c089cdf7d14a3"
            );
            stalker_class.ReflexSave = library.Get<BlueprintStatProgression>(
                "dc0c7c1aba755c54f96c089cdf7d14a3"
            );
            stalker_class.WillSave = library.Get<BlueprintStatProgression>(
                "ff4662bde9e75f145853417313842751"
            );
            stalker_class.Spellbook = rogue.Spellbook;
            stalker_class.ClassSkills = rogue.ClassSkills.RemoveFromArray(
                StatType.SkillUseMagicDevice
            );
            stalker_class.IsDivineCaster = rogue.IsDivineCaster;
            stalker_class.IsArcaneCaster = rogue.IsArcaneCaster;
            stalker_class.StartingGold = rogue.StartingGold;
            stalker_class.PrimaryColor = 31;
            stalker_class.SecondaryColor = 17;
            stalker_class.RecommendedAttributes = monk.RecommendedAttributes;
            stalker_class.NotRecommendedAttributes = monk.NotRecommendedAttributes;
            stalker_class.EquipmentEntities = rogue.EquipmentEntities;
            stalker_class.MaleEquipmentEntities = rogue.MaleEquipmentEntities;
            stalker_class.FemaleEquipmentEntities = rogue.FemaleEquipmentEntities;
            stalker_class.ComponentsArray = rogue.ComponentsArray;
            stalker_class.StartingItems = new BlueprintItem[5]
            {
                library.Get<BlueprintItem>("afbe88d27a0eb544583e00fa78ffb2c7"),
                library.Get<BlueprintItem>("bd5a0998ef4616a45abf591d78c1452e"),
                library.Get<BlueprintItem>("94b24d55f514cdd45a79d34904fea944"),
                library.Get<BlueprintItem>("f991f3051c3b9e64fabc87891077b613"),
                library.Get<BlueprintItem>("cde78c8bd7a0b514ab0669852ded248e"),
            };
            casting_stat_property = CastingStatPropertyGetter.createProperty(
                "StalkerCastingStatProperty",
                "e4583e910c344f6da04952e6d25c213a",
                StatType.Wisdom,
                stalker_class
            );
            createProgression();
            stalker_class.Progression = stalker_progression;
            stalker_class.Archetypes = new BlueprintArchetype[0];
            Helpers.RegisterClass(stalker_class);
        }

        private static void createProgression()
        {
            BlueprintFeature uncanny_dodge = library.Get<BlueprintFeature>(
                "3c08d842e802c3e4eb19d15496145709"
            );
            createDefensiveReflexes();
            createBlindsight();
            createProficiencies();
            createAnimusPool();
            createACBonus();
            createCertainStrike();
            createCriticalTraining();
            createMartialStrikes();
            createDeadlyStrike();
            createMasterStrike();
            createBlending();
            createCriticalFocus();
            createStalkerArt();
            createEnhancePerception();
            createSixthSense();
            stalker_progression = Helpers.CreateProgression(
                "StalkerProgression",
                stalker_class.Name,
                stalker_class.Description,
                "",
                stalker_class.Icon,
                FeatureGroup.None
            );
            stalker_progression.Classes = getStalkerArray();
            stalker_progression.LevelEntries = new LevelEntry[20]
            {
                Helpers.LevelEntry(
                    1,
                    deadly_strike,
                    deadly_strike_apply,
                    stalker_proficiencies,
                    animus_pool,
                    counterstrike,
                    stalker_art,
                    enhance_perception
                ),
                Helpers.LevelEntry(2, defensive_reflexes, subtle_intent),
                Helpers.LevelEntry(3, stalker_art),
                Helpers.LevelEntry(4, uncanny_dodge),
                Helpers.LevelEntry(5, deadly_insight),
                Helpers.LevelEntry(6, blending, martial_strikes),
                Helpers.LevelEntry(7, stalker_art, sixth_sense),
                Helpers.LevelEntry(8, critical_focus),
                Helpers.LevelEntry(9, certain_strike),
                Helpers.LevelEntry(10, martial_strikes),
                Helpers.LevelEntry(11, stalker_art),
                Helpers.LevelEntry(12, strike_recovery),
                Helpers.LevelEntry(13),
                Helpers.LevelEntry(14, martial_strikes),
                Helpers.LevelEntry(15, stalker_art),
                Helpers.LevelEntry(16, blending_imp),
                Helpers.LevelEntry(17),
                Helpers.LevelEntry(18, blindsight),
                Helpers.LevelEntry(19, martial_strikes, stalker_art),
                Helpers.LevelEntry(20, master_strike),
            };
            stalker_progression.UIDeterminatorsGroup = new BlueprintFeatureBase[2]
            {
                stalker_proficiencies,
                animus_pool,
            };
            stalker_progression.UIGroups = new UIGroup[5]
            {
                Helpers.CreateUIGroup(deadly_strike, master_strike),
                Helpers.CreateUIGroup(counterstrike, martial_strikes),
                Helpers.CreateUIGroup(
                    enhance_perception,
                    deadly_insight,
                    sixth_sense,
                    certain_strike
                ),
                Helpers.CreateUIGroup(subtle_intent, blending, blending_imp),
                Helpers.CreateUIGroup(
                    defensive_reflexes,
                    uncanny_dodge,
                    critical_focus,
                    strike_recovery,
                    blindsight
                ),
            };
        }

        static void createCertainStrike()
        {
            var certain_strike_buff = Helpers.CreateBuff(
                "StalkerCertainStrikeBuff",
                "Combat Insight",
                "As a swift action the stalker may spend 1 animus point to reroll a failed attack roll.",
                "",
                Helpers.GetIcon("2c38da66e5a599347ac95b3294acbe00"),
                null,
                Helpers.Create<NewMechanics.ModifyD20WithActions>(m =>
                {
                    m.RollsAmount = 1;
                    m.TakeBest = true;
                    m.Rule = NewMechanics.ModifyD20WithActions.RuleType.AttackRoll;
                    m.RerollOnlyIfFailed = true;
                    m.required_resource = animus_resource;
                    m.actions = Helpers.CreateActionList(
                        Common.createContextActionSpendResource(animus_resource, 1)
                    );
                })
            );

            certain_strike = Helpers.CreateFeature(
                "StalkerCertainStrikeFeature",
                certain_strike_buff.Name,
                certain_strike_buff.Description,
                "",
                certain_strike_buff.Icon,
                FeatureGroup.None,
                Helpers.CreateAddAbilityResource(animus_resource)
            );

            var toggle = Helpers.CreateActivatableAbility(
                "StalkerCertainStrikeToggleAbility",
                certain_strike_buff.Name,
                certain_strike_buff.Description,
                "a3ed2638f2e343dd822a3fac640d0ff0",
                Helpers.GetIcon("2c38da66e5a599347ac95b3294acbe00"),
                certain_strike_buff,
                AbilityActivationType.Immediately,
                CommandType.Swift,
                null,
                Helpers.CreateActivatableResourceLogic(
                    animus_resource,
                    ActivatableAbilityResourceLogic.ResourceSpendType.Never
                ),
                Helpers.Create<ResourceMechanics.RestrictionHasEnoughResource>(r =>
                    r.resource = animus_resource
                )
            );
            toggle.DeactivateImmediately = true;

            certain_strike.AddComponent(Helpers.CreateAddFact(toggle));
        }

        static void createStrikeRecovery(BlueprintAbilityResource resource)
        {
            BlueprintAbility ability = Helpers.CreateAbility(
                "StalkerStrikeRecovery",
                "Martial Recovery",
                "The stalker can expend 2 points of animus as a standard action to recover 1 use of a Martial Maneuver.",
                "",
                Helpers.GetIcon("72dcf1fb106d5054a81fd804fdc168d3"),
                AbilityType.Supernatural,
                UnitCommand.CommandType.Standard,
                AbilityRange.Personal,
                "",
                "",
                Helpers.CreateRunActions(
                    Helpers.Create<ResourceMechanics.ContextRestoreResource>(c =>
                        c.Resource = resource
                    )
                ),
                animus_resource.CreateResourceLogic(spend: true, 2)
            );
            ability.setMiscAbilityParametersSelfOnly();

            strike_recovery = Common.AbilityToFeature(ability, hide: false);
        }

        static void createBlindsight()
        {
            var icon = Helpers.GetIcon("30e5dc243f937fc4b95d2f8f4e1b7ff3");

            blindsight = Helpers.CreateFeature(
                "StalkerBlindsightFeature",
                "Blindsight",
                "The heightened precognitive abilities of the stalker manifest in their ability to sense things around them that others cannot, granting them blindsight of 30-ft.",
                "",
                icon,
                FeatureGroup.None,
                Common.createBlindsight(30),
                Common.createBuffDescriptorImmunity(SpellDescriptor.GazeAttack),
                Common.createSpellImmunityToSpellDescriptor(SpellDescriptor.GazeAttack)
            );
        }

        private static void createProficiencies()
        {
            stalker_proficiencies = library.CopyAndAdd<BlueprintFeature>(
                "33e2a7e4ad9daa54eaf808e1483bb43c",
                "StalkerProficiencies",
                ""
            );

            stalker_proficiencies.ReplaceComponent(
                delegate(AddProficiencies a)
                {
                    a.WeaponProficiencies = new WeaponCategory[1] { WeaponCategory.HandCrossbow };
                }
            );

            stalker_proficiencies.AddComponents(
                library.Get<BlueprintFeature>("203992ef5b35c864390b4e4a1e200629").CreateAddFact()
            );
            stalker_proficiencies.SetNameDescription(
                "Stalker Proficiencies",
                "Stalkers are proficient with all simple and martial weapons, and with light armor. Stalkers are not proficient with shields of any kind."
            );
        }

        private static void createAnimusPool()
        {
            ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>(
                "299aa766dee3cbf4790da4efb8c72484"
            );
            animus_resource = Helpers.CreateAbilityResource(
                "StalkerAnimusResource",
                "",
                "",
                "",
                null
            );
            animus_resource.SetIncreasedByStat(0, StatType.Wisdom);
            animus_resource.SetIncreasedByLevelStartPlusDivStep(
                0,
                0,
                0,
                2,
                1,
                0,
                0f,
                getStalkerArray()
            );
            animus_pool = Helpers.CreateFeature(
                "AnimusPoolstalker",
                "Animus Pool",
                "At 1st level, a stalker gains a pool of animus points, supernatural energy he can use to accomplish amazing feats. The number of points in the stalker’s animus pool is equal to 1/2 their stalker level + their Wisdom modifier (minimum of 1).",
                "",
                null,
                FeatureGroup.None,
                animus_resource.CreateAddAbilityResource()
            );
        }

        private static void createDeadlyStrike()
        {
            deadly_strike_buff = Helpers.CreateBuff(
                "StalkerDeadlyStrikeBuff",
                "Deadly Strike",
                "The stalker is capable of maximizing their deadliness whenever they land a critical blow upon their opponent, opening their target up for future punishment as the stalker becomes attuned to their prey. Whenever the stalker scores a successful critical hit against a creature, their deadly strike ability activates against that creature for a number of rounds equal to their Wisdom modifier. Deadly strike inflicts extra damage, to only this target creature, on all of the stalker’s attacks. This extra damage is 1d6 at 1st level, and increases by 1d6 for every four stalker levels thereafter. If the stalker scores a successful critical hit during the time his deadly strike is active, the duration of this ability is extended by one round (no more than one extension can be made per round). Ranged attacks can count as deadly strikes only if the target is within 30-ft. Deadly strike is more effective with weapons with higher critical multipliers, such as scythes and battle axes. Weapons with a x3 critical multiplier inflict damage with deadly strikes using a d8 instead of a d6, and weapons with a critical multiplier of x4 use d10’s. Damage multipliers higher than x4 use d10’s for deadly strikes damage.",
                "",
                null,
                null
            );
            deadly_strike_buff.SetBuffFlags(
                BuffFlags.HiddenInUi | deadly_strike_buff.GetBuffFlags()
            );
            deadly_strike_buff.Stacking = StackingType.Summ;
            BlueprintBuff buff = Helpers.CreateBuff(
                "StalkerDeadlyStrikeCooldown",
                "",
                "",
                "",
                null,
                null
            );
            buff.SetBuffFlags(BuffFlags.HiddenInUi | buff.GetBuffFlags());
            ContextActionApplyBuff contextActionApplyBuff = Common.createContextActionApplyBuff(
                buff,
                Helpers.CreateContextDuration(),
                is_from_spell: false,
                is_child: false,
                is_permanent: true,
                dispellable: false
            );
            ContextActionApplyBuff contextActionApplyBuff2 = Common.createContextActionApplyBuff(
                deadly_strike_buff,
                Helpers.CreateContextDuration(AbilityRankType.Default.CreateContextValue()),
                is_from_spell: false,
                is_child: false,
                is_permanent: false,
                dispellable: false
            );
            ContextActionApplyBuff contextActionApplyBuff3 = Common.createContextActionApplyBuff(
                deadly_strike_buff,
                Helpers.CreateContextDuration(1),
                is_from_spell: false,
                is_child: false,
                is_permanent: false,
                dispellable: false
            );
            Conditional ifFalse = Helpers.CreateConditional(
                Common.createContextConditionHasBuffFromCaster(buff),
                new GameAction[2]
                {
                    Common.createContextActionRemoveBuff(buff),
                    contextActionApplyBuff2,
                },
                new GameAction[1] { contextActionApplyBuff2 }
            );
            Conditional ifTrue = Helpers.CreateConditional(
                Common.createContextConditionHasBuffFromCaster(buff),
                new GameAction[0],
                new GameAction[2] { contextActionApplyBuff3, contextActionApplyBuff }
            );
            deadly_strike_cond = Helpers.CreateConditional(
                Common.createContextConditionHasBuffFromCaster(deadly_strike_buff),
                ifTrue,
                ifFalse
            );
            BlueprintAbility blueprintAbility = Helpers.CreateAbility(
                "StalkerStudiedTarget",
                "Deadly Insight",
                "The stalker may use his deadly strikes in conjunction with his combat insight to “read” his opponent’s defenses and effortlessly attack beyond his foe’s guard. The stalker spends 1 point of animus as a swift action to read his target opponent, and may apply his deadly strike to all of his attacks for a number of rounds equal to their Wisdom modifier against this target. If the stalker scores a successful critical hit against the target while his deadly strike ability is active, the duration of this ability is extended by one round (no more than one extension can be made per round).",
                "",
                Helpers.GetIcon("385260ca07d5f1b4e907ba22a02944fc"),
                AbilityType.Supernatural,
                UnitCommand.CommandType.Swift,
                AbilityRange.Close,
                "1 round/Wisdom modifier",
                "",
                Helpers.CreateRunActions(deadly_strike_cond),
                Helpers.CreateContextRankConfig(
                    ContextRankBaseValueType.CustomProperty,
                    ContextRankProgression.AsIs,
                    AbilityRankType.Default,
                    customProperty: casting_stat_property,
                    min: 1,
                    max: null
                ),
                Helpers.Create(
                    delegate(RecalculateOnStatChange r)
                    {
                        r.Stat = StatType.Wisdom;
                    }
                ),
                animus_resource.CreateResourceLogic()
            );
            blueprintAbility.setMiscAbilityParametersSingleTargetRangedHarmful(
                works_on_allies: true
            );
            blueprintAbility.EffectOnEnemy = AbilityEffectOnUnit.None;
            deadly_insight = Common.AbilityToFeature(blueprintAbility, hide: false);
            deadly_strike_apply = Helpers.CreateFeature(
                "StalkerDeadlyStrikeApply",
                "",
                "",
                "",
                null,
                FeatureGroup.None,
                Helpers.Create(
                    delegate(AddInitiatorAttackRollTrigger a)
                    {
                        a.OnlyHit = true;
                        a.CriticalHit = true;
                        a.Action = Helpers.CreateActionList(
                            Common.createContextActionOnContextCaster(
                                Helpers.Create<ResourceMechanics.ContextRestoreResource>(c =>
                                {
                                    c.Resource = strike_resource;
                                    c.amount = 1;
                                })
                            ),
                            deadly_strike_cond
                        );
                    }
                ),
                Helpers.CreateContextRankConfig(
                    ContextRankBaseValueType.CustomProperty,
                    ContextRankProgression.AsIs,
                    AbilityRankType.Default,
                    customProperty: casting_stat_property,
                    min: 1,
                    max: null
                ),
                Helpers.Create(
                    delegate(RecalculateOnStatChange r)
                    {
                        r.Stat = StatType.Wisdom;
                    }
                )
            );
            deadly_strike_apply.HideInUI = true;
            deadly_strike_apply.HideInCharacterSheetAndLevelUp = true;
            string description = deadly_strike_buff.Description;
            Sprite icon = Helpers.GetIcon("df4f34f7cac73ab40986bc33f87b1a3c");
            BlueprintCharacterClass[] stalkerArray = getStalkerArray();

            deadly_strike = Helpers.CreateFeature(
                "StalkerDeadlyStrike",
                "Deadly Strike",
                description,
                "",
                icon,
                FeatureGroup.None,
                Helpers.Create(
                    delegate(DamageBonusPrecisionAgainstFactOwner a)
                    {
                        a.bonus = DiceType.D6.CreateContextDiceValue(
                            AbilityRankType.Default.CreateContextValue(),
                            0
                        );
                        a.attack_types = new AttackType[2] { AttackType.Melee, AttackType.Ranged };
                        a.only_from_caster = true;
                        a.checked_fact = deadly_strike_buff;
                    }
                ),
                Helpers.CreateContextRankConfig(
                    ContextRankBaseValueType.ClassLevel,
                    ContextRankProgression.StartPlusDivStep,
                    AbilityRankType.Default,
                    null,
                    null,
                    1,
                    4,
                    exceptClasses: false,
                    StatType.Unknown,
                    null,
                    stalkerArray
                )
            );
            library
                .Get<BlueprintFeature>("9b9eac6709e1c084cb18c3a366e0ec87")
                .AddComponents(
                    Helpers.Create(
                        delegate(FeatureReplacement f)
                        {
                            f.replacement_feature = deadly_strike;
                        }
                    )
                );
        }

        private static void createMasterStrike()
        {
            Conditional conditional = Helpers.CreateConditional(
                Common.createContextConditionHasBuffFromCaster(deadly_strike_buff),
                SavingThrowType.Fortitude.CreateActionSavingThrow(
                    Helpers.CreateConditionalSaved(
                        Common.createContextActionRemoveBuff(deadly_strike_buff),
                        Helpers.Create<ContextActionKillTarget>()
                    )
                )
            );
            BlueprintAbility ability = Helpers.CreateAbility(
                "StalkerMasterStrike",
                "Master Stalker",
                "The stalker becomes a master of execution, striking with lethal precision. By expending 3 points of animus, they can make a single attack as a standard action against a target marked by their deadly strike, using their full attack bonus. If the attack hits, the target takes damage as normal and must succeed on a Fortitude saving throw (DC = 10 + 1/2 the stalker’s level + their Wisdom modifier) or be instantly slain. If the target succeeds on the save, the stalker must study them again before making another attempt.",
                "",
                Helpers.GetIcon("c3d2294a6740bc147870fff652f3ced5"),
                AbilityType.Supernatural,
                UnitCommand.CommandType.Standard,
                AbilityRange.Weapon,
                "",
                "",
                Helpers.Create<AttackAnimation>(),
                Helpers.CreateRunActions(conditional),
                Common.createContextCalculateAbilityParamsBasedOnClasses(
                    getStalkerArray(),
                    StatType.Wisdom
                ),
                animus_resource.CreateResourceLogic(spend: true, 3)
            );
            ability.setMiscAbilityParametersSingleTargetRangedHarmful(works_on_allies: true);
            master_strike = Common.AbilityToFeature(ability, hide: false);
        }

        private static void createACBonus()
        {
            var rankConfig = Helpers.CreateContextRankConfig(
                ContextRankBaseValueType.ClassLevel,
                ContextRankProgression.Custom,
                AbilityRankType.Default,
                customProgression: new (int, int)[] { (4, 1), (8, 2), (12, 3), (16, 4), (20, 5) },
                classes: getStalkerArray()
            );

            subtle_intent = Helpers.CreateFeature(
                "StalkerSubtleIntent",
                "Subtle Intent",
                "A stalker excels at disguising their intentions, blending patience with lethal timing. Starting at 2nd level, they gain a +1 dodge bonus to AC. This bonus improves to +2 at 6th level, +3 at 10th, +4 at 14th, and +5 at 18th level. When the stalker remains unseen—whether through Stealth, distraction, or invisibility—any attack made against a creature that has not detected the stalker deals extra damage equal to this bonus.",
                "",
                Helpers.GetIcon("7a5b5bf845779a941a67251539545762"),
                FeatureGroup.None,
                Helpers.CreateAddContextStatBonus(
                    StatType.AC,
                    ModifierDescriptor.Dodge,
                    ContextValueType.Rank,
                    AbilityRankType.Default
                ),
                Helpers.Create<NewMechanics.DamageBonusIfInvisibleToTarget>(f =>
                {
                    f.Bonus = 0;
                    f.ContextBonus = Helpers.CreateContextValue(AbilityRankType.Default);
                }),
                rankConfig
            );
        }

        private static void createDefensiveReflexes()
        {
            defensive_reflexes = Helpers.CreateFeature(
                "StalkerCombatInsight",
                "Defensive Reflexes",
                "The keen senses of the stalker delivers them a sort of heightened awareness. This insight performs as an intuitive alarm, alerting them of danger. The stalker may add their Wisdom modifier to their initiative score and to Reflex saving throws as an insight bonus.",
                "",
                Helpers.GetIcon("a28693b24cc412c478b8b85877f2dad2"),
                FeatureGroup.None,
                Helpers.CreateAddContextStatBonus(StatType.Initiative, ModifierDescriptor.Insight),
                Helpers.CreateAddContextStatBonus(StatType.SaveReflex, ModifierDescriptor.Insight),
                Helpers.CreateContextRankConfig(
                    ContextRankBaseValueType.StatBonus,
                    ContextRankProgression.AsIs,
                    AbilityRankType.Default,
                    1,
                    null,
                    0,
                    0,
                    exceptClasses: false,
                    StatType.Wisdom
                ),
                Helpers.Create(
                    delegate(RecalculateOnStatChange r)
                    {
                        r.Stat = StatType.Wisdom;
                    }
                )
            );
        }

        private static void createBlending()
        {
            blending = Helpers.CreateFeature(
                "StalkerBlending",
                "Blending",
                "The stalker’s keen awareness and disciplined focus grant them an intuitive understanding of others, enhancing their ability to read subtle cues and move unnoticed. They gain a +2 insight bonus to Perception and Stealth checks.",
                "",
                Helpers.GetIcon("c927a8b0cd3f5174f8c0b67cdbfde539"),
                FeatureGroup.None,
                StatType.SkillPerception.CreateAddStatBonus(2, ModifierDescriptor.Insight),
                StatType.SkillStealth.CreateAddStatBonus(2, ModifierDescriptor.Insight)
            );
            BlueprintAbility blueprintAbility = library.CopyAndAdd<BlueprintAbility>(
                "b26a123a009d4a141ac9c19355913285",
                "StalkerHide",
                ""
            );
            blueprintAbility.RemoveComponents<AbilityCasterIsOnFavoredTerrain>();
            blueprintAbility.SetName("Hide in Plain Sight");
            blueprintAbility.Type = AbilityType.Extraordinary;
            blueprintAbility.SetDescription(
                "A stalker can use the Stealth skill even while being observed. As long as they are within 10 feet of an area of dim light, a stalker can hide themselves from view in the open without anything to actually hide behind. They cannot, however, hide in their own shadow."
            );
            blending_imp = Common.AbilityToFeature(blueprintAbility, hide: false);
        }

        private static void createCriticalFocus()
        {
            critical_focus = Helpers.CreateFeature(
                "StalkerInstinct",
                "Killer’s Instinct",
                "The stalkers instinct is honed to a razor’s fine edge, allowing them to add their Wisdom modifier as a competence bonus to confirm critical hits. This ability counts as if the character possessed the Critical Focus feat, and for the purposes of taking critical feats that the character qualifies for.",
                "",
                Helpers.GetIcon("43696e91d2a67e34981925f0c62bff40"),
                FeatureGroup.None,
                Helpers.Create(
                    delegate(CriticalConfirmationBonus c)
                    {
                        c.Value = AbilityRankType.Default.CreateContextValue();
                    }
                ),
                Helpers.CreateContextRankConfig(
                    ContextRankBaseValueType.StatBonus,
                    ContextRankProgression.AsIs,
                    AbilityRankType.Default,
                    1,
                    null,
                    0,
                    0,
                    exceptClasses: false,
                    StatType.Wisdom
                ),
                Helpers.Create(
                    delegate(RecalculateOnStatChange r)
                    {
                        r.Stat = StatType.Wisdom;
                    }
                )
            );
            BlueprintFeature blueprintFeature = library.Get<BlueprintFeature>(
                "787e56055e3ef864d9c78a3ec21e56be"
            );
            BlueprintFeature blueprintFeature2 = library.Get<BlueprintFeature>(
                "12c1b556df3b667458ae200bfb38ccb8"
            );
            BlueprintFeature blueprintFeature3 = library.Get<BlueprintFeature>(
                "7c492212d25d8f04fbd43eb99d780b1e"
            );
            BlueprintFeature blueprintFeature4 = library.Get<BlueprintFeature>(
                "4c7205d859a1e114895e798af383d76a"
            );
            BlueprintFeature blueprintFeature5 = library.Get<BlueprintFeature>(
                "055fb8dcaf6be334ca13172bafa9e782"
            );
            BlueprintFeature[] array = new BlueprintFeature[5]
            {
                blueprintFeature,
                blueprintFeature2,
                blueprintFeature3,
                blueprintFeature4,
                blueprintFeature5,
            };
            foreach (BlueprintFeature obj in array)
            {
                obj.GetComponent<PrerequisiteFeature>().Group = Prerequisite.GroupType.Any;
                Common.addFeaturePrerequisiteAny(obj, critical_focus);
            }
            library
                .Get<BlueprintFeature>("8ac59959b1b23c347a0361dc97cc786d")
                .AddComponents(
                    Helpers.Create(
                        delegate(FeatureReplacement f)
                        {
                            f.replacement_feature = critical_focus;
                        }
                    )
                );
        }

        private static void createMartialStrikes()
        {
            BlueprintAbilityResource resource = Helpers.CreateAbilityResource(
                "StalkerExtraAttackResource",
                "",
                "",
                "",
                null
            );
            resource.SetIncreasedByLevelStartPlusDivStep(2, 0, 0, 3, 1, 0, 0f, getStalkerArray());
            BlueprintAbility extra_attack = library.CopyAndAdd<BlueprintAbility>(
                "7f6ea312f5dad364fa4a896d7db39fdd",
                "StalkerExtraAttackAbility",
                ""
            );
            extra_attack.SetNameDescription(
                "Martial Maneuver",
                "The stalker can make one additional attack at their highest attack bonus when making a full attack. This bonus attack stacks with haste and similar effects."
            );
            extra_attack.RemoveComponents<AbilityResourceLogic>();
            extra_attack.RemoveComponents<AddAbilityResources>();
            extra_attack.AddComponents(
                resource.CreateAddAbilityResource(),
                resource.CreateResourceLogic()
            );
            martial_strikes = library.CopyAndAdd<BlueprintFeatureSelection>(
                "7bc6a93f6e48eff49be5b0cde83c9450",
                "MartialManeuverSelection",
                ""
            );
            martial_strikes.Features = new BlueprintFeature[0];
            martial_strikes.SetName("Martial Maneuver");
            martial_strikes.SetDescription(
                "The stalker learns Martial Maneuvers over time, and can use this ability twice per day at 1st level, gaining an additional use every three levels thereafter. Spent uses are recovered on a successful critical hit."
            );

            createDualStrike(extra_attack);
            createBladeDash(resource);
            createCounterStrike(resource);
            createStrikeRecovery(resource);

            strike_resource = resource;
        }

        private static void createCounterStrike(BlueprintAbilityResource resource)
        {
            var effect_buff = Helpers.CreateBuff(
                "StalkerCounterstrikeDamageBuff",
                "",
                "",
                "",
                null,
                null,
                Helpers.Create<NewMechanics.AttackOfOpportunityDamgeBonus>(a =>
                {
                    a.value = Helpers.CreateContextValue(AbilityRankType.Default);
                    a.descriptor = ModifierDescriptor.UntypedStackable;
                }),
                Helpers.CreateContextRankConfig(
                    ContextRankBaseValueType.ClassLevel,
                    ContextRankProgression.DivStep,
                    AbilityRankType.Default,
                    1,
                    null,
                    0,
                    3,
                    exceptClasses: false,
                    StatType.Unknown,
                    null,
                    getStalkerArray()
                ),
                Common.createAddInitiatorAttackWithWeaponTrigger(
                    Helpers.CreateActionList(Helpers.Create<ContextActionRemoveSelf>()),
                    only_hit: false,
                    on_initiator: true,
                    wait_for_attack_to_resolve: true
                )
            );
            var apply_effect = Common.createContextActionApplyBuff(
                effect_buff,
                Helpers.CreateContextDuration(1)
            );

            var buff = Helpers.CreateBuff(
                "StalkerCounterstrikeBuff",
                "Martial Maneuver (Counterstrike)",
                "At 1st level, when the stalker is damaged by a melee attack, they can immediately make a single attack at their highest base attack bonus against the creature that hit them, provided that they threaten that creature. If the attack hits, they gain a bonus on the damage roll equal to 1/3 their stalker level (rounded down, minimum +1). This attack counts as an attack of opportunity.",
                "",
                Helpers.GetIcon("36c8971e91f1745418cc3ffdfac17b74"), //blade barrier
                null,
                Helpers.Create<AooMechanics.ApplyActionToCasterAndMakeAttackOfOpportunityOnAttack>(
                    a =>
                    {
                        a.consume_swift_action = true;
                        a.required_resource = resource;
                        a.resource_amount = 1;
                        a.actions = Helpers.CreateActionList(apply_effect);
                    }
                )
            );

            var toggle = Common.buffToToggle(
                buff,
                CommandType.Free,
                true,
                resource.CreateAddAbilityResource(),
                resource.CreateActivatableResourceLogic(
                    ActivatableAbilityResourceLogic.ResourceSpendType.Never
                )
            );

            counterstrike = Common.ActivatableAbilityToFeature(toggle, false);
            martial_strikes.AllFeatures = martial_strikes.AllFeatures.AddToArray(counterstrike);
            martial_strikes.Features = martial_strikes.Features.AddToArray(counterstrike);
        }

        private static void createBladeDash(BlueprintAbilityResource resource)
        {
            var blade_dash_buff = Helpers.CreateBuff(
                "StalkerBladeDashBuff",
                "Martial Maneuver (Blade Dash)",
                "The stalker surges forward with relentless momentum, striking multiple times before his foe can recover. When the stalker charges, they can make a full attack at the end of the charge instead of a single attack.",
                "",
                Helpers.GetIcon("0087fc2d64b6095478bc7b8d7d512caf"),
                null,
                Helpers.CreateAddMechanics(
                    Kingmaker.UnitLogic.FactLogic.AddMechanicsFeature.MechanicsFeatureType.Pounce
                ),
                Helpers.Create<NewMechanics.AddInitiatorAttackWithWeaponTriggerOnCharge>(a =>
                    a.Action = Helpers.CreateActionList(
                        Helpers.Create<ResourceMechanics.ContextActionSpendResourceFromCaster>(c =>
                        {
                            c.resource = resource;
                            c.amount = 1;
                        })
                    )
                )
            );
            blade_dash_buff.SetBuffFlags(BuffFlags.RemoveOnRest);
            blade_dash_buff.AddComponent(
                Helpers.Create<NewMechanics.AddInitiatorAttackWithWeaponTriggerOnCharge>(a =>
                    a.Action = Helpers.CreateActionList(
                        Helpers.Create<ResourceMechanics.ContextActionSpendResourceFromCaster>(c =>
                        {
                            c.resource = resource;
                            c.amount = 1;
                        }),
                        Common.createContextActionRemoveBuffFromCaster(blade_dash_buff)
                    )
                )
            );

            blade_dash = Helpers.CreateFeature(
                "StalkerBladeDashFeature",
                blade_dash_buff.Name,
                blade_dash_buff.Description,
                "",
                blade_dash_buff.Icon,
                FeatureGroup.None,
                Helpers.CreateAddAbilityResource(resource)
            );

            var toggle = Helpers.CreateActivatableAbility(
                "StalkerBladeDashToggleAbility",
                blade_dash_buff.Name,
                blade_dash_buff.Description,
                "8da53dff7c37499b96b1e24a971929ef",
                Helpers.GetIcon("0087fc2d64b6095478bc7b8d7d512caf"),
                blade_dash_buff,
                AbilityActivationType.Immediately,
                CommandType.Swift,
                null,
                Helpers.CreateActivatableResourceLogic(
                    resource,
                    ActivatableAbilityResourceLogic.ResourceSpendType.Never
                ),
                Helpers.Create<ResourceMechanics.RestrictionHasEnoughResource>(r =>
                    r.resource = resource
                )
            );

            blade_dash.AddComponent(Helpers.CreateAddFact(toggle));

            martial_strikes.AllFeatures = martial_strikes.AllFeatures.AddToArray(blade_dash);
            martial_strikes.Features = martial_strikes.Features.AddToArray(blade_dash);
        }

        private static void createDualStrike(BlueprintAbility extra_attack)
        {
            BlueprintFeature[] allFeatures = martial_strikes.AllFeatures;
            martial_strikes.AllFeatures = new BlueprintFeature[0];
            BlueprintFeature[] array = allFeatures;

            foreach (BlueprintFeature blueprint in array)
            {
                BlueprintActivatableAbility blueprintActivatableAbility =
                    blueprint.GetComponent<AddFacts>().Facts[0] as BlueprintActivatableAbility;
                BlueprintBuff blueprintBuff = library.CopyAndAdd(
                    blueprintActivatableAbility.Buff,
                    "Stalker" + blueprintActivatableAbility.Buff.name,
                    Helpers.MergeIds(
                        blueprintActivatableAbility.AssetGuid,
                        "2046e0de342b4239b13070133d3c82df"
                    )
                );
                blueprintBuff.MaybeReplaceComponent(
                    delegate(CallOfTheWild.NewMechanics.DoubleDamageDiceOnAttack d)
                    {
                        d.WeaponType = null;
                    }
                );
                blueprintBuff.MaybeReplaceComponent(
                    delegate(AddInitiatorAttackWithWeaponTrigger a)
                    {
                        a.WeaponType = null;
                    }
                );
                blueprintBuff.MaybeReplaceComponent(
                    delegate(IgnoreDamageReductionOnAttack d)
                    {
                        d.WeaponType = null;
                    }
                );
                blueprintBuff.SetBuffFlags((BuffFlags)0);
                blueprintBuff.SetNameDescriptionIcon(blueprintActivatableAbility);
                ContextActionApplyBuff apply_buff = Common.createContextActionApplyBuff(
                    blueprintBuff,
                    Helpers.CreateContextDuration(1),
                    is_from_spell: false,
                    is_child: false,
                    is_permanent: false,
                    dispellable: false
                );
                BlueprintAbility blueprintAbility2 = library.CopyAndAdd(
                    extra_attack,
                    "Stalker" + blueprintActivatableAbility.name,
                    Helpers.MergeIds(
                        blueprintActivatableAbility.AssetGuid,
                        "d22f03b4969743e5a3277b712018ceea"
                    )
                );
                blueprintAbility2.ReplaceComponent(
                    delegate(AbilityEffectRunAction a)
                    {
                        a.Actions.Actions = a.Actions.Actions.AddToArray(apply_buff);
                    }
                );
                blueprintAbility2.SetNameDescriptionIcon(
                    extra_attack.Name + " (" + blueprintActivatableAbility.Name + ")",
                    blueprintAbility2.Description
                        + "\n"
                        + blueprintActivatableAbility.Name
                        + ": "
                        + blueprintActivatableAbility.Description,
                    blueprintActivatableAbility.Icon
                );
                BlueprintFeature feature = Common.AbilityToFeature(blueprintAbility2, hide: false);
                martial_strikes.AllFeatures = martial_strikes.AllFeatures.AddToArray(feature);
                martial_strikes.Features = martial_strikes.Features.AddToArray(feature);

                if (feature.Name.Contains("Sweep"))
                {
                    library
                        .Get<BlueprintFeature>("0f15c6f70d8fb2b49aa6cc24239cc5fa")
                        .AddComponents(
                            Helpers.Create(
                                delegate(FeatureReplacement f)
                                {
                                    f.replacement_feature = feature;
                                }
                            )
                        );
                }
            }
        }

        static void createShadowStrike(BlueprintAbilityResource resource)
        {
            var dimension_door_free = library.CopyAndAdd<BlueprintAbility>(
                "a9b8be9b87865744382f7c64e599aeb2",
                "StalkerShadowStrikeTeleportAbility",
                ""
            );
            dimension_door_free.ActionType = UnitCommand.CommandType.Free;
            dimension_door_free.CanTargetEnemies = true;
            dimension_door_free.CanTargetFriends = false;
            dimension_door_free.Type = AbilityType.Special;

            var buff = Helpers.CreateBuff(
                "StalkerShadowStrikeBuff",
                "",
                "",
                "",
                null,
                null,
                Helpers.CreateAddStatBonus(
                    StatType.AdditionalAttackBonus,
                    2,
                    ModifierDescriptor.Circumstance
                )
            );
            buff.SetBuffFlags(BuffFlags.HiddenInUi);

            var ability = Helpers.CreateAbility(
                "StalkerShadowStrikeAbility",
                "Shadow Strike",
                "The stalker instantly repositions to an enemy within medium range, and makes a single melee attack against it at their base attack bonus with a +2 circumstance bonus.",
                "",
                dimension_door_free.Icon,
                AbilityType.Supernatural,
                UnitCommand.CommandType.Standard,
                AbilityRange.Medium,
                "",
                "",
                Helpers.CreateRunActions(
                    Common.createContextActionOnContextCaster(
                        Common.createContextActionApplyBuff(
                            buff,
                            Helpers.CreateContextDuration(1),
                            dispellable: false
                        )
                    ),
                    Helpers.Create<ContextActionCastSpell>(c => c.Spell = dimension_door_free),
                    Common.createContextActionAttackWithAnimation(
                        Helpers.CreateActionList(
                            Common.createContextActionOnContextCaster(
                                Common.createContextActionRemoveBuff(buff)
                            )
                        ),
                        Helpers.CreateActionList(
                            Common.createContextActionOnContextCaster(
                                Common.createContextActionRemoveBuff(buff)
                            )
                        )
                    )
                ),
                resource.CreateAddAbilityResource(),
                resource.CreateResourceLogic()
            );

            ability.setMiscAbilityParametersSingleTargetRangedHarmful(false);
            shadow_strike = Common.AbilityToFeature(ability, hide: false);
        }

        private static void createEnhancePerception()
        {
            BlueprintBuff buff = Helpers.CreateBuff(
                "StalkerPerceptionBuff",
                "Acute Awareness",
                "The stalker can sharpen their awareness as a swift action, spending 1 point of animus to gain a +4 insight bonus on a single Perception check, allowing them to detect subtle details and hidden intentions.",
                "",
                Helpers.GetIcon("0bcbe9e450b0e7b428f08f66c53c5136"),
                null,
                StatType.SkillPerception.CreateAddStatBonus(4, ModifierDescriptor.Insight)
            );
            buff.SetBuffFlags(BuffFlags.RemoveOnRest);
            buff.AddComponents(
                Helpers.Create(
                    delegate(AddInitiatorSkillRollTrigger a)
                    {
                        a.Skill = StatType.SkillPerception;
                        a.Action = Helpers.CreateActionList(
                            Common.createContextActionRemoveBuff(buff)
                        );
                    }
                )
            );
            BlueprintAbility ability = Helpers.CreateAbility(
                "StalkerPerception",
                buff.Name,
                buff.Description,
                "",
                buff.Icon,
                AbilityType.Supernatural,
                UnitCommand.CommandType.Swift,
                AbilityRange.Personal,
                Helpers.hourPerLevelDuration,
                "",
                Helpers.CreateRunActions(
                    Common.createContextActionApplyBuff(
                        buff,
                        Helpers.CreateContextDuration(
                            AbilityRankType.Default.CreateContextValue(),
                            DurationRate.Hours
                        ),
                        is_from_spell: false,
                        is_child: false,
                        is_permanent: false,
                        dispellable: false
                    )
                ),
                animus_resource.CreateResourceLogic()
            );
            ability.setMiscAbilityParametersSelfOnly();
            enhance_perception = Common.AbilityToFeature(ability, hide: false);
        }

        private static void createSixthSense()
        {
            BlueprintBuff buff = Helpers.CreateBuff(
                "StalkerSavesBuff",
                "Sixth Sense",
                "As a swift action, the stalker can spend 1 point of animus to gain a +4 insight bonus on their next saving throw, heightening their awareness and resilience against threats.",
                "",
                Helpers.GetIcon("e84fc922ccf952943b5240293669b171"),
                null,
                StatType.SaveWill.CreateAddStatBonus(4, ModifierDescriptor.Insight),
                StatType.SaveFortitude.CreateAddStatBonus(4, ModifierDescriptor.Insight),
                StatType.SaveReflex.CreateAddStatBonus(4, ModifierDescriptor.Insight)
            );
            buff.SetBuffFlags(BuffFlags.RemoveOnRest);
            buff.AddComponents(
                Helpers.Create(
                    delegate(
                        Kingmaker.UnitLogic.Mechanics.Components.AddInitiatorSavingThrowTrigger a
                    )
                    {
                        a.Action = Helpers.CreateActionList(
                            Common.createContextActionRemoveBuff(buff)
                        );
                    }
                )
            );
            BlueprintAbility ability = Helpers.CreateAbility(
                "StalkerSaves",
                buff.Name,
                buff.Description,
                "",
                buff.Icon,
                AbilityType.Supernatural,
                UnitCommand.CommandType.Swift,
                AbilityRange.Personal,
                Helpers.hourPerLevelDuration,
                "",
                Helpers.CreateRunActions(
                    Common.createContextActionApplyBuff(
                        buff,
                        Helpers.CreateContextDuration(
                            AbilityRankType.Default.CreateContextValue(),
                            DurationRate.Hours
                        ),
                        is_from_spell: false,
                        is_child: false,
                        is_permanent: false,
                        dispellable: false
                    )
                ),
                animus_resource.CreateResourceLogic()
            );
            ability.setMiscAbilityParametersSelfOnly();
            sixth_sense = Common.AbilityToFeature(ability, hide: false);
        }

        private static void createStalkerArt()
        {
            stalker_art = library.CopyAndAdd<BlueprintFeatureSelection>(
                "c074a5d615200494b8f2a9c845799d93",
                "StalkerArtSelection",
                ""
            );
            stalker_art.SetNameDescriptionIcon(
                "Stalker Arts",
                "As a stalker gains experience, they learn a number of arts that aid them and confound their foes. Starting at 1st level, a stalker gains one art; they gain an additional art at 3rd level and new arts every four class levels attained after 3rd level. A stalker cannot select an individual art more than once.",
                null
            );
            BlueprintFeature evasion = library.CopyAndAdd<BlueprintFeature>(
                "576933720c440aa4d8d42b0c54b77e80",
                "StalkerEvasion",
                ""
            );

            createCriticalEdge();
            createKillersImplements();
            createDeadlyAmbush();
            createShadowStrike(animus_resource);
            addToStalkerArts(evasion);
            addToStalkerArts(critical_edge);
            addToStalkerArts(killers_implements);
            addToStalkerArts(deadly_ambush);
            addToStalkerArts(critical_training);
            addToStalkerArts(shadow_strike);
        }

        private static void addToStalkerArts(BlueprintFeature feature)
        {
            feature.Groups = feature.Groups.AddToArray(FeatureGroup.RogueTalent);
            stalker_art.AllFeatures = stalker_art.AllFeatures.AddToArray(feature);
        }

        private static void createCriticalEdge()
        {
            critical_edge = Helpers.CreateFeature(
                "StalkerCriticalEdge",
                "Critical Edge",
                "The stalker’s deadly efficiency in combat allows them to increase the critical threat range of any weapon they wield by 1. This bonus is applied after abilities such Improved Critical or the keen weapon property and cannot be doubled.",
                "",
                Helpers.GetIcon("f4201c85a991369408740c6888362e20"),
                FeatureGroup.Feat,
                Helpers.Create(
                    delegate(AttackTypeCriticalEdgeIncrease w)
                    {
                        w.Type = AttackTypeAttackBonus.WeaponRangeType.Melee;
                        w.bonus = 1;
                    }
                ),
                Helpers.Create(
                    delegate(AttackTypeCriticalEdgeIncrease w)
                    {
                        w.Type = AttackTypeAttackBonus.WeaponRangeType.Ranged;
                        w.bonus = 1;
                    }
                )
            );
        }

        private static void createKillersImplements()
        {
            WeaponCategory[] KillerWeaponCategories =
            {
                WeaponCategory.UnarmedStrike,
                WeaponCategory.Dagger,
                WeaponCategory.PunchingDagger,
                WeaponCategory.LightMace,
                WeaponCategory.Sickle,
                WeaponCategory.Club,
                WeaponCategory.Shortspear,
                WeaponCategory.Trident,
                WeaponCategory.LightHammer,
                WeaponCategory.Handaxe,
                WeaponCategory.Kukri,
                WeaponCategory.LightPick,
                WeaponCategory.Shortsword,
                WeaponCategory.Starknife,
                WeaponCategory.Battleaxe,
                WeaponCategory.Flail,
                WeaponCategory.Longsword,
                WeaponCategory.DuelingSword,
                WeaponCategory.HeavyPick,
                WeaponCategory.Rapier,
                WeaponCategory.Scimitar,
                WeaponCategory.Warhammer,
            };

            killers_implements = Helpers.CreateFeature(
                "StalkerKillersImplements",
                "Killer’s Implements",
                "The stalker chooses a melee weapon they are proficient with, and gains the benefits of the Weapon Finesse and Deadly Agility feats when using that weapon, even if that weapon could not normally be used with those feats.",
                "",
                Helpers.GetIcon("697d64669eb2c0543abb9c9b07998a38"),
                FeatureGroup.Feat
            );

            killers_implements.AddComponent(
                Helpers.Create<AttackStatReplacementForWeaponCategory>(c =>
                {
                    c.categories = KillerWeaponCategories;
                    c.ReplacementStat = StatType.Dexterity;
                })
            );

            foreach (var category in KillerWeaponCategories)
            {
                killers_implements.AddComponent(
                    Helpers.Create<DamageGraceForWeapon>(d =>
                    {
                        d.category = category;
                    })
                );
            }

            library
                .Get<BlueprintFeature>("90e54424d682d104ab36436bd527af09")
                .AddComponents(
                    Helpers.Create<FeatureReplacement>(f =>
                    {
                        f.replacement_feature = killers_implements;
                    })
                );
        }

        private static void createDeadlyAmbush()
        {
            BlueprintBuff buff = Helpers.CreateBuff(
                "StalkerDeadlyAmbushBuff",
                "",
                "",
                "",
                null,
                null,
                StatType.SneakAttack.CreateAddStatBonus(-1, ModifierDescriptor.UntypedStackable)
            );
            buff.SetBuffFlags(BuffFlags.HiddenInUi | deadly_strike_buff.GetBuffFlags());
            ActionList action = Helpers.CreateActionList(
                Common.createContextActionApplyBuffToCaster(
                    buff,
                    Helpers.CreateContextDuration(),
                    is_from_spell: false,
                    is_child: false,
                    is_permanent: true,
                    dispellable: false
                ),
                deadly_strike_cond
            );
            deadly_ambush = Helpers.CreateFeature(
                "StalkerDeadlyAmbush",
                "Deadly Ambush",
                "The stalker with this art can now apply their deadly strike to flat-footed targets and those denied their Dexterity bonus to AC, in addition to critical hits and when using their animus to read an opponent’s defenses.",
                "",
                Helpers.GetIcon("9f0187869dc23744292c0e5bb364464e"),
                FeatureGroup.Feat,
                StatType.SneakAttack.CreateAddStatBonus(1, ModifierDescriptor.UntypedStackable),
                Common.createAddInitiatorAttackRollTrigger2(
                    action,
                    only_hit: true,
                    critical_hit: false,
                    sneak_attack: true
                ),
                Common.createAddInitiatorAttackWithWeaponTrigger(
                    Helpers.CreateActionList(Common.createContextActionRemoveBuff(buff)),
                    only_hit: false,
                    critical_hit: false,
                    check_weapon_range_type: false,
                    reduce_hp_to_zero: false,
                    on_initiator: true,
                    AttackTypeAttackBonus.WeaponRangeType.Melee,
                    wait_for_attack_to_resolve: true
                ),
                Helpers.CreateContextRankConfig(
                    ContextRankBaseValueType.CustomProperty,
                    ContextRankProgression.AsIs,
                    AbilityRankType.Default,
                    customProperty: casting_stat_property,
                    min: 1,
                    max: null
                ),
                Helpers.Create(
                    delegate(RecalculateOnStatChange r)
                    {
                        r.Stat = StatType.Wisdom;
                    }
                )
            );
        }

        private static void createCriticalTraining()
        {
            critical_training = Helpers.CreateFeature(
                "StalkerCriticalTraining",
                "Critical Training",
                "The stalker’s deadly strike damage increases by an additional damage die, and they may treat their class level as their base attack bonus for the purposes of qualifying for critical feats.",
                "",
                Helpers.GetIcon("fc85925cd241dd0408ab2f4cb171b7e3"),
                FeatureGroup.Feat,
                Common.createReplace34BabWithClassLevel(stalker_class)
            );
        }
    }
}
