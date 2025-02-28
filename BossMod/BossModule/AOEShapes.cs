﻿namespace BossMod;

public abstract record class AOEShape
{
    public abstract bool Check(WPos position, WPos origin, Angle rotation);
    public abstract void Draw(MiniArena arena, WPos origin, Angle rotation, uint color = 0);
    public abstract void Outline(MiniArena arena, WPos origin, Angle rotation, uint color = 0);
    public abstract Func<WPos, float> Distance(WPos origin, Angle rotation);

    public bool Check(WPos position, Actor? origin) => origin != null && Check(position, origin.Position, origin.Rotation);

    public void Draw(MiniArena arena, Actor? origin, uint color = 0)
    {
        if (origin != null)
            Draw(arena, origin.Position, origin.Rotation, color);
    }

    public void Outline(MiniArena arena, Actor? origin, uint color = 0)
    {
        if (origin != null)
            Outline(arena, origin.Position, origin.Rotation, color);
    }
}

public sealed record class AOEShapeCone(float Radius, Angle HalfAngle, Angle DirectionOffset = default, bool InvertForbiddenZone = false) : AOEShape
{
    public override string ToString() => $"Cone: r={Radius:f3}, angle={HalfAngle * 2}, off={DirectionOffset}, ifz={InvertForbiddenZone}";
    public override bool Check(WPos position, WPos origin, Angle rotation) => position.InCircleCone(origin, Radius, rotation + DirectionOffset, HalfAngle);
    public override void Draw(MiniArena arena, WPos origin, Angle rotation, uint color = 0) => arena.ZoneCone(origin, 0, Radius, rotation + DirectionOffset, HalfAngle, color);
    public override void Outline(MiniArena arena, WPos origin, Angle rotation, uint color = 0) => arena.AddCone(origin, Radius, rotation + DirectionOffset, HalfAngle, color);
    public override Func<WPos, float> Distance(WPos origin, Angle rotation)
    {
        return !InvertForbiddenZone
            ? ShapeDistance.Cone(origin, Radius, rotation + DirectionOffset, HalfAngle)
            : ShapeDistance.InvertedCone(origin, Radius, rotation + DirectionOffset, HalfAngle);
    }
}

public sealed record class AOEShapeCircle(float Radius, bool InvertForbiddenZone = false) : AOEShape
{
    public override string ToString() => $"Circle: r={Radius:f3}, ifz={InvertForbiddenZone}";
    public override bool Check(WPos position, WPos origin, Angle rotation = new()) => position.InCircle(origin, Radius);
    public override void Draw(MiniArena arena, WPos origin, Angle rotation = new(), uint color = 0) => arena.ZoneCircle(origin, Radius, color);
    public override void Outline(MiniArena arena, WPos origin, Angle rotation = new(), uint color = 0) => arena.AddCircle(origin, Radius, color);
    public override Func<WPos, float> Distance(WPos origin, Angle rotation)
    {
        return !InvertForbiddenZone
            ? ShapeDistance.Circle(origin, Radius)
            : ShapeDistance.InvertedCircle(origin, Radius);
    }
}

public sealed record class AOEShapeDonut(float InnerRadius, float OuterRadius, bool InvertForbiddenZone = false) : AOEShape
{
    public override string ToString() => $"Donut: r={InnerRadius:f3}-{OuterRadius:f3}, ifz={InvertForbiddenZone}";
    public override bool Check(WPos position, WPos origin, Angle rotation = new()) => position.InDonut(origin, InnerRadius, OuterRadius);
    public override void Draw(MiniArena arena, WPos origin, Angle rotation = new(), uint color = 0) => arena.ZoneDonut(origin, InnerRadius, OuterRadius, color);
    public override void Outline(MiniArena arena, WPos origin, Angle rotation = new(), uint color = 0)
    {
        arena.AddCircle(origin, InnerRadius, color);
        arena.AddCircle(origin, OuterRadius, color);
    }
    public override Func<WPos, float> Distance(WPos origin, Angle rotation)
    {
        return !InvertForbiddenZone
            ? ShapeDistance.Donut(origin, InnerRadius, OuterRadius)
            : ShapeDistance.InvertedDonut(origin, InnerRadius, OuterRadius);
    }
}

