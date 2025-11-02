

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
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            Raylib.DrawText("Hello", 200, 200, 30, Color.DarkPurple);
            Raylib.EndDrawing();
        }

    }
}
