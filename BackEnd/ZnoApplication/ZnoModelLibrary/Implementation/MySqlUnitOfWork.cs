using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;
using Zno.DAL.Context;
using Zno.DAL.Interfaces;

namespace Zno.DAL.Implementation
{
    public class MySqlUnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        private IDbContextTransaction _transaction;

        public MySqlUnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        private UserRepository _userRepository;
        private SubjectRepository _subjectsRepository;
        private TestRepository _testsRepository;
        private TestSettingsRepository _testSettingsRepository;
        private TestTypeRepository _testTypesRepository;
        private QuestionRepository _questionRepository;
        private QuestionTypeRepository _questionTypeRepository;
        private ContentTypeRepository _contentTypeRepository;

        public UserRepository Users
        {
            get
            {
                if (_userRepository is null)
                    _userRepository = new UserRepository(_context);

                return _userRepository;
            }
        }

        public SubjectRepository Subjects
        {
            get
            {
                if (_subjectsRepository is null)
                    _subjectsRepository = new SubjectRepository(_context);

                return _subjectsRepository;
            }
        }

        public TestRepository Tests
        {
            get
            {
                if (_testsRepository is null)
                    _testsRepository = new TestRepository(_context);

                return _testsRepository;
            }
        }

        public TestSettingsRepository TestSettings
        {
            get
            {
                if (_testSettingsRepository is null)
                    _testSettingsRepository = new TestSettingsRepository(_context);

                return _testSettingsRepository;
            }
        }

        public TestTypeRepository TestTypes
        {
            get
            {
                if (_testTypesRepository is null)
                    _testTypesRepository = new TestTypeRepository(_context);

                return _testTypesRepository;
            }
        }

        public QuestionRepository Questions
        {
            get
            {
                if (_questionRepository is null)
                    _questionRepository = new QuestionRepository(_context);

                return _questionRepository;
            }
        }

        public QuestionTypeRepository QuestionTypes
        {
            get
            {
                if (_questionTypeRepository is null)
                    _questionTypeRepository = new QuestionTypeRepository(_context);

                return _questionTypeRepository;
            }
        }

        public ContentTypeRepository ContentTypes
        {
            get
            {
                if (_contentTypeRepository is null)
                    _contentTypeRepository = new ContentTypeRepository(_context);

                return _contentTypeRepository;
            }
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}