public sealed record class AOEShapeDonutSector(float InnerRadius, float OuterRadius, Angle HalfAngle, Angle DirectionOffset = default, bool InvertForbiddenZone = false) : AOEShape
{
    public override string ToString() => $"Donut sector: r={InnerRadius:f3}-{OuterRadius:f3}, angle={HalfAngle * 2}, off={DirectionOffset}, ifz={InvertForbiddenZone}";
    public override bool Check(WPos position, WPos origin, Angle rotation) => position.InDonutCone(origin, InnerRadius, OuterRadius, rotation + DirectionOffset, HalfAngle);
    public override void Draw(MiniArena arena, WPos origin, Angle rotation, uint color = 0) => arena.ZoneCone(origin, InnerRadius, OuterRadius, rotation + DirectionOffset, HalfAngle, color);
    public override void Outline(MiniArena arena, WPos origin, Angle rotation, uint color = 0) => arena.AddDonutCone(origin, InnerRadius, OuterRadius, rotation + DirectionOffset, HalfAngle, color);
    public override Func<WPos, float> Distance(WPos origin, Angle rotation)
    {
        return !InvertForbiddenZone
            ? ShapeDistance.DonutSector(origin, InnerRadius, OuterRadius, rotation + DirectionOffset, HalfAngle)
            : ShapeDistance.InvertedDonutSector(origin, InnerRadius, OuterRadius, rotation + DirectionOffset, HalfAngle);
    }
}

public sealed record class AOEShapeRect(float LengthFront, float HalfWidth, float LengthBack = 0, Angle DirectionOffset = default, bool InvertForbiddenZone = false) : AOEShape
{
    public override string ToString() => $"Rect: l={LengthFront:f3}+{LengthBack:f3}, w={HalfWidth * 2}, off={DirectionOffset}, ifz={InvertForbiddenZone}";
    public override bool Check(WPos position, WPos origin, Angle rotation) => position.InRect(origin, rotation + DirectionOffset, LengthFront, LengthBack, HalfWidth);
    public override void Draw(MiniArena arena, WPos origin, Angle rotation, uint color = 0) => arena.ZoneRect(origin, rotation + DirectionOffset, LengthFront, LengthBack, HalfWidth, color);
    public override void Outline(MiniArena arena, WPos origin, Angle rotation, uint color = 0) => arena.AddRect(origin, (rotation + DirectionOffset).ToDirection(), LengthFront, LengthBack, HalfWidth, color);
    public override Func<WPos, float> Distance(WPos origin, Angle rotation)
    {
        return !InvertForbiddenZone
            ? ShapeDistance.Rect(origin, rotation + DirectionOffset, LengthFront, LengthBack, HalfWidth)
            : ShapeDistance.InvertedRect(origin, rotation + DirectionOffset, LengthFront, LengthBack, HalfWidth);
    }
}

public sealed record class AOEShapeCross(float Length, float HalfWidth, Angle DirectionOffset = default, bool InvertForbiddenZone = false) : AOEShape
{
    public override string ToString() => $"Cross: l={Length:f3}, w={HalfWidth * 2}, off={DirectionOffset}, ifz={InvertForbiddenZone}";
    public override bool Check(WPos position, WPos origin, Angle rotation) => position.InRect(origin, rotation + DirectionOffset, Length, Length, HalfWidth) || position.InRect(origin, rotation + DirectionOffset, HalfWidth, HalfWidth, Length);
    public override void Draw(MiniArena arena, WPos origin, Angle rotation, uint color = 0) => arena.ZonePoly((GetType(), origin, rotation + DirectionOffset, Length, HalfWidth), ContourPoints(origin, rotation), color);
    public override void Outline(MiniArena arena, WPos origin, Angle rotation, uint color = 0)
    {
        var points = ContourPoints(origin, rotation);
        for (var i = 0; i < 12; ++i)
            arena.PathLineTo(points[i]);
        MiniArena.PathStroke(true, color);
    }

    private WPos[] ContourPoints(WPos origin, Angle rotation, float offset = 0)
    {
        var dx = (rotation + DirectionOffset).ToDirection();
        var dy = dx.OrthoL();

        var lengthOffset = Length + offset;
        var halfWidthOffset = HalfWidth + offset;

        var dxLength = dx * lengthOffset;
        var dxWidth = dx * halfWidthOffset;
        var dyLength = dy * lengthOffset;
        var dyWidth = dy * halfWidthOffset;

        return
        [
            origin + dxLength - dyWidth,
            origin + dxWidth - dyWidth,
            origin + dxWidth - dyLength,
            origin - dxWidth - dyLength,
            origin - dxWidth - dyWidth,
            origin - dxLength - dyWidth,
            origin - dxLength + dyWidth,
            origin - dxWidth + dyWidth,
            origin - dxWidth + dyLength,
            origin + dxWidth + dyLength,
            origin + dxWidth + dyWidth,
            origin + dxLength + dyWidth
        ];
    }

