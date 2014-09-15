using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EntityFramework.Extensions;
using System.ComponentModel.DataAnnotations;

namespace MvcAlbum.Models
{
    public class Album
    {
        public int AlbumID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public string Thumbnail { get; set; }
        public IEnumerable<Picture> Pictures { get; set; }
    }

    public class Picture
    {
        public int PictureID { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        public string Path { get; set; }
        public int AlbumID { get; set; }
    }

    public class AlbumContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Picture> Pictures { get; set; }
    }
}