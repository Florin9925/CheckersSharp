using CheckersV4.Models;
using CheckersV4.Services;
using CheckersV4.ViewModels;
using CheckersV4.Views;
using System;
using System.Collections.ObjectModel;

namespace CheckersV4.Commands
{
    public class SelectTileCommand : BaseCommand
    {
        private readonly CellVM cellVM;
        public SelectTileCommand(CellVM cell)
        {
            this.cellVM = cell;
        }

        public ObservableCollection<PieceVM> Pieces { get; set; }
        public ObservableCollection<CellVM> Tiles { get; set; }

        public bool TurnOf { get; set; }

        public override void Execute(object parameter)
        {
            if (CanSelect())
            {


                Pieces = (parameter as BoardVM).Pieces;
                Tiles = (parameter as BoardVM).Tiles;

                Utils.RedrawTiles();

                foreach (var tile in GameBusiness.selectedTiles)
                {
                    if (tile == cellVM)
                    {
                        GameBusiness.movesForSelectedPiece.Sort( 
                            (move1, move2) => -move1.TakenPieces.Count.CompareTo(move2.TakenPieces.Count));

                        foreach (var move in GameBusiness.movesForSelectedPiece)
                        {
                            if (move.FinalLocation.Equals(tile.Location))
                            {
                                foreach (var piece in move.TakenPieces)
                                {
                                    Pieces.Remove(piece);
                                }
                                break;
                            }
                        }

                        GameBusiness.selectedPiece.Piece.PieceLocation = tile.Location;
                        break;

                    }
                }
                GameBusiness.IsKing(GameBusiness.selectedPiece);
                if (GameBusiness.IsFinal(Pieces))
                {
                    BoardVM.CurentWindows.StartNewGame();
                }
                GameBusiness.ChangePlayer();
                (parameter as BoardVM).IsPlayer1 = true;

                GameBusiness.selectedPiece.IsSelected = System.Windows.Media.Colors.Black;
            }
        }

        private bool CanSelect()
        {
            if (GameBusiness.selectedTiles == null)
                return false;
            foreach (var tile in GameBusiness.selectedTiles)
            {
                if (tile.Equals(cellVM))
                    return true;
            }

            return false;
        }
    }
}
