using CRUD_Operations_.Net_Core___Movies_Site.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Operations_.Net_Core___Movies_Site.ViewModels
{
    public class MovieFormViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(250)]
        public string Title { get; set; }

        public int Year { get; set; }

        [Range(1,10)]
        public Double Rate { get; set; }

        [Required, StringLength(2500)]
        public string StoryLine { get; set; }


        [Display(Name = "Select Poster")]
        public byte[] Poster { get; set; }

        [Display(Name ="Genre")]
        public int GenreId { get; set; }

        public IEnumerable<Genre> Genres { get; set; }
    }
}