    public override Func<WPos, float> Distance(WPos origin, Angle rotation)
    {
        return !InvertForbiddenZone
            ? ShapeDistance.Cross(origin, rotation + DirectionOffset, Length, HalfWidth)
            : ShapeDistance.InvertedCross(origin, rotation + DirectionOffset, Length, HalfWidth);
    }
}

public sealed record class AOEShapeTriCone(float SideLength, Angle HalfAngle, Angle DirectionOffset = default, bool InvertForbiddenZone = false) : AOEShape
{
    public override string ToString() => $"TriCone: side={SideLength:f3}, angle={HalfAngle * 2}, off={DirectionOffset}, ifz={InvertForbiddenZone}";
    public override bool Check(WPos position, WPos origin, Angle rotation) => position.InTri(origin, origin + SideLength * (rotation + DirectionOffset + HalfAngle).ToDirection(), origin + SideLength * (rotation + DirectionOffset - HalfAngle).ToDirection());
    public override void Draw(MiniArena arena, WPos origin, Angle rotation, uint color = 0) => arena.ZoneTri(origin, origin + SideLength * (rotation + DirectionOffset + HalfAngle).ToDirection(), origin + SideLength * (rotation + DirectionOffset - HalfAngle).ToDirection(), color);
    public override void Outline(MiniArena arena, WPos origin, Angle rotation, uint color = 0) => arena.AddTriangle(origin, origin + SideLength * (rotation + DirectionOffset + HalfAngle).ToDirection(), origin + SideLength * (rotation + DirectionOffset - HalfAngle).ToDirection(), color);

    public override Func<WPos, float> Distance(WPos origin, Angle rotation)
    {
        var direction1 = SideLength * (rotation + DirectionOffset + HalfAngle).ToDirection();
        var direction2 = SideLength * (rotation + DirectionOffset - HalfAngle).ToDirection();
        var shape = new RelTriangle(default, direction1, direction2);
        return !InvertForbiddenZone ? ShapeDistance.Tri(origin, shape) : ShapeDistance.InvertedTri(origin, shape);
    }
}

public sealed record class AOEShapeCapsule(float Radius, float Length, Angle DirectionOffset = default, bool InvertForbiddenZone = false) : AOEShape
{
    public override string ToString() => $"Capsule: radius={Radius:f3}, length={Length}, off={DirectionOffset}, ifz={InvertForbiddenZone}";
    public override bool Check(WPos position, WPos origin, Angle rotation) => position.InCapsule(origin, (rotation + DirectionOffset).ToDirection(), Radius, Length);

    public override void Draw(MiniArena arena, WPos origin, Angle rotation, uint color = 0) => arena.ZoneCapsule(origin, (rotation + DirectionOffset).ToDirection(), Radius, Length, color);

    public override Func<WPos, float> Distance(WPos origin, Angle rotation)
    {
        return !InvertForbiddenZone ? ShapeDistance.Capsule(origin, rotation, Length, Radius) : ShapeDistance.InvertedCapsule(origin, rotation, Length, Radius);
    }

    public override void Outline(MiniArena arena, WPos origin, Angle rotation, uint color = 0)
    => arena.AddCapsule(origin, (rotation + DirectionOffset).ToDirection(), Radius, Length, color);
}

public enum OperandType
{
    Union,
    Xor,
    Intersection,
    Difference
}

// shapes1 for unions, shapes 2 for shapes for XOR/intersection with shapes1, differences for shapes that get subtracted after previous operations
// always create a new instance of AOEShapeCustom if something other than the invertforbiddenzone changes
// if the origin of the AOE can change, edit the origin default value to prevent cache issues
public sealed record class AOEShapeCustom(IEnumerable<Shape> Shapes1, IEnumerable<Shape>? DifferenceShapes = null, IEnumerable<Shape>? Shapes2 = null, bool InvertForbiddenZone = false, OperandType Operand = OperandType.Union, WPos Origin = default) : AOEShape
{
    private RelSimplifiedComplexPolygon? polygon;
    private readonly int hashkey = CreateCacheKey(Shapes1, Shapes2 ?? [], DifferenceShapes ?? [], Operand, Origin);
    public static readonly Dictionary<int, RelSimplifiedComplexPolygon> Cache = [];
    public static readonly LinkedList<int> CacheOrder = new();
    public void AddToCache(RelSimplifiedComplexPolygon value)
    {
        if (Cache.Count >= 50)
        {
            var lruKey = CacheOrder.Last?.Value;
            if (lruKey != null)
            {
                Cache.Remove(lruKey.Value);
                CacheOrder.RemoveLast();
            }
        }
        Cache[hashkey] = value;
        CacheOrder.Remove(hashkey);
        CacheOrder.AddFirst(hashkey);
    }
    public override string ToString() => $"Custom AOE shape: hashkey={hashkey}, ifz={InvertForbiddenZone}";

