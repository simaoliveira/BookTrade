namespace BookTrade.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Autor
    {
        public Autor()
        {
            Livros = new HashSet<Livro>();
        }
        [Key]
        public int Id { get; set; }

        [Required]

        public string Nome { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? DataNasc { get; set; }

        [DataType(DataType.Text)]
        public string Descricao { get; set; }

        public string Fotografia { get; set; }

        public virtual ICollection<Livro> Livros { get; set; }
    }
}
