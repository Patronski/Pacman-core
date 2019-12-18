﻿namespace GameEngine.Handlers
{
    using Globals;
    using Enums;
    using Models.LevelObjects;
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections.Generic;
    using Models;
    public class GhostGoodRandomMovement : ObjectMover
    {
        private PacMan pacman;
        private Random random;
        List<Direction> possibleDirections;

        public GhostGoodRandomMovement(Ghost gameObject, Core.Matrix levelMatrix, PacMan pacman)
            : base(gameObject, levelMatrix)
        {
            this.pacman = pacman;
            this.pixelMoved = Global.DefaultGhostSpeed;
            this.random = new Random(DateTime.Now.Millisecond);
            this.possibleDirections = new List<Direction>();
        }

        private Direction SeePackman()
        {
            // watch left and right
            if (this.gameObject.QuadrantY == this.pacman.QuadrantY)
            {
                int distanceToSee = 1;
                while (this.gameObject.QuadrantX - distanceToSee >= 0
                    && this.obstacles[this.gameObject.QuadrantY, this.gameObject.QuadrantX - distanceToSee] != true)
                {
                    if (this.gameObject.QuadrantX - distanceToSee == this.pacman.QuadrantX)
                    {
                        return Direction.Left;
                    }
                    distanceToSee++;
                }

                distanceToSee = 1;
                while (this.gameObject.QuadrantX + distanceToSee <= Global.XMax - 1
                    && this.obstacles[this.gameObject.QuadrantY, this.gameObject.QuadrantX + distanceToSee] != true)
                {
                    if (this.gameObject.QuadrantX + distanceToSee == this.pacman.QuadrantX)
                    {
                        return Direction.Right;
                    }
                    distanceToSee++;
                }
            }

            // watch up and down
            if (this.gameObject.QuadrantX == this.pacman.QuadrantX)
            {
                int distanceToSee = 1;
                while (this.gameObject.QuadrantY - distanceToSee >= 0
                    && this.obstacles[this.gameObject.QuadrantY - distanceToSee, this.gameObject.QuadrantX] != true)
                {
                    if (this.gameObject.QuadrantY - distanceToSee == this.pacman.QuadrantY)
                    {
                        return Direction.Up;
                    }
                    distanceToSee++;
                }

                distanceToSee = 1;
                while (this.gameObject.QuadrantY + distanceToSee <= Global.YMax - 1
                    && this.obstacles[this.gameObject.QuadrantY + distanceToSee, this.gameObject.QuadrantX] != true)
                {
                    if (this.gameObject.QuadrantY + distanceToSee == this.pacman.QuadrantY)
                    {
                        return Direction.Down;
                    }
                    distanceToSee++;
                }
            }

            return Direction.None;
        }

        protected override void CalculateDirection(Direction bannedDirection)
        {
            this.possibleDirections.Clear();
            if (Global.Difficulty == DifficultyEnumerable.Easy)
            {
                this.EasyGame(bannedDirection);
            }

            if (Global.Difficulty == DifficultyEnumerable.Hard)
            {
                this.HardGame();
            }
        }


        private void HardGame()
        {
            // check where is PacMan and go directly to him
            if (this.pacman.QuadrantX < this.gameObject.QuadrantX)
            {
                if (!Core.Matrix.IsThereABrick(this.gameObject.QuadrantX - 1, this.gameObject.QuadrantY))
                {
                    this.currentDir = Direction.Left;
                    return;
                }
                else if (this.pacman.QuadrantY < this.gameObject.QuadrantY &&
                    !Core.Matrix.IsThereABrick(this.gameObject.QuadrantX, this.gameObject.QuadrantY - 1))
                {
                    this.currentDir = Direction.Up;
                    return;
                }
                else if (!Core.Matrix.IsThereABrick(this.gameObject.QuadrantX, this.gameObject.QuadrantY + 1))
                {
                    this.currentDir = Direction.Down;
                    return;
                }
            }

            if (this.pacman.QuadrantX > this.gameObject.QuadrantX)
            {
                if (!Core.Matrix.IsThereABrick(this.gameObject.QuadrantX + 1, this.gameObject.QuadrantY))
                {
                    this.currentDir = Direction.Right;
                    return;
                }
                else if (this.pacman.QuadrantY < this.gameObject.QuadrantY &&
                    !Core.Matrix.IsThereABrick(this.gameObject.QuadrantX, this.gameObject.QuadrantY - 1))
                {
                    this.currentDir = Direction.Up;
                    return;
                }
                else if (!Core.Matrix.IsThereABrick(this.gameObject.QuadrantX, this.gameObject.QuadrantY + 1))
                {
                    this.currentDir = Direction.Down;
                    return;
                }
            }

            if (this.pacman.QuadrantY < this.gameObject.QuadrantY)
            {
                if (!Core.Matrix.IsThereABrick(this.gameObject.QuadrantX, this.gameObject.QuadrantY - 1))
                {
                    this.currentDir = Direction.Up;
                    return;
                }
                else if (this.pacman.QuadrantX < this.gameObject.QuadrantX &&
                    !Core.Matrix.IsThereABrick(this.gameObject.QuadrantX - 1, this.gameObject.QuadrantY))
                {
                    this.currentDir = Direction.Left;
                    return;
                }
                else if (!Core.Matrix.IsThereABrick(this.gameObject.QuadrantX + 1, this.gameObject.QuadrantY))
                {
                    this.currentDir = Direction.Right;
                    return;
                }
            }

            if (this.pacman.QuadrantY > this.gameObject.QuadrantY)
            {
                if (!Core.Matrix.IsThereABrick(this.gameObject.QuadrantX, this.gameObject.QuadrantY + 1))
                {
                    this.currentDir = Direction.Down;
                    return;
                }
                else if (this.pacman.QuadrantX < this.gameObject.QuadrantX &&
                    !Core.Matrix.IsThereABrick(this.gameObject.QuadrantX - 1, this.gameObject.QuadrantY))
                {
                    this.currentDir = Direction.Left;
                    return;
                }
                else if (!Core.Matrix.IsThereABrick(this.gameObject.QuadrantX + 1, this.gameObject.QuadrantY))
                {
                    this.currentDir = Direction.Right;
                    return;
                }
            }
        }

        private void EasyGame(Direction bannedDirection)
        {
            this.possibleDirections.Clear();

            // checks if ghost is can randomize direction if not going back
            // preferred left, right, front
            if (this.currentDir == Direction.Up)
            {
                if (this.IsMovingLeftPossible() && bannedDirection != Direction.Left)
                {
                    this.possibleDirections.Add(Direction.Left);
                }
                if (this.IsMovingRightPossible() && bannedDirection != Direction.Right)
                {
                    this.possibleDirections.Add(Direction.Right);
                }
                if (this.IsMovingUpPossible() && bannedDirection != Direction.Up)
                {
                    this.possibleDirections.Add(Direction.Up);
                }
                if (this.IsMovingDownPossible() && this.possibleDirections.Count == 0 && bannedDirection != Direction.Down) // go back
                {
                    this.currentDir = Direction.Down;
                    return;
                }
                else if (this.possibleDirections.Count == 0)
                {
                    this.currentDir = Direction.None;
                }
            }
            else if (this.currentDir == Direction.Down)
            {
                if (this.IsMovingLeftPossible() && bannedDirection != Direction.Left)
                {
                    this.possibleDirections.Add(Direction.Left);
                }
                if (this.IsMovingRightPossible() && bannedDirection != Direction.Right)
                {
                    this.possibleDirections.Add(Direction.Right);
                }
                if (this.IsMovingDownPossible() && bannedDirection != Direction.Down)
                {
                    this.possibleDirections.Add(Direction.Down);
                }
                if (this.IsMovingUpPossible() && this.possibleDirections.Count == 0 && bannedDirection != Direction.Up) // go back
                {
                    this.currentDir = Direction.Up;
                    return;
                }
                else if (this.possibleDirections.Count == 0)
                {
                    this.currentDir = Direction.None;
                    return;
                }
            }
            else if (this.currentDir == Direction.Left)
            {
                if (this.IsMovingLeftPossible() && bannedDirection != Direction.Left)
                {
                    this.possibleDirections.Add(Direction.Left);
                }
                if (this.IsMovingDownPossible() && bannedDirection != Direction.Down)
                {
                    this.possibleDirections.Add(Direction.Down);
                }
                if (this.IsMovingUpPossible() && bannedDirection != Direction.Up)
                {
                    this.possibleDirections.Add(Direction.Up);
                }
                if (this.IsMovingRightPossible() && this.possibleDirections.Count == 0 && bannedDirection != Direction.Right) // go back
                {
                    this.currentDir = Direction.Right;
                    return;
                }
                else if (this.possibleDirections.Count == 0)
                {
                    this.currentDir = Direction.None;
                    return;
                }
            }
            else if (this.currentDir == Direction.Right)
            {
                if (this.IsMovingRightPossible() && bannedDirection != Direction.Right)
                {
                    this.possibleDirections.Add(Direction.Right);
                }
                if (this.IsMovingDownPossible() && bannedDirection != Direction.Down)
                {
                    this.possibleDirections.Add(Direction.Down);
                }
                if (this.IsMovingUpPossible() && bannedDirection != Direction.Up)
                {
                    this.possibleDirections.Add(Direction.Up);
                }
                if (this.IsMovingLeftPossible() && this.possibleDirections.Count == 0 && bannedDirection != Direction.Left) // go back
                {
                    this.currentDir = Direction.Left;
                    return;
                }
                else if (this.possibleDirections.Count == 0)
                {
                    this.currentDir = Direction.None;
                    return;
                }
            }
            else if (this.currentDir == Direction.None && bannedDirection == Direction.None)
            {
                if (this.IsMovingLeftPossible())
                {
                    this.possibleDirections.Add(Direction.Left);
                }
                if (this.IsMovingDownPossible())
                {
                    this.possibleDirections.Add(Direction.Down);
                }
                if (this.IsMovingUpPossible())
                {
                    this.possibleDirections.Add(Direction.Up);
                }
                if (this.IsMovingRightPossible())
                {
                    this.possibleDirections.Add(Direction.Right);
                }
            }

            if (this.possibleDirections.Count > 0)
            {
                this.ChooseRandomDirection();
            }
        }

        private void ChooseRandomDirection()
        {
            if (this.possibleDirections.Count == 1)
            {
                this.currentDir = this.possibleDirections[0];
            }
            else
            {
                Direction randomDir = (Direction)this.possibleDirections.ToArray().GetValue(this.random.Next(this.possibleDirections.Count));
                this.currentDir = randomDir;
            }
        }

        protected override Vector2 GetNextPointToMove()
        {
            if (this.IsReadyToChangeQuadrant())
            {
                if (Global.Difficulty == DifficultyEnumerable.Easy)
                {
                    Direction directionToPacman = this.SeePackman();
                    if (directionToPacman != Direction.None)
                    {
                        this.CalculateDirection(directionToPacman);
                    }
                    else
                    {
                        this.CalculateDirection(Direction.None);
                    }
                }

                if (Global.Difficulty == DifficultyEnumerable.Hard)
                {
                    this.CalculateDirection(Direction.None);
                }

            }


            Vector2 nextPointToMove = new Vector2();
            switch (this.currentDir)
            {
                case Direction.Up:
                    nextPointToMove.X = 0;
                    nextPointToMove.Y = 0 - this.pixelMoved;
                    break;
                case Direction.Down:
                    nextPointToMove.X = 0;
                    nextPointToMove.Y = this.pixelMoved;
                    break;
                case Direction.Left:
                    nextPointToMove.X = 0 - this.pixelMoved;
                    nextPointToMove.Y = 0;
                    break;
                case Direction.Right:
                    nextPointToMove.X = this.pixelMoved;
                    nextPointToMove.Y = 0;
                    break;
                case Direction.None:
                    nextPointToMove.X = 0;
                    nextPointToMove.Y = 0;
                    break;
            }

            return nextPointToMove;
        }

        public override void DecreaseSpeed()
        {
            this.pixelMoved--;
        }

        public override void GetDrunkThenRehab()
        {
        }
    }
}
