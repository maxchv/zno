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

        public UserRepository Users
        {
            get
            {
                if (_userRepository is null)
                    _userRepository = new UserRepository(_context);

                return _userRepository;
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