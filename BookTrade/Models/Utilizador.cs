namespace BookTrade.Models {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Utilizador {
        public Utilizador() {
            Comentarios = new HashSet<Comentarios>();
        }
        //Chave Primária
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        [RegularExpression("[A-ZÍÉÂÁ][a-záéíóúàèìòùâêîôûäëïöüãõç]+(( |'|-| dos | da | de | e | d')[A-ZÍÉÂÁ][a-záéíóúàèìòùâêîôûäëïöüãõç]+){1,5}",
           ErrorMessage = "O {0} apenas pode conter letras e espaços em branco. Cada palavra começa em Maiúscula, seguida de minúsculas...")]
        public string NomeCompleto { get; set; }

        [Column(TypeName = "date")]
        //formata a data para aparecer Ano/Mês/Dia
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DataNasc { get; set; }

        [Required]
        [StringLength(20)]
        public string Email { get; set; }

        public string Fotografia { get; set; }

        //Especifica que um Utilizador pode ter vários Comentários
        public virtual ICollection<Comentarios> Comentarios { get; set; }
    }
}
