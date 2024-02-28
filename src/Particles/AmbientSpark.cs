﻿using UnityEngine;
using Random = UnityEngine.Random;
using Color = UnityEngine.Color;
using RWCustom;

namespace Hailstorm;

//----------------------------------------------------------------------------------
//----------------------------------------------------------------------------------

public class AmbientSpark : GreenSparks.GreenSpark
{

    public int flightDir;

    public AmbientSpark(Vector2 pos, Color? col, bool TrueforleftFalseforright) : base(pos)
    {
        base.pos = pos;
        lastLastPos = pos;
        lastPos = pos;
        life = 1f;
        lifeTime = Mathf.Lerp(600f, 1200f, Random.value);
        if (Random.value < 0.4f)
        {
            depth = 0f;
        }
        else if (Random.value < 0.3f)
        {
            depth = -0.5f * Random.value;
        }
        else
        {
            depth = Mathf.Pow(Random.value, 1.5f) * 3f;
        }
        this.col = col ?? new Color(0, 1, 1/85f);
        flightDir = TrueforleftFalseforright ? -1 : 1;
    }

    //--------------------------------------------------------------------------------

    public override void Update(bool eu)
    {
        vel *= 0.99f;
        vel += new Vector2(0.11f * flightDir, Custom.LerpMap(life, 0, 0.5f, -0.1f, 0.05f));
        vel += dir * 0.2f;
        Vector2 flightpathVariance = dir + Custom.RNV() * 0.6f * flightDir;
        dir = flightpathVariance.normalized;
        life -= 1f / lifeTime;
        lastLastPos = lastPos;
        lastPos = pos;
        pos += vel / (depth + 1f);
        if (InPlayLayer)
        {
            if (room.GetTile(pos).Solid)
            {
                life -= 0.025f;
                if (!room.GetTile(lastPos).Solid)
                {
                    IntVector2? intVector = SharedPhysics.RayTraceTilesForTerrainReturnFirstSolid(room, room.GetTilePosition(lastPos), room.GetTilePosition(pos));
                    FloatRect floatRect = Custom.RectCollision(pos, lastPos, room.TileRect(intVector.Value).Grow(2f));
                    pos = floatRect.GetCorner(FloatRect.CornerLabel.D);
                    float num = 0.3f;
                    if (floatRect.GetCorner(FloatRect.CornerLabel.B).x < 0f)
                    {
                        vel.x = Mathf.Abs(vel.x) * num;
                    }
                    else if (floatRect.GetCorner(FloatRect.CornerLabel.B).x > 0f)
                    {
                        vel.x = (0f - Mathf.Abs(vel.x)) * num;
                    }
                    else if (floatRect.GetCorner(FloatRect.CornerLabel.B).y < 0f)
                    {
                        vel.y = Mathf.Abs(vel.y) * num;
                    }
                    else if (floatRect.GetCorner(FloatRect.CornerLabel.B).y > 0f)
                    {
                        vel.y = (0f - Mathf.Abs(vel.y)) * num;
                    }
                }
                else
                {
                    pos.y = room.MiddleOfTile(pos).y + 10f;
                }
            }
            if (room.PointSubmerged(pos))
            {
                pos.y = room.FloatWaterLevel(pos.x);
                life -= 0.025f;
            }
        }
        if (life < 0f || (Custom.VectorRectDistance(pos, room.RoomRect) > 100f && !room.ViewedByAnyCamera(pos, 400f)))
        {
            Destroy();
        }
        if (depth <= 0f && room.Darkness(pos) > 0f)
        {
            if (light == null)
            {
                light = new LightSource(pos, environmentalLight: false, col, this);
                light.noGameplayImpact = true;
                light.requireUpKeep = true;
                room.AddObject(light);
            }
            light.setPos = pos;
            light.setAlpha = 0.4f * Mathf.InverseLerp(0f, 0.2f, life) * Mathf.InverseLerp(-0.6f, 0f, depth);
            light.setRad = 80f;
            light.stayAlive = true;
        }
        else if (light is not null)
        {
            light.Destroy();
            light = null;
        }
        if (!room.BeingViewed)
        {
            Destroy();
        }
    }

}

//----------------------------------------------------------------------------------