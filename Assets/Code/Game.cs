using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	public static Game Instance
    {
        get; private set;
    }
    private Player player;

    private List<Enemy> enemies;

	private List<Projectile> projectiles;

    private void Awake()
    {
        Instance = this;
    }

    private bool screenShake = false;
    [SerializeField] private float maxShake = 200;
    private float currentShakeTime = 0;
    [SerializeField] private float shakeTime = 2;

    private void Start()
    {
        player = new Player();

        enemies = new List<Enemy>();
        for(int i = 0; i < 1; ++i)
        {
            enemies.Add(new Enemy(new DevMath.Vector2(Random.Range(.0f, Screen.width), Random.Range(.0f, Screen.height))));
        }
		projectiles = new List<Projectile>();
    }

    private void OnGUI()
    {
        if (screenShake)
        {
            ScreenShake();
        }

        player?.Render(enemies);

        enemies.ForEach(e => e.Render());

        projectiles.ForEach(p => p.Render());

        if(player == null)
        {
            GUI.color = new Color(DevMath.DevMath.Lerp(0f, 1.0f, Mathf.Sin(Time.time)), DevMath.DevMath.Lerp(0f, 1.0f, Mathf.Cos(Time.time)), DevMath.DevMath.Lerp(0f, 1.0f, Mathf.Tan(Time.time)), DevMath.DevMath.Lerp(0.5f, 1.0f, Mathf.Sin(Time.time)));
            //GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, DevMath.DevMath.Lerp(0.5f, 1.0f, Mathf.Sin(Time.time)));
            GUI.Label(new Rect(Screen.width * .5f - 50.0f, Screen.height * .5f - 10.0f, 100.0f, 100.0f), "YOU LOSE!");
        }

        GUI.matrix = Matrix4x4.identity;
    }

    public void CreateProjectile(DevMath.Vector2 position, DevMath.Vector2 direction, float startVelocity, float acceleration)
    {
        projectiles.Add(new Projectile(position, direction, startVelocity, acceleration));
    }

    private void ScreenShake()
    {
        Debug.Log("Screenshake");
        //Implement screen shake with Sin + Matrices
        //float y = Mathf.Lerp(0, maxShake, Mathf.Sin(Time.time));
        float y = Mathf.Sin(Time.frameCount) * maxShake;
        float x = Mathf.Sin(-Time.frameCount) * maxShake;
        Debug.Log(y);
        GUI.matrix *= Matrix4x4.Translate(new Vector3(x,y,0));
        currentShakeTime += Time.deltaTime;
        if (currentShakeTime >= shakeTime)
        {
            screenShake = false;
            currentShakeTime = 0;
        }

        //GUI.matrix = Matrix4x4.identity;
    }

    private void Update()
    {
		if (player == null) return;

        player.Update();

        enemies.ForEach(e => e.Update(player));

        for (int i = projectiles.Count - 1; i >= 0; i--)
        {
            projectiles[i].Update();
            if(projectiles[i].ShouldDie)
            {
                projectiles.RemoveAt(i);
            }
        }

        foreach(Enemy e in enemies)
        {
            if(e.Circle.CollidesWith(player.Circle))
            {
                screenShake = true;
                player = null;
            }
        }

        for (int i = projectiles.Count - 1; i >= 0; i--)
        {
            for(int j = enemies.Count - 1; j >= 0; --j)
            {
                if(projectiles[i].Circle.CollidesWith(enemies[j].Circle))
                {
                    screenShake = true;
                    enemies.RemoveAt(j);
                    projectiles.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
