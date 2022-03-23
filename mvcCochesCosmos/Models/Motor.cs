using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvcCochesCosmos.Models {
    public class Motor {
        public string Tipo { get; set; }
        public int Caballos { get; set; }
        public int Cilindrada {get;set;}
        public bool Turbo { get; set; }
    }
}
