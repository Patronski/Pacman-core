﻿namespace Pacman.Handlers
{
    using Globals;
    using Interfaces;
    using Models;
    using Enums;
    using Microsoft.Xna.Framework;

    public abstract class ObjectMover : IMovable
    {
        protected GameObject gameObject;
        protected Direction currentDir;
        protected bool[,] obstacles;
        protected int pixelMoved; //inicialize how many pixels will move per iteration

        public int PixelMoved
        {
            get { return pixelMoved; }
        }

        public ObjectMover(GameObject gameObject, IMatrix levelMatrix)
        {
            this.gameObject = gameObject;
            this.currentDir = Direction.Right;
            this.obstacles = new bool[Global.YMax, Global.XMax];

            foreach (var coordinate in levelMatrix.Level.LevelCoordinates)
            {
                this.obstacles[coordinate.QuadrantY, coordinate.QuadrantX] = coordinate.isWall;
            }
        }

        public virtual void Reset()
        {
            this.currentDir = Direction.Right;
        }

        protected virtual bool IsReadyToChangeQuadrant()
        {
            if (this.gameObject.X % Global.quad_Width == 0
                && this.gameObject.Y % Global.quad_Height == 0)
            {
                this.gameObject.QuadrantX = (int) this.gameObject.X / Global.quad_Width;
                this.gameObject.QuadrantY = (int) this.gameObject.Y / Global.quad_Height;
                return true;
            }

            return false;
        }

        protected abstract void CalculateDirection(Direction directionToAvoid);

        protected abstract Vector2 GetNextPointToMove();

        public virtual Vector2 Move(GameTime gameTime)
        {
            var nextPointToMove = this.GetNextPointToMove();

            this.gameObject.X += nextPointToMove.X /** (float)gameTime.ElapsedGameTime.TotalSeconds*/;
            this.gameObject.Y += nextPointToMove.Y /** (float)gameTime.ElapsedGameTime.TotalSeconds*/;
            this.gameObject.UpdateBoundingBox();

            return nextPointToMove;
        }

        protected virtual bool IsMovingLeftPossible()
        {
            if (this.gameObject.QuadrantX > 0
               && this.obstacles[this.gameObject.QuadrantY, this.gameObject.QuadrantX - 1] == false)
            {
                return true;
            }
            return false;
        }

        protected virtual bool IsMovingRightPossible()
        {
            if (this.gameObject.QuadrantX < Global.XMax - 1
               && this.obstacles[this.gameObject.QuadrantY, this.gameObject.QuadrantX + 1] == false)
            {
                return true;
            }
            return false;
        }

        protected virtual bool IsMovingUpPossible()
        {
            if (this.gameObject.QuadrantY > 0
               && this.obstacles[this.gameObject.QuadrantY - 1, this.gameObject.QuadrantX] == false)
            {
                return true;
            }
            return false;
        }

        protected virtual bool IsMovingDownPossible()
        {
            if (this.gameObject.QuadrantY < Global.YMax - 1
               && this.obstacles[this.gameObject.QuadrantY + 1, this.gameObject.QuadrantX] == false)
            {
                return true;
            }
            return false;
        }

        public abstract void DecreaseSpeed();

        public abstract void DrunkMovement();
    }
}
