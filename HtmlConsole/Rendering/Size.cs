namespace HtmlConsole.Rendering
{
    public struct Size
    {
        public int Width { get; }
        public int Height { get; }

        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override bool Equals(object obj)
        {
            return obj is Size && this == (Size)obj;
        }

        public override int GetHashCode()
        {
            return Width.GetHashCode() ^ Height.GetHashCode();
        }

        public static bool operator ==(Size a, Size b)
        {
            return a.Width == b.Width && a.Height == b.Height;
        }

        public static bool operator !=(Size a, Size b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return $"[{Width}, {Height}]";
        }
    }
}