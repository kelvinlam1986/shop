using Shop.Data.Infrastructure;
using Shop.Data.Repositories;
using Shop.Model.Models;
using System;

namespace Shop.Service
{
    public interface IErrorService
    {
        void Create(Error error);
        void Save();
    }

    public class ErrorService : IErrorService
    {
        private IErrorRepository _errorRepository;
        private IUnitOfWork _unitOfWork;

        public ErrorService(IErrorRepository errorRepository, IUnitOfWork unitOfWork)
        {
            this._errorRepository = errorRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Create(Error error)
        {
            this._errorRepository.Add(error);
        }

        public void Save()
        {
            this._unitOfWork.Commit();
        }
    }
}
