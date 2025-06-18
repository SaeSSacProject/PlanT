using UnityEngine;

public class Resolution
{
    public float Width
    {
        get
        {
            if(_width == 0.0f)
            {
                _width = Screen.currentResolution.width;
            }

            return _width;
        }
    }

    public float Height
    {
        get
        {
            if(_height == 0.0f)
            {
                _height = Screen.currentResolution.height;
            }

            return _height;
        }
    }

    private float _width = 0.0f;
    private float _height = 0.0f;
}
