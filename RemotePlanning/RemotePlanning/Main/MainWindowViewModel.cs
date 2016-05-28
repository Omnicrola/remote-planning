using System;
using RemotePlanning.ViewModels;

namespace RemotePlanning.Main
{
    public class MainWindowViewModel : ViewModel
    {
        private const double SCALE_RATE = 1.1;

        private double _scaleX;
        private double _scaleY;

        public MainWindowViewModel()
        {
            ScaleX = 1;
            ScaleY = 1;
        }

        public double ScaleX
        {
            get { return _scaleX; }
            set { SetPropertyField(ref _scaleX, value); }
        }

        public double ScaleY
        {
            get { return _scaleY; }
            set { SetPropertyField(ref _scaleY, value); }
        }

        public void AdjustScale(int delta)
        {
            Console.WriteLine("Delta: " + delta);
            if (delta > 0)
            {
                ScaleX *= SCALE_RATE;
                ScaleY *= SCALE_RATE;
            }
            else
            {
                ScaleX /= SCALE_RATE;
                ScaleY /= SCALE_RATE;
            }
            Console.WriteLine($"X: {ScaleX} Y:{ScaleY}");
        }
    }
}