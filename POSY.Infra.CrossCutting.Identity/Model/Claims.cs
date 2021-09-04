﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POSY.Infra.CrossCutting.Identity.Model
{
    [Table("Claims")]
    public class Claims
    {
        public Claims()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Fornceça um nome para a Claim")]
        [MaxLength(128, ErrorMessage = "Tamanho máximo {0} excedido")]
        [Display(Name = "Nome da Claim")]
        public string Name { get; set; }
    }
}
