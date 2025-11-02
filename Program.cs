

using Raylib_cs;

class Program
{   

    


    static void Main()
    {   
        Raylib.InitWindow(800, 600, "P1");

        var ecs = new ECSManager();
        CreatePlayer.Init(ecs);
        ecs.AddSystem(new CountFpsSystem());

        while (!Raylib.WindowShouldClose())
        {
            float dt = Raylib.GetFrameTime();
            ecs.UpdateSystems(dt);
            string FpsDraw = "FPS: " + ecs.GetComponents<FpsCounter>()[0].component.lastFps;
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            Raylib.DrawText(FpsDraw, 200, 200, 30, Color.DarkPurple);
            Raylib.EndDrawing();
        }

    }
}
