using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Operations_.Net_Core___Movies_Site.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required , MaxLength(250)]
        public string Title { get; set; }

        public int Year { get; set; }

        [Required]
        public Double Rate { get; set; }

        [Required, MaxLength(2500)]  
        public string StoryLine { get; set; }

        [Required]  //[Required, StringLength(2500)]
        public byte[] Poster { get; set; }


        // Relation Using Navigation Prop
        public int GenreId { get; set; }

        public Genre Genre { get; set; }

    }
}
