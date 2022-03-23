﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvcCochesCosmos.Models {
    public class Vehiculo {
        [JsonProperty(PropertyName= "id")]
        public string Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int VelocidadMaxima { get; set; }
        public Motor Motor { get; set; }
    }
}
