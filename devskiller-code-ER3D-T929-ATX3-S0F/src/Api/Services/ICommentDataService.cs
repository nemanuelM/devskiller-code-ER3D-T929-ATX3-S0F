using Model;
using System;
using System.Collections.Generic;

namespace Api.Services
{
    public interface ICommentDataService
    {
        Comment Create(Comment comment);
        bool Delete(Guid id);
        Comment Get(Guid id);
        List<Comment> GetAll();
        bool Update(Comment p);
    }
}