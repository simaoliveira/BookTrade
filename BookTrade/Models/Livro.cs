namespace BookTrade.Models {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Livro {
        public Livro() {
            Comentarios = new HashSet<Comentarios>();
            Categorias = new HashSet<Categorias>();
        }
        //Chave Primária
        [Key]
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Sinopse { get; set; }

        public int AnoLanc { get; set; }

        [Required]
        public string Editora { get; set; }

        [Required]
        public string Idioma { get; set; }

        public int NumeroDePaginas { get; set; }

        //Chave forasteira da tabela Livro
        [ForeignKey("Autores")]
        [Display(Name = "Autor")]
        public int AutorId { get; set; }

        public string Fotografia { get; set; }

        public virtual Autor Autores { get; set; }

        //Especifica que um Livro pode ter vários Comentários
        public virtual ICollection<Comentarios> Comentarios { get; set; }

        //Especifica que um Livro pode ter várias Categorias
        public virtual ICollection<Categorias> Categorias { get; set; }
    }
}
