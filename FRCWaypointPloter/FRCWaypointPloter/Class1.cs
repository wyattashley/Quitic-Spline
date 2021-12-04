using System;
using System.Collections.Generic;

namespace NavigationAlgorithmEngine
{
    public class Point2d
    {
        private double x, y;

        public Point2d(double _x, double _y)
        {
            x = _x;
            y = _y;
        }

        public double getX()
        {
            return x;
        }

        public double getY()
        {
            return y;
        }

        public double getDistance(Point2d other)
        {
            return Math.Sqrt(Math.Pow(other.getX() - x, 2) + Math.Pow(other.getY() - y, 2));
        }

        public double getNorm()
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }

        public Point2d rotateBy(Rotation2d angle)
        {
            return new Point2d(x * angle.getCos() - y * angle.getSin(), x * angle.getSin() + y * angle.getCos());
        }

        public Point2d plus(Point2d other)
        {
            return new Point2d(x + other.getX(), y + other.getY());
        }

        public Point2d minus(Point2d other)
        {
            return new Point2d(x - other.getX(), y - other.getY());
        }

        public Point2d unaryMinus()
        {
            return new Point2d(-x, -y);
        }

        public Point2d times(double scalar)
        {
            return new Point2d(x * scalar, y * scalar);
        }

        public double getVectorMagnitude(Point2d other)
        {
            return Math.Sqrt(Math.Pow(getX() - other.getX(), 2) + Math.Pow(getY() - other.getY(), 2));
        }

        public Pose2d vectorizeNormal(Point2d other)
        {
            double mag = getVectorMagnitude(other);
            double x = Math.Abs(getX() - other.getX()) / mag;
            double y = Math.Abs(getY() - other.getY()) / mag;
            Rotation2d r = Rotation2d.fromRadians(Math.Atan2(y, x));

            return new Pose2d(x, y, r);
        }

