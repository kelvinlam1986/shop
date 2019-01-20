using System;
using Shop.Model.Models;
using Shop.Data.Repositories;
using Shop.Data.Infrastructure;
using Shop.Common;

namespace Shop.Service
{
    public interface ICommonService
    {
        Footer GetFooter();
    }

    public class CommonService : ICommonService
    {
        private IFooterRepository _footerRepository;
        private IUnitOfWork _unitOfWork;

        public CommonService(IFooterRepository footerRepository, IUnitOfWork unitOfWork)
        {
            this._footerRepository = footerRepository;
            this._unitOfWork = unitOfWork;
        }

        public Footer GetFooter()
        {
            return _footerRepository.GetSingleByCondition(x => x.ID == CommonConstants.DefaultFooterId);
        }
    }
}
