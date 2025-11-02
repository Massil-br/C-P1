
public class CountFpsSystem : ISystem
{
    public void Update(ECSManager ecs, float dt)
    {
        var fpsCounters = ecs.GetComponents<FpsCounter>();

        // Parallel.For(0, fpsCounters.Count, i =>
        // {
        //     var (entityId, fpsCounter) = fpsCounters[i];

        //     fpsCounter.counter += dt;
        //     fpsCounter.fps += 1;

        //     if (fpsCounter.counter >= fpsCounter.delay)
        //     {
        //         fpsCounter.lastFps = fpsCounter.fps;
        //         fpsCounter.counter = 0;
        //         fpsCounter.fps = 0;
        //     }

        //     fpsCounters.Add(entityId, fpsCounter); 
        // });

        for(int i = 0; i < fpsCounters.Count; i++)
        {
            var (entityId, fpsCounter) = fpsCounters[i];

            fpsCounter.counter += dt;
            fpsCounter.fps += 1;

            if (fpsCounter.counter >= fpsCounter.delay)
            {
                fpsCounter.lastFps = fpsCounter.fps;
                fpsCounter.counter = 0;
                fpsCounter.fps = 0;
            }

            fpsCounters.Add(entityId, fpsCounter); 
        }
    }
}
