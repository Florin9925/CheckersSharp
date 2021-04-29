using CheckersV4.Models;
using CheckersV4.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace CheckersV4.Services
{
    public static class GameBusiness
    {
        public static PieceVM selectedPiece = null;

        public static List<CellVM> selectedTiles = null;
        public static List<Move> movesForSelectedPiece = null;


        public static void IsKing(PieceVM piece)
        {
            if (piece != null)
            {
                if (piece.Piece.PieceColor == Piece.Color.RED && piece.Piece.PieceLocation.Row == 0)
                {
                    piece.Piece.PieceType = Piece.Type.KING;
                    piece.IsKing = true;
                    return;
                }
                if (piece.Piece.PieceColor == Piece.Color.WHITE && piece.Piece.PieceLocation.Row == 7)
                {
                    piece.Piece.PieceType = Piece.Type.KING;
                    piece.IsKing = true;
                    return;
                }
            }
        }

        public static bool IsFinal(ObservableCollection<PieceVM> pieces)
        {
            bool white = false;
            bool red = false;

            if(IsDraw(pieces))
            {
                MessageBoxResult result = MessageBox.Show("Draw!");
                BoardVM.Player1.Draws += 1;
                BoardVM.Player2.Draws += 1;
                return true;
            }


            if (pieces == null)
                return false;
            foreach (var piece in pieces)
            {
                if (piece.Piece.PieceColor == Piece.Color.WHITE)
                {
                    white = true;
                }
                else
                {
                    red = true;
                }
            }
            if (white == false)
            {
                MessageBoxResult result = MessageBox.Show(BoardVM.Player1.Name + " wins!");
                BoardVM.Player1.Wins += 1;
                BoardVM.Player2.Losses += 1;
                return true;
            }
            if (red == false)
            {
                MessageBoxResult result = MessageBox.Show(BoardVM.Player2.Name + " wins!");
                BoardVM.Player1.Losses += 1;
                BoardVM.Player2.Wins += 1;
                return true;
            }
            return false;
        }

        public static bool IsDraw(ObservableCollection<PieceVM> pieces)
        {
            foreach(var piece in pieces)
            {
                var moves = Utils.GetMoves(piece.Piece.PieceLocation);
                if(moves.Count > 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool CheckPiece(PieceVM piece)
        {
            if (piece == null)
                return false;
            if (piece.Piece.PieceColor == BoardVM.Player1.Color && BoardVM.Player1.HasTurn)
            {
                return true;
            }
            if (piece.Piece.PieceColor == BoardVM.Player2.Color && BoardVM.Player2.HasTurn)
            {
                return true;
            }
            return false;
        }

        public static void ChangePlayer()
        {
            BoardVM.Player1.HasTurn = !BoardVM.Player1.HasTurn;
            BoardVM.Player2.HasTurn = !BoardVM.Player2.HasTurn;
            
        }
    }

}
