﻿namespace BossMod.Endwalker.Variant.V01SS.V015ThorneKnight;

class BlisteringBlow(BossModule module) : Components.SingleTargetCast(module, ActionID.MakeSpell(AID.BlisteringBlow));

class BlazingBeacon1(BossModule module) : Components.SelfTargetedAOEs(module, ActionID.MakeSpell(AID.BlazingBeacon1), new AOEShapeRect(50, 16));
class BlazingBeacon2(BossModule module) : Components.SelfTargetedAOEs(module, ActionID.MakeSpell(AID.BlazingBeacon2), new AOEShapeRect(50, 16));
class BlazingBeacon3(BossModule module) : Components.SelfTargetedAOEs(module, ActionID.MakeSpell(AID.BlazingBeacon3), new AOEShapeRect(50, 16));

class SignalFlareAOE(BossModule module) : Components.SelfTargetedAOEs(module, ActionID.MakeSpell(AID.SignalFlareAOE), new AOEShapeCircle(10));

class Explosion(BossModule module) : Components.SelfTargetedAOEs(module, ActionID.MakeSpell(AID.Explosion), new AOEShapeCross(50, 6));

class SacredFlay1(BossModule module) : Components.SelfTargetedAOEs(module, ActionID.MakeSpell(AID.SacredFlay1), new AOEShapeCone(50, 50.Degrees()));
class SacredFlay2(BossModule module) : Components.SelfTargetedAOEs(module, ActionID.MakeSpell(AID.SacredFlay2), new AOEShapeCone(50, 50.Degrees()));
class SacredFlay3(BossModule module) : Components.SelfTargetedAOEs(module, ActionID.MakeSpell(AID.SacredFlay3), new AOEShapeCone(50, 50.Degrees()));

class ForeHonor(BossModule module) : Components.SelfTargetedAOEs(module, ActionID.MakeSpell(AID.ForeHonor), new AOEShapeCone(50, 180.Degrees()));

class Cogwheel(BossModule module) : Components.RaidwideCast(module, ActionID.MakeSpell(AID.Cogwheel));

[ModuleInfo(BossModuleInfo.Maturity.WIP, Contributors = "CombatReborn Team", PrimaryActorOID = (uint)OID.Boss, GroupType = BossModuleInfo.GroupType.CFC, GroupID = 868, NameID = 11419)]
public class V015ThorneKnight(WorldState ws, Actor primary) : BossModule(ws, primary, new ArenaBoundsRect(new(289, -230), 20, 20, 45.Degrees()));