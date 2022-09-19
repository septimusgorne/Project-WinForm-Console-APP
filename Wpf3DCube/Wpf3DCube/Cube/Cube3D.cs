using System.Windows.Media;
using System.Windows.Media.Media3D;



namespace Wpf3DCube.Cube
{

    public class Cube3D : ModelVisual3D
    {
        private readonly static Brush _defaultColor = Brushes.Gray;

        public Cube3D()
        {
            DrawCube(_size, _pos, _front, _top, _left, _right, _bottom, _back);
        }


        private double _size = 0.5;//0.5
        public double Size
        {
            get => _size;
            set
            {
                _size = value;

                DrawCube(_size, _pos, _front, _top, _left, _right, _bottom, _back);
            }
        }

        private Point3D _pos;
        public Point3D Position
        {
            get => _pos;
            set
            {
                _pos = value;

                DrawCube(_size, _pos, _front, _top, _left, _right, _bottom, _back);
            }
        }

        // Материалы граней
        private Brush _front = _defaultColor;
        public Brush Front
        {
            get => _front;
            set
            {
                _front = value;
                DrawCube(_size, _pos, _front, _top, _left, _right, _bottom, _back);
            }
        }

        private Brush _top = _defaultColor;
        public Brush Top
        {
            get => _top;
            set
            {
                _top = value;
                DrawCube(_size, _pos, _front, _top, _left, _right, _bottom, _back);
            }
        }

        private Brush _left = _defaultColor;
        public Brush Left
        {
            get => _left;
            set
            {
                _left = value;
                DrawCube(_size, _pos, _front, _top, _left, _right, _bottom, _back);
            }
        }

        private Brush _right = _defaultColor;
        public Brush Right
        {
            get => _right;
            set
            {
                _right = value;
                DrawCube(_size, _pos, _front, _top, _left, _right, _bottom, _back);
            }
        }

        private Brush _back = _defaultColor;
        public Brush Back
        {
            get => _back;
            set
            {
                _back = value;
                DrawCube(_size, _pos, _front, _top, _left, _right, _bottom, _back);
            }
        }

        private Brush _bottom = _defaultColor;
        public Brush Bottom
        {
            get => _bottom;
            set
            {
                _bottom = value;
                DrawCube(_size, _pos, _front, _top, _left, _right, _bottom, _back);
            }
        }


        private static GeometryModel3D AddFace(
            Point3D point1,
            Point3D point2,
            Point3D point3,
            Point3D point4,
            Material material)
        {
            GeometryModel3D geometryModel3D = new()
            {
                Geometry = new MeshGeometry3D()
                {
                    Positions = new()
                    {
                        point1,
                        point2,
                        point3,
                        point3,
                        point4,
                        point1
                    }
                },
                Material = material
            };

            return geometryModel3D;
        }


        private void DrawCube(
            double size,
            Point3D pos,
            Brush front,
            Brush top,
            Brush left,
            Brush right,
            Brush bottom,
            Brush back)
        {
            // Отсчёт точек от левого нижнего угла грани.


            // Размерности граней симметричны в обе стороны в абсолютных величинах.
            double absX = size / 2;//2
            double absY = size / 2;//2
            double absZ = size / 2;//2


            Point3D front_left_bottom = new(-absX + pos.X, -absY + pos.Y, absZ + pos.Z);
            Point3D front_right_bottom = new(absX + pos.X, -absY + pos.Y, absZ + pos.Z);
            Point3D front_right_top = new(absX + pos.X, absY + pos.Y, absZ + pos.Z);
            Point3D front_left_top = new(-absX + pos.X, absY + pos.Y, absZ + pos.Z);
            Point3D backside_right_top = new(absX + pos.X, absY + pos.Y, -absZ + pos.Z);
            Point3D backside_left_top = new(-absX + pos.X, absY + pos.Y, -absZ + pos.Z);
            Point3D backside_left_bottom = new(-absX + pos.X, -absY + pos.Y, -absZ + pos.Z);
            Point3D backside_right_bottom = new(absX + pos.X, -absY + pos.Y, -absZ + pos.Z);


            Model3DGroup m3dg = new();

            // 1 Передняя
            DiffuseMaterial material = new(front);
            GeometryModel3D faceFront = AddFace(
                    front_left_bottom,
                    front_right_bottom,
                    front_right_top,
                    front_left_top,
                    material);

            m3dg.Children.Add(faceFront);

            // 2 Верхняя
            material = new(top);
            GeometryModel3D faceTop =
                AddFace(
                    front_left_top,
                    front_right_top,
                    backside_right_top,
                    backside_left_top,
                    material);
            m3dg.Children.Add(faceTop);

            // 3 Левая
            material = new(left);
            GeometryModel3D faceLeft =
                AddFace(
                    backside_left_bottom,
                    front_left_bottom,
                    front_left_top,
                    backside_left_top,
                    material);
            m3dg.Children.Add(faceLeft);

            // 4 Правая
            material = new(right);
            GeometryModel3D faceRight =
                AddFace(
                    front_right_bottom,
                    backside_right_bottom,
                    backside_right_top,
                    front_right_top,
                    material);
            m3dg.Children.Add(faceRight);

            // 5 Нижняя
            material = new(bottom);
            GeometryModel3D faceBottom =
                AddFace(
                    backside_left_bottom,
                    backside_right_bottom,
                    front_right_bottom,
                    front_left_bottom,
                    material);
            m3dg.Children.Add(faceBottom);

            // 6 Задняя
            material = new(back);
            GeometryModel3D faceBack =
                AddFace(
                    backside_right_bottom,
                    backside_left_bottom,
                    backside_left_top,
                    backside_right_top,
                    material);
            m3dg.Children.Add(faceBack);


            Content = m3dg;
        }
    }
}
