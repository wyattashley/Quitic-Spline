using NavigationAlgorithmEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FRCWaypointPloter
{
    public partial class Plotter : Form
    {
        Series series;
        Series pathSeries;
        PurePersuit purePersuit;

        List<Point2d> selectedPositions = new List<Point2d>();

        public Plotter()
        {
            InitializeComponent();

            series = mainChart.Series[0];

            series.ChartType = SeriesChartType.Point;

            mainChart.ChartAreas[0].AxisX.Enabled = AxisEnabled.False;
            mainChart.ChartAreas[0].AxisX.IsLabelAutoFit = false;
            mainChart.ChartAreas[0].AxisX.ScrollBar.Enabled = false;
            mainChart.ChartAreas[0].AxisX.ScaleView.Size = 50.4375;
            mainChart.ChartAreas[0].AxisX.Maximum = 50.4375;
            mainChart.ChartAreas[0].AxisX.Minimum = 0;

            mainChart.ChartAreas[0].AxisY.Enabled = AxisEnabled.False;
            mainChart.ChartAreas[0].AxisY.IsLabelAutoFit = false;
            mainChart.ChartAreas[0].AxisY.ScrollBar.Enabled = false;
            mainChart.ChartAreas[0].AxisY.ScaleView.Size = 26.9375;
            mainChart.ChartAreas[0].AxisY.Maximum = 26.9375;
            mainChart.ChartAreas[0].AxisY.Minimum = 0;

            mainChart.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            mainChart.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount;

            mainChart.ChartAreas[0].CursorY.Position = 0;

            /*
            List<WayPoint> points = new List<WayPoint>();
            WayPoint tmp = new WayPoint();
            tmp.x = 0.0;
            tmp.y = 0.0;
            tmp.v = 0.0;
            points.Add(tmp);
            series.Points.AddXY(0, 0);

            tmp = new WayPoint();
            tmp.x = 4.0;
            tmp.y = 4.0;
            tmp.v = 0.0;
            points.Add(tmp);
            series.Points.AddXY(4, 4);

            tmp = new WayPoint();
            tmp.x = 14.0;
            tmp.y = 5.0;
            tmp.v = 0.0;
            points.Add(tmp);
            series.Points.AddXY(14, 5);

            tmp = new WayPoint();
            tmp.x = 14.0;
            tmp.y = 12.0;
            tmp.v = 0.0;
            points.Add(tmp);
            series.Points.AddXY(14, 12);
            */
            /*
            List<Pose2d> points = new List<Pose2d>();
            points.Add(new Pose2d(new Point2d(0, 0), Rotation2d.fromDegrees(0)));
            points.Add(new Pose2d(new Point2d(8, 5), Rotation2d.fromDegrees(0)));
            points.Add(new Pose2d(new Point2d(15, 8), Rotation2d.fromDegrees(0)));

            QuinticHermiteSpline spline = new QuinticHermiteSpline(points, 1.2);//0.8
            List<PoseWithCurvature> poseWithCurvature = spline.getSplinePoints();

            int amount = poseWithCurvature.Count();
            for (int i = 0; i < amount; i++)
            {
                series.Points.AddXY(poseWithCurvature[i].pose.getX(), poseWithCurvature[i].pose.getY());
                Console.WriteLine(poseWithCurvature[i].pose.getX() + "," + poseWithCurvature[i].pose.getY());
            }
            */
            /*
            PurePersuit pure = new PurePersuit(points);
            List<WayPoint> injectedPoints = pure.injectPoints(.5);
            WayPoint[] final = pure.smoother(.02, .98, .005);

            for (int i = 0; i < final.Length; i++)
            {
                series.Points.AddXY(Math.Round(final[i].x, 2), Math.Round(final[i].y, 2));
            }
            
            pathSeries = new Series("Path");
            mainChart.Series.Add(pathSeries);
            pathSeries.ChartType = SeriesChartType.Point;

            purePersuit = new PurePersuit(new List<WayPoint>());
            */
            //values.View = View.Details;
            selectedPositions.Add(new Point2d(0, 0));
        }

        private void addPoint(object sender, EventArgs e)
        {
            double x = mainChart.ChartAreas[0].CursorX.Position;
            double y = mainChart.ChartAreas[0].CursorY.Position;
            series.Points.Clear();
            values.Items.Add("(" + x + ", " + y + ")");
            selectedPositions.Add(new Point2d(x, y));

            /*
            List<Pose2d> list = new List<Pose2d>();

            if (selectedPositions.Count() == 1)
            {
                list.Add(new Pose2d(0, 0, Rotation2d.fromDegrees(0)));
                list.Add(new Pose2d(x, y, Rotation2d.fromDegrees(0)));
            }
            else
            {
                list.Add(selectedPositions[selectedPositions.Count() - 1]);
                list.Add(selectedPositions[selectedPositions.Count() - 2]);
            }
            */

            QuinticHermiteSpline spline = new QuinticHermiteSpline(selectedPositions, 0.8, Rotation2d.fromDegrees(0), Rotation2d.fromDegrees(180));//0.8
            List<PoseWithCurvature> poseWithCurvature = spline.getSplinePoints();

            int amount = poseWithCurvature.Count();
            for (int i = 0; i < amount; i++)
            {
                series.Points.AddXY(poseWithCurvature[i].pose.getX(), poseWithCurvature[i].pose.getY());
                //Console.WriteLine(poseWithCurvature[i].pose.getX() + "," + poseWithCurvature[i].pose.getY());
            }

            if (PurePursuitEnabled.Checked)
            {
                purePersuit.updateWayPoints(new WayPoint(x, y, 0));
                purePersuit.injectPoints(1);
                WayPoint[] path = purePersuit.smoother(0.15, 0.85, 0.001);
                pathSeries.Points.Clear();
                for (int i = 0; i < path.Length; i++)
                {
                    pathSeries.Points.AddXY(path[i].x, path[i].y);
                }
            }
        }

        private void mainChart_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePoint = new Point(e.X, e.Y);

            mainChart.ChartAreas[0].CursorX.Interval = 0.25;
            mainChart.ChartAreas[0].CursorY.Interval = 0.25;
            mainChart.ChartAreas[0].CursorX.SetCursorPixelPosition(mousePoint, true);
            mainChart.ChartAreas[0].CursorY.SetCursorPixelPosition(mousePoint, true);

            try
            {
                xInput.Value = (decimal)mainChart.ChartAreas[0].CursorX.Position;
                yInput.Value = (decimal)mainChart.ChartAreas[0].CursorY.Position;
            } catch (Exception error)
            {}
            //labelX.Text = "X: " + mainChart.ChartAreas[0].CursorX.Position;
            //labelY.Text = "Y: " + mainChart.ChartAreas[0].CursorY.Position;
        }

        private void Input_ValueChanged(object sender, EventArgs e)
        {
            mainChart.ChartAreas[0].CursorX.SetCursorPosition((double) xInput.Value);
            mainChart.ChartAreas[0].CursorY.SetCursorPosition((double) yInput.Value);
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            if (Export.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(Export.FileName);
                DataPointCollection points = series.Points;

                writer.WriteLine("FieldID,0");
                writer.WriteLine("X,Y");
                for (int i = 0; i < points.Count; i++)
                {
                    writer.WriteLine(points[i].XValue.ToString() + "," + points[i].YValues[0].ToString());
                }
                writer.Close();
            }
        }
        
    }
}
