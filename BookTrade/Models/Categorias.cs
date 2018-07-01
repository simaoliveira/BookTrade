namespace BookTrade.Models {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Categorias {
        public Categorias() {
            Livros = new HashSet<Livro>();
        }
        //Chave Primária
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        //Especifica que uma Categoria pode ter vários Livros
        public virtual ICollection<Livro> Livros { get; set; }
    }
}
