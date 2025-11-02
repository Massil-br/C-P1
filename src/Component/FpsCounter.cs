
public struct FpsCounter
{
    public int lastFps;
    public int fps;
    public float counter;
    public float delay;

    public FpsCounter(float delay)
    {
        lastFps = 0;
        counter = 0;
        fps = 0;
        this.delay = delay;
    }
}