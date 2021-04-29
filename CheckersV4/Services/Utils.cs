using CheckersV4.Models;
using CheckersV4.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace CheckersV4.Services
{
    public static class Utils
    {

        public static BoardVM boardVM;

        private static int NORTH = -1;
        private static int SOUTH = 1;
        private static int EAST = 1;
        private static int WEST = -1;

        public static string redPiece = @"Resources/red_piece.png";
        public static string whitePiece = @"Resources/white_piece.png";
        public static string redKing = @"Resources/red_king.png";
        public static string whiteKing = @"Resources/white_king.png";

        private static bool IsOnTable(Location location)
        {
            int x = location.Row;
            int y = location.Column;

            return (x >= 0 && x < 8) && (y >= 0 && y < 8);
        }

        private static PieceVM GetPieceByLocation(Location location)
        {
            foreach (var item in boardVM.Pieces)
            {
                if (item.Piece.PieceLocation.Equals(location))
                    return item;
            }

            return null;
        }

        public static CellVM GetCellByLocation(Location location)
        {
            foreach (var item in boardVM.Tiles)
            {
                if (item.Location.Equals(location))
                {
                    return item;
                }
            }
            return null;
        }


        private static List<Move> GetMovesUp(PieceVM piece, bool checksForFreeTiles = true, List<PieceVM> pastPiecesTaken = null)
        {
            var moves = new List<Move>();
            var cameFrom = piece.Piece.PieceLocation;

            int x = piece.Piece.PieceLocation.Row;
            int y = piece.Piece.PieceLocation.Column;


            Location NorthWest = new Location(x + NORTH, y + WEST);
            Location NorthEast = new Location(x + NORTH, y + EAST);

            if (IsOnTable(NorthWest))
            {
                var NorthWestPiece = GetPieceByLocation(NorthWest);
                if (NorthWestPiece == null)
                {
                    if (checksForFreeTiles)
                    {
                        //Add plain move
                        var plainMove = new Move(NorthWest);
                        moves.Add(plainMove);
                    }
                }
                else if (NorthWestPiece.Piece.PieceColor != piece.Piece.PieceColor)
                {
                    //if enemy piece
                    Location NextNW = new Location(NorthWest.Row + NORTH, NorthWest.Column + WEST);
                    if (IsOnTable(NextNW))
                    {
                        var PseudoNWPiece = GetPieceByLocation(NextNW);
                        if (PseudoNWPiece == null)
                        {
                            //if there's no piexe on NextNW
                            //add this Move
                            var simpleJumpMove = new Move(NextNW);

                            if (!checksForFreeTiles)
                            {
                                foreach (var pastPiece in pastPiecesTaken)
                                {
                                    simpleJumpMove.TakenPieces.Add(pastPiece);
                                }
                            }
                            //we then add the appropiate piece to be taken
                            simpleJumpMove.TakenPieces.Add(NorthWestPiece);
                            moves.Add(simpleJumpMove);
                            //Moves.addRange(call GetMovesUp() for NextNW with the already taken pieces);
                            moves.AddRange(GetMovesUp(piece.GetPhantomAt(NextNW), false, simpleJumpMove.TakenPieces));
                        }
                    }
                }
            }

            if (IsOnTable(NorthEast))
            {
                var NorthEastPiece = GetPieceByLocation(NorthEast);
                if (NorthEastPiece == null)
                {
                    if (checksForFreeTiles)
                    {
                        //Add plain move
                        var plainMove = new Move(NorthEast);
                        moves.Add(plainMove);
                    }
                }
                else if (NorthEastPiece.Piece.PieceColor != piece.Piece.PieceColor)
                {
                    //if enemy piece
                    Location NextNE = new Location(NorthEast.Row + NORTH, NorthEast.Column + EAST);
                    if (IsOnTable(NextNE))
                    {
                        var PseudoNEPiece = GetPieceByLocation(NextNE);
                        if (PseudoNEPiece == null)
                        {
                            //if there's no piexe on NextNE
                            //add this Move
                            var simpleJumpMove = new Move(NextNE);

                            if (!checksForFreeTiles)
                            {
                                foreach (var pastPiece in pastPiecesTaken)
                                {
                                    simpleJumpMove.TakenPieces.Add(pastPiece);
                                }
                            }
                            //we then add the appropiate piece to be taken
                            simpleJumpMove.TakenPieces.Add(NorthEastPiece);
                            moves.Add(simpleJumpMove);
                            //Moves.addRange(call GetMovesUp() for NextNE with the already taken pieces);
                            moves.AddRange(GetMovesUp(piece.GetPhantomAt(NextNE), false, simpleJumpMove.TakenPieces));
                        }
                    }
                }
            }
            piece.Piece.PieceLocation = cameFrom;

            if (piece.IsPhantom)
                boardVM.Pieces.Remove(piece);

            return moves;
        }

        private static List<Move> GetMovesDown(PieceVM piece, bool checksForFreeTiles = true, List<PieceVM> pastPiecesTaken = null)
        {
            var moves = new List<Move>();
            var cameFrom = piece.Piece.PieceLocation;

            int x = piece.Piece.PieceLocation.Row;
            int y = piece.Piece.PieceLocation.Column;


            Location SouthWest = new Location(x + SOUTH, y + WEST);
            Location SouthEast = new Location(x + SOUTH, y + EAST);

            if (IsOnTable(SouthWest))
            {
                var SouthWestPiece = GetPieceByLocation(SouthWest);
                if (SouthWestPiece == null)
                {
                    if (checksForFreeTiles)
                    {
                        //Add plain move
                        var plainMove = new Move(SouthWest);
                        moves.Add(plainMove);
                    }
                }
                else if (SouthWestPiece.Piece.PieceColor != piece.Piece.PieceColor)
                {
                    //if enemy piece
                    Location NextSW = new Location(SouthWest.Row + SOUTH, SouthWest.Column + WEST);
                    if (IsOnTable(NextSW))
                    {
                        var PseudoNWPiece = GetPieceByLocation(NextSW);
                        if (PseudoNWPiece == null)
                        {
                            //if there's no piexe on NextSW
                            //add this Move
                            var simpleJumpMove = new Move(NextSW);

                            if (!checksForFreeTiles)
                            {
                                foreach (var pastPiece in pastPiecesTaken)
                                {
                                    simpleJumpMove.TakenPieces.Add(pastPiece);
                                }
                            }
                            //we then add the appropiate piece to be taken
                            simpleJumpMove.TakenPieces.Add(SouthWestPiece);
                            moves.Add(simpleJumpMove);
                            //Moves.addRange(call GetMovesUp() for NextSW with the already taken pieces);
                            moves.AddRange(GetMovesDown(piece.GetPhantomAt(NextSW), false, simpleJumpMove.TakenPieces));
                        }
                    }
                }
            }

            if (IsOnTable(SouthEast))
            {
                var SouthEastPiece = GetPieceByLocation(SouthEast);
                if (SouthEastPiece == null)
                {
                    if (checksForFreeTiles)
                    {
                        //Add plain move
                        var plainMove = new Move(SouthEast);
                        moves.Add(plainMove);
                    }
                }
                else if (SouthEastPiece.Piece.PieceColor != piece.Piece.PieceColor)
                {
                    //if enemy piece
                    Location NextSE = new Location(SouthEast.Row + SOUTH, SouthEast.Column + EAST);
                    if (IsOnTable(NextSE))
                    {
                        var PseudoNEPiece = GetPieceByLocation(NextSE);
                        if (PseudoNEPiece == null)
                        {
                            //if there's no piexe on NextSE
                            //add this Move
                            var simpleJumpMove = new Move(NextSE);

                            if (!checksForFreeTiles)
                            {
                                foreach (var pastPiece in pastPiecesTaken)
                                {
                                    simpleJumpMove.TakenPieces.Add(pastPiece);
                                }
                            }
                            //we then add the appropiate piece to be taken
                            simpleJumpMove.TakenPieces.Add(SouthEastPiece);
                            moves.Add(simpleJumpMove);
                            //Moves.addRange(call GetMovesUp() for NextSE);
                            moves.AddRange(GetMovesDown(piece.GetPhantomAt(NextSE), false, simpleJumpMove.TakenPieces));
                        }
                    }
                }
            }
            piece.Piece.PieceLocation = cameFrom;

            if (piece.IsPhantom)
                boardVM.Pieces.Remove(piece);

            return moves;
        }

        public static List<Move> GetMoves(Location location)
        {
            var result = new List<Move>();

            var piece = GetPieceByLocation(location);

            if (piece == null)
            {
                ConsoleManager.Show();
                System.Console.WriteLine("ERROR: at postion: " + location.ToString() + " there is no piece");
            }
            else if(GameBusiness.CheckPiece(piece))
            {
                if (piece.Piece.PieceType == Piece.Type.KING)
                {
                    result.AddRange(GetMovesKing(piece));
                }
                else if (piece.Piece.PieceColor == Piece.Color.WHITE)
                {
                    //if we have a common white piece, we can only search down
                    result.AddRange(GetMovesDown(piece));
                }
                else
                {
                    //if we have a common red piece, we can only search up
                    result.AddRange(GetMovesUp(piece));
                }
            }

            return result;
        }

        private static List<Move> GetMovesKing(PieceVM piece, bool checksForFreeTiles = true, List<PieceVM> pastPiecesTaken = null)
        {
            var moves = new List<Move>();
            var cameFrom = piece.Piece.PieceLocation;

            int x = piece.Piece.PieceLocation.Row;
            int y = piece.Piece.PieceLocation.Column;

            Location NorthWest = new Location(x + NORTH, y + WEST);
            Location NorthEast = new Location(x + NORTH, y + EAST);

            if (IsOnTable(NorthWest))
            {
                var NorthWestPiece = GetPieceByLocation(NorthWest);
                if (NorthWestPiece == null)
                {
                    if (checksForFreeTiles)
                    {
                        //Add plain move
                        var plainMove = new Move(NorthWest);
                        moves.Add(plainMove);
                    }
                }
                else if (NorthWestPiece.Piece.PieceColor != piece.Piece.PieceColor)
                {
                    //if enemy piece
                    Location NextNW = new Location(NorthWest.Row + NORTH, NorthWest.Column + WEST);
                    if (IsOnTable(NextNW))
                    {
                        var PseudoNWPiece = GetPieceByLocation(NextNW);
                        if (PseudoNWPiece == null)
                        {
                            //if there's no piexe on NextNW
                            //add this Move
                            var simpleJumpMove = new Move(NextNW);

                            if (!checksForFreeTiles)
                            {
                                //if we're in a recursion state, we do not check for free tiles, instead we add
                                //the past taken pieces to every move we branch into
                                foreach (var pastPiece in pastPiecesTaken)
                                {
                                    simpleJumpMove.TakenPieces.Add(pastPiece);
                                }
                            }
                            //we then add the appropiate piece to be taken
                            simpleJumpMove.TakenPieces.Add(NorthWestPiece);
                            moves.Add(simpleJumpMove);
                            //Moves.addRange(call GetMovesUp() for NextNW with the already taken pieces);
                            moves.AddRange(GetMovesKing(piece.GetPhantomAt(NextNW), false, simpleJumpMove.TakenPieces));
                        }
                    }
                }
            }

            if (IsOnTable(NorthEast))
            {
                var NorthEastPiece = GetPieceByLocation(NorthEast);
                if (NorthEastPiece == null)
                {
                    if (checksForFreeTiles)
                    {
                        //Add plain move
                        var plainMove = new Move(NorthEast);
                        moves.Add(plainMove);
                    }
                }
                else if (NorthEastPiece.Piece.PieceColor != piece.Piece.PieceColor)
                {
                    //if enemy piece
                    Location NextNE = new Location(NorthEast.Row + NORTH, NorthEast.Column + EAST);
                    if (IsOnTable(NextNE))
                    {
                        var PseudoNEPiece = GetPieceByLocation(NextNE);
                        if (PseudoNEPiece == null)
                        {
                            //if there's no piexe on NextNE
                            //add this Move
                            var simpleJumpMove = new Move(NextNE);

                            if (!checksForFreeTiles)
                            {
                                //if we're in a recursion state, we do not check for free tiles, instead we add
                                //the past taken pieces to every move we branch into
                                foreach (var pastPiece in pastPiecesTaken)
                                {
                                    simpleJumpMove.TakenPieces.Add(pastPiece);
                                }
                            }
                            //we then add the appropiate piece to be taken
                            simpleJumpMove.TakenPieces.Add(NorthEastPiece);
                            moves.Add(simpleJumpMove);
                            //Moves.addRange(call GetMovesUp() for NextNE with the already taken pieces);
                            moves.AddRange(GetMovesKing(piece.GetPhantomAt(NextNE), false, simpleJumpMove.TakenPieces));
                        }
                    }
                }
            }

            Location SouthWest = new Location(x + SOUTH, y + WEST);
            Location SouthEast = new Location(x + SOUTH, y + EAST);

            if (IsOnTable(SouthWest))
            {
                var SouthWestPiece = GetPieceByLocation(SouthWest);
                if (SouthWestPiece == null)
                {
                    if (checksForFreeTiles)
                    {
                        //Add plain move
                        var plainMove = new Move(SouthWest);
                        moves.Add(plainMove);
                    }
                }
                else if (SouthWestPiece.Piece.PieceColor != piece.Piece.PieceColor)
                {
                    //if enemy piece
                    Location NextSW = new Location(SouthWest.Row + SOUTH, SouthWest.Column + WEST);
                    if (IsOnTable(NextSW))
                    {
                        var PseudoNWPiece = GetPieceByLocation(NextSW);
                        if (PseudoNWPiece == null)
                        {
                            //if there's no piexe on NextSW
                            //add this Move
                            var simpleJumpMove = new Move(NextSW);

                            if (!checksForFreeTiles)
                            {
                                //if we're in a recursion state, we do NOT check for free tiles, instead we add
                                //the past taken pieces to every move we branch into
                                foreach (var pastPiece in pastPiecesTaken)
                                {
                                    simpleJumpMove.TakenPieces.Add(pastPiece);
                                }
                            }
                            //we then add the appropiate piece to be taken
                            simpleJumpMove.TakenPieces.Add(SouthWestPiece);
                            moves.Add(simpleJumpMove);
                            //Moves.addRange(call GetMovesUp() for NextSW with the already taken pieces);
                            moves.AddRange(GetMovesKing(piece.GetPhantomAt(NextSW), false, simpleJumpMove.TakenPieces));
                        }
                    }
                }
            }

            if (IsOnTable(SouthEast))
            {
                var SouthEastPiece = GetPieceByLocation(SouthEast);
                if (SouthEastPiece == null)
                {
                    if (checksForFreeTiles)
                    {
                        //Add plain move
                        var plainMove = new Move(SouthEast);
                        moves.Add(plainMove);
                    }
                }
                else if (SouthEastPiece.Piece.PieceColor != piece.Piece.PieceColor)
                {
                    //if enemy piece
                    Location NextSE = new Location(SouthEast.Row + SOUTH, SouthEast.Column + EAST);
                    if (IsOnTable(NextSE))
                    {
                        var PseudoNEPiece = GetPieceByLocation(NextSE);
                        if (PseudoNEPiece == null)
                        {
                            //if there's no piexe on NextSE
                            //add this Move
                            var simpleJumpMove = new Move(NextSE);

                            if (!checksForFreeTiles)
                            {
                                //if we're in a recursion state, we do not check for free tiles, instead we add
                                //the past taken pieces to every move we branch into
                                foreach (var pastPiece in pastPiecesTaken)
                                {
                                    simpleJumpMove.TakenPieces.Add(pastPiece);
                                }
                            }
                            //we then add the appropiate piece to be taken
                            simpleJumpMove.TakenPieces.Add(SouthEastPiece);
                            moves.Add(simpleJumpMove);
                            //Moves.addRange(call GetMovesUp() for NextSE);
                            moves.AddRange(GetMovesKing(piece.GetPhantomAt(NextSE), false, simpleJumpMove.TakenPieces));
                        }
                    }
                }
            }

            piece.Piece.PieceLocation = cameFrom;

            if (piece.IsPhantom)
                boardVM.Pieces.Remove(piece);

            return moves;
        }

        public static void RedrawTiles()
        {
            Func<int, bool> IsOdd = x => x % 2 != 0;

            foreach (var tile in boardVM.Tiles)
            {
                int i = tile.Location.Row;
                int j = tile.Location.Column;
                if (IsOdd(i))
                {
                    if (IsOdd(j))
                    {
                        tile.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#fece9e"));
                    }
                    else
                    {
                        tile.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#d18a47"));
                    }
                }
                else
                {
                    if (IsOdd(j))
                    {
                        tile.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#d18a47"));
                    }
                    else
                    {
                        tile.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#fece9e"));
                    }
                }
            }
        }
    }
}
