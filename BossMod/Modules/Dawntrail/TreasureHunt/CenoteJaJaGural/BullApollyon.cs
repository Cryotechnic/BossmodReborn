namespace BossMod.Dawntrail.TreasureHunt.CenoteJaJaGural.BullApollyon;
public enum OID : uint
{
    Boss = 0x4305, // R7
    TuraliOnion = 0x4300, // R0.84, icon 1, needs to be killed in order from 1 to 5 for maximum rewards
    TuraliEggplant = 0x4301, // R0.84, icon 2, needs to be killed in order from 1 to 5 for maximum rewards
    TuraliGarlic = 0x4302, // R0.84, icon 3, needs to be killed in order from 1 to 5 for maximum rewards
    TuraliTomato = 0x4303, // R0.84, icon 4, needs to be killed in order from 1 to 5 for maximum rewards
    TuligoraQueen = 0x4304, // R0.84, icon 5, needs to be killed in order from 1 to 5 for maximum rewards
    UolonOfFortune = 0x42FF, // R3.5
    Helper = 0x233C, // R0.5
}
public enum AID : uint
{
    AutoAttack = 870, // Boss->player, no cast, single-target
    Teleport = 38334, // Boss->location, no cast, single-target
    Blade = 38261, // Boss->player, 5s cast, single-target tankbuster

    PyreburstVisual = 38263, // Helper->self, no cast, range 60 circle visual
    Pyreburst = 38262, // Boss->self, 5s cast, single-target raidwide

    FlameBlade1 = 38249, // Boss->self, 4s cast, range 40 width 10 rect
    FlameBlade2 = 38250, // Helper->self, 11s cast, range 40 width 10 rect
    FlameBlade3 = 38251, // Helper->self, 2.5s cast, range 40 width 5 rect

    BlazingBreathVisual = 38257, // Boss->self, 2.3+0.7s cast, single-target visual
    BlazingBreath = 38258, // Helper->player, 3s cast, range 44 width 10 rect

    CrossfireBlade1 = 38253, // Boss->self, 4s cast, range 20 width 10 cross
    CrossfireBlade2 = 38254, // Helper->self, 11s cast, range 20 width 10 cross
    CrossfireBlade3 = 38255, // Helper->self, 2.5s cast, range 40 width 5 rect

    BlazingBlastVisual = 38259, // Boss->self, 3s cast, single-target visual
    BlazingBlast = 38260, // Helper->location, 3s cast, range 6 circle
}

class Blade(BossModule module) : Components.SingleTargetCast(module, ActionID.MakeSpell(AID.Blade));
class Pyreburst(BossModule module) : Components.RaidwideCast(module, ActionID.MakeSpell(AID.Pyreburst));
class FlameBlade1(BossModule module) : Components.SelfTargetedAOEs(module, ActionID.MakeSpell(AID.FlameBlade1), new AOEShapeRect(40, 5, 40));
class FlameBlade2(BossModule module) : Components.SelfTargetedAOEs(module, ActionID.MakeSpell(AID.FlameBlade2), new AOEShapeRect(40, 5, 40));
class FlameBlade3(BossModule module) : Components.SelfTargetedAOEs(module, ActionID.MakeSpell(AID.FlameBlade3), new AOEShapeRect(40, 2.5f, 40));
class BlazingBreath(BossModule module) : Components.SelfTargetedAOEs(module, ActionID.MakeSpell(AID.BlazingBreath), new AOEShapeRect(44, 5));
class CrossfireBlade1(BossModule module) : Components.SelfTargetedAOEs(module, ActionID.MakeSpell(AID.CrossfireBlade1), new AOEShapeCross(20, 5));
class CrossfireBlade2(BossModule module) : Components.SelfTargetedAOEs(module, ActionID.MakeSpell(AID.CrossfireBlade2), new AOEShapeCross(20, 5));
class CrossfireBlade3(BossModule module) : Components.SelfTargetedAOEs(module, ActionID.MakeSpell(AID.CrossfireBlade3), new AOEShapeRect(40, 2.5f, 40));
class BlazingBlast(BossModule module) : Components.LocationTargetedAOEs(module, ActionID.MakeSpell(AID.BlazingBlast), 6);

class BullApollyonStates : StateMachineBuilder
{
    public BullApollyonStates(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<Blade>()
            .ActivateOnEnter<Pyreburst>()
            .ActivateOnEnter<FlameBlade1>()
            .ActivateOnEnter<FlameBlade2>()
            .ActivateOnEnter<FlameBlade3>()
            .ActivateOnEnter<BlazingBreath>()
            .ActivateOnEnter<CrossfireBlade1>()
            .ActivateOnEnter<CrossfireBlade2>()
            .ActivateOnEnter<CrossfireBlade3>()
            .ActivateOnEnter<BlazingBlast>()
            .Raw.Update = () => module.Enemies(OID.Boss).All(e => e.IsDead) && module.Enemies(OID.TuraliOnion).All(e => e.IsDead) && module.Enemies(OID.TuraliEggplant).All(e => e.IsDead) && module.Enemies(OID.TuraliGarlic).All(e => e.IsDead) && module.Enemies(OID.TuraliTomato).All(e => e.IsDead) && module.Enemies(OID.TuligoraQueen).All(e => e.IsDead) && module.Enemies(OID.UolonOfFortune).All(e => e.IsDead);
    }
}

[ModuleInfo(BossModuleInfo.Maturity.Contributed, Contributors = "Kismet", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 993, NameID = 13247)]
public class BullApollyon(WorldState ws, Actor primary) : BossModule(ws, primary, new(0, -372), new ArenaBoundsCircle(20))
{
    protected override void DrawEnemies(int pcSlot, Actor pc)
    {
        Arena.Actor(PrimaryActor);
        Arena.Actors(Enemies(OID.TuraliTomato), Colors.Vulnerable);
        Arena.Actors(Enemies(OID.TuligoraQueen), Colors.Vulnerable);
        Arena.Actors(Enemies(OID.TuraliGarlic), Colors.Vulnerable);
        Arena.Actors(Enemies(OID.TuraliEggplant), Colors.Vulnerable);
        Arena.Actors(Enemies(OID.TuraliOnion), Colors.Vulnerable);
        Arena.Actors(Enemies(OID.UolonOfFortune), Colors.Vulnerable);
    }
}
