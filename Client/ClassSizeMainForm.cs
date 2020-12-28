using System;
using System.Collections.Generic;
using System.Text;

namespace Client
{
    [Serializable]
    class SizeMainForm
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public SizeMainForm(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
        }
    }
}
