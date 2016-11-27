﻿using HtmlConsole.Dom;

namespace HtmlConsole.Rendering
{
    public class InlineRenderer : ElementRenderer
    {
        public override bool IsBlock => false;
        public override bool IsInline => true;

        public InlineRenderer(ElementNode domNode) : base(domNode)
        {
        }

        public override IRenderer Clone()
        {
            return new InlineRenderer((ElementNode)DomNode);
        }
    }
}