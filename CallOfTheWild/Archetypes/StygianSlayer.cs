using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.Root;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.Localization;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UI.Common;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Utility;
using RewiredConsts;
using static Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityResourceLogic;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace CallOfTheWild.Archetypes
{
    public class StygianSlayer
    {
        public static BlueprintArchetype archetype;
        public static BlueprintFeature invisibility;
        public static BlueprintFeature mist_form;
        public static BlueprintFeature proficiencies;
        public static BlueprintSpellbook spellbook;

        static LibraryScriptableObject library => Main.library;

        internal static void create()
        {
            var slayer_class = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>(
                "c75e0971973957d4dbad24bc7957e4fb"
            );

            archetype = Helpers.Create<BlueprintArchetype>(a =>
            {
                a.name = "StygianSlayerArchetype";
                a.LocalizedName = Helpers.CreateString($"{a.name}.Name", "Stygian Slayer");
                a.LocalizedDescription = Helpers.CreateString(
                    $"{a.name}.Description",
                    "A stygian slayer crawls out of the darkest shadows to strike fear into the hearts of civilized folk. He’s a merciless killer who can control a sliver of magic, allowing him to arrive unseen, commit murder, and depart without detection."
                );
            });
            Helpers.SetField(archetype, "m_ParentClass", slayer_class);
            library.AddAsset(archetype, "");

            var slayer_talent2 = library.Get<BlueprintFeatureSelection>(
                "04430ad24988baa4daa0bcd4f1c7d118"
            );
            var slayer_talent6 = library.Get<BlueprintFeatureSelection>(
                "43d1b15873e926848be2abf0ea3ad9a8"
            );
            var slayer_talent10 = library.Get<BlueprintFeatureSelection>(
                "913b9cf25c9536949b43a2651b7ffb66"
            );
            var slayer_proficiencies = library.Get<BlueprintFeature>(
                "41cd5ff7ad1bc5848906e050b06d02dc"
            );
            var proficiencies = library.CopyAndAdd<BlueprintFeature>(
                "33e2a7e4ad9daa54eaf808e1483bb43c",
                "StygianSlayerProficiencies",
                ""
            );
            proficiencies.AddComponents(
                library.Get<BlueprintFeature>("203992ef5b35c864390b4e4a1e200629").CreateAddFact()
            );
            proficiencies.SetNameDescription(
                "Stygian Slayer Proficiencies",
                "A stygian slayer is proficient with light armor, but not with medium armor, heavy armor, or any kind of shield (including tower shields)."
            );

            createInvisibility();
            createMistForm();

            archetype.RemoveFeatures = new LevelEntry[]
            {
                Helpers.LevelEntry(1, slayer_proficiencies),
                Helpers.LevelEntry(4, slayer_talent2),
                Helpers.LevelEntry(10, slayer_talent10),
            };

            archetype.AddFeatures = new LevelEntry[]
            {
                Helpers.LevelEntry(1, proficiencies),
                Helpers.LevelEntry(4, invisibility),
                Helpers.LevelEntry(10, mist_form),
            };

            slayer_class.Progression.UIDeterminatorsGroup[0] = proficiencies;
            slayer_class.Progression.UIGroups[0].Features.Add(invisibility);
            slayer_class.Progression.UIGroups[0].Features.Add(mist_form);
            slayer_class.Archetypes = slayer_class.Archetypes.AddToArray(archetype);
        }

        static void createInvisibility()
        {
            var slayer_array = new BlueprintCharacterClass[] { archetype.GetParentClass() };
            BlueprintAbilityResource resource = Helpers.CreateAbilityResource(
                "StygianSlayerInvisibilityResource",
                "",
                "",
                "",
                null
            );
            resource.SetIncreasedByLevelStartPlusDivStep(1, 4, 0, 4, 1, 0, 0f, slayer_array);

            var invisibility_buff = library.Get<BlueprintBuff>("525f980cb29bc2240b93e953974cb325");

            var apply_invisibility = Common.createContextActionApplyBuff(
                invisibility_buff,
                Helpers.CreateContextDuration(Helpers.CreateContextValue(AbilityRankType.Default)),
                dispellable: false
            );

            var ability = Helpers.CreateAbility(
                "StygianSlayerInvisibilityAbility",
                "Invisibility",
                "At 4th level, a stygian slayer can cast invisibility once per day, using his slayer level as his caster level. The slayer uses his Intelligence modifier for concentration checks when using this ability. The slayer can use this an additional time per day at 8th level and every 4 levels thereafter.",
                "",
                invisibility_buff.Icon,
                AbilityType.Spell,
                CommandType.Standard,
                AbilityRange.Personal,
                Helpers.roundsPerLevelDuration,
                "",
                Helpers.CreateRunActions(apply_invisibility),
                Helpers.CreateContextRankConfig(
                    ContextRankBaseValueType.ClassLevel,
                    classes: slayer_array,
                    progression: ContextRankProgression.AsIs
                ),
                resource.CreateResourceLogic()
            );
            ability.setMiscAbilityParametersSelfOnly();

            invisibility = Common.AbilityToFeature(ability, false);
            invisibility.AddComponent(Helpers.CreateAddAbilityResource(resource));
        }

        static void createMistForm()
        {
            var slayer_array = new BlueprintCharacterClass[] { archetype.GetParentClass() };
            var resource = Helpers.CreateAbilityResource(
                "StygianSlayerMistFormResource",
                "",
                "",
                "",
                null
            );
            resource.SetIncreasedByLevel(0, 1, slayer_array);

            var buff = library.CopyAndAdd<BlueprintBuff>(
                "e82c0ec9a87a8514ba34fad5926ef129",
                "StygianSlayerMistFormBuff",
                ""
            );
            buff.AddComponent(Common.createAddOutgoingGhost());
            buff.AddComponent(
                Helpers.Create<AddConditionImmunity>(a =>
                    a.Condition = UnitCondition.DifficultTerrain
                )
            );
            buff.SetNameDescription(
                "Shadowy Mist Form",
                "At 10th level, a stygian slayer can transform into an inky black cloud of mist that functions as incorporeal form. The slayer can use this ability for a number of minutes per day equal to his level. These minutes need not be consecutive, but must be used in 1-minute increments."
            );

            var ability = Helpers.CreateAbility(
                "StygianSlayerMistFormAbility",
                buff.Name,
                buff.Description,
                "",
                buff.Icon,
                AbilityType.Supernatural,
                CommandType.Swift,
                AbilityRange.Personal,
                Helpers.oneMinuteDuration,
                "",
                Helpers.CreateRunActions(
                    Common.createContextActionApplyBuff(
                        buff,
                        Helpers.CreateContextDuration(1, DurationRate.Minutes),
                        dispellable: false
                    )
                ),
                resource.CreateResourceLogic()
            );
            ability.setMiscAbilityParametersSelfOnly();

            mist_form = Common.AbilityToFeature(ability, false);
            mist_form.AddComponent(Helpers.CreateAddAbilityResource(resource));
        }
    }
}
