using Microsoft.EntityFrameworkCore.Storage;
using ZnoModelLibrary.EF;
using ZnoModelLibrary.Interfaces;

namespace ZnoModelLibrary.Implementation
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ApplicationContext _context;
        private IDbContextTransaction _transaction;

        public EFUnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        private UserRepository _userRepository;
        private TestRepository _testRepository;
        private TestSettingsRepository _testSettingsRepository;
        private QuestionRepository _questionRepository;
        private GeneratedTestRepository _generatedTestRepository;
        private SubjectRepository _subjectRepository;
        private TestTypeRepository _testTypeRepository;
        private UserAnswerRepository _userAnswerRepository;

        public UserRepository Users
        {
            get
            {
                if (_userRepository is null)
                    _userRepository = new UserRepository(_context);

                return _userRepository;
            }
        }

        public TestRepository Tests
        {
            get
            {
                if (_testRepository is null)
                    _testRepository = new TestRepository(_context);

                return _testRepository;
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

        public TestSettingsRepository TestSettings
        {
            get
            {
                if (_testSettingsRepository is null)
                    _testSettingsRepository = new TestSettingsRepository(_context);

                return _testSettingsRepository;
            }
        }

        public GeneratedTestRepository GeneratedTests
        {
            get
            {
                if (_generatedTestRepository is null)
                    _generatedTestRepository = new GeneratedTestRepository(_context);

                return _generatedTestRepository;
            }
        }

        public SubjectRepository Subjects
        {
            get
            {
                if (_subjectRepository is null)
                    _subjectRepository = new SubjectRepository(_context);

                return _subjectRepository;
            }
        }

        public TestTypeRepository TestTypes
        {
            get
            {
                if (_testTypeRepository is null)
                    _testTypeRepository = new TestTypeRepository(_context);

                return _testTypeRepository;
            }
        }

        public UserAnswerRepository UserAnswers
        {
            get
            {
                if (_userAnswerRepository is null)
                    _userAnswerRepository = new UserAnswerRepository(_context);

                return _userAnswerRepository;
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

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}