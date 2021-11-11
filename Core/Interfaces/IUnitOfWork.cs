using Core.Interfaces.Repositories;
using NepFlex.Core.Interfaces.Repositories;
using System;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMenuTopRepository MenuTopRepository { get; }
        ISearchRepository SearchRepository { get; }
        IItemDescriptionRepository ItemDescriptionRepository { get; }
        IReportRepository ReportRepository { get; }
        IDetailRepository DetailRepository { get; }
        ISendEmailRepository SendEmailRepository { get; }
        ILoginRepository LoginRepository { get; }

        bool Save();
    }
}
