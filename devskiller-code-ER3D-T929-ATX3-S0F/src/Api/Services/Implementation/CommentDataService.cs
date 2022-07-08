using Model;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Implementation
{
    public class CommentDataService : ICommentDataService
    {
        #region Properties / Attributes

        private readonly IUnitOfWork _unitOfWork;

        #endregion Properties / Attributes

        #region CTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="ContractTypeService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public CommentDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Public Methods
        public Comment Create(Comment comment)
        {
            _unitOfWork.CommentRepository.Create(comment);
            if (_unitOfWork.Save())
                return comment;
            else
                return null;
        }

        public bool Delete(Guid id)
        {
            _unitOfWork.PostRepository.Delete(_ => _.Id == id);
            return _unitOfWork.Save();
        }

        public Comment Get(Guid id)
        {
            return _unitOfWork.CommentRepository.Get(id);
        }

        public List<Comment> GetAll()
        {
            return _unitOfWork.CommentRepository.GetAll().ToList();
        }


        public bool Update(Comment p)
        {
            _unitOfWork.CommentRepository.Update(p);
            return _unitOfWork.Save();
        }

        #endregion
    }
}
