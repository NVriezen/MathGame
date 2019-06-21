using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Enemy
{
    private const float CHASE_DISTANCE = 250.0f;
    private readonly float moveSpeed = 300.0f;

    private Texture2D visual;

    public DevMath.Vector2 Position
    {
        get { return Circle.Position; }
        set { Circle.Position = value; }
    }

    public DevMath.Circle Circle
    {
        get; private set;
    }

    public float Rotation
    {
        get; private set;
    }

    public Enemy(DevMath.Vector2 position)
    {
        visual = Resources.Load<Texture2D>("pacman");

        Circle = new DevMath.Circle();
        Circle.Radius = visual.width * .5f;

        Position = position;
    }

    public void Render()
    {
        Matrix4x4 originalMatrix = GUI.matrix;
        //GUIUtility.RotateAroundPivot(Rotation, Position.ToUnity());
        GUI.matrix *= Matrix4x4.Translate(Position.ToUnity()) * Matrix4x4.Rotate(Quaternion.Euler(0, 0, Rotation)) * Matrix4x4.Translate(Position.ToUnity()).inverse;

        GUI.color = Color.red;

        GUI.DrawTexture(new Rect(Position.x - Circle.Radius, Position.y - Circle.Radius, visual.width, visual.height), visual);

        GUI.color = Color.white;

        //GUI.matrix = Matrix4x4.identity;
        GUI.matrix = originalMatrix;
    }

    public void Update(Player player)
    {
        var directionToPlayer = player.Position - Position;
        float distanceToPlayer = directionToPlayer.Magnitude;

        if(distanceToPlayer < CHASE_DISTANCE)
        {
            float playerFacing = DevMath.Vector2.Dot(directionToPlayer.Normalized, player.Direction);

            DevMath.Vector2 moveDirection;
            if(playerFacing > .0f)
            {
                moveDirection = directionToPlayer.Normalized;
            }
            else
            {
                moveDirection = -directionToPlayer.Normalized;
            }

            Position += moveDirection * moveSpeed * Time.deltaTime;

            Rotation = DevMath.DevMath.RadToDeg(DevMath.Vector2.Angle(new DevMath.Vector2(.0f, .0f), moveDirection));
        }
    }
}
