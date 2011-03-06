using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using SkillDesk.Domain.Abstract;
using SkillDesk.Domain.Entities;

namespace SkillDesk.Domain.Concrete
{
    public class SqlSkillsRepository : ISkillsRepository
    {
        private readonly string _connectionString;
        private Table<Skill> _skillsTable;

        public SqlSkillsRepository(string connectionString)
        {
            _connectionString = connectionString;
            this._skillsTable = (new DataContext(connectionString).GetTable<Skill>());
        }

       

        public IQueryable<Skill> Skills
        {
            get { return _skillsTable; }
        }

        public void Save(Skill skill)
        {
            // If it's a new skill, just attach it to the DataContext
            if (skill.Id == 0)
                _skillsTable.InsertOnSubmit(skill);

            else if (_skillsTable.GetOriginalEntityState(skill) == null)
            {
                // We're updating an existing skill, but it's not attached to
                // this data context, so attach it and detect the changes
                _skillsTable.Attach(skill);
                _skillsTable.Context.Refresh(RefreshMode.KeepCurrentValues, skill);
            }
            _skillsTable.Context.SubmitChanges();
        }

        public void DeleteSkill(Skill skill)
        {
            _skillsTable.DeleteOnSubmit(skill);
            _skillsTable.Context.SubmitChanges();
        }

        public SkillsPageResult GetSkillsPaged(int pageNumber, int pageSize) {
            SkillsPageResult r = new SkillsPageResult();
            r.SkillsToDisplay = Skills.Skip(pageSize*pageNumber).Take(pageSize);
            r.TotalCount = Skills.Count();
            return r;
        }
    }

    public class SkillsPageResult {
        public IQueryable<Skill> SkillsToDisplay { get; set; }
        public int TotalCount { get; set; }
    }
}