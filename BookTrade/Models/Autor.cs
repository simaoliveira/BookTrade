namespace BookTrade.Models {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Autor {
        public Autor() {
            Livros = new HashSet<Livro>();
        }
        //Chave Primária
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Column(TypeName = "date")]
        //formata a data para aparecer Ano/Mês/Dia
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? DataNasc { get; set; }

        [DataType(DataType.Text)]
        public string Descricao { get; set; }

        public string AutorFotografia { get; set; }

        //Especifica que um autor pode ter vários Livros
        public virtual ICollection<Livro> Livros { get; set; }
    }
}
