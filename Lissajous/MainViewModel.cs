using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Lissajous
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly LissajousModel _model = new LissajousModel();

        private double _ax = 5;
        public double Ax { get => _ax; set { if (value > 0) { _ax = value; OnPropertyChanged(); } } }

        private double _ay = 5;
        public double Ay { get => _ay; set { if (value > 0) { _ay = value; OnPropertyChanged(); } } }

        private double _fx = 1;
        public double Fx { get => _fx; set { if (value > 0) { _fx = value; OnPropertyChanged(); } } }

        private double _fy = 2;
        public double Fy { get => _fy; set { if (value > 0) { _fy = value; OnPropertyChanged(); } } }

        private double _phiX = 90;
        public double PhiX { get => _phiX; set { _phiX = value; OnPropertyChanged(); } }

        private double _phiY = 0;
        public double PhiY { get => _phiY; set { _phiY = value; OnPropertyChanged(); } }

        private PointCollection _plotPoints;
        public PointCollection PlotPoints
        {
            get => _plotPoints;
            set { _plotPoints = value; OnPropertyChanged(); }
        }

        public ICommand PlotCommand { get; }

        public MainViewModel()
        {
            PlotCommand = new RelayCommand(_ => GeneratePlot());
            GeneratePlot();
        }

        private void GeneratePlot()
        {
            var rawPoints = _model.GeneratePoints(Ax, Ay, Fx, Fy, PhiX, PhiY).ToList();

            double viewportSize = 500;
            double center = viewportSize / 2;

            double maxAmplitude = Math.Max(Ax, Ay);
            double scale = (center - 20) / maxAmplitude;

            var uiPoints = new PointCollection();
            foreach (var p in rawPoints)
            {
                double screenX = center + p.X * scale;
                double screenY = center - p.Y * scale;
                uiPoints.Add(new Point(screenX, screenY));
            }

            PlotPoints = uiPoints;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}