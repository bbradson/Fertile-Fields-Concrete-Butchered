using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Reflection;
using UnityEngine;
using Verse;

namespace RFFConcrete_Code {

    [StaticConstructorOnStartup]
	internal static class RFFConcrete_Initializer {
		static RFFConcrete_Initializer() {
			var harmony = new Harmony("net.rainbeau.rimworld.mod.rffconcrete");
			harmony.PatchAll();
            //harmony.PatchAll( Assembly.GetExecutingAssembly() );
        }
    }

	[HarmonyPatch(typeof(GenLeaving), "DoLeavingsFor", new Type[] { typeof(TerrainDef), typeof(IntVec3), typeof(Map) })]
	public static class GenLeaving_DoLeavingsFor_Floors {
		public static bool Prefix(TerrainDef terrain, IntVec3 cell, Map map) {
            if (terrain == TerrainDef.Named("Concrete")) return false;
			if (terrain == TerrainDef.Named("PavedTile"))
            { 
                Thing leaving = ThingMaker.MakeThing(ThingDef.Named("CrushedRocks"), null);
				GenPlace.TryPlaceThing(leaving, cell, map, ThingPlaceMode.Near, null);
				return false;
			}
			return true;
		}
	}

	/*[HarmonyPatch(typeof(GenLeaving), "DoLeavingsFor", new Type[] { typeof(Thing), typeof(Map), typeof(DestroyMode), typeof(CellRect), typeof(Predicate<IntVec3>), typeof(List<Thing>) })]
	public static class GenLeaving_DoLeavingsFor_Walls {
		public static bool Prefix(Thing diedThing, Map map, DestroyMode mode, CellRect leavingsRect, Predicate<IntVec3> nearPlaceValidator = null, List<Thing> listOfLeavingsOut = null) {
            switch (diedThing.def.defName)
            {
                case "RFFConcreteDoor":
                case "RFFConcreteAutodoor":
                case "RFFPlasticreteDoor":
                case "RFFPlasticreteAutodoor":
                case "RFFConcreteWall":
                case "RFFReinforcedConcreteWall":
                case "RFFPlasticreteWall":
                case "RFFConcreteEmbrasure":
                case "RFFPlasticreteEmbrasure":
                case "RBB_DeepBridge":
                case "RBB_DeepBridge_Stone":
                    {
                        List<IntVec3> list = leavingsRect.Cells.InRandomOrder<IntVec3>(null).ToList<IntVec3>();
                        Thing leaving = ThingMaker.MakeThing(ThingDef.Named("CrushedRocks"), null);
                        GenPlace.TryPlaceThing(leaving, list[0], map, ThingPlaceMode.Near, null);
                        if (diedThing.def.defName == "RFFConcreteDoor")
                        {
                            Thing leaving2 = ThingMaker.MakeThing(ThingDefOf.Steel, null);
                            leaving2.stackCount = Rand.RangeInclusive(2, 4);
                            GenPlace.TryPlaceThing(leaving2, list[0], map, ThingPlaceMode.Near, null);
                        }
                        if (diedThing.def.defName == "RFFConcreteAutodoor")
                        {
                            Thing leaving2 = ThingMaker.MakeThing(ThingDefOf.Steel, null);
                            leaving2.stackCount = Rand.RangeInclusive(2, 4);
                            GenPlace.TryPlaceThing(leaving2, list[0], map, ThingPlaceMode.Near, null);
                            Thing leaving3 = ThingMaker.MakeThing(ThingDefOf.ChunkSlagSteel, null);
                            leaving3.stackCount = Rand.RangeInclusive(2, 3);
                            GenPlace.TryPlaceThing(leaving3, list[0], map, ThingPlaceMode.Near, null);
                            Thing leaving4 = ThingMaker.MakeThing(ThingDefOf.ComponentIndustrial, null);
                            if (Rand.Value < 0.75f)
                            {
                                GenPlace.TryPlaceThing(leaving4, list[0], map, ThingPlaceMode.Near, null);
                            }
                        }
                        if (diedThing.def.defName == "RFFPlasticreteDoor")
                        {
                            Thing leaving2 = ThingMaker.MakeThing(ThingDefOf.Steel, null);
                            leaving2.stackCount = Rand.RangeInclusive(3, 5);
                            GenPlace.TryPlaceThing(leaving2, list[0], map, ThingPlaceMode.Near, null);
                            Thing leaving3 = ThingMaker.MakeThing(ThingDefOf.Plasteel, null);
                            leaving3.stackCount = Rand.RangeInclusive(2, 4);
                            GenPlace.TryPlaceThing(leaving3, list[0], map, ThingPlaceMode.Near, null);
                        }
                        if (diedThing.def.defName == "RFFPlasticreteAutodoor")
                        {
                            Thing leaving2 = ThingMaker.MakeThing(ThingDefOf.Steel, null);
                            leaving2.stackCount = Rand.RangeInclusive(2, 4);
                            GenPlace.TryPlaceThing(leaving2, list[0], map, ThingPlaceMode.Near, null);
                            Thing leaving3 = ThingMaker.MakeThing(ThingDefOf.ChunkSlagSteel, null);
                            leaving3.stackCount = Rand.RangeInclusive(2, 3);
                            GenPlace.TryPlaceThing(leaving3, list[0], map, ThingPlaceMode.Near, null);
                            Thing leaving4 = ThingMaker.MakeThing(ThingDefOf.Plasteel, null);
                            leaving4.stackCount = Rand.RangeInclusive(2, 4);
                            GenPlace.TryPlaceThing(leaving4, list[0], map, ThingPlaceMode.Near, null);
                            Thing leaving5 = ThingMaker.MakeThing(ThingDefOf.ComponentIndustrial, null);
                            if (Rand.Value < 0.75f)
                            {
                                GenPlace.TryPlaceThing(leaving5, list[0], map, ThingPlaceMode.Near, null);
                            }
                        }
                        if (diedThing.def.defName == "RFFReinforcedConcreteWall" || diedThing.def.defName == "RFFConcreteEmbrasure")
                        {
                            Thing leaving2 = ThingMaker.MakeThing(ThingDefOf.Steel, null);
                            if (Rand.Value < 0.75f)
                            {
                                GenPlace.TryPlaceThing(leaving2, list[0], map, ThingPlaceMode.Near, null);
                            }
                        }
                        if (diedThing.def.defName == "RFFPlasticreteWall" || diedThing.def.defName == "RFFPlasticreteEmbrasure")
                        {
                            Thing leaving2 = ThingMaker.MakeThing(ThingDefOf.Steel, null);
                            leaving2.stackCount = Rand.RangeInclusive(1, 3);
                            GenPlace.TryPlaceThing(leaving2, list[0], map, ThingPlaceMode.Near, null);
                            Thing leaving3 = ThingMaker.MakeThing(ThingDefOf.Plasteel, null);
                            leaving3.stackCount = Rand.RangeInclusive(1, 3);
                            GenPlace.TryPlaceThing(leaving3, list[0], map, ThingPlaceMode.Near, null);
                        }
                        if (diedThing.def.defName.Contains("DeepBridge"))
                        {
                            List<ThingDefCountClass> thingCountClasses = diedThing.CostListAdjusted();
                            for (int l = 0; l < thingCountClasses.Count; l++)
                            {
                                ThingDefCountClass thingCountClass = thingCountClasses[l];
                                if (thingCountClass.thingDef.defName != "RFFConcrete")
                                {
                                    int buildingResourcesLeaveCalculator = GetBuildingResourcesLeaveCalculator(diedThing, mode)(thingCountClass.count);
                                    if (buildingResourcesLeaveCalculator > 0)
                                    {
                                        Thing thing1 = ThingMaker.MakeThing(thingCountClass.thingDef, null);
                                        thing1.stackCount = buildingResourcesLeaveCalculator;
                                        GenPlace.TryPlaceThing(thing1, list[0], map, ThingPlaceMode.Near, null);
                                    }
                                }
                            }
                        }
                        return false;
                    }
            }
            return true;
		}
		private static Func<int, int> GetBuildingResourcesLeaveCalculator(Thing destroyedThing, DestroyMode mode) {
			if (!GenLeaving.CanBuildingLeaveResources(destroyedThing, mode)) {
				return (int count) => 0;
			}
			if (mode == DestroyMode.Deconstruct && typeof(Frame).IsAssignableFrom(destroyedThing.GetType())) {
				mode = DestroyMode.Cancel;
			}
			switch (mode) {
				case DestroyMode.Vanish: {
					return (int count) => 0;
				}
				case DestroyMode.WillReplace: {
					return (int count) => 0;
				}
				case DestroyMode.KillFinalize: {
					return (int count) => GenMath.RoundRandom((float)count * 0.5f);
				}
				case DestroyMode.Deconstruct: {
					return (int count) => GenMath.RoundRandom(Math.Min((float)count * destroyedThing.def.resourcesFractionWhenDeconstructed, (float)(count - 1)));
				}
				case DestroyMode.FailConstruction: {
					return (int count) => GenMath.RoundRandom((float)count * 0.5f);
				}
				case DestroyMode.Cancel: {
					return (int count) => GenMath.RoundRandom((float)count * 1f);
				}
				case DestroyMode.Refund: {
					return (int count) => count;
				}
			}
			throw new ArgumentException(string.Concat("Unknown destroy mode ", mode));
		}
	}

	[HarmonyPatch(typeof(GenConstruct), "CanPlaceBlueprintOver", null)]
	public static class GenConstruct_CanPlaceBlueprintOver {
		public static bool Prefix(BuildableDef newDef, ThingDef oldDef, ref bool __result) {
			ThingDef thingDef = newDef as ThingDef;
			ThingDef thingDef1 = oldDef;
			if (thingDef == null || thingDef1 == null) {
				return true;
			}
			BuildableDef buildableDef = GenConstruct.BuiltDefOf(oldDef);
			ThingDef thingDef2 = buildableDef as ThingDef;
			if (thingDef.building != null && thingDef.building.canPlaceOverWall && thingDef2 != null) {
				if (thingDef2.defName.Equals("RFFConcreteWall") || thingDef2.defName.Equals("RFFReinforcedConcreteWall") || thingDef2.defName.Equals("RFFPlasticreteWall")) {
					__result = true;
					return false;
				}
			}
			return true;
		}
	}

	[HarmonyPatch(typeof(GenConstruct), "BlocksConstruction", null)]
	public static class GenConstruct_BlocksConstruction {
		public static bool Prefix(Thing constructible, Thing t, ref bool __result) {
			ThingDef thingDef;
			if (constructible == null || t == null) {
				return true;
			}
			if (!(constructible is Blueprint)) {
				thingDef = (!(constructible is Frame) ? constructible.def.blueprintDef : constructible.def.entityDefToBuild.blueprintDef);
			}
			else {
				thingDef = constructible.def;
			}
			ThingDef thingDef1 = thingDef.entityDefToBuild as ThingDef;
			if (thingDef1 != null && thingDef1.building != null && thingDef1.building.canPlaceOverWall) {
				if (t.def.defName.Equals("RFFConcreteWall") || t.def.defName.Equals("RFFReinforcedConcreteWall") || t.def.defName.Equals("RFFPlasticreteWall")) {
					__result = false;
					return false;
				}
			}
			return true;
		}
	}

	[HarmonyPatch(typeof(GenSpawn), "SpawningWipes", null)]
	public static class GenSpawn_SpawningWipes {
		public static bool Prefix(BuildableDef newEntDef, BuildableDef oldEntDef, ref bool __result) {
			ThingDef thingDef = newEntDef as ThingDef;
			ThingDef thingDef1 = oldEntDef as ThingDef;
			if (thingDef == null || thingDef1 == null) {
				return true;
			}
			ThingDef thingDef2 = thingDef.entityDefToBuild as ThingDef;
			if (thingDef1.IsBlueprint) {
				if (thingDef.IsBlueprint) {
					if (thingDef2 != null && thingDef2.building != null && thingDef2.building.canPlaceOverWall) {
						if (thingDef1.entityDefToBuild is ThingDef) {
							if (thingDef1.entityDefToBuild.defName.Equals("RFFConcreteWall") || thingDef1.entityDefToBuild.defName.Equals("RFFReinforcedConcreteWall") || thingDef1.entityDefToBuild.defName.Equals("RFFPlasticreteWall")) {
								__result = true;
								return false;
							}
						}
					}
				}
			}
			return true;
		}
	}*/
	
}
