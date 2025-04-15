using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Guernica2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point start;
        private Point end;
        private Polyline _line;
        private Shape CurrentShape { get; set; }
       
        private enum Myshapes
        {
            Circle,
            Square,
            Line,
            Free
        }

        private Myshapes currentSelectedEnumShape;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Kollar om vänster musknapp är nedtryckt och kollar vilken form man valt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                end = e.GetPosition(MyCanvas);
             
                if (currentSelectedEnumShape == Myshapes.Circle)
                {
                    if (CurrentShape == null)
                    {
                        Ellipse newEllipse = new Ellipse();
                        MyCanvas.Children.Add(newEllipse);
                        CurrentShape = newEllipse;
                    }

                    var mycircle = CurrentShape as Ellipse;
                    DrawEllipse(mycircle, Brushes.BlueViolet);

                }
                else if (currentSelectedEnumShape == Myshapes.Line)
                {
                    if (CurrentShape == null)
                    {
                        Line line = new Line();
                        MyCanvas.Children.Add(line);
                        CurrentShape = line;
                    }

                    var myfree = CurrentShape as Line;
                    DrawLine(myfree, Brushes.DeepPink);
                }

                else if (currentSelectedEnumShape == Myshapes.Square)
                {
                    if (CurrentShape == null)
                    {
                        Rectangle newRectangle = new Rectangle();
                        MyCanvas.Children.Add(newRectangle);
                        CurrentShape = newRectangle;
                    }

                    var rectangle = CurrentShape as Rectangle;
                    DrawRectangle(rectangle, Brushes.IndianRed);
                }
                else if (currentSelectedEnumShape == Myshapes.Free)
                {
                    Line myLine = new Line();

                    MyCanvas.Children.Add(myLine);
                    CurrentShape = myLine;

                    var free = CurrentShape as Line;
                    DrawFree(free, Brushes.Black);
                    start = end;
                }
            }
            else
            {
                CurrentShape = null;
            }
        }

        private void Line_OnClick(object sender, RoutedEventArgs e)
        {
            currentSelectedEnumShape = Myshapes.Line;    
        }

        private void Free_OnClick(object sender, RoutedEventArgs e)
        {
            currentSelectedEnumShape = Myshapes.Free;
        }

        private void Circle_OnClick(object sender, RoutedEventArgs e)
        {
            currentSelectedEnumShape = Myshapes.Circle;
        }

        private void Square_OnClick(object sender, RoutedEventArgs e)
        {
            currentSelectedEnumShape = Myshapes.Square;
        }

        private void Canvas_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            start = e.GetPosition(MyCanvas);
        }

        private void DrawRectangle(Rectangle myRectangle, SolidColorBrush myBrush)
        {
            var i = CurrentShape;
            myRectangle.Stroke = myBrush;
            myRectangle.Fill = myBrush;

            if (end.X >= start.X)
            {
                // Defines the left part of the rectangle
                myRectangle.SetValue(Canvas.LeftProperty, start.X);
                myRectangle.Width = end.X - start.X;
            }
            else
            {
                myRectangle.SetValue(Canvas.LeftProperty, end.X);
                myRectangle.Width = start.X - end.X;
            }

            if (end.Y >= start.Y)
            {
                // Defines the top part of the rectangle
                myRectangle.SetValue(Canvas.TopProperty, start.Y);
                myRectangle.Height = end.Y - start.Y;
            }
            else
            {
                myRectangle.SetValue(Canvas.TopProperty, end.Y);
                myRectangle.Height = start.Y - end.Y;
            }
        }

        private void DrawLine(Line myline, SolidColorBrush Fill)
        {
            var i = CurrentShape;
            myline.Stroke = Fill;
            myline.X1 = start.X;
            myline.Y1 = start.Y;
            myline.X2 = end.X;
            myline.Y2 = end.Y;
        }

        private void DrawEllipse(Ellipse myellipse, SolidColorBrush Fill)
        {
            var i = CurrentShape;
            myellipse.Stroke = Fill;
            myellipse.Fill = Fill;

            if (end.X >= start.X)
            {
                // Defines the left part of the ellipse
                myellipse.SetValue(Canvas.LeftProperty, start.X);
                myellipse.Width = end.X - start.X;
            }
            else
            {
                myellipse.SetValue(Canvas.LeftProperty, end.X);
                myellipse.Width = start.X - end.X;
            }

            if (end.Y >= start.Y)
            {
                // Defines the top part of the ellipse
                myellipse.SetValue(Canvas.TopProperty, start.Y);
                myellipse.Height = end.Y - start.Y;
            }
            else
            {
                myellipse.SetValue(Canvas.TopProperty, end.Y);
                myellipse.Height = start.Y - end.Y;
            }
        }

        private void DrawFree(Line myline, SolidColorBrush Fill)
        {
            var i = CurrentShape;
            myline.Stroke = Fill;
            myline.StrokeThickness = 2.0;
            myline.X1 = start.X;
            myline.Y1 = start.Y;
            myline.X2 = end.X;
            myline.Y2 = end.Y;
        }

        private void Canvas_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CurrentShape = null;
        }

        private void MainWindow_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CurrentShape = null;
        }

        private void Clear_OnClick(object sender, RoutedEventArgs e)
        {
            MyCanvas.Children.Clear();
        }
    }
}

