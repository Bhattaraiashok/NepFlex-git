﻿using Core.Interfaces.Repositories;
using NepFlex.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        bool Save();
    }
}
