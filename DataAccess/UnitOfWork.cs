using Core.Interfaces;
using Core.Interfaces.Repositories;
using DataAccess.Repositories;
using NepFlex.Core.Interfaces.Repositories;
using NepFlex.DataAccess.Context;
using NepFlex.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Transactions;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private static readonly TraceSource TraceSource = new TraceSource("NepFlex.DataAccess");
        private IOnlinePasalContext _context;
        //private readonly IEncryptionService _encryptionService;
        private bool _disposed;

        // TODO: Add Repositories
        public IMenuTopRepository MenuTopRepository { get; private set; }
        public ISearchRepository SearchRepository { get; private set; }
        public IItemDescriptionRepository ItemDescriptionRepository { get; private set; }
        public IReportRepository ReportRepository { get; private set; }
        public IDetailRepository DetailRepository { get; private set; }
        public ISendEmailRepository SendEmailRepository { get; private set; }
        public ILoginRepository LoginRepository { get; private set; }

        public UnitOfWork(IOnlinePasalContext context
           // IEncryptionService encryptionService
           )
        {
            _context = context;
            // _encryptionService = encryptionService;
            Initialize();
        }

        private void Initialize()
        {
            // TODO: Add Repositories
            MenuTopRepository = new MenuTopRepository(_context);
            SearchRepository = new SearchRepository(_context);
            ItemDescriptionRepository = new ItemDescriptionRepository(_context);
            ReportRepository = new ReportRepository(_context);
            DetailRepository = new DetailRepository(_context);
            SendEmailRepository = new SendEmailRepository(_context);
            LoginRepository = new LoginRepository(_context);
        }
        public List<ValidationResult> GetValidationErrors()
        {
            var errors = _context.GetValidationErrors();
            List<ValidationResult> validationResults = new List<ValidationResult>();
            foreach (var error in errors)
            {
                foreach (var internalError in error.ValidationErrors)
                    validationResults.Add(new ValidationResult(internalError.ErrorMessage, new List<string>() { internalError.PropertyName }));
            }
            return validationResults;
        }

        public bool Save()
        {
            bool isSuccessful;
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    _context.SaveChanges();
                    scope.Complete();
                    isSuccessful = true;
                }
                catch (DbEntityValidationException ex)
                {
                    //TraceSource.TraceDataAsPrettyJson(TraceEventType.Warning, 0, new
                    //{
                    //    Description = "exception saving changes to database.",
                    //    Exception = ex
                    //});
                    isSuccessful = false;
                    throw ex;

                    //swallow these because we have messages in GetValidationErrors
                }
            }
            return isSuccessful;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                _context.Dispose();
            }
            _context = null;
            _disposed = true;
        }
    }
}
