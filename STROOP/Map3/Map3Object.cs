﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using STROOP.Controls.Map;
using OpenTK.Graphics.OpenGL;
using STROOP.Utilities;
using STROOP.Structs.Configurations;
using STROOP.Structs;
using OpenTK;

namespace STROOP.Map3
{
    public abstract class Map3Object : IDisposable
    {
        protected readonly Map3Graphics Graphics;

        public Map3Object(Map3Graphics graphics)
        {
            Graphics = graphics;
        }

        public abstract void DrawOnControl();

        public abstract void Dispose();
    }
}