    private RelSimplifiedComplexPolygon GetCombinedPolygon(WPos origin)
    {
        if (Cache.TryGetValue(hashkey, out var cachedResult)) // for moving custom AOEs we don't want to recalculate the polygon every frame since they move at server ticks and not frame
        {
            CacheOrder.Remove(hashkey);
            CacheOrder.AddFirst(hashkey);
            return polygon = cachedResult;
        }
        var shapes1 = CreateOperandFromShapes(Shapes1, origin);
        var shapes2 = CreateOperandFromShapes(Shapes2, origin);
        var differenceOperands = CreateOperandFromShapes(DifferenceShapes, origin);

        var clipper = new PolygonClipper();
        var combinedShapes = Operand switch
        {
            OperandType.Xor => clipper.Xor(shapes1, shapes2),
            OperandType.Intersection => clipper.Intersect(shapes1, shapes2),
            _ => null
        };

        polygon = combinedShapes != null
            ? clipper.Difference(new PolygonClipper.Operand(combinedShapes), differenceOperands)
            : clipper.Difference(shapes1, differenceOperands);
        AddToCache(polygon);
        return polygon;
    }

    private static PolygonClipper.Operand CreateOperandFromShapes(IEnumerable<Shape>? shapes, WPos origin)
    {
        var operand = new PolygonClipper.Operand();
        if (shapes != null)
            foreach (var shape in shapes)
                operand.AddPolygon(shape.ToPolygon(origin));
        return operand;
    }

    public override bool Check(WPos position, WPos origin, Angle rotation)
    {
        var relativePosition = position - origin;
        var result = (polygon ?? GetCombinedPolygon(origin)).Contains(new(relativePosition.X, relativePosition.Z));
        return result;
    }

    private static int CreateCacheKey(IEnumerable<Shape> shapes1, IEnumerable<Shape> shapes2, IEnumerable<Shape> differenceShapes, OperandType operand, WPos origin)
    {
        var hashCode = new HashCode();
        foreach (var shape in shapes1.Concat(shapes2).Concat(differenceShapes))
            hashCode.Add(shape.GetHashCode());
        hashCode.Add(operand);
        hashCode.Add(origin);
        return hashCode.ToHashCode();
    }

    public override void Draw(MiniArena arena, WPos origin, Angle rotation, uint color = 0)
    {
        arena.ZoneRelPoly(hashkey, polygon ?? GetCombinedPolygon(origin), color);
    }

    public override void Outline(MiniArena arena, WPos origin, Angle rotation, uint color = 0)
    {
        var combinedPolygon = polygon ?? GetCombinedPolygon(origin);
        for (var i = 0; i < combinedPolygon.Parts.Count; ++i)
        {
            var part = combinedPolygon.Parts[i];
            var exteriorEdges = part.ExteriorEdges.ToList();
            for (var j = 0; j < exteriorEdges.Count; ++j)
            {
                var (start, end) = exteriorEdges[j];
                arena.PathLineTo(origin + start);
                if (j != exteriorEdges.Count - 1)
                    arena.PathLineTo(origin + end);
            }
            MiniArena.PathStroke(true, color);

            foreach (var holeIndex in part.Holes)
            {
                var interiorEdges = part.InteriorEdges(holeIndex).ToList();
                for (var j = 0; j < interiorEdges.Count; ++j)
                {
                    var (start, end) = interiorEdges[j];
                    arena.PathLineTo(origin + start);
                    if (j != interiorEdges.Count - 1)
                        arena.PathLineTo(origin + end);
                }
                MiniArena.PathStroke(true, color);
            }
        }
    }

    public override Func<WPos, float> Distance(WPos origin, Angle rotation)
    {
        var shapeDistance = new PolygonWithHolesDistanceFunction(origin, polygon ?? GetCombinedPolygon(origin)).Distance;
        return InvertForbiddenZone ? p => -shapeDistance(p) : shapeDistance;
    }
}
