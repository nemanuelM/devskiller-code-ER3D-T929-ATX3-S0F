using Model;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Services.Implementation
{
    public class PostDataService : IPostDataService
    {
        #region Properties / Attributes

        private readonly IUnitOfWork _unitOfWork;

        #endregion Properties / Attributes

        #region CTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="ContractTypeService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public PostDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        #endregion

        #region Public Methods
        public Post Create(Post post)
        {
            _unitOfWork.PostRepository.Create(post);
            if (_unitOfWork.Save())
                return post;
            else
                return null;
        }

        public bool Delete(Guid id)
        {
            _unitOfWork.PostRepository.Delete(_=>_.Id == id);
            return _unitOfWork.Save();
        }

        public Post Get(Guid id)
        {
            return _unitOfWork.PostRepository.Get(id);
        }

        public List<Post> GetAll()
        {
            return _unitOfWork.PostRepository.GetAll().ToList();
        }
        
        public List<Comment> GetByPostId(Guid id)
        {
            return _unitOfWork.CommentRepository.GetMany(_=>_.PostId == id).ToList();
        }

        public bool Update(Post p)
        {
            _unitOfWork.PostRepository.Update(p);
            return _unitOfWork.Save();
        }

        #endregion

    }
}