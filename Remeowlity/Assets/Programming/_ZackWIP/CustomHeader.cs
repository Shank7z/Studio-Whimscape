using UnityEngine;

public class CustomHeader : PropertyAttribute
{
    public string header;
    public int fontSize;

    public CustomHeader(string header, int fontSize = 14)
    {
        this.header = header;
        this.fontSize = fontSize;
    }
}