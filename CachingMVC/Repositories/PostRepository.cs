using CachingMVC.Models;
using CachingMVC.Models.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CachingMVC.Repositories
{
    public class PostRepository
    {
        private MobileContext db;
        public PostRepository(MobileContext context)
        {
            db = context;
        }

        public void Initialize()
        {
            if (!db.Posts.Any())
            {
                db.Posts.AddRange(
                    new Post { Topic = "Phones", Description="Some data" },
                    new Post { Topic = "Animals", Description="Lorem ipsum" }
                );
                db.SaveChanges();
            }
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await db.Posts.ToListAsync();
        }

        public async Task<Post> AddProduct(Post post)
        {
            await db.Posts.AddAsync(post);
            db.SaveChanges();

            return post;
        }

        public async Task<Post> GetPost(int id)
        {
            Post post = await db.Posts.FirstOrDefaultAsync(p => p.Id == id);

            return post;
        }
    }
}
