using CheckersV4.Models;
using CheckersV4.Services;
using CheckersV4.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace CheckersV4.Commands
{
    public class SelectPieceCommand : BaseCommand
    {
        private readonly PieceVM pieceVM;
        public SelectPieceCommand(PieceVM piece)
        {
            this.pieceVM = piece;
        }

        public ObservableCollection<PieceVM> Pieces { get; set; }
        public ObservableCollection<CellVM> Tiles { get; set; }

        

        public override void Execute(object parameter)
        {
            if (GameBusiness.selectedPiece != null)
            {
                GameBusiness.selectedPiece.IsSelected = System.Windows.Media.Colors.Black;
            }

            GameBusiness.selectedPiece = pieceVM;

            pieceVM.IsSelected = System.Windows.Media.Colors.Gold;

            Pieces = (parameter as BoardVM).Pieces;
            Tiles = (parameter as BoardVM).Tiles;

            var moves = Utils.GetMoves(GameBusiness.selectedPiece.Piece.PieceLocation);
            
            Utils.RedrawTiles();
            GameBusiness.selectedTiles = new System.Collections.Generic.List<CellVM>();
            foreach (var move in moves)
            {
                var finalTile = Utils.GetCellByLocation(move.FinalLocation);
                GameBusiness.selectedTiles.Add(finalTile);
                finalTile.Background = Brushes.SeaGreen;
            }
            GameBusiness.movesForSelectedPiece = moves;
        }
    }
}
