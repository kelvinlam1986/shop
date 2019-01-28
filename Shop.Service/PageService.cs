using System;
using Shop.Model.Models;
using Shop.Data.Repositories;
using Shop.Data.Infrastructure;

namespace Shop.Service
{

    public interface IPageService
    {
        Page GetPageByAlias(string alias);
    }

    public class PageService : IPageService
    {
        private IPageRepository _pageRepository;
        private IUnitOfWork _unitOfWork;

        public PageService(IPageRepository pageRepository, IUnitOfWork unitOfWork)
        {
            this._pageRepository = pageRepository;
            this._unitOfWork = unitOfWork;
        }

        public Page GetPageByAlias(string alias)
        {
            return this._pageRepository.GetSingleByCondition(x => x.Alias == alias);
        }
    }
}
