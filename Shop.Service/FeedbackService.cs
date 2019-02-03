using System;
using Shop.Model.Models;
using Shop.Data.Repositories;
using Shop.Data.Infrastructure;

namespace Shop.Service
{
    public interface IFeedbackService
    {
        Feedback Create(Feedback feedback);
        void Save();
    }

    public class FeedbackService : IFeedbackService
    {
        private IFeedbackRepository _feedbackRepository;
        private IUnitOfWork _unitOfWork;

        public FeedbackService(IFeedbackRepository feedbackRepository, IUnitOfWork unitOfWork)
        {
            this._feedbackRepository = feedbackRepository;
            this._unitOfWork = unitOfWork;
        }

        public Feedback Create(Feedback feedback)
        {
            return this._feedbackRepository.Add(feedback);
        }

        public void Save()
        {
            this._unitOfWork.Commit();
        }
    }
}
