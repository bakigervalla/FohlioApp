using System;
using System.Drawing;
using System.Windows.Forms;

namespace Fohlio.RevitReportsIntegration.Views.BackgroundWorker
{
    internal class PersentageProgressBar : ProgressBar
    {
        public PersentageProgressBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | 
                ControlStyles.UserPaint | 
                ControlStyles.OptimizedDoubleBuffer | 
                ControlStyles.ResizeRedraw, true);
        }

        public TextProgressStyle TextProgressStyle
        {
            get;
            set;
        }

        public bool ShowProgress { get; set; }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var rect = ClientRectangle;

            if (ProgressBarRenderer.IsSupported)
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, rect);

            rect.Inflate(-2, -2);
            if (Value > 0)
            {
                var clip = new Rectangle(rect.X, rect.Y, (int)Math.Round(((float)Value / Maximum) * rect.Width), rect.Height);
                if (ProgressBarRenderer.IsSupported)
                    ProgressBarRenderer.DrawHorizontalChunks(e.Graphics, clip);                
            }

            if (ShowProgress)
            {
                var textToDisplay = string.Empty;
                switch (TextProgressStyle)
                {
                    case TextProgressStyle.Percent:
                        textToDisplay = $"{Value/(double) Maximum:P0}";
                        break;
                    case TextProgressStyle.Count:
                        textToDisplay = $"{Value}/{Maximum}";
                        break;
                }

                
                TextRenderer.DrawText(e.Graphics,
                                      textToDisplay,
                                      SystemFonts.StatusFont,
                                      ClientRectangle,
                                      Color.Black,                                      
                                      TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                    );
            }
        }    
    }
}