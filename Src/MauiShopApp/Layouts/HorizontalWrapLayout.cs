﻿using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;

namespace MauiShopApp.Layouts
{
    public class HorizontalWrapLayout : StackLayout
    {
        public HorizontalWrapLayout()
        {
        }

        protected override ILayoutManager CreateLayoutManager()
        {
            return new HorizontalWrapLayoutManager(this);
        }
    }
}
