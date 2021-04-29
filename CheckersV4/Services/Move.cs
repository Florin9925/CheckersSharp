using CheckersV4.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersV4.Services
{
    public class Move
    {
        public Location FinalLocation
        {
            get;
            set;
        }
        
        public List<PieceVM> TakenPieces
        {
            get;
            set;
        }

        public Move(Location final)
        {
            TakenPieces = new List<PieceVM>();
            FinalLocation = final;
        }
    }
}
