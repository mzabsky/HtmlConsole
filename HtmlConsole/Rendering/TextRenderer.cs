﻿using HtmlConsole.Css;
using HtmlConsole.Dom;

namespace HtmlConsole.Rendering
{
    public class TextRenderer : Renderer
    {
        public override bool IsBlock => false;
        public override bool IsInline => true;

        public TextRenderer(TextNode domNode) : base(domNode)
        {
        }

        public override void Paint(VisualLayer target)
        {
            // TODO: Get proper z index
            target.Write(Position, ((TextNode)DomNode).Text, DomNode.Parent.GetStyleValue<ColorStyleValue>("color").Color, 0);
        }

        public override IRenderer Clone()
        {
            return new TextRenderer((TextNode)DomNode);
        }
    }
}