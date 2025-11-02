
public static class CreatePlayer
{
    public static int Init(ECSManager ecs)
    {
        int Player = ecs.CreateEntity();
        ecs.AddComponent(Player, new FpsCounter(1f));
        return Player;
    }
}