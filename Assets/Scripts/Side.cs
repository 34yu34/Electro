
[System.Flags]
public enum Side
{
    bot = 1 << 0,
    left = 1 << 1,
    top = 1 << 2,
    right = 1 << 3,
    all = bot | left | top | right
}
