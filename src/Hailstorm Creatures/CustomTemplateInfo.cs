﻿using System.Collections.Generic;
using UnityEngine;
using MoreSlugcats;
using System;

namespace Hailstorm;

//----------------------------------------------------------------------------------
//----------------------------------------------------------------------------------

public static class CustomTemplateInfo
{
    public static bool IsColdCreature(CreatureTemplate.Type creatureType)
    {
        if (creatureType == HailstormCreatures.IcyBlue ||
            creatureType == HailstormCreatures.Freezer ||
            creatureType == HailstormCreatures.Chillipede)
        {
            return true;
        }
        return false;
    }
    public static bool IsFireCreature(Creature creature)
    {
        if (creature is Player player &&
            IncanInfo.IncanData.TryGetValue(player, out IncanInfo Incan) &&
            Incan.isIncan)
        {
            return true;
        }
        if (creature.Template.type == MoreSlugcatsEnums.CreatureTemplateType.FireBug)
        {
            return true;
        }
        return false;
    }

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    public static void ApplyWeatherResistances()
    {
        WeatherResistances.AddCreatureHailResistances();
        WeatherResistances.AddCreatureWindResistances();
    }

    public static class DamageResistances
    {
        public static readonly Creature.DamageType Cold = new("HailstormCold", true);
        public static readonly Creature.DamageType Heat = new("HailstormHeat", true);
        public static readonly Creature.DamageType Venom = new("HailstormVenom", true);

        // Damage resistances for Lizards are set in LizardBreeds.BreedTemplate_Type_CreatureTemplate_CreatureTemplate_CreatureTemplate_CreatureTemplate.
        // Long method name, I know.

        public static void AddNewDamageResistances(CreatureTemplate temp, CreatureTemplate.Type type)
        {
            if (HailstormDamageTypes.Cold.index >= temp.damageRestistances.GetLength(0)) return;

            temp.damageRestistances[HailstormDamageTypes.Cold.index, 0] = 1; // 0 is damage resistance
            temp.damageRestistances[HailstormDamageTypes.Cold.index, 1] = 1; // 1 is stun resistance
            temp.damageRestistances[HailstormDamageTypes.Heat.index, 0] = 1;
            temp.damageRestistances[HailstormDamageTypes.Heat.index, 1] = 1;
            temp.damageRestistances[HailstormDamageTypes.Venom.index, 0] = 1;
            temp.damageRestistances[HailstormDamageTypes.Venom.index, 1] = 1;

            AddCreatureColdResistances(temp, type);
            AddCreatureHeatResistances(temp, type);
            AddCreatureVenomResistances(temp, type);
        }

