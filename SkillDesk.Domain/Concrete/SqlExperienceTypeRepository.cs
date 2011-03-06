using System;
using System.Data.Linq;
using System.Linq;
using SkillDesk.Domain.Abstract;
using SkillDesk.Domain.Entities;

namespace SkillDesk.Domain.Concrete
{
    public class SqlExperienceTypeRepository : IExperienceTypeRepository
    {
        private readonly string _connectionString;
        private Table<ExperienceType> _table;

        public SqlExperienceTypeRepository(string connectionString)
        {
            _connectionString = connectionString;
            this._table = (new DataContext(_connectionString).GetTable<ExperienceType>());
        }


        public IQueryable<ExperienceType> ExperienceLevels {
            get { return _table; }
        }
    }
}