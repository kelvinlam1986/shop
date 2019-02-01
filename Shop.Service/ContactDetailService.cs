using System;
using Shop.Model.Models;
using Shop.Data.Repositories;
using Shop.Data.Infrastructure;

namespace Shop.Service
{
    public interface IContactDetailService
    {
        ContactDetail GetDefaultContactDetail(); 
    }

    public class ContactDetailService : IContactDetailService
    {
        private IContactDetailRepository _contactDetailRepository;
        private IUnitOfWork _unitOfWork;

        public ContactDetailService(IContactDetailRepository contactDetailRepository, IUnitOfWork unitOfWork)
        {
            this._contactDetailRepository = contactDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        public ContactDetail GetDefaultContactDetail()
        {
            return this._contactDetailRepository.GetSingleByCondition(x => x.Status);
        }
    }
}
