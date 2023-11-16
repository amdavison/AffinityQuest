/// <summary>
/// Vector2 structure that uses integers.
/// </summary>
[System.Serializable]
public struct IntVector2
{
	public int x, z;

    /// <summary>
    /// Constructor for new IntVector2.
    /// </summary>
    /// <param name="x">int x coordinate</param>
    /// <param name="z">int z coordinate</param>
    public IntVector2(int x, int z)
    {
		this.x = x;
		this.z = z;
	}

    /// <summary>
    /// Adds two IntVector2 objects.
    /// </summary>
    /// <param name="a">IntVector2 left of operator</param>
    /// <param name="b">IntVector2 right of operator</param>
    /// <returns></returns>
    public static IntVector2 operator +(IntVector2 a, IntVector2 b)
    {
		a.x += b.x;
		a.z += b.z;
		return a;
	}
}