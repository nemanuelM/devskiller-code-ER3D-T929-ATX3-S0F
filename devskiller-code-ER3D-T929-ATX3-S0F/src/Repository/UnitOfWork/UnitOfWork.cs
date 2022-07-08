using Microsoft.EntityFrameworkCore;
using Model;
using Repository.GenericRepository;
using System;
using System.Data.Entity.Validation;

namespace Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Properties / Attributes

        private readonly BlogContext _context;
        public GenericRepository<Post> _postRepository;
        public GenericRepository<Comment> _commentRepository;       

        #endregion

        #region _CTOR_

        public UnitOfWork(BlogContext blogContext)
        {
            _context = blogContext;
        }

        #endregion

        #region Public Repository Creation

        public GenericRepository<Comment> CommentRepository
        {
            get
            {
                if (_commentRepository == null)
                    _commentRepository = new GenericRepository<Comment>(_context);
                return _commentRepository;
            }
        }

        public GenericRepository<Post> PostRepository
        {
            get
            {
                if (_postRepository == null)
                    _postRepository = new GenericRepository<Post>(_context);
                return _postRepository;
            }
        }

        #endregion

        #region Public Methods
        public bool Save()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException e)
            {
                return false;
            }
        }
        #endregion

        #region Implementing IDisposable

        private bool disposed = false;

        #region Properties / Attributes

        #endregion


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                if (disposing)
                    _context.Dispose();

            disposed = true;
        }

        #endregion
    }
}