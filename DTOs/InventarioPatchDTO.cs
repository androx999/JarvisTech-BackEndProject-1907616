﻿using ApiProducto.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiProducto.DTOs
{
    public class InventarioPatchDTO
    {
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo puede tener hasta 250 caracteres")]
        [PrimeraLetraMayuscula]

        public string Name { get; set; }

        public DateTime FechaRecibo { get; set; }
    }
}