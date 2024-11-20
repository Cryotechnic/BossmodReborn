namespace BossMod.Dawntrail.Dungeon.D09YuweyawataFieldStation.D90SprightlyClayGolem;

public enum OID : uint
{
    Boss = 0x461B, // R6.38
    SprightlyStone1 = 0x46B1, // R3.0
    SprightlyStone2 = 0x461D, // R3.0
    SprightlyDhara = 0x4618 // R1.755
}

public enum AID : uint
{
    AutoAttack1 = 41119, // SprightlyStone1/SprightlyStone2->player, no cast, single-target
    AutoAttack2 = 872, // SprightlyDhara/Boss->player, no cast, single-target

    WildHorn = 40675, // Boss->self, 4.0s cast, range 15 120-degree cone
    Plummet = 40676 // SprightlyStone2->self, 4.0s cast, range 10 circle
}

class WildHorn(BossModule module) : Components.SelfTargetedAOEs(module, ActionID.MakeSpell(AID.WildHorn), new AOEShapeCone(15, 60.Degrees()));
class Plummet(BossModule module) : Components.SelfTargetedAOEs(module, ActionID.MakeSpell(AID.Plummet), new AOEShapeCircle(10));

class D90SprightlyClayGolemStates : StateMachineBuilder
{
    public D90SprightlyClayGolemStates(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<WildHorn>()
            .ActivateOnEnter<Plummet>()
            .Raw.Update = () => Module.WorldState.Actors.Where(x => x.IsTargetable && !x.IsAlly && x.Position.AlmostEqual(Module.Arena.Center, Module.Bounds.Radius))
            .All(x => x.IsDeadOrDestroyed);
    }
}

