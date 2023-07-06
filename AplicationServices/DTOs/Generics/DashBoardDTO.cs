using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.DTOs.Generics
{
    public class DashBoardOperationsDTO
    {
        public List<DashBoardDTO> Cards { get; set; }
        public DashBoardGaugeDTO Gauge { get; set; }
    }
    public class DashBoardDTO
    {
        public int Items { get; set; }
        public int Progres { get; set; }
    }
    public class DashBoardGaugeDTO
    {
        public int Max { get; set; }
        public int Current { get; set; }
    }
}
