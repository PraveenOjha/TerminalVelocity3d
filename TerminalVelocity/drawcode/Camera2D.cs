using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerminalVelocity
{

    public class Camera2D
    {
        private Vector2 _position;
        private float _zoom;
        private float _rotation;
        private float _cameraSpeed;
        private Viewport _viewport;
        private Matrix _viewMatrix;
        private Matrix _viewMatrixIverse;

        public static float MinZoom = float.Epsilon;
        public static float MaxZoom = float.MaxValue;
        private float p1;
        private float p2;

        public Camera2D(Viewport viewport)
        {
            _viewMatrix = Matrix.Identity;
            _viewport = viewport;
            _cameraSpeed = 4.0f;
            _zoom = 1.0f;
            _rotation = 0.0f ;
            _position = Vector2.Zero;
        }

        public Camera2D(float p1, float p2)
        {
            // TODO: Complete member initialization
            this.p1 = p1;
            this.p2 = p2;
        }

        public void Move(Vector2 amount)
        {
            _position += amount;
        }

        public void Zoom(float amount)
        {
            _zoom += amount;
            _zoom = MathHelper.Clamp(_zoom, MaxZoom, MinZoom);
            UpdateViewTransform();
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; UpdateViewTransform(); }
        }

        public Matrix ViewMatrix
        {
            get { return _viewMatrix; }
        }


        private void UpdateViewTransform()
        {
            Matrix proj = Matrix.CreateTranslation(new Vector3(_viewport.Width * 0.5f, _viewport.Height * 0.5f, 0)) *
                          Matrix.CreateScale(new Vector3(1f, -1f, 1f));

            _viewMatrix = Matrix.CreateRotationZ(_rotation) *
                         Matrix.CreateScale(new Vector3(_zoom, _zoom, 1.0f)) *
                         Matrix.CreateTranslation(_position.X, _position.Y, 0.0f);

            _viewMatrix = proj * _viewMatrix;

        }



    }
}
