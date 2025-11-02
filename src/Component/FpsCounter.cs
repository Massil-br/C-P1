
public struct FpsCounter
{
    public int fps;
    public float counter;
    public float delay;

    public FpsCounter(float delay)
    {
        counter = 0;
        fps = 0;
        this.delay = delay;
    }
}