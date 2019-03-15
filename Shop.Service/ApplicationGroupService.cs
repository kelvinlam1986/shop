using Shop.Model.Models;
using System.Collections;
using System.Collections.Generic;
using System;
using Shop.Data.Repositories;
using Shop.Data.Infrastructure;
using System.Linq;
using Shop.Common;

namespace Shop.Service
{
    public interface IApplicationGroupService
    {
        ApplicationGroup GetDetail(int id);
        IEnumerable<ApplicationGroup> GetAll(int page, int pageSize, out int totalRow, string filter);
        IEnumerable<ApplicationGroup> GetAll();
        ApplicationGroup Add(ApplicationGroup group);
        void Update(ApplicationGroup group);
        ApplicationGroup Delete(int id);
        bool AddUserToGroups(IEnumerable<ApplicationUserGroup> userGroups, string userId);
        IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId);
        IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId);
        void Save();
    }

    public class ApplicationGroupService : IApplicationGroupService
    {
        private IApplicationGroupRepository _applicationGroupRepository;
        private IApplicationUserGroupRepository _applicationUserGroupRepository;
        private IUnitOfWork _unitOfWork;

        public ApplicationGroupService(IApplicationGroupRepository applicationGroupRepository, 
            IApplicationUserGroupRepository applicationUserGroupRepository,
            IUnitOfWork unitOfWork)
        {
            this._applicationGroupRepository = applicationGroupRepository;
            this._applicationUserGroupRepository = applicationUserGroupRepository;
            this._unitOfWork = unitOfWork;
        }

        public ApplicationGroup Add(ApplicationGroup group)
        {
            if (this._applicationGroupRepository.CheckContains(x => x.Name == group.Name))
            {
                throw new NameDuplicatedException("Tên không được trùng.");
            }

            return this._applicationGroupRepository.Add(group);
        }

        public bool AddUserToGroups(IEnumerable<ApplicationUserGroup> userGroups, string userId)
        {
            this._applicationUserGroupRepository.DeleteMulti(x => x.UserId == userId);
            foreach (var userGroup in userGroups)
            {
                this._applicationUserGroupRepository.Add(userGroup);
            }
            return true;
        }

        public ApplicationGroup Delete(int id)
        {
            var group = this._applicationGroupRepository.GetSingleById(id);
            return this._applicationGroupRepository.Delete(group);
        }

        public IEnumerable<ApplicationGroup> GetAll()
        {
            return this._applicationGroupRepository.GetAll();
        }

        public IEnumerable<ApplicationGroup> GetAll(int page, int pageSize, out int totalRow, string filter)
        {
            var query = this._applicationGroupRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Name.Contains(filter));
            }

            totalRow = query.Count();
            return query.OrderBy(x => x.Name).Skip(page * pageSize).Take(pageSize);
        }

        public ApplicationGroup GetDetail(int id)
        {
            return this._applicationGroupRepository.GetSingleById(id);
        }

        public IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId)
        {
            return this._applicationGroupRepository.GetListGroupByUserId(userId);
        }

        public IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId)
        {
            return this._applicationGroupRepository.GetListUserByGroupId(groupId);
        }

        public void Save()
        {
            this._unitOfWork.Commit();
        }

        public void Update(ApplicationGroup group)
        {
            if (this._applicationGroupRepository.CheckContains(x => x.Name == group.Name && x.ID != group.ID))
            {
                throw new NameDuplicatedException("Tên không được trùng.");
            }
            this._applicationGroupRepository.Update(group);
        }
    }
}
