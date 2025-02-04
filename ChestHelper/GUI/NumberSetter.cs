﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StructureHelper.GUI;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StructureHelper.ChestHelper.GUI
{
    class NumberSetter : UIElement
    {
        public int Value;
        string Text;
        string Suffix;

        UIImageButton IncrementButton = new UIImageButton(ModContent.Request<Texture2D>("StructureHelper/GUI/Up"));
        UIImageButton DecrementButton = new UIImageButton(ModContent.Request<Texture2D>("StructureHelper/GUI/Down"));

        public NumberSetter(int value, string text, int xOff, string suffix = "")
        {
            Value = value;
            Text = text;
            Suffix = suffix;

            Width.Set(32, 0);
            Height.Set(32, 0);
            Left.Set(-xOff, 1);

            IncrementButton.Width.Set(12, 0);
            IncrementButton.Height.Set(8, 0);
            IncrementButton.Top.Set(7, 0);
            IncrementButton.OnLeftClick += (n, w) => Value++;
            Append(IncrementButton);

            DecrementButton.Width.Set(12, 0);
            DecrementButton.Height.Set(8, 0);
            DecrementButton.Top.Set(15, 0);
            DecrementButton.OnLeftClick += (n, w) => Value--;
            Append(DecrementButton);
        }

        public override void ScrollWheel(UIScrollWheelEvent evt)
        {
            Value += evt.ScrollWheelValue > 0 ? 1 : -1;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 pos = GetDimensions().ToRectangle().TopLeft();
            var target = new Rectangle((int)pos.X + 14, (int)pos.Y + 8, 20, 16);

            spriteBatch.Draw(Terraria.GameContent.TextureAssets.MagicPixel.Value, target, Color.Black * 0.5f);
            Utils.DrawBorderString(spriteBatch, Value.ToString() + Suffix, target.Center(), Color.White, 0.7f, 0.5f, 0.4f);

            if (Value < 1)
                Value = 1;

            if (IsMouseHovering)
                Main.hoverItemName = Text;

            base.Draw(spriteBatch);
        }
    }
}
