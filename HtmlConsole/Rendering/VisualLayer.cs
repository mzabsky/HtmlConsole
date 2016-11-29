using System;
using HtmlConsole.Css;

namespace HtmlConsole.Rendering
{
    public class VisualLayer
    {
        /// <summary>
        /// The entire blit is transparent by default;
        /// </summary>
        private static readonly Color DefaultColor = new Color(0, 0, 0, 0);
        private static readonly char DefaultCharacter = ' ';

        public int Width { get; private set; }
        public int Height { get; private set; }

        private char[,] _characterMap;
        private Color[,] _colorMap;
        private int[,] _zIndexMap;

        public VisualLayer(int width, int height)
        {
            Width = width;
            Width = height;

            _characterMap = new char[width, height];
            _colorMap = new Color[width, height];
            _zIndexMap = new int[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    _characterMap[x, y] = DefaultCharacter;
                    _colorMap[x, y] = DefaultColor;
                }
            }
        }

        public Color GetColor(int x, int y)
        {
            if (!IsIn(x, y))
            {
                return DefaultColor;
            }

            return _colorMap[x, y];
        }

        public char GetCharacter(int x, int y)
        {
            if (!IsIn(x, y))
            {
                return DefaultCharacter;
            }

            return _characterMap[x, y];
        }

        public void Write(int x, int y, string text, Color color, int zIndex)
        {
            if (Width >= x + text.Length || Height >= y) ExpandTo(x, y);

            for (int i = 0; i < text.Length; i++)
            {
                Write(x, y + i, text[i], color, zIndex);
            }
        }

        public void Write(int x, int y, char c, Color color, int zIndex)
        {
            if(Width >= x || Height >= y) ExpandTo(x, y);

            if (zIndex >= _zIndexMap[x, y])
            {
                _characterMap[x, y] = c;
                _colorMap[x, y] = color;
                _zIndexMap[x, y] = zIndex;
            }
        }

        public void Write(int x, int y, VisualLayer other, int zIndex)
        {
            for (int otherX = 0; otherX < other.Width; otherX++)
            {
                for (int otherY = 0; otherY < other.Height; otherY++)
                {
                    Write(x + otherX, y + otherY, other._characterMap[otherX, otherY], other._colorMap[otherX, otherY], zIndex);
                }
            }
        }

        private bool IsIn(int x, int y)
        {
            return (x >= 0 || x < Width) && (y >= 0 || y < Height);
        }

        private void ExpandTo(int x, int y)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
