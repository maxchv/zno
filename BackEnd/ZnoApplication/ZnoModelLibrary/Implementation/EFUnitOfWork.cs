﻿using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;
using ZnoModelLibrary.Context;
using ZnoModelLibrary.Interfaces;

namespace ZnoModelLibrary.Implementation
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
        private AnswerTypeRepository _answerTypeRepository;

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

        public AnswerTypeRepository AnswerTypes
        {
            get
            {
                if (_answerTypeRepository is null)
                    _answerTypeRepository = new AnswerTypeRepository(_context);

                return _answerTypeRepository;
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