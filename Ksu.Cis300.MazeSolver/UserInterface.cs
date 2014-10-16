/* UserInterface.cs
 * Author: Kevin Harrison Manase
 * Homework 3 Assignment
 */

using Ksu.Cis300.MazeLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Ksu.Cis300.MazeSolver
{
    public partial class UserInterface : Form
    {
        /// <summary>
        /// Initializes the component,
        /// This is automatically added
        /// by the IDE
        /// </summary>
        public UserInterface()
        {
            InitializeComponent();

        }

        /// <summary>
        /// The method used to solve the maze generated
        /// </summary>
        /*
        private void Solve(Cell cell)
        {
            _directionStack = new Stack<Direction>();
            _rows = uxMaze.MazeHeight;
            _cols = uxMaze.MazeWidth;
            _theDirection = Direction.North;
            _isVisited = new bool[_rows, _cols];
            _isVisited[cell.Row, cell.Column] = true;

            while (uxMaze.IsInMaze(cell))
            {

                if (_theDirection <= Direction.West)
                {
                    if (uxMaze.IsClear(cell, _theDirection) && ((uxMaze.IsInMaze(uxMaze.Step(cell, _theDirection))==false ||
                         _isVisited[uxMaze.Step(cell, _theDirection).Row,uxMaze.Step(cell, _theDirection).Column]==false)))
                    {
                        // Console.WriteLine("Is clear and is not visited");
                        uxMaze.DrawPath(cell, _theDirection);
                        _isVisited[cell.Row, cell.Column] = true;
                        cell = uxMaze.Step(cell, _theDirection);
                        _directionStack.Push(_theDirection);
                        _theDirection = Direction.North;
                    }

                    else
                    {
                        if (_theDirection <= Direction.West)
                        {
                            // Console.WriteLine("Incrementing");
                            _theDirection++;
                            
                        }
                    }
                }

                if (_theDirection > Direction.West &&
                    _directionStack.Count != 0)
                {
                    Console.WriteLine("Pop direction");
                    _theDirection = _directionStack.Pop();
                    cell = uxMaze.ReverseStep(cell, _theDirection);
                    uxMaze.ErasePath(cell, _theDirection);
                    _theDirection++;
                }

                if (_theDirection > Direction.West && _directionStack.Count == 0)
                {
                    MessageBox.Show("Can't go anywhere from here");
                    break;
                }
            }
        }
         */

        /// <summary>
        /// Handles new maze button event on click.
        /// This will generate a completely new maze.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">Event e</param>
        private void uxButton_Click(object sender, EventArgs e)
        {
            uxMaze.Generate();
        }

        /// <summary>
        /// Handles the user interaction with the maze.
        /// This will calculate the cell where the user clicked
        /// From the the data received, thr program will try to 
        /// solve the maze using the location by calling the
        /// Solve() method.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">Mouse click handler</param>
        private void uxMaze_MouseClick(object sender, MouseEventArgs e)
        {
            Cell Borders = uxMaze.GetCellFromPixel(e.Location);
            bool [,] flag = new bool [uxMaze.MazeHeight, uxMaze.MazeWidth];
            uxMaze.EraseAllPaths();
            if (PathFound(Borders, flag) == false)
            {
                uxMaze.EraseAllPaths();
                MessageBox.Show("Can't go anywhere from here");
            }
            uxMaze.Invalidate();
            


        }

        private bool PathFound(Cell cell, bool[,] flag)
        {

            if (uxMaze.IsInMaze(cell) == true)
            {

                for (Direction d = Direction.North; d < Direction.West; d++)
                {
                    //flag[cell.Row, cell.Column] = true;
                    Cell newCell = uxMaze.Step(cell, d);
                    if (uxMaze.IsClear(cell, d) && ((uxMaze.IsInMaze(newCell) == false ||
                                 flag[newCell.Row, newCell.Column] == false)))
                    {
                        uxMaze.DrawPath(cell, d);
                        flag[cell.Row, cell.Column] = true;
                        bool t = PathFound(newCell, flag);
                        if (t == true)
                        {
                            return true;
                        }
                        else
                        {
                            uxMaze.ErasePath(cell, d);
                        }
                    }
                }

                return false;

            }
            else {
                return true;
            }
        }



    }
}
