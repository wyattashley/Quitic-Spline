using MathNet.Numerics.LinearAlgebra;
using NavigationAlgorithmEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCWaypointPloter
{

    public class Spline
    {
        private int m_degree;
        private Matrix<double> coefficients;

        public Spline(int degree, Matrix<double> coe)
        {
            m_degree = degree;
            coefficients = coe;
        }

        public Spline(Matrix<double> coe)
        {
            m_degree = 5;
            coefficients = coe;
        }

        public Matrix<double> getCoefficients()
        {
            return coefficients;
        }

        public PoseWithCurvature getPoint(double t)
        {
            Matrix<double> polynomialBases = Matrix<double>.Build.DenseOfArray(new double[m_degree + 1, 1]);

            for (int i = 0; i <= m_degree; i++)
            {
                polynomialBases[i, 0] = Math.Pow(t, m_degree - i);
            }

            Matrix<double> mixed = coefficients * polynomialBases;

            double x = mixed[0, 0];
            double y = mixed[1, 0];

            double dx, dy, ddx, ddy;

            if (t == 0)
            {
                dx = coefficients[2, m_degree - 1];
                dy = coefficients[3, m_degree - 1];
                ddx = coefficients[4, m_degree - 2];
                ddy = coefficients[5, m_degree - 2];
            }
            else
            {
                dx = mixed[2, 0] / t;
                dy = mixed[3, 0] / t;

                ddx = mixed[4, 0] / t / t;
                ddy = mixed[5, 0] / t / t;
            }

            double curve = (dx * ddy - ddx * dy) / ((dx * dx + dy * dy) * Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2)));

            return new PoseWithCurvature(new Pose2d(new Point2d(x, y), new Rotation2d(dx, dy)), Rotation2d.fromRadians(curve));
        }
    }

    public class QuinticHermiteSpline
    {
        private Matrix<double> hermiteBasisMatrix;
        private List<Spline> splines = new List<Spline>();

        public QuinticHermiteSpline(List<Pose2d> waypoints, double horizontalScaler)
        {
            hermiteBasisMatrix = getHermiteBasis();

            for (int a = 0; a < waypoints.Count() - 1; a++)
            {
                ControlVectorPair controlVector = new ControlVectorPair(waypoints[a], waypoints[a + 1], horizontalScaler);

                var x = controlVector.getXControlMatrix();
                var y = controlVector.getYControlMatrix();

                var xCoeffs = (hermiteBasisMatrix * x).Transpose();
                var yCoeffs = (hermiteBasisMatrix * y).Transpose();
                
                Matrix<double> tmp = Matrix<double>.Build.DenseOfArray(new double[6, 6]);
                tmp.SetRow(0, xCoeffs.Row(0).ToArray());
                tmp.SetRow(1, yCoeffs.Row(0).ToArray());
                
                for(int i = 0; i < 6; i++)
                {
                    tmp[2, i] = tmp[0, i] * (5 - i);
                    tmp[3, i] = tmp[1, i] * (5 - i);
                }
                for(int i = 0; i < 6; i++)
                {
                    tmp[4, i] = tmp[2, i] * (4 - i);
                    tmp[5, i] = tmp[3, i] * (4 - i);
                }

                splines.Add(new Spline(tmp));
            }

            Console.WriteLine(splines.Count());
        }

        public QuinticHermiteSpline(List<Point2d> waypoints, double horizontalScaler, Rotation2d startAngle, Rotation2d endAngle)
        {
            List<Pose2d> pose = new List<Pose2d>();
            
            pose.Add(new Pose2d(waypoints[0], startAngle));

            for (int a = 1; a < waypoints.Count()-1; a++)
            {
                //var c = waypoints[a].vectorizeToNext(waypoints[a + 1]);
                //var d = waypoints[a-1].vectorizeToNext(waypoints[a]);
                //pose.Add(new Pose2d(c.getX(), c.getY(), Rotation2d.fromDegrees((c.getRotation().getDegrees() + d.getRotation().getDegrees()) / 2)));
                pose.Add(new Pose2d(waypoints[a], Rotation2d.fromDegrees(0)));
            }

            pose.Add(new Pose2d(waypoints[waypoints.Count()-1], endAngle));
            
            Console.WriteLine(pose.Count());
            
            QuinticHermiteSpline b = new QuinticHermiteSpline(pose, horizontalScaler);
            splines = b.getSplines();
        }

        public static Matrix<double> getHermiteBasis()
        {
            double[,] matrix = new double[,]
            {
                { -06.0, -03.0, -00.5, +06.0, -03.0, +00.5 },
                { +15.0, +08.0, +01.5, -15.0, +07.0, +01.0 },
                { -10.0, -06.0, -01.5, +10.0, -04.0, +00.5 },
                { +00.0, +00.0, +00.5, +00.0, +00.0, +00.0 },
                { +00.0, +01.0, +00.0, +00.0, +00.0, +00.0 },
                { +01.0, +00.0, +00.0, +00.0, +00.0, +00.0 }
            };
            return Matrix<double>.Build.DenseOfArray(matrix);
        }

        public List<Spline> getSplines()
        {
            return splines;
        }

        public List<PoseWithCurvature> getSplinePoints()
        {
            var splinePoints = new List<PoseWithCurvature>();

            splinePoints.Add(splines[0].getPoint(0.0));

            for(int i = 0; i < splines.Count(); i++)
            {
                var points = parameterizeSpline(splines[i]);
                points.RemoveAt(0);
                splinePoints.AddRange(points);
            }

            return splinePoints;
        }

        private List<PoseWithCurvature> parameterizeSpline(Spline spline)
        {
            return parameterizeSpline(spline, 0.0, 1.0, 0.00127, 0.127, 0.0872, 5000);
        }

        private List<PoseWithCurvature> parameterizeSpline(Spline spline, double t0, double t1, double kMaxDy, double kMaxDx, double kMaxDTheata, double kMaxIterations)
        {
            var splinePoints = new List<PoseWithCurvature>();

            splinePoints.Add(spline.getPoint(t0));

            Point2d current;
            PoseWithCurvature start;
            PoseWithCurvature end;

            List<Point2d> toRun = new List<Point2d>();
            toRun.Add(new Point2d(t0, t1));
            
            int index = 0;

            while(toRun.Count() != 0)
            {
                current = toRun[0];
                toRun.RemoveAt(0);
                start = spline.getPoint(current.getX());
                end = spline.getPoint(current.getY());

                var twist = start.pose.log(end.pose);

                //Console.WriteLine(start.pose.getRotation().getRadians() + "," + end.pose.getRotation().getRadians()); This is fine
                //Console.WriteLine(twist.getAngularComponent().getRadians());
                //Console.WriteLine(current.getX() + "," + current.getY() + "," + Math.Abs(twist.getLinearComponent().getY()) + "," + Math.Abs(twist.getLinearComponent().getX()) + "," + Math.Abs(twist.getAngularComponent().getRadians()) + ","  + start.pose.getX() + "," + start.pose.getY() + "," + end.pose.getX() + "," + end.pose.getY());

                if (Math.Abs(twist.getLinearComponent().getY()) > kMaxDy
                    || Math.Abs(twist.getLinearComponent().getX()) > kMaxDx
                    || Math.Abs(twist.getAngularComponent().getRadians()) > kMaxDTheata)
                {
                    toRun.Insert(0, new Point2d((current.getX() + current.getY()) / 2, current.getY()));
                    //Console.WriteLine("Adding " + ((current.getX() + current.getY()) / 2) + " and " + current.getY());

                    toRun.Insert(0, new Point2d(current.getX(), (current.getX() + current.getY()) / 2));
                    //Console.WriteLine("Adding " + (current.getX()) + " and " + ((current.getX() + current.getY()) / 2));
                }
                else
                {
                    splinePoints.Add(spline.getPoint(current.getY()));
                }

                index++;

                if(index >= kMaxIterations)
                {
                    throw new Exception("Could Not parameterize a malformed spline");
                }
            }

            return splinePoints;
        }
    }

    public class ControlVectorPair
    {
        private Pose2d poseInitial;
        private Pose2d poseFinal;
        private double scalar;

        public ControlVectorPair(Pose2d pose1, Pose2d pose2, double _horizontalScalar)
        {
            poseInitial = pose1;
            poseFinal = pose2;
            scalar = _horizontalScalar * pose1.getTranslation().getDistance(pose2.getTranslation());
        }

        public Matrix<double> getXControlMatrix()
        {
            if (poseInitial == null || poseFinal == null)
                throw new ArgumentNullException("Posed passed is null");
            return Matrix<double>.Build.DenseOfArray(new double[,]
            {
                { poseInitial.getX() }, { poseInitial.getRotation().getCos() * scalar }, {0.0 },
                { poseFinal.getX() }, { poseFinal.getRotation().getCos() * scalar }, { 0.0 }
            });
        }

        public Matrix<double> getYControlMatrix()
        {
            if (poseInitial == null || poseFinal == null)
                throw new ArgumentException("Pose passed is null");

            return Matrix<double>.Build.DenseOfArray(new double[,]
            {
                { poseInitial.getY() }, { poseInitial.getRotation().getSin() * scalar }, { 0.0 },
                { poseFinal.getY() }, { poseFinal.getRotation().getSin() * scalar }, { 0.0 }
            });
        }
    }
}