        public static void AddCreatureColdResistances(CreatureTemplate temp, CreatureTemplate.Type type)
        {
            // Bugs and plants are generally weak to the cold.

            // Base-game creatures
            if (type == CreatureTemplate.Type.PoleMimic)
            {
                temp.damageRestistances[Cold.index, 0] = 2 / 3f; // +50% damage
                temp.damageRestistances[Cold.index, 1] = 0.5f; // +100% stun
            }
            else if (type == CreatureTemplate.Type.TentaclePlant)
            {
                temp.damageRestistances[Cold.index, 0] = 2 / 3f; // +50% damage
                temp.damageRestistances[Cold.index, 1] = 0.5f; // +100% stun
            }
            else if (type == CreatureTemplate.Type.EggBug)
            {
                temp.damageRestistances[Cold.index, 0] = 0.75f; // +33% damage
                temp.damageRestistances[Cold.index, 1] = 0.75f; // +33% stun
            }
            else if (type == CreatureTemplate.Type.BigNeedleWorm) // Adult Noodlefly
            {
                temp.damageRestistances[Cold.index, 0] = 0.75f; // +33% damage
                temp.damageRestistances[Cold.index, 1] = 0.75f; // +33% stun
            }
            else if (type == CreatureTemplate.Type.SmallNeedleWorm) // Infant Noodlefly
            {
                temp.damageRestistances[Cold.index, 0] = 2 / 3f; // +50% damage
                temp.damageRestistances[Cold.index, 1] = 2 / 3f; // +50% stun
            }
            else if (type == CreatureTemplate.Type.DropBug)
            {
                temp.damageRestistances[Cold.index, 0] = 0.8f; // +25% damage
                temp.damageRestistances[Cold.index, 1] = 0.8f; // +25% stun
            }
            else if (type == CreatureTemplate.Type.SmallCentipede)
            {
                temp.damageRestistances[Cold.index, 0] = 0.6f; // +66% damage
                temp.damageRestistances[Cold.index, 1] = 0.5f; // +100% stun
                                                               // Stun is further increased by 1 second in Centipede.Violence.
            }
            else if (type == CreatureTemplate.Type.Centipede)
            {
                temp.damageRestistances[Cold.index, 0] = 2 / 3f; // +50% damage
                temp.damageRestistances[Cold.index, 1] = 4 / 7f; // +75% stun
                                                                 // Stun is further increased by 1 second in Centipede.Violence.
            }
            else if (type == CreatureTemplate.Type.Centiwing)
            {
                temp.damageRestistances[Cold.index, 0] = 0.75f; // +33% damage
                temp.damageRestistances[Cold.index, 1] = 2 / 3f;  // +50% stun
                                                                  // Stun is further increased by 1 second in Centipede.Violence.
            }
            else if (type == CreatureTemplate.Type.RedCentipede)
            {
                temp.damageRestistances[Cold.index, 0] = 0.8f;  // +25% damage
                temp.damageRestistances[Cold.index, 1] = 8 / 11f; // +37.5% stun
                                                                  // Stun is further increased by 1 second in Centipede.Violence.
            }
            else if (type == CreatureTemplate.Type.Leech) // Normal red leech
            {
                temp.damageRestistances[Cold.index, 0] = 0.8f; // +25% damage
                temp.damageRestistances[Cold.index, 1] = 0.8f; // +25% stun
            }
            else if (type == CreatureTemplate.Type.TubeWorm) // Grapple Worm
            {
                temp.damageRestistances[Cold.index, 0] = 1 / 3f; // +200% damage
                temp.damageRestistances[Cold.index, 1] = 1 / 3f; // +200% stun
            }
            else if (type == CreatureTemplate.Type.Spider) // The little baby Coalescipede spiders
            {
                temp.damageRestistances[Cold.index, 0] = 0.8f; // +25% damage
                temp.damageRestistances[Cold.index, 1] = 0.8f; // +25% stun
            }
            else if (type == CreatureTemplate.Type.SpitterSpider)
            {
                temp.damageRestistances[Cold.index, 0] = 0.83f; // +20% damage
                temp.damageRestistances[Cold.index, 1] = 0.83f; // +20% stun
            }
            else if (type == CreatureTemplate.Type.BrotherLongLegs)
            {
                temp.damageRestistances[Cold.index, 0] = 0.25f; // +300% damage
                temp.damageRestistances[Cold.index, 1] = 0.25f; // +300% stun
            }
            else if (type == CreatureTemplate.Type.DaddyLongLegs)
            {
                temp.damageRestistances[Cold.index, 0] = 0.25f; // +300% damage
                temp.damageRestistances[Cold.index, 1] = 0.25f; // +300% stun
            }
            //____________________
            // Downpour creatures
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.AquaCenti)
            {
                temp.damageRestistances[Cold.index, 0] = 1.5f; // -33% damage
                                                               // Stun is unchanged.
            }
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.BigJelly)
            {
                temp.damageRestistances[Cold.index, 0] = 2f; // -50% damage
                temp.damageRestistances[Cold.index, 1] = 2f; // -50% stun
            }
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.MotherSpider)
            {
                temp.damageRestistances[Cold.index, 0] = 1.25f; // -20% damage
                temp.damageRestistances[Cold.index, 1] = 1.25f; // -20% stun
            }
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.Yeek)
            {
                temp.damageRestistances[Cold.index, 0] = 2f;   // -50% damage
                temp.damageRestistances[Cold.index, 1] = 4 / 3f; // -25% stun
            }
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.StowawayBug)
            {
                temp.damageRestistances[Cold.index, 0] = 0.8f; // +25% damage
                temp.damageRestistances[Cold.index, 1] = 0.8f; // +25% stun
            }
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.JungleLeech)
            {
                temp.damageRestistances[Cold.index, 0] = 0.25f; // +300% damage
                temp.damageRestistances[Cold.index, 1] = 0.25f; // +300% stun
            }
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.FireBug)
            {
                temp.damageRestistances[Cold.index, 0] = 0.25f; // +300% damage
                temp.damageRestistances[Cold.index, 1] = 0.25f; // +300% stun
            }
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.HunterDaddy)
            {
                temp.damageRestistances[Cold.index, 0] = 0.25f; // +300% damage
                temp.damageRestistances[Cold.index, 1] = 0.25f; // +300% stun
            }
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.TerrorLongLegs)
            {
                temp.damageRestistances[Cold.index, 0] = 0.25f; // +300% damage
                temp.damageRestistances[Cold.index, 1] = 0.25f; // +300% stun
            }
            //____________________
            // Hailstorm creatures
            else if (type == HailstormCreatures.InfantAquapede)
            {
                temp.damageRestistances[Cold.index, 0] = 4 / 3f; // -25% damage
                temp.damageRestistances[Cold.index, 1] = 0.8f; // +25% stun
            }
            else if (type == HailstormCreatures.Raven)
            {
                temp.damageRestistances[Cold.index, 0] = 4 / 3f; // -25% damage
                temp.damageRestistances[Cold.index, 1] = 2f;   // -50% stun
            }
            else if (type == HailstormCreatures.PeachSpider)
            {
                temp.damageRestistances[Cold.index, 0] = 1.50f; // -33% damage
                temp.damageRestistances[Cold.index, 1] = 1.25f; // -20% stun
            }
            else if (type == HailstormCreatures.Cyanwing)
            {
                temp.damageRestistances[Cold.index, 0] = 0.8f;  // +25% damage
                temp.damageRestistances[Cold.index, 1] = 0.5f;  // +100% stun
                                                                // Stun is further increased by 1 second in Centipede.Violence.
            }
            else if (type == HailstormCreatures.GorditoGreenie)
            {
                temp.damageRestistances[Cold.index, 0] = 2f;  // -50% stun
                temp.damageRestistances[Cold.index, 1] = 5f;  // -80% stun
            }

            /*else if (type == HailstormEnums.BezanBud)
            {
                temp.damageRestistances[Cold.index, 0] = 2f;  // -50% damage
                temp.damageRestistances[Cold.index, 1] = 10f; // -90% stun
            }*/

            else if (type == HailstormCreatures.Chillipede)
            {
                temp.damageRestistances[Cold.index, 0] = 100f; // -99% damage
                temp.damageRestistances[Cold.index, 1] = 100f; // -99% stun
            }
            else if (type == HailstormCreatures.Luminescipede)
            {
                temp.damageRestistances[Cold.index, 0] = 4 / 3f; // -25% damage
                temp.damageRestistances[Cold.index, 1] = 0.8f; // +25% stun
            }

            /*else if (type == HailstormEnums.Strobelegs)
            {
                temp.damageRestistances[Cold.index, 0] = 2f; // -50% damage
                // Stun is unchanged
            }*/
        }
        public static void AddCreatureHeatResistances(CreatureTemplate temp, CreatureTemplate.Type type)
        {
            if (type == CreatureTemplate.Type.PoleMimic)
            {
                temp.damageRestistances[Heat.index, 0] = 2 / 3f; // +50% damage
                temp.damageRestistances[Heat.index, 1] = 0.5f; // +100% stun
            }
            else if (type == CreatureTemplate.Type.TentaclePlant)
            {
                temp.damageRestistances[Heat.index, 0] = 2 / 3f; // +50% damage
                temp.damageRestistances[Heat.index, 1] = 0.5f; // +100% stun
            }
            else if (type == CreatureTemplate.Type.EggBug)
            {
                temp.damageRestistances[Heat.index, 0] = 0.75f; // +33% damage
                temp.damageRestistances[Heat.index, 1] = 0.75f; // +33% stun
            }
            else if (type == CreatureTemplate.Type.BigNeedleWorm) // Adult Noodlefly
            {
                temp.damageRestistances[Heat.index, 0] = 0.75f; // +33% damage
                temp.damageRestistances[Heat.index, 1] = 0.75f; // +33% stun
            }
            else if (type == CreatureTemplate.Type.SmallNeedleWorm) // Infant Noodlefly
            {
                temp.damageRestistances[Heat.index, 0] = 2 / 3f; // +50% damage
                temp.damageRestistances[Heat.index, 1] = 2 / 3f; // +50% stun
            }
            else if (type == CreatureTemplate.Type.DropBug)
            {
                temp.damageRestistances[Heat.index, 0] = 0.8f; // +25% damage
                temp.damageRestistances[Heat.index, 1] = 0.8f; // +25% stun
            }
            else if (type == CreatureTemplate.Type.SmallCentipede)
            {
                temp.damageRestistances[Heat.index, 0] = 4/3f; // -33% damage
                temp.damageRestistances[Heat.index, 1] = 4/3f; // -33% stun
            }
            else if (type == CreatureTemplate.Type.Centipede)
            {
                temp.damageRestistances[Heat.index, 0] = 2f; // -50% damage
                temp.damageRestistances[Heat.index, 1] = 2f; // -50% stun
            }
            else if (type == CreatureTemplate.Type.Centiwing)
            {
                temp.damageRestistances[Heat.index, 0] = 2f; // -50% damage
                temp.damageRestistances[Heat.index, 1] = 2f; // -50% stun
            }
            else if (type == CreatureTemplate.Type.RedCentipede)
            {
                temp.damageRestistances[Heat.index, 0] = 3f; // -66% damage
                temp.damageRestistances[Heat.index, 1] = 3f; // -66% stun
            }
            else if (type == CreatureTemplate.Type.Snail)
            {
                temp.damageRestistances[Heat.index, 0] = 2f;  // -50% damage
                temp.damageRestistances[Heat.index, 1] = 10f; // -90% stun
            }
            else if (type == CreatureTemplate.Type.Leech) // Normal red leech
            {
                temp.damageRestistances[Heat.index, 0] = 2 / 3f; // +50% damage
                                                                 // Stun is unchanged.
            }
            else if (type == CreatureTemplate.Type.TubeWorm) // Grapple Worm
            {
                temp.damageRestistances[Heat.index, 0] = 1 / 3f; // +200% damage
                temp.damageRestistances[Heat.index, 1] = 1 / 3f; // +200% stun
            }
            else if (type == CreatureTemplate.Type.Spider) // The little baby Coalescipede spiders
            {
                temp.damageRestistances[Heat.index, 0] = 2 / 3f; // +50% damage
                                                                 // Stun is unchanged.
            }
            else if (type == CreatureTemplate.Type.BrotherLongLegs)
            {
                temp.damageRestistances[Heat.index, 0] = 0.25f; // +300% damage
                temp.damageRestistances[Heat.index, 1] = 0.25f; // +300% stun
            }
            else
            if (type == CreatureTemplate.Type.DaddyLongLegs)
            {
                temp.damageRestistances[Heat.index, 0] = 0.25f; // +300% damage
                temp.damageRestistances[Heat.index, 1] = 0.25f; // +300% stun
            }
            //____________________
            // Downpour creatures
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.AquaCenti)
            {
                temp.damageRestistances[Heat.index, 0] = 5f; // -80% damage
                temp.damageRestistances[Heat.index, 1] = 5f; // -80% stun
            }
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.BigJelly)
            {
                temp.damageRestistances[Heat.index, 0] = 2f; // -50% damage
                temp.damageRestistances[Heat.index, 1] = 2f; // -50% stun
            }
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.MotherSpider)
            {
                temp.damageRestistances[Heat.index, 0] = 0.8f; // +25% damage
                temp.damageRestistances[Heat.index, 1] = 0.8f; // +25% stun
            }
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.JungleLeech)
            {
                temp.damageRestistances[Heat.index, 0] = 5f; // -80% damage
                temp.damageRestistances[Heat.index, 1] = 2f; // -50% stun
            }
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.FireBug)
            {
                temp.damageRestistances[Heat.index, 0] = 10000f; // -99.99% damage
                temp.damageRestistances[Heat.index, 1] = 3f;     // -66.66% stun
            }
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.HunterDaddy)
            {
                temp.damageRestistances[Heat.index, 0] = 0.25f; // +300% damage
                temp.damageRestistances[Heat.index, 1] = 0.25f; // +300% stun
            }
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.TerrorLongLegs)
            {
                temp.damageRestistances[Heat.index, 0] = 0.25f; // +300% damage
                temp.damageRestistances[Heat.index, 1] = 0.25f; // +300% stun
            }
            //____________________
            // Hailstorm creatures
            else if (type == HailstormCreatures.InfantAquapede)
            {
                temp.damageRestistances[Heat.index, 0] = 4f; // -75% damage
                temp.damageRestistances[Heat.index, 1] = 4f; // -75% stun
            }
            else if (temp.TopAncestor().type == HailstormCreatures.SnowcuttleTemplate)
            {
                temp.damageRestistances[Heat.index, 0] = 1.5f; // -33% damage
                temp.damageRestistances[Heat.index, 1] = 0.5f; // +100% stun
            }
            else if (type == HailstormCreatures.PeachSpider)
            {
                temp.damageRestistances[Heat.index, 0] = 0.8f; // +25% damage
                temp.damageRestistances[Heat.index, 1] = 0.8f; // +25% stun
            }
            else if (type == HailstormCreatures.Cyanwing)
            {
                temp.damageRestistances[Heat.index, 0] = 4f; // -75% damage
                temp.damageRestistances[Heat.index, 1] = 4f; // -75% stun
            }

            /*else if (type == HailstormEnums.BezanBud)
            {
                // Damage is unaffected.
                temp.damageRestistances[Heat.index, 1] = 1/3f; // +200% stun
            }*/

            else if (type == HailstormCreatures.Chillipede)
            {
                temp.damageRestistances[Heat.index, 0] = 0.5f;  // +100% damage
                temp.damageRestistances[Heat.index, 1] = 0.25f; // +300% stun
            }
            else if (type == HailstormCreatures.Luminescipede)
            {
                temp.damageRestistances[Heat.index, 0] = 1.25f; // -20% damage
                temp.damageRestistances[Heat.index, 1] = 1.25f; // -20% stun
            }

            /*else if (type == HailstormEnums.Strobelegs)
            {
                temp.damageRestistances[Heat.index, 0] = 5/3f; // -40% damage
                temp.damageRestistances[Heat.index, 1] = 5/3f; // -40% stun
            }*/
        }
        public static void AddCreatureVenomResistances(CreatureTemplate temp, CreatureTemplate.Type type)
        {
            // Base-game creatures
            if (type == CreatureTemplate.Type.PoleMimic)
            {
                temp.damageRestistances[Venom.index, 0] = 10f; // -90% damage
                temp.damageRestistances[Venom.index, 1] = 10f; // -90% damage
            }
            else if (type == CreatureTemplate.Type.TentaclePlant) // Monster Kelp
            {
                temp.damageRestistances[Venom.index, 0] = 10f; // -90% damage
                temp.damageRestistances[Venom.index, 1] = 10f; // -90% damage
            }
            else if (type == CreatureTemplate.Type.BigEel) // Leviathan
            {
                temp.damageRestistances[Venom.index, 0] = 10000f; // -99.99% damage
                temp.damageRestistances[Venom.index, 1] = 10000f; // -99.99% stun
            }
            else if (type == CreatureTemplate.Type.BrotherLongLegs)
            {
                temp.damageRestistances[Venom.index, 0] = 1 / 3f; // +200% damage
                temp.damageRestistances[Venom.index, 1] = 2 / 3f; // +50% stun
            }
            else if (type == CreatureTemplate.Type.DaddyLongLegs)
            {
                temp.damageRestistances[Venom.index, 0] = 1 / 3f; // +200% damage
                temp.damageRestistances[Venom.index, 1] = 2 / 3f; // +50% stun
            }
            //____________________
            // Downpour creatures
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.MotherSpider)
            {
                temp.damageRestistances[Venom.index, 0] = 4 / 3f; // -25% damage
                temp.damageRestistances[Venom.index, 1] = 4 / 3f; // -25% stun
            }
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.StowawayBug)
            {
                temp.damageRestistances[Venom.index, 0] = 2.0f; // -50% damage
                                                                // Stun is unchanged
            }
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.MirosVulture)
            {
                // Damage is unchanged
                temp.damageRestistances[Venom.index, 1] = 1.25f; // -20% stun
            }
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.HunterDaddy)
            {
                temp.damageRestistances[Venom.index, 0] = 1 / 3f; // +200% damage
                temp.damageRestistances[Venom.index, 1] = 2 / 3f; // +50% stun

            }
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.TerrorLongLegs)
            {
                temp.damageRestistances[Venom.index, 0] = 1 / 3f; // +200% damage
                temp.damageRestistances[Venom.index, 1] = 2 / 3f; // +50% stun

            }
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.FireBug)
            {
                temp.damageRestistances[Venom.index, 0] = 0.8f; // +25% damage
                temp.damageRestistances[Venom.index, 1] = 0.6f; // +66% stun
            }
            //_____________________
            // Hailstorm creatures
            else if (temp.TopAncestor().type == HailstormCreatures.SnowcuttleTemplate)
            {
                if (type == HailstormCreatures.SnowcuttleFemale)
                {
                    temp.damageRestistances[Venom.index, 0] = 4f;   // -75% damage
                    temp.damageRestistances[Venom.index, 1] = 100f; // -99% stun
                }
                else if (type == HailstormCreatures.SnowcuttleMale)
                {
                    temp.damageRestistances[Venom.index, 0] = 4 / 3f; // -25% damage
                    temp.damageRestistances[Venom.index, 1] = 1.5f; // -33% stun
                }
            }
            else if (type == HailstormCreatures.IcyBlue)
            {
                temp.damageRestistances[Venom.index, 0] = 1.25f; // -20% damage
                temp.damageRestistances[Venom.index, 1] = 1.25f; // -20% stun
            }
            else if (type == HailstormCreatures.Freezer)
            {
                temp.damageRestistances[Venom.index, 0] = 2f; // -50% damage
                temp.damageRestistances[Venom.index, 1] = 2f; // -50% stun
            }
            else if (type == HailstormCreatures.GorditoGreenie)
            {
                temp.damageRestistances[Venom.index, 0] = 2f;   // -50% damage
                temp.damageRestistances[Venom.index, 1] = 0.4f; // +150% stun
            }
            else if (type == HailstormCreatures.PeachSpider)
            {
                temp.damageRestistances[Venom.index, 0] = 100f; // -99% damage
                temp.damageRestistances[Venom.index, 1] = 100f; // -99% stun
            }
            else if (type == HailstormCreatures.Cyanwing)
            {
                temp.damageRestistances[Venom.index, 0] = 100f; // -99% damage
                temp.damageRestistances[Venom.index, 1] = 100f; // -99% stun
            }

            /*else if (type == HailstormEnums.BezanBud)
            {
                temp.damageRestistances[Venom.index, 0] = 2/3f; // +50% damage
                temp.damageRestistances[Venom.index, 1] = 2/3f; // +50% stun
            }*/

            else if (type == HailstormCreatures.Chillipede)
            {
                temp.damageRestistances[Venom.index, 0] = 2f;   // -50% damage
                temp.damageRestistances[Venom.index, 1] = 0.5f; // +100% stun
            }

        }

        public static void AddLizardDamageResistances(ref CreatureTemplate temp, CreatureTemplate.Type type)
        {
            // Base-game lizards
            if (type == CreatureTemplate.Type.BlueLizard)
            {
                temp.damageRestistances[Venom.index, 0] = 0.8f; // +25% damage
                temp.damageRestistances[Venom.index, 1] = 0.5f; // +100% stun
            }
            else if (type == CreatureTemplate.Type.Salamander)
            {
                temp.damageRestistances[Cold.index, 0] = 4 / 3f; // -25% damage
                temp.damageRestistances[Cold.index, 1] = 4 / 3f; // -25% stun
                temp.damageRestistances[Heat.index, 0] = 4 / 3f; // -25% damage
                temp.damageRestistances[Heat.index, 1] = 4 / 3f; // -25% stun
                temp.damageRestistances[Venom.index, 0] = 4f; // -75% damage
                temp.damageRestistances[Venom.index, 1] = 4f; // -75% stun
            }
            else if (type == CreatureTemplate.Type.RedLizard)
            {
                temp.damageRestistances[Cold.index, 0] = 4 / 3f; // -25% damage
                temp.damageRestistances[Cold.index, 1] = 4 / 3f; // -25% stun
                temp.damageRestistances[Heat.index, 0] = 0.8f; // +25% damage
                temp.damageRestistances[Heat.index, 1] = 0.8f; // +25% stun
            }
            //____________________
            // Downpour lizards
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.EelLizard)
            {
                temp.damageRestistances[Cold.index, 0] = 2 / 3f; // +50% damage
                temp.damageRestistances[Cold.index, 1] = 2 / 3f; // +50% stun
                temp.damageRestistances[Heat.index, 0] = 1.5f; // -33% damage
                temp.damageRestistances[Heat.index, 1] = 1.5f; // -33% stun
            }
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.SpitLizard)
            {
                temp.damageRestistances[Cold.index, 0] = 4 / 3f; // -25% damage
                temp.damageRestistances[Cold.index, 1] = 4 / 3f; // -25% stun
                temp.damageRestistances[Heat.index, 0] = 0.8f; // +25% damage
                temp.damageRestistances[Heat.index, 1] = 0.8f; // +25% stun
            }
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.ZoopLizard)
            {
                temp.damageRestistances[Heat.index, 0] = 2 / 3f; // +50% damage
                temp.damageRestistances[Heat.index, 1] = 2 / 3f; // +50% stun
                temp.damageRestistances[Venom.index, 0] = 0.8f; // +25% damage
                temp.damageRestistances[Venom.index, 1] = 0.8f; // +25% stun
            }
            //_____________________
            // Hailstorm lizards
            else if (type == HailstormCreatures.IcyBlue)
            {
                // Bite damage is unchanged
                temp.damageRestistances[Creature.DamageType.Bite.index, 1] = 1.5f; // -33% stun
                temp.damageRestistances[Creature.DamageType.Electric.index, 0] = 3f;   // -66% damage
                temp.damageRestistances[Creature.DamageType.Electric.index, 1] = 4/3f; // -25% stun
                temp.damageRestistances[Cold.index, 0] = 10f; // -90% damage
                temp.damageRestistances[Cold.index, 1] = 10f; // -90% stun
                temp.damageRestistances[Heat.index, 0] = 2/3f; // +50% damage
                temp.damageRestistances[Heat.index, 1] = 2/3f; // +50% stun
                temp.damageRestistances[Venom.index, 0] = 1.5f; // -33% damage
                temp.damageRestistances[Venom.index, 1] = 1.5f; // -33% stun
            }
            else if (type == HailstormCreatures.Freezer)
            {
                temp.damageRestistances[Creature.DamageType.Bite.index, 0] = 6f; // -83% damage
                temp.damageRestistances[Creature.DamageType.Bite.index, 1] = 2f; // -50% stun
                temp.damageRestistances[Creature.DamageType.Explosion.index, 0] = 1.5f; // -33% damage
                temp.damageRestistances[Creature.DamageType.Explosion.index, 1] = 2f; // -50% stun
                temp.damageRestistances[Creature.DamageType.Electric.index, 0] = 6f; // -83% damage
                temp.damageRestistances[Creature.DamageType.Electric.index, 1] = 2f; // -50% stun
                temp.damageRestistances[Cold.index, 0] = 100f; // -99% damage
                temp.damageRestistances[Cold.index, 1] = 100f; // -99% stun
                temp.damageRestistances[Heat.index, 0] = 0.5f; // +100% damage
                temp.damageRestistances[Heat.index, 1] = 0.5f; // +100% stun
                temp.damageRestistances[Venom.index, 0] = 3f; // -66% damage
                temp.damageRestistances[Venom.index, 1] = 3f; // -66% stun
            }
            else if (type == HailstormCreatures.GorditoGreenie)
            {
                temp.damageRestistances[Creature.DamageType.Bite.index, 0] = 100f; // -99% damage
                temp.damageRestistances[Creature.DamageType.Bite.index, 1] = 100f; // -99% damage
                temp.damageRestistances[Creature.DamageType.Electric.index, 0] = 2/3f; // +50% damage
                temp.damageRestistances[Creature.DamageType.Electric.index, 1] = 2/3f; // +50% damage
            }
        }

        public static float SlugcatDamageMultipliers(Player self, Creature.DamageType dmgType)
        {
            if (self is null)
            {
                return 1f;
            }

            if (IncanInfo.IncanData.TryGetValue(self, out IncanInfo Incan) && Incan.isIncan)
            {
                if (dmgType == HailstormDamageTypes.Cold)
                {
                    return 2f;
                }
                if (dmgType == HailstormDamageTypes.Heat)
                {
                    return 0.25f;
                }
            }
            else if (self.SlugCatClass == SlugcatStats.Name.Red)
            {
                if (dmgType == HailstormDamageTypes.Cold)
                {
                    return 1.1f;
                }
                if (dmgType == HailstormDamageTypes.Heat)
                {
                    return 1.1f;
                }
                if (dmgType == HailstormDamageTypes.Venom)
                {
                    return 0.5f;
                }
            }
            else if (self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Gourmand)
            {
                if (dmgType == HailstormDamageTypes.Cold)
                {
                    return 0.75f;
                }
                if (dmgType == HailstormDamageTypes.Heat)
                {
                    return 1.1f;
                }
                if (dmgType == HailstormDamageTypes.Venom)
                {
                    return 0.75f;
                }
            }
            else if (self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Artificer)
            {
                if (dmgType == HailstormDamageTypes.Cold)
                {
                    return 0.8f;
                }
                if (dmgType == HailstormDamageTypes.Heat)
                {
                    return 1.2f;
                }
                if (dmgType == HailstormDamageTypes.Venom)
                {
                    return 1.5f;
                }
            }
            else if (self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Rivulet)
            {
                if (dmgType == HailstormDamageTypes.Cold)
                {
                    return 1.2f;
                }
                if (dmgType == HailstormDamageTypes.Heat)
                {
                    return 0.8f;
                }
                if (dmgType == HailstormDamageTypes.Venom)
                {
                    return 2/3f;
                }
            }
            else if (self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Spear)
            {
                if (dmgType == HailstormDamageTypes.Cold)
                {
                    return 1.2f;
                }
                if (dmgType == HailstormDamageTypes.Heat)
                {
                    return 0.8f;
                }
            }
            else if (self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Saint)
            {
                if (dmgType == HailstormDamageTypes.Venom)
                {
                    return 1.5f;
                }
            }

            return 1f;
        }
        public static float IncanStoryResistances(CreatureTemplate temp, Creature.DamageType dmgType, bool TrueforstunFalsefordamage)
        {
            CreatureTemplate.Type type = temp.type;
            if (temp.TopAncestor().type == CreatureTemplate.Type.LizardTemplate)
            {
                if (type == CreatureTemplate.Type.Salamander)
                {
                    if (dmgType == Creature.DamageType.Electric)
                    {
                        return 2 / 3f;
                    }
                    if (dmgType == Creature.DamageType.Water ||
                        dmgType == Creature.DamageType.Blunt)
                    {
                        return 2f;
                    }
                }
                else if (type == CreatureTemplate.Type.YellowLizard)
                {
                    if (dmgType == Creature.DamageType.Electric)
                    {
                        return 2f;
                    }
                }
                else if (type == CreatureTemplate.Type.WhiteLizard)
                {
                    if (dmgType == Creature.DamageType.Electric)
                    {
                        return TrueforstunFalsefordamage ? 2 / 3f : 1f;
                    }
                }
                else if (type == MoreSlugcatsEnums.CreatureTemplateType.SpitLizard)
                {
                    if (dmgType == Creature.DamageType.Electric)
                    {
                        return 2f;
                    }
                }
                else if (type == MoreSlugcatsEnums.CreatureTemplateType.EelLizard)
                {
                    if (dmgType == Creature.DamageType.Water ||
                        dmgType == Creature.DamageType.Electric)
                    {
                        return 5f;
                    }
                }
            }
            else if (type == MoreSlugcatsEnums.CreatureTemplateType.AquaCenti)
            {
                if (dmgType == Creature.DamageType.Water)
                {
                    return 2f;
                }
            }
            return 1f;
        }

    }

    public static class WeatherResistances
    {
        public static Dictionary<CreatureTemplate.Type, float> Hail = new();
        public static Dictionary<CreatureTemplate.Type, float> Fog = new();
        public static Dictionary<CreatureTemplate.Type, float> Wind = new();

        public static void AddCreatureHailResistances()
        {
            // For each listed creature, Hail damage will be multiplied by the given number.
            // Entries are sorted from lowest resistance to highest.
            Hail.Add(CreatureTemplate.Type.BigSpider, 0.9f);
            Hail.Add(CreatureTemplate.Type.SpitterSpider, 0.8f);
            Hail.Add(CreatureTemplate.Type.Vulture, 0.5f);
            Hail.Add(CreatureTemplate.Type.KingVulture, 0.25f);
            Hail.Add(CreatureTemplate.Type.Salamander, 0);
            Hail.Add(CreatureTemplate.Type.Deer, 0);
            Hail.Add(CreatureTemplate.Type.BigEel, 0);

            Hail.Add(MoreSlugcatsEnums.CreatureTemplateType.MirosVulture, 0.8f);
            Hail.Add(MoreSlugcatsEnums.CreatureTemplateType.EelLizard, 0.35f);
            Hail.Add(MoreSlugcatsEnums.CreatureTemplateType.MotherSpider, 0.35f);
            Hail.Add(MoreSlugcatsEnums.CreatureTemplateType.StowawayBug, 0.25f);
            Hail.Add(MoreSlugcatsEnums.CreatureTemplateType.BigJelly, 0);

            // HailResistantCreatures.Add(HailstormEnums.Raven, 0.75f);
            Hail.Add(HailstormCreatures.GorditoGreenie, 0);

        }

        public static void AddCreatureWindResistances()
        {
            // For each listed creature, Blizzard push force will be multiplied by the given number.
            // Entries are sorted from lowest resistance to highest.
            Wind.Add(CreatureTemplate.Type.CicadaA, 0.9f); // White Squidcada
            Wind.Add(CreatureTemplate.Type.CicadaB, 0.9f); // Black Squidcada
            Wind.Add(CreatureTemplate.Type.Centiwing, 0.9f);
            Wind.Add(CreatureTemplate.Type.Vulture, 0.85f);
            Wind.Add(CreatureTemplate.Type.YellowLizard, 0.85f);
            Wind.Add(CreatureTemplate.Type.CyanLizard, 0.85f);
            Wind.Add(CreatureTemplate.Type.SpitterSpider, 0.85f);
            Wind.Add(CreatureTemplate.Type.DropBug, 0.75f);
            Wind.Add(CreatureTemplate.Type.RedCentipede, 0.75f);
            Wind.Add(CreatureTemplate.Type.BlackLizard, 0.7f);
            Wind.Add(CreatureTemplate.Type.RedLizard, 0.65f);
            Wind.Add(CreatureTemplate.Type.GreenLizard, 0.6f);
            Wind.Add(CreatureTemplate.Type.MirosBird, 0.3f);
            Wind.Add(CreatureTemplate.Type.Deer, 0.3f);
            Wind.Add(CreatureTemplate.Type.WhiteLizard, 0);
            Wind.Add(CreatureTemplate.Type.BigEel, 0);
            Wind.Add(CreatureTemplate.Type.KingVulture, 0);

            Wind.Add(MoreSlugcatsEnums.CreatureTemplateType.MirosVulture, 0.75f);
            Wind.Add(MoreSlugcatsEnums.CreatureTemplateType.MotherSpider, 0.5f);
            Wind.Add(MoreSlugcatsEnums.CreatureTemplateType.SpitLizard, 0.5f);
            Wind.Add(MoreSlugcatsEnums.CreatureTemplateType.TrainLizard, 0.25f);
            Wind.Add(MoreSlugcatsEnums.CreatureTemplateType.BigJelly, 0);
            Wind.Add(MoreSlugcatsEnums.CreatureTemplateType.StowawayBug, 0);

            Wind.Add(HailstormCreatures.Cyanwing, 0.75f);
            Wind.Add(HailstormCreatures.Chillipede, 0.5f);
            Wind.Add(HailstormCreatures.Freezer, 0.5f);
            // Wind.Add(HailstormEnums.BezanBud, 0.5f);
            // Wind.Add(HailstormEnums.Strobelegs, 0.25f);
            Wind.Add(HailstormCreatures.GorditoGreenie, 0);
        }

    }

}

//--------------------------------------------------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------------------------------------