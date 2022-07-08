using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IPostDataService
    {
        List<Post> GetAll();
        Post Get(Guid id);
        Post Create(Post post);
        bool Update(Post p);
        bool Delete(Guid id);
        List<Comment> GetByPostId(Guid id);
    }
}
