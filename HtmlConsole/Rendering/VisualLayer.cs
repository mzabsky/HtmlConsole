using System;
using System.Text;
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

        public Size Size { get; private set; }

        private char[,] _characterMap;
        private Color[,] _colorMap;
        private int[,] _zIndexMap;

        public VisualLayer(Size size)
        {
            Size = size;

            _characterMap = new char[size.Width, size.Height];
            _colorMap = new Color[size.Width, size.Height];
            _zIndexMap = new int[size.Width, size.Height];

            for (int y = 0; y < size.Height; y++)
            {
                for (int x = 0; x < size.Width; x++)
                {
                    _characterMap[x, y] = DefaultCharacter;
                    _colorMap[x, y] = DefaultColor;
                }
            }
        }

        public Color GetColor(Position position)
        {
            if (!IsIn(position))
            {
                return DefaultColor;
            }

            return _colorMap[position.X, position.Y];
        }

        public char GetCharacter(Position position)
        {
            if (!IsIn(position))
            {
                return DefaultCharacter;
            }

            return _characterMap[position.X, position.Y];
        }

        public void Write(Position position, string text, Color color, int zIndex)
        {
            if (Size.Width < position.X + text.Length || Size.Height < position.Y) ExpandTo(position);

            for (int i = 0; i < text.Length; i++)
            {
                Write(new Position(position.X + i, position.Y), text[i], color, zIndex);
            }
        }

        public void Write(Position position, char c, Color color, int zIndex)
        {
            if (Size.Width < position.X || Size.Height < position.Y) ExpandTo(position);

            if (zIndex >= _zIndexMap[position.X, position.Y])
            {
                _characterMap[position.X, position.Y] = c;
                _colorMap[position.X, position.Y] = color;
                _zIndexMap[position.X, position.Y] = zIndex;
            }
        }

        public void Write(Position position, VisualLayer other, int zIndex)
        {
            for (int otherY = 0; otherY < other.Size.Height; otherY++)
            {
                for (int otherX = 0; otherX < other.Size.Width; otherX++)
                {
                    Write(new Position(position.X + otherX, position.Y + otherY), other._characterMap[otherX, otherY], other._colorMap[otherX, otherY], zIndex);
                }
            }
        }

        public string GetText()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < Size.Height; y++)
            {
                for (int x = 0; x < Size.Width; x++)
                {
                    sb.Append(_characterMap[x, y]);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private bool IsIn(Position position)
        {
            return (position.X >= 0 || position.X < Size.Height) && (position.Y >= 0 || position.Y < Size.Height);
        }

        private void ExpandTo(Position position)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
