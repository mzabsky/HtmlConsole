﻿using HtmlConsole.Dom;

namespace HtmlConsole.Rendering
{
    public class BlockRenderer : ElementRenderer
    {
        public override bool IsBlock => true;
        public override bool IsInline => false;

        public BlockRenderer(ElementNode domNode) : base(domNode)
        {
        }

        public override IRenderer Clone()
        {
            return new BlockRenderer((ElementNode)DomNode);
        }
    }
}