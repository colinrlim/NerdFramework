﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdFramework
{
    public class MeshTriangle3Collection
    {
        public List<MeshTriangle3> triangles;

        private Vector3 _origin = Vector3.Zero;
        private Vector3 _scale = Vector3.One;

        public Vector3 origin
        {
            get => _origin;
            set
            {
                if (_origin == value) return;

                Vector3 amount = value - _origin;
                foreach (Triangle3 triangle in triangles)
                {
                    triangle.Move(amount);
                }
                _origin = value;
            }
        }
        public Vector3 scale
        {
            get => _scale;
            set
            {
                if (_scale == value) return;

                Vector3 amount = value / _scale;
                foreach (Triangle3 triangle in triangles)
                {
                    triangle.Scale(amount, _origin);
                }
                _scale = value;
            }
        }

        public MeshTriangle3Collection(List<MeshTriangle3> triangles)
        {
            this.triangles = triangles;
        }

        public MeshTriangle3Collection(params MeshTriangle3[] triangles)
        {
            List<MeshTriangle3> triangleList = new List<MeshTriangle3>();
            for (int i = 0; i < triangles.Length; i++)
            {
                triangleList.Add(triangles[i]);
            }
            this.triangles = triangleList;
        }

        public MeshTriangle3Collection Clone()
        {
            Vector3 oldOrigin = this.origin;
            Vector3 oldScale = this.scale;
            this.origin = Vector3.Zero;
            this.scale = Vector3.One;
            List<MeshTriangle3> clonedTriangles = new List<MeshTriangle3>();
            Parallel.ForEach(triangles, triangle =>
            {
                clonedTriangles.Add(new MeshTriangle3(triangle.a, triangle.b, triangle.c, triangle.textureU, triangle.textureV, triangle.textureW, triangle.normalA, triangle.normalB, triangle.normalC));
            });
            this.origin = oldOrigin;
            this.scale = oldScale;

            MeshTriangle3Collection clone = new MeshTriangle3Collection(clonedTriangles);
            clone.origin = oldOrigin;
            clone.scale = oldScale;

            return clone;
        }

        public MeshTriangle3Collection Inverted()
        {
            MeshTriangle3Collection clone = Clone();
            Parallel.ForEach(triangles, triangle =>
            {
                Vector3 temp = triangle.b;
                triangle.b = triangle.c;
                triangle.c = temp;
            });

            return clone;
        }

        /*public Box3 Bounds()
        {
            Vector3 min = triangles[0].a;
            Vector3 max = triangles[0].a;
            for (int i = 0; i < triangles.Count; i++)
            {
                if (triangles.a)
            }
        }*/

        public static void Move(List<MeshTriangle3> triangles, Vector3 offset)
        {
            Parallel.ForEach(triangles, triangle =>
            {
                triangle.Move(offset);
            });
        }

        public static void Scale(List<MeshTriangle3> triangles, Vector3 scale, Vector3 origin)
        {
            Parallel.ForEach(triangles, triangle =>
            {
                triangle.Scale(scale, origin);
            });
        }

        public bool Meets(Ray3 ray)
        {
            bool meets = false;
            Parallel.ForEach(triangles, triangle =>
            {
                if (triangle.Meets(ray)) {
                    meets = true;
                    return;
                }
            });
            return meets;
        }

        public void RotateX(double radians, Vector3 origin)
        {
            Vector3 offset = origin + _origin;
            Parallel.ForEach(triangles, triangle =>
            {
                triangle.RotateX(radians, offset);
            });
            //_origin = (_origin - origin).RotateX(radians) + origin;
        }

        public void RotateY(double radians, Vector3 origin)
        {
            Vector3 offset = origin + _origin;
            Parallel.ForEach(triangles, triangle =>
            {
                triangle.RotateY(radians, offset);
            });
            //_origin = (_origin - origin).RotateY(radians) + origin;
        }

        public void RotateZ(double radians, Vector3 origin)
        {
            Vector3 offset = origin + _origin;
            Parallel.ForEach(triangles, triangle =>
            {
                triangle.RotateZ(radians, offset);
            });
            //_origin = (_origin - origin).RotateZ(radians) + origin;
        }

        public void Rotate(double r1, double r2, double r3, Vector3 origin)
        {
            Vector3 offset = origin + _origin;
            Parallel.ForEach(triangles, triangle =>
            {
                triangle.Rotate(r1, r2, r3, offset);
            });
            //_origin = (_origin - origin).Rotate(r1, r2, r3) + origin;
        }
    }
}
