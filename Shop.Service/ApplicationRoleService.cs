using Shop.Model.Models;
using System.Collections.Generic;
using System;
using Shop.Data.Repositories;
using Shop.Data.Infrastructure;
using Shop.Common;
using System.Linq;

namespace Shop.Service
{
    public interface IApplicationRoleServie
    {
        ApplicationRole GetDetail(string id);
        IEnumerable<ApplicationRole> GetAll(int page, int pageSize, out int totalRow, string filter);
        IEnumerable<ApplicationRole> GetAll();
        ApplicationRole Add(ApplicationRole appRole);
        void Update(ApplicationRole AppRole);
        void Delete(string id);
        
        //Add roles to a sepcify group
        bool AddRolesToGroup(IEnumerable<ApplicationRoleGroup> roleGroups, int groupId);

        //Get list role by group id
        IEnumerable<ApplicationRole> GetListRoleByGroupId(int groupId);

        void Save();
    }

    public class ApplicationRoleService : IApplicationRoleServie
    {
        private IApplicationRoleRepository _applicationRoleRepository;
        private IApplicationRoleGroupRepository _applicationRoleGroupRepository;
        private IUnitOfWork _unitOfWork;

        public ApplicationRoleService(
            IApplicationRoleRepository applicationRoleRepository,
            IApplicationRoleGroupRepository applicationRoleGroupRepository,
            IUnitOfWork unitOfWork)
        {
            this._applicationRoleRepository = applicationRoleRepository;
            this._applicationRoleGroupRepository = applicationRoleGroupRepository;
            this._unitOfWork = unitOfWork;
        }

        public ApplicationRole Add(ApplicationRole appRole)
        {
            if (this._applicationRoleRepository.CheckContains(x => x.Name == appRole.Name))
            {
                throw new Exception("Tên không được trùng.");
            }

            return this._applicationRoleRepository.Add(appRole);
        }

        public bool AddRolesToGroup(IEnumerable<ApplicationRoleGroup> roleGroups, int groupId)
        {
            this._applicationRoleGroupRepository.DeleteMulti(x => x.GroupId == groupId);
            foreach (var roleGroup in roleGroups)
            {
                this._applicationRoleGroupRepository.Add(roleGroup);
            }

            return true;
        }

        public void Delete(string id)
        {
            this._applicationRoleRepository.DeleteMulti(x => x.Id == id);
        }

        public IEnumerable<ApplicationRole> GetAll()
        {
            return this._applicationRoleRepository.GetAll();
        }

        public IEnumerable<ApplicationRole> GetAll(int page, int pageSize, out int totalRow, string filter = null)
        {
            var query = this._applicationRoleRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Name.Contains(filter));
            }

            totalRow = query.Count();
            return query.OrderBy(x => x.Name).Skip(page * pageSize).Take(pageSize);

        }

        public ApplicationRole GetDetail(string id)
        {
            return this._applicationRoleRepository.GetSingleByCondition(x => x.Id == id);
        }

        public IEnumerable<ApplicationRole> GetListRoleByGroupId(int groupId)
        {
            return this._applicationRoleRepository.GetListRoleByGroupId(groupId);
        }

        public void Save()
        {
            this._unitOfWork.Commit();
        }

        public void Update(ApplicationRole appRole)
        {
            if (this._applicationRoleRepository.CheckContains(x => x.Name == appRole.Name && 
                x.Id != appRole.Id))
            {
                throw new NameDuplicatedException("Tên không được trùng");
            }

            this._applicationRoleRepository.Update(appRole);
        }
    }
}