[ModuleInfo(BossModuleInfo.Maturity.Verified, Contributors = "The Combat Reborn Team (Malediktus)", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 1008, NameID = 13582, SortOrder = 8)]
public class D90SprightlyClayGolem(WorldState ws, Actor primary) : BossModule(ws, primary, arena.Center, arena)
{
    private static readonly WPos[] vertices = [new(107.50f, -419.32f), new(108.05f, -418.91f), new(108.99f, -417.90f), new(109.55f, -416.49f), new(109.98f, -416.05f),
    new(110.47f, -415.84f), new(110.99f, -415.45f), new(111.18f, -414.97f), new(111.60f, -414.50f), new(111.99f, -413.88f),
    new(112.43f, -413.37f), new(112.52f, -412.82f), new(113.03f, -411.52f), new(113.41f, -410.96f), new(113.69f, -410.29f),
    new(113.78f, -409.79f), new(113.97f, -409.10f), new(114.23f, -408.48f), new(115.08f, -407.58f), new(115.55f, -407.17f),
    new(115.86f, -406.76f), new(116.68f, -404.95f), new(116.81f, -403.94f), new(117.44f, -401.46f), new(117.70f, -400.83f),
    new(118.08f, -400.25f), new(118.40f, -399.61f), new(118.53f, -398.60f), new(119.44f, -395.83f), new(119.73f, -395.28f),
    new(120.21f, -394.88f), new(120.45f, -394.41f), new(120.71f, -392.38f), new(121.07f, -391.91f), new(121.50f, -391.46f),
    new(121.88f, -390.88f), new(122.23f, -389.68f), new(122.51f, -387.75f), new(122.69f, -387.12f), new(122.99f, -386.47f),
    new(123.15f, -385.99f), new(123.60f, -383.73f), new(123.92f, -383.11f), new(124.11f, -382.63f), new(124.20f, -382.12f),
    new(124.40f, -381.43f), new(124.49f, -380.92f), new(124.72f, -380.27f), new(125.47f, -379.11f), new(125.69f, -378.62f),
    new(125.75f, -377.88f), new(125.78f, -375.91f), new(126.25f, -374.66f), new(126.37f, -373.93f), new(126.26f, -372.43f),
    new(126.42f, -371.84f), new(126.94f, -370.61f), new(127.66f, -367.43f), new(127.99f, -366.84f), new(128.08f, -366.31f),
    new(127.97f, -364.77f), new(128.44f, -361.16f), new(128.30f, -358.05f), new(128.77f, -356.80f), new(128.95f, -356.09f),
    new(129.06f, -355.35f), new(128.98f, -354.10f), new(129.17f, -353.52f), new(129.29f, -352.80f), new(129.25f, -352.09f),
    new(129.35f, -350.67f), new(129.62f, -350.12f), new(129.82f, -349.42f), new(129.34f, -346.93f), new(129.50f, -345.59f),
    new(129.51f, -345.08f), new(129.34f, -343.12f), new(129.53f, -342.55f), new(130.13f, -341.31f), new(129.99f, -340.09f),
    new(130.00f, -338.76f), new(129.87f, -338.14f), new(130.03f, -337.44f), new(129.99f, -336.93f), new(129.88f, -336.28f),
    new(129.83f, -334.94f), new(129.60f, -333.60f), new(129.55f, -330.98f), new(129.71f, -329.10f), new(129.13f, -325.87f),
    new(129.11f, -324.34f), new(129.27f, -323.33f), new(129.50f, -322.75f), new(129.62f, -322.10f), new(129.18f, -319.69f),
    new(129.11f, -318.30f), new(128.99f, -317.60f), new(128.71f, -314.20f), new(128.96f, -312.93f), new(128.96f, -312.43f),
    new(128.49f, -312.18f), new(127.98f, -312.12f), new(127.47f, -312.16f), new(126.75f, -312.32f), new(116.84f, -315.01f),
    new(116.44f, -315.32f), new(116.20f, -315.78f), new(115.96f, -316.37f), new(115.65f, -316.92f), new(115.16f, -317.33f),
    new(114.13f, -319.21f), new(113.72f, -319.79f), new(113.45f, -320.38f), new(113.42f, -320.91f), new(113.79f, -322.11f),
    new(113.64f, -322.72f), new(113.35f, -323.41f), new(113.54f, -323.90f), new(113.70f, -324.66f), new(114.23f, -325.72f),
    new(114.45f, -326.42f), new(114.49f, -327.05f), new(114.47f, -327.69f), new(114.65f, -328.30f), new(114.58f, -329.00f),
    new(114.87f, -330.26f), new(114.90f, -330.93f), new(114.82f, -331.56f), new(114.84f, -332.19f), new(114.74f, -332.90f),
    new(114.97f, -334.74f), new(115.07f, -335.34f), new(115.00f, -336.01f), new(114.72f, -336.57f), new(114.57f, -337.21f),
    new(114.84f, -339.04f), new(114.87f, -340.38f), new(114.81f, -341.03f), new(115.09f, -344.84f), new(114.91f, -345.43f),
    new(114.54f, -346.06f), new(114.52f, -346.74f), new(114.74f, -350.77f), new(114.61f, -351.43f), new(114.27f, -351.97f),
    new(113.86f, -352.45f), new(113.86f, -353.14f), new(113.67f, -354.36f), new(113.71f, -357.70f), new(113.44f, -359.10f),
    new(113.37f, -359.84f), new(113.38f, -360.86f), new(113.19f, -361.42f), new(113.07f, -362.14f), new(113.05f, -363.41f),
    new(112.93f, -364.03f), new(112.70f, -364.67f), new(112.51f, -365.84f), new(112.33f, -369.21f), new(111.96f, -369.78f),
    new(111.78f, -370.41f), new(111.19f, -371.58f), new(110.81f, -372.08f), new(110.61f, -372.79f), new(110.49f, -374.70f),
    new(110.10f, -376.72f), new(109.70f, -377.18f), new(109.09f, -378.49f), new(108.63f, -382.02f), new(108.33f, -382.57f),
    new(108.10f, -383.15f), new(107.64f, -383.65f), new(107.45f, -384.20f), new(107.30f, -385.22f), new(107.17f, -385.86f),
    new(106.62f, -386.36f), new(106.53f, -386.91f), new(106.41f, -388.13f), new(106.11f, -388.77f), new(105.92f, -389.44f),
    new(104.74f, -391.15f), new(104.49f, -393.61f), new(104.22f, -394.14f), new(103.72f, -394.59f), new(103.37f, -395.19f),
    new(103.30f, -395.88f), new(102.97f, -396.42f), new(102.93f, -396.92f), new(102.80f, -397.55f), new(102.38f, -398.01f),
    new(102.02f, -398.63f), new(101.78f, -400.73f), new(101.09f, -401.77f), new(99.60f, -404.79f), new(98.72f, -405.87f),
    new(98.38f, -406.46f), new(98.14f, -407.01f), new(97.63f, -407.41f), new(97.28f, -407.97f), new(96.03f, -409.58f),
    new(95.69f, -410.22f), new(95.63f, -410.92f), new(95.41f, -411.42f), new(94.93f, -411.90f), new(94.39f, -412.23f),
    new(93.72f, -413.32f), new(93.46f, -413.92f), new(93.00f, -414.40f), new(93.22f, -414.95f), new(106.99f, -419.44f),
    new(107.50f, -419.32f)];
    private static readonly ArenaBoundsComplex arena = new([new PolygonCustom(vertices)]);

    protected override bool CheckPull() => WorldState.Actors.Any(x => x.InCombat && x.Position.AlmostEqual(Arena.Center, Bounds.Radius));

    protected override void DrawEnemies(int pcSlot, Actor pc)
    {
        Arena.Actors(WorldState.Actors.Where(x => !x.IsAlly && x.Position.AlmostEqual(Arena.Center, Bounds.Radius)));
    }
}
