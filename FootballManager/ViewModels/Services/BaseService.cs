using AutoMapper;
using FootballManager.Data;
using System;

namespace FootballManager.ViewModels.Services
{
    public class BaseService : IDisposable
    {
        protected readonly ContextFM _context;
        protected readonly IMapper _mapper;
        private bool _isDisposed = false;
        protected BaseService(ContextFM context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool flag)
        {
            if (_isDisposed) return;

            _context?.Dispose();
            _isDisposed = true;

            if (flag) GC.SuppressFinalize(this);
        }

        ~BaseService()
        {
            Dispose(false);
        }
    }
}
