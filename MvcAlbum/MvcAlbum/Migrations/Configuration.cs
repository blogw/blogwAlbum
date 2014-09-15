namespace MvcAlbum.Migrations
{
    using MvcAlbum.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcAlbum.Models.AlbumContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MvcAlbum.Models.AlbumContext context)
        {
            var albums = new List<Album>
            {
                new Album{
                    Name="t",Thumbnail="spring.jpg"
                },
                new Album{
                    Name="‰Ä",Thumbnail="summer.jpg"
                }
            };

            albums.ForEach(a => context.Albums.AddOrUpdate(p => p.AlbumID, a));
            context.SaveChanges();

            var pictures = new List<Picture>
            {
                new Picture{
                    AlbumID=albums.Single(s=>s.Name=="t").AlbumID,Path="1",Title="a"
                },
                new Picture{
                    AlbumID=albums.Single(s=>s.Name=="t").AlbumID,Path="2",Title="b"
                },
                new Picture{
                    AlbumID=albums.Single(s=>s.Name=="t").AlbumID,Path="3",Title="c"
                },
                new Picture{
                    AlbumID=albums.Single(s=>s.Name=="‰Ä").AlbumID,Path="4",Title="‚ "
                },
                new Picture{
                    AlbumID=albums.Single(s=>s.Name=="‰Ä").AlbumID,Path="5",Title="‚¢"
                },
                new Picture{
                    AlbumID=albums.Single(s=>s.Name=="‰Ä").AlbumID,Path="6",Title="‚¤"
                },
            };

            pictures.ForEach(a => context.Pictures.AddOrUpdate(p => p.PictureID, a));
            context.SaveChanges();

        }
    }
}
