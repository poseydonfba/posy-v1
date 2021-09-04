using System;
using System.ComponentModel.DataAnnotations;

namespace POSY.Infra.CrossCutting.Identity.Model
{
    public class RoleViewModel
    {
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Nome da Role")]
        public string Name { get; set; }
    }
}