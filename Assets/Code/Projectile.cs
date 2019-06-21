using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Projectile
{
    private Texture2D visual;

    public DevMath.Circle Circle
    {
        get; private set;
    }

    private DevMath.Vector2 Position
    {
        get { return Circle.Position; }
        set { Circle.Position = value; }
    }

    private const float SCALE = .25f;

    private float velocity;
    private float accelerationPerSecond;

    public const float LIFETIME = 5.0f;
    private float lifetime;

    public bool ShouldDie => lifetime >= LIFETIME;

    private DevMath.Vector2 Direction
    {
        get; set;
    }

    public Projectile(DevMath.Vector2 position, DevMath.Vector2 direction, float startVelocity, float acceleration)
    {
        visual = Resources.Load<Texture2D>("pacman");

        Circle = new DevMath.Circle
        {
            Position = position,
            Radius = visual.width * .5f * SCALE
        };

        velocity = startVelocity;
        accelerationPerSecond = acceleration;

        Direction = direction;
    }

    public void Render()
    {
        Matrix4x4 originalMatrix = GUI.matrix;
        GUI.color = Color.blue;

        //GUIUtility.RotateAroundPivot(DevMath.Vector2.Angle(new DevMath.Vector2(.0f, .0f), Direction), Position.ToUnity());
        //GUIUtility.ScaleAroundPivot(Vector2.one * SCALE, Position.ToUnity());
        float rotation = DevMath.DevMath.RadToDeg(DevMath.Vector2.Angle(new DevMath.Vector2(0f, 0f), Direction));
        GUI.matrix *= Matrix4x4.Translate(Position.ToUnity()) * Matrix4x4.Rotate(Quaternion.Euler(0, 0, rotation)) * Matrix4x4.Scale((new DevMath.Vector3(1.0f, 1.0f, 1.0f) * SCALE).ToUnity()) * Matrix4x4.Translate(Position.ToUnity()).inverse;

        GUI.DrawTexture(new Rect(Position.x - visual.width * .5f, Position.y - visual.height * .5f, visual.width, visual.height), visual);

        //GUI.matrix = Matrix4x4.identity;
        GUI.matrix = originalMatrix;

        GUI.color = Color.white;
    }

    public void Update()
    {
        velocity += accelerationPerSecond * Time.deltaTime;

        Position += Direction * velocity * Time.deltaTime;

        lifetime += Time.deltaTime;
    }
}
