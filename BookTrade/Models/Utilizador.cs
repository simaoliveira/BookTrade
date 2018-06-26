namespace BookTrade.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Utilizador
    {
        public Utilizador()
        {
            Comentarios = new HashSet<Comentarios>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string NomeCompleto { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode =true)]
        public DateTime DataNasc { get; set; }

        [Required]
        [StringLength(20)]
        public string Email { get; set; }

        public string Fotografia { get; set; }

        public virtual ICollection<Comentarios> Comentarios { get; set; }
    }
}