        public Pose2d vectorizeToNext(Point2d other)
        {
            double mag = getVectorMagnitude(other);
            double x = (getX() - other.getX()) / mag;
            double y = (getY() - other.getY()) / mag;
            Rotation2d r = Rotation2d.fromRadians(Math.Atan2(x, y));

            return new Pose2d(other.getX(), other.getY(), r);
        }

    }
    
    public class Rotation2d
    {
        private double angleRadians;
        private double angleCos;
        private double angleSin;

        private Rotation2d(double radians)
        {
            angleRadians = radians;
            angleCos = Math.Cos(radians);
            angleSin = Math.Sin(radians);
        }

        public Rotation2d(double x, double y)
        {
            double magnitude = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            if (magnitude > 1e-6)
            {
                angleSin = y / magnitude;
                angleCos = x / magnitude;
            } else
            {
                angleSin = 0.0;
                angleCos = 1.0;
            }

            angleRadians = Math.Atan2(angleSin, angleCos);
        }

        public static Rotation2d fromDegrees(double value)
        {
            return new Rotation2d(degreeToRadians(value));
        }

        public static Rotation2d fromRadians(double value)
        {
            return new Rotation2d(value);
        }

        private static double radiansToDegree(double radians)
        {
            return (180.0 / Math.PI) * radians;
        }

        public static double degreeToRadians(double degree)
        {
            return (Math.PI / 180.0) * degree;
        }

        public double getDegrees()
        {
            return radiansToDegree(angleRadians);
        }

        public double getRadians()
        {
            return angleRadians;
        }

        public double getCos()
        {
            return angleCos;
        }

        public double getSin()
        {
            return angleSin;
        }

        public Rotation2d rotateBy(Rotation2d other)
        {
            return new Rotation2d(angleCos * other.getCos() - angleSin * other.getSin(), angleCos * other.getSin() + angleSin * other.getCos());
        }

        public Rotation2d plus(Rotation2d other)
        {
            return rotateBy(other);
        }
        
        public Rotation2d minus(Rotation2d other)
        {
            return rotateBy(other.unaryMinus());
        }

        public Rotation2d unaryMinus()
        {
            return new Rotation2d(-angleRadians);
        }
    }

    public class Pose2d
    {
        private Point2d point;
        private Rotation2d angle;

        public Pose2d(Point2d _point, Rotation2d _angle)
        {
            point = _point;
            angle = _angle;
        }

        public Pose2d(double x, double y, Rotation2d _angle)
        {
            point = new Point2d(x, y);
            angle = _angle;
        }

        public Point2d getTranslation()
        {
            return point;
        }

        public double getX()
        {
            return point.getX();
        }

        public double getY()
        {
            return point.getY();
        }

        public Rotation2d getRotation()
        {
            return angle;
        }
        
        public Pose2d plus(Transform2d other)
        {
            return transformBy(other);
        }

        public Pose2d transformBy(Transform2d other)
        {
            return new Pose2d(point.plus(other.getTranslation().rotateBy(angle)), angle.plus(other.getRotation()));
        }

        public Pose2d relativeTo(Pose2d other)
        {
            Transform2d transform = new Transform2d(other, this);
            return new Pose2d(transform.getTranslation(), transform.getRotation());
        }

        public Twist2d log(Pose2d end)
        {
            var transform = end.relativeTo(this);
            var dtheta = transform.getRotation().getRadians();
            var halfDTheata = dtheta / 2.0;

            var cosMinusOne = transform.getRotation().getCos() - 1;

            double halfThetaByTanOfHalftheta;
            if (Math.Abs(cosMinusOne) < 1E-9)
            {
                halfThetaByTanOfHalftheta = 1.0 - 1.0 / 12.0 * dtheta * dtheta;
            } else
            {
                halfThetaByTanOfHalftheta = -(halfDTheata * transform.getRotation().getSin()) / cosMinusOne;
            }

            Point2d point = transform.getTranslation()
                .rotateBy(new Rotation2d(halfThetaByTanOfHalftheta, -halfDTheata))
                .times(Math.Sqrt(Math.Pow(halfThetaByTanOfHalftheta, 2) + Math.Pow(halfDTheata, 2)));
           

            return new Twist2d(point, Rotation2d.fromRadians(dtheta));
        }

        override
        public String ToString()
        {
            return "X: " + getX() + " Y: " + getY() + " R: " + getRotation().getDegrees();
        }
    }

    public class Transform2d
    {
        private Point2d point;
        private Rotation2d rotation;

        public Transform2d(Pose2d inital, Pose2d last)
        {
            point = last.getTranslation().minus(inital.getTranslation()).rotateBy(inital.getRotation().unaryMinus());

            rotation = last.getRotation().minus(inital.getRotation());
        }

        public Transform2d(Point2d _translation, Rotation2d _rotation)
        {
            point = _translation;
            rotation = _rotation;
        }

        public Point2d getTranslation()
        {
            return point;
        }

        public Rotation2d getRotation()
        {
            return rotation;
        }
    }

    public class Twist2d
    {

        private Point2d xyChange;
        private Rotation2d thetaChange;

        public Twist2d(Point2d _xy, Rotation2d theta)
        {
            xyChange = _xy;
            thetaChange = theta;
        }

        public Point2d getLinearComponent()
        {
            return xyChange;
        }

        public Rotation2d getAngularComponent()
        {
            return thetaChange;
        }
    }

    public class PoseWithCurvature
    {
        public Pose2d pose;
        public Rotation2d curvature; //Angle Unit Per Foot

        public PoseWithCurvature(Pose2d _pose, Rotation2d curve)
        {
            this.pose = _pose;
            this.curvature = curve;
        }
    }

    public struct WayPoint
    {
        public double x;
        public double y;
        public double v;

        public WayPoint(double _x, double _y, double _v)
        {
            x = _x;
            y = _y;
            v = _v;
        }
    }

    public class PurePersuit
    {
        private List<WayPoint> wayPoints;
        private List<WayPoint> injectedWayPointList;

        public PurePersuit(List<WayPoint> _wayPoints)
        {
            this.wayPoints = _wayPoints;
            this.injectedWayPointList = new List<WayPoint>();
        }

        public void updateWayPoints(WayPoint _wayPoints)
        {
            this.wayPoints.Add(_wayPoints);
        }

        public List<WayPoint> injectPoints(double spacing)
        {
            for(int path = 0; path < wayPoints.Count - 1; path++)
            {
                double vectorMag = getVectorMagnitude(wayPoints[path], wayPoints[path + 1]);
                double pointToAdd = Math.Ceiling(vectorMag / spacing);
                WayPoint vectorNormilized = normilizeVector(wayPoints[path], wayPoints[path + 1]);
                

                for (int i = 0; i < pointToAdd; i++)
                {
                    WayPoint tmp = new WayPoint();
                    tmp.x = wayPoints[path].x + ((spacing * vectorNormilized.x) * i);
                    tmp.y = wayPoints[path].y + ((spacing * vectorNormilized.y) * i);
                    tmp.v = wayPoints[path].v + ((spacing * vectorNormilized.v) * i);

                    injectedWayPointList.Add(tmp);
                }
            }

            return injectedWayPointList;
        }

        public WayPoint[] smoother(double a, double b, double tolerance)
        {
            WayPoint[] newPath = injectedWayPointList.ToArray();
            double change = tolerance;
            while (change >= tolerance)
            {
                change = 0.0;
                double aux = 0;
                for (int i = 1; i < injectedWayPointList.Count - 1; i++)
                {
                    aux = newPath[i].x;
                    newPath[i].x += a * (injectedWayPointList[i].x - newPath[i].x) + b * (newPath[i - 1].x + newPath[i + 1].x - (2.0 * newPath[i].x));
                    change += Math.Abs(aux - newPath[i].x);
                }
                
                for (int i = 1; i < injectedWayPointList.Count - 1; i++)
                {
                    aux = newPath[i].y;
                    newPath[i].y += a * (injectedWayPointList[i].y - newPath[i].y) + b * (newPath[i - 1].y + newPath[i + 1].y - (2.0 * newPath[i].y));
                    change += Math.Abs(aux - newPath[i].y);
                }
            }

            for (int i = 0; i < newPath.Length; i++)
            {
                Console.WriteLine("(" + newPath[i].x + "," + newPath[i].y + ")");
            }

            return newPath;
        }

        public void printInjectedPoints()
        {
            for (int i = 0; i < injectedWayPointList.Count; i++) {
                Console.WriteLine("(" + injectedWayPointList[i].x + "," + injectedWayPointList[i].y + "," + injectedWayPointList[i].v + ")");
            }
        }

        public double getVectorMagnitude(WayPoint point1, WayPoint point2)
        {
            return Math.Sqrt(Math.Pow(point1.x - point2.x, 2) + Math.Pow(point1.y - point2.y, 2));
        }

        public WayPoint normilizeVector(WayPoint point1, WayPoint point2)
        {
            double mag = getVectorMagnitude(point1, point2);

            WayPoint tmp = new WayPoint();
            tmp.x = Math.Abs(point1.x - point2.x) / mag;
            tmp.y = Math.Abs(point1.y - point2.y) / mag;
            tmp.v = Math.Abs(point1.v - point2.v) / mag;

            return tmp;
        }
    }
}
