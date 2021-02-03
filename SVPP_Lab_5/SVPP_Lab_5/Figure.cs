using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SVPP_Lab_5
{
    class Figure
    {
        private readonly Polygon _FigureObject;
        private Color _BackgroundColor;
        private Color _LineColor;
        private int _Border;
        private Point _MousePosition;
        public Polygon GetFigeure
        {
            get { return _FigureObject; }
        }

        public Color LineColor
        {
            get { return _LineColor; }
            set { _LineColor = value; }
        }
        public Color BackgroundColor
        {
            get { return _BackgroundColor; }
            set { _BackgroundColor = value; }
        }

        public int Border
        {
            get { return _Border; }
            set { _Border = value; }
        }

        public Figure(Point mousePosition, int border, Color lineColor, Color backgroundColor)
        {
            _BackgroundColor = backgroundColor;
            _LineColor = lineColor;
            _Border = border;
            _MousePosition = mousePosition;
            _FigureObject = new Polygon();
            PointCollection polygonPoints = new PointCollection
            {
                new Point(mousePosition.X - 30, mousePosition.Y),
                new Point(mousePosition.X, mousePosition.Y - 30),
                new Point(mousePosition.X + 30, mousePosition.Y),
                new Point(mousePosition.X, mousePosition.Y + 30)
            };

            _FigureObject.Points = polygonPoints;

            ColorBrush();
        }
        private void ColorBrush()
        {
            SolidColorBrush lineBrush = new SolidColorBrush
            {
                Color = _LineColor
            };
            SolidColorBrush backBrush = new SolidColorBrush
            {
                Color = _BackgroundColor
            };
            _FigureObject.Stroke = lineBrush;
            _FigureObject.Fill = backBrush;
            _FigureObject.StrokeThickness = _Border;

        }

        public override string ToString()
        {
            return _MousePosition.X.ToString() + " " + _MousePosition.Y.ToString() + " " + _FigureObject.Points.ToString() + " " + _LineColor.ToString() + " " + _BackgroundColor.ToString() + " " + _Border.ToString();
        }
    }
}
