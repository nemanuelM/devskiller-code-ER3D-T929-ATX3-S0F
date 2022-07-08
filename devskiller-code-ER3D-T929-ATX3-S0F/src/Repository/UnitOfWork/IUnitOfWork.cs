using Model;
using Repository.GenericRepository;

namespace Repository.UnitOfWork
{
    public interface IUnitOfWork
    {        
        bool Save();
        void Dispose();
        GenericRepository<Post> PostRepository { get; }
        GenericRepository<Comment> CommentRepository { get; }
    }